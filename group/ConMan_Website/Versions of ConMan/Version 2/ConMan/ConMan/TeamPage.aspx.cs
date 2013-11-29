using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConMan.App_Code.DAL;
using System.Drawing;
using ConMan.App_Code.Entities;

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

        protected void lnkRead_Click(object sender, EventArgs e)
        {
            TeamMultiView.ActiveViewIndex = 0;
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
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

        private void ClearFields()
        {
            tbxCreateTeamName.Text = "";
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
                bool isUserNowAdmin = TeamDAL.AddTeamMember(teamID, currentUser.ID, true);

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
                ddlTeams.DataBind();
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
    }
}