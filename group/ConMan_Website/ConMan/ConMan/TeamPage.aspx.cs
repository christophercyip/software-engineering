using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConMan.App_Code.DAL;
using System.Drawing;
using ConMan.App_Code.Entities;
using System.Data;

namespace ConMan
{
    public partial class TeamPage : System.Web.UI.Page
    {
        private const int TEAM_NAME_MAX_SIZE = 50;
        private String currentUserEmail = null;
        private User currentUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //
            this.currentUserEmail = HttpContext.Current.User.Identity.Name;
            currentUser = UserDAL.GetUserByEmail(this.currentUserEmail);
            tbxCreateTeamOwner.Text = currentUser.Email;
        }

        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            // Go to ReadView
            TeamMultiView.ActiveViewIndex = 0;
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            // Go to Read Vie
            TeamMultiView.ActiveViewIndex = 1;
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            TeamMultiView.ActiveViewIndex = 2;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            TeamMultiView.ActiveViewIndex = 3;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearFields()
        {
            tbxCreateTeamName.Text = "";
            
            tbxViewTeamName.Text = "";
            tbxViewTeamOwner.Text = "";
        }
        /// <summary>
        /// Attempts to create a team.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateTeam_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && this.isCreateTeamInfoValid())
            {
                // Create the new team and add the current user as the admin of this team
                Int64 teamID = TeamDAL.CreateNewTeam(tbxCreateTeamName.Text, this.currentUser.ID);
                bool isUserNowAdmin = TeamMemberDAL.CreateTeamMember(teamID, currentUser.ID, true);

                // Display Results to user
                lblCreateTeamResults.Text = "";
                if (teamID > 0 && isUserNowAdmin == true)
                {
                    lblCreateTeamResults.ForeColor = Color.Blue;
                    lblCreateTeamResults.Text += "Successfully created team (ID = " + teamID + ") and you are now the admin of the team";
                }
                else
                {
                    lblCreateTeamResults.ForeColor = Color.Red;
                    lblCreateTeamResults.Text += "Unable to create Team (ID = " + teamID + ")";
                }

                this.ClearFields();

                // Force the dropdownlist to bind again to its data source...pretty
                // much forcing it to update its list to reflect the database table
                ddlTeamsView.DataBind();
                ddlTeamsDelete.DataBind();
                ddlTeamsUpdate.DataBind();
                ddlUpdateRemoveMember.DataBind();
            }
        }

        /// <summary>
        /// Makes sure data in the create team form is valid
        /// </summary>
        /// <returns></returns>
        private bool isCreateTeamInfoValid()
        {
            String teamName = tbxCreateTeamName.Text;
            String teamOwner = tbxCreateTeamOwner.Text;

            bool isDataValid = true;

            // Make sure team name is <= 50 characters
            if (teamName.Length > TEAM_NAME_MAX_SIZE)
            {
                lblCreateTeamResults.Text = "";
                lblCreateTeamResults.ForeColor = Color.Red;
                lblCreateTeamResults.Text += "Team Name must contain 50 or less characters. ";
                isDataValid = false;
            }

            return isDataValid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewTeam_Click(object sender, EventArgs e)
        {

            lblViewTeamResults.Text = "";

            // Make sure the user selected a team
            if (ddlTeamsView.SelectedValue.Equals("") || ddlTeamsView.SelectedValue.Equals("0") || ddlTeamsView.SelectedValue == null)
            {
                lblViewTeamResults.ForeColor = Color.Red;
                lblViewTeamResults.Text = "You must first select a team. ";
                return;
            }

            Team team = TeamDAL.GetTeamById( Convert.ToInt64(ddlTeamsView.SelectedValue) );

            

            if (team != null)
            {
                tbxViewTeamName.Text = team.TeamName;
                tbxViewTeamOwner.Text = UserDAL.GetUserByID( team.TeamOwner ).Email;
            }
            else
            {
                lblViewTeamResults.ForeColor = Color.Red;
                lblViewTeamResults.Text = "Unable to find details about the selected Team";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteTeam_Click(object sender, EventArgs e)
        {
            // Make the sure user selected a team
            if (ddlTeamsDelete.SelectedValue.Equals("") || ddlTeamsDelete.Equals("0") || ddlTeamsDelete.SelectedValue == null)
            {
                lblDeleteTeamResults.ForeColor = Color.Red;
                lblDeleteTeamResults.Text = "You must first select a team to delete. ";
                return;
            }

            // First make sure the current user is the admin of the selected team
            Team team = TeamDAL.GetTeamById( Convert.ToInt64(ddlTeamsDelete.SelectedValue) );

            lblDeleteTeamResults.Text = "";

            // If the user is NOT the team admin...
            if (currentUser.ID != team.TeamOwner)
            {
                lblDeleteTeamResults.ForeColor = Color.Red;
                lblDeleteTeamResults.Text = "Cannot delete a team that you are not the admin of. ";
                return;
            }
            
            // Attempt to delete all information about the tasks associated with the team
            // to delete (task, task notes, and mappings to assigned users)
            List<Int64> listOfTasksToDelete = TaskDAL.GetAllTeamTasks( team.TeamID);
            foreach( Int64 taskID in listOfTasksToDelete)
            {
                TaskNotesDAL.DeleteAllTaskNotes(taskID);
                UserTasksDAL.DeleteAllTaskUsers(taskID);
                TaskDAL.DeleteTask(taskID);
            }

            // Atttempt to delete the team
            if (TeamDAL.DeleteTeam(team.TeamID) && TeamMemberDAL.RemoveAllMembers(team.TeamID))
            {
                lblDeleteTeamResults.ForeColor = Color.Blue;
                lblDeleteTeamResults.Text = "Successfully deleted the team.";

                // Remember to update the drop down lists on the page
                ddlTeamsDelete.DataBind();
                ddlTeamsView.DataBind();
                ddlTeamsUpdate.DataBind();
                ddlUpdateRemoveMember.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateAddMember_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblUpdateResults.Text = "";

                // Make sure that the user selected a team
                // Make the sure user selected a team
                if (ddlTeamsUpdate.SelectedValue.Equals("") || ddlTeamsUpdate.Equals("0") || ddlTeamsUpdate.SelectedValue == null)
                {
                    lblUpdateResults.ForeColor = Color.Red;
                    lblUpdateResults.Text = "You must first select a team to add a member to. ";
                    return;
                }

                // First check if current user is the admin of the team before 
                // he adds a member
                Team teamToUpdate = TeamDAL.GetTeamById( Convert.ToInt64(ddlTeamsUpdate.SelectedValue));
                if (currentUser.ID == teamToUpdate.TeamOwner)
                {
                    // Check if the email supplied exists in the UserInfo table
                    User newMember = UserDAL.GetUserByEmail(tbxUpdateNewMember.Text);
                    if (newMember != null)
                    {
                        if (TeamMemberDAL.IsUserMemberOfTeam(Convert.ToInt64(ddlTeamsUpdate.SelectedValue), newMember.ID) == false)
                        {
                            if (TeamMemberDAL.CreateTeamMember(Convert.ToInt64(ddlTeamsUpdate.SelectedValue), newMember.ID, false))
                            {
                                lblUpdateResults.ForeColor = Color.Blue;
                                lblUpdateResults.Text += "Successfully added new team member (" + newMember.Email + "). ";

                                // Reset the textbox text
                                tbxUpdateNewMember.Text = "";

                                // Make sure to force drop down list to update
                                ddlUpdateRemoveMember.DataBind();
                            }
                            else
                            {
                                lblUpdateResults.ForeColor = Color.Red;
                                lblUpdateResults.Text += "Unable to add new team member (make sure member is not already a member of the team). ";
                            }
                        }
                        else
                        {
                            lblUpdateResults.ForeColor = Color.Red;
                            lblUpdateResults.Text += "Member is already part of the team. ";
                        }
                        
                    }
                    // The supplied email DOES exist in ConMan Database -> invalid
                    else
                    {
                        lblUpdateResults.ForeColor = Color.Red;
                        lblUpdateResults.Text += "The team member must be in ConMan database to be added. ";
                    }
                }
                // Else, the current user is NOT authorized to update the team
                else
                {
                    lblUpdateResults.ForeColor = Color.Red;
                    lblUpdateResults.Text += "You are not authorized to make changes to this team. ";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateRemoveMember_Click(object sender, EventArgs e)
        {
            if (ddlTeamsUpdate.SelectedValue.Equals("") || ddlTeamsUpdate.SelectedValue.Equals("0") || ddlTeamsUpdate.SelectedValue == null
                || ddlUpdateRemoveMember.SelectedValue.Equals("") || ddlUpdateRemoveMember.SelectedValue.Equals("0") || ddlUpdateRemoveMember.SelectedValue == null)
            {
                lblUpdateResults.ForeColor = Color.Red;
                lblUpdateResults.Text = "You must first select a team and user to remove a user from a team. ";
                return;
            }

            if (Page.IsValid)
            {
                lblUpdateResults.Text = "";

                // First check if current user is the admin of the team before 
                // he adds a member
                Team teamToUpdate = TeamDAL.GetTeamById(Convert.ToInt64(ddlTeamsUpdate.SelectedValue));
                if (currentUser.ID == teamToUpdate.TeamOwner)
                {
                    // Do not allow the admin to remove himself from the team...this can only be done
                    // when the admin deletes the team
                    if (Convert.ToInt64(ddlUpdateRemoveMember.SelectedValue) != teamToUpdate.TeamOwner)
                    {
                        if (TeamMemberDAL.DeleteTeamMember(Convert.ToInt64(ddlUpdateRemoveMember.SelectedValue)))
                        {
                            // Make sure to have the drop down list refresh
                            ddlUpdateRemoveMember.DataBind();
                            lblUpdateResults.ForeColor = Color.Blue;
                            lblUpdateResults.Text += "Successfully removed team member from team. ";
                        }
                        else
                        {
                            lblUpdateResults.ForeColor = Color.Red;
                            lblUpdateResults.Text += "Unable to remove team member from team. ";
                        }
                    }
                    //
                    else
                    {
                        lblUpdateResults.ForeColor = Color.Red;
                        lblUpdateResults.Text = "Cannot remove the team admin from the team. ";
                    }
                }
                //
                else
                {
                    lblUpdateResults.ForeColor = Color.Red;
                    lblUpdateResults.Text = "You are not authorized to make changes to this team. ";
                }
            }
        }
    }
}