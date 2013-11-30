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

        protected void lnkAddRemoveMembers_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 1;
        }

        protected void lnkAddNote_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 2;
        }

        protected void lnkUpdateTask_Click(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = 3;
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
                Team team = TeamDAL.GetTeamById(Convert.ToInt64(ddlCreateTeam.SelectedValue));

                // If the user is NOT the team admin...
                if (currentUser.ID != team.TeamOwner)
                {
                    lblCreateTaskResults.ForeColor = Color.Red;
                    lblCreateTaskResults.Text += "Cannot add tasks to a team that you are not the admin of. ";
                    return;
                }

                // Now all data is valid... so create team
                Int64 newTaskID = TaskDAL.CreateNewTask(tbxCreateTaskName.Text, cdlCreateCalendar.SelectedDate, 
                                        tbxCreateDescription.Text, Convert.ToInt64(ddlCreateTeam.SelectedValue));
                lblCreateTaskResults.ForeColor = Color.Blue;
                lblCreateTaskResults.Text = "Successfully created task. ";
 
            }
        }

        protected void btnARMemAddUser_Click(object sender, EventArgs e)
        {

        }

        protected void btnARMemRemoveUser_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddNoteButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}