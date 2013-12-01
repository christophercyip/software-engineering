using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using ConMan.App_Code.DAL;
using ConMan.App_Code.Entities;

namespace ConMan
{
    public partial class TaskPage : System.Web.UI.Page
    {
        private String currentUserEmail = null;
        private User currentUser = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            this.currentUserEmail = HttpContext.Current.User.Identity.Name;
            currentUser = UserDAL.GetUserByEmail(this.currentUserEmail);

            cdlCreateCalendar.SelectedDate = DateTime.Now;
        }

        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 0;
        }

        protected void lnkViewTask_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 1;
        }

        protected void lnkUpdateTask_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 2;
        }

        protected void lnkAddRemoveMembers_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 3;
        }

        protected void lnkAddNote_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 4;
        }

        protected void lnkDeleteTask_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateTask_Click(object sender, EventArgs e)
        {
            lblCreateTaskResults.Text = "";

            if (Page.IsValid)
            {

                // Make sure name and description are not empty
                if (tbxCreateTaskName.Text.Equals("") || tbxCreateDescription.Equals(""))
                {
                    lblCreateTaskResults.ForeColor = Color.Red;
                    lblCreateTaskResults.Text = "Must complete all fields. ";
                    return;
                }

                // Make sure current user is the admin of the selected team
                // First make sure the current user is the admin of the selected team
                Team team = TeamDAL.GetTeamById(Convert.ToInt64(ddlCreateTaskTeam.SelectedValue));

                // If the user is NOT the team admin...
                if (currentUser.ID != team.TeamOwner)
                {
                    lblCreateTaskResults.ForeColor = Color.Red;
                    lblCreateTaskResults.Text += "Cannot add tasks to a team that you are not the admin of. ";
                    return;
                }

                // Now all data is valid... so create task
                Int64 newTaskID = TaskDAL.CreateNewTask(tbxCreateTaskName.Text, cdlCreateCalendar.SelectedDate, 
                                        tbxCreateDescription.Text, Convert.ToInt64(ddlCreateTaskTeam.SelectedValue));
                lblCreateTaskResults.ForeColor = Color.Blue;
                lblCreateTaskResults.Text = "Successfully created task. ";

                // Now refresh all of the drop down lists on the page
                this.RefreshAllDropDownLists();

                // Clear the fields in the Create Task View
                tbxCreateTaskName.Text = "";
                tbxCreateDescription.Text = "";
                cdlCreateCalendar.SelectedDate = DateTime.Now;
 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnARMemAddUser_Click(object sender, EventArgs e)
        {

            // Reset the results label for this view
            lblARMemResults.Text = "";

            // First make sure the current user is the admin of the selected team
            Team selectedTeam = TeamDAL.GetTeamById(Convert.ToInt64( ddlARMemTeam.SelectedValue));
            User newUser = UserDAL.GetUserByEmail(tbxARMemEmail.Text);

            // Show error messsage and abort
            if (currentUser.ID != selectedTeam.TeamOwner)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Cannot assign user to a task of a team that you are not the admin of. ";
                return;
            }

            // Make sure the specified user exists in the ConMan database
            if (newUser == null)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Cannot add user. User must have account with ConMan. ";
                return;
            }

            // Make sure the specified user is not already assigned to this task
            Int64 selectedTaskID = Convert.ToInt64(ddlARMemTask.SelectedValue);
            Int64 selectedUserID = newUser.ID;
            if (UserTasksDAL.IsTaskAssignedToUser(Convert.ToInt64(ddlARMemTask.SelectedValue), newUser.ID))
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Cannot add user. User is already assigned to this task. ";
                return;
            }

            // If we got this far, all data is valid
            Int64 userID = UserTasksDAL.AddUserToTask(Convert.ToInt64(ddlARMemTask.SelectedValue), 
                                                        newUser.ID);
            if (userID > 0)
            {
                lblARMemResults.ForeColor = Color.Blue;
                lblARMemResults.Text = "Successfully assigned user to this task. ";

                // Refresh the drop down lists
                this.RefreshAllDropDownLists();

                // Reset the add user text box
                tbxARMemEmail.Text = "";
            }
            else
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Unable to assign user to this task. ";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnARMemRemoveUser_Click(object sender, EventArgs e)
        {
            // Reset the results label
            lblARMemResults.Text = "";

            // First make sure the current user is the admin of the selected team
            Team selectedTeam = TeamDAL.GetTeamById(Convert.ToInt64(ddlARMemTeam.SelectedValue));
            if (currentUser.ID != selectedTeam.TeamOwner)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Cannot remove user from a task of a team that you are not the admin of. ";
                return;
            }

            Int64 selectedTaskID = Convert.ToInt64(ddlARMemTask.SelectedValue);
            User userToRemove = UserDAL.GetUserByID( Convert.ToInt64(ddlARMemTaskUsers.SelectedValue));

            // Attempt to remove user from this task and signal result within the results label
            if (UserTasksDAL.RemoveUserFromTask(selectedTaskID, userToRemove.ID))
            {
                lblARMemResults.ForeColor = Color.Blue;
                lblARMemResults.Text = "Successfully removed user from task. ";

                // Remember to refresh all of the drop down lists
                this.RefreshAllDropDownLists();
            }
            else
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Unable to remove user from task. ";
            }
        }

        protected void btnAddNoteButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Forces all drop down lists on this page to refresh
        /// </summary>
        private void RefreshAllDropDownLists()
        {
            ddlAddNoteTask.DataBind();
            ddlAddNoteTeam.DataBind();

            ddlARMemTask.DataBind();
            ddlARMemTaskUsers.DataBind();
            ddlARMemTeam.DataBind();

            ddlCreateTaskTeam.DataBind();

            ddlUpdateTask.DataBind();
            ddlUpdateTeam.DataBind();
        }

        
    }
}