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

            // Make sure all drop down lists are up-to-date
            if (!IsPostBack)
            {
                this.RefreshAllDropDownLists();
            }
            
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
                // Make sure the user selected a team
                if (ddlCreateTaskTeam.SelectedValue.Equals("") || ddlCreateTaskTeam.SelectedValue.Equals("0") 
                        || ddlCreateTaskTeam.SelectedValue == null)
                {
                    lblCreateTaskResults.ForeColor = Color.Red;
                    lblCreateTaskResults.Text = "You must select a team. ";
                    return;
                }

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

            // Make sure a team and task is selected
            if (ddlARMemTeam.SelectedValue.Equals("") || ddlARMemTeam.SelectedValue.Equals("0") || ddlARMemTeam.SelectedValue == null
                || ddlARMemTask.SelectedValue.Equals("") || ddlARMemTask.SelectedValue.Equals("0") || ddlARMemTask.SelectedValue == null)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "You must select a team and task before attempting to assign a task to a user.";
                return;
            }

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
        protected void btnAddNoteButton_Click(object sender, EventArgs e)
        {

            // First make sure a team and task where selected
            if (ddlAddNoteTeam.SelectedValue.Equals("") || ddlAddNoteTeam.SelectedValue.Equals("0") || ddlAddNoteTeam.SelectedValue == null
                || ddlAddNoteTask.SelectedValue.Equals("") || ddlAddNoteTask.SelectedValue.Equals("0") || ddlAddNoteTask.SelectedValue == null)
            {
                lblAddNoteResults.ForeColor = Color.Red;
                lblAddNoteResults.Text = "You must first select a team and task to add a note. ";
                return;
            }

            Int64 selectedTaskID = Convert.ToInt64(ddlAddNoteTask.SelectedValue);

            // Maker sure user is assigned to the task before they can add a note
            if (UserTasksDAL.IsTaskAssignedToUser(selectedTaskID, currentUser.ID) == false)
            {
                lblAddNoteResults.ForeColor = Color.Red;
                lblAddNoteResults.Text = "Cannot add note to task that you are not assigned to. ";
                return;
            }

            // Attempt to create note
            if (TaskNotesDAL.AddNotesToTask(selectedTaskID, currentUser.ID, tbxAddNoteNote.Text, DateTime.Now))
            {
                lblAddNoteResults.ForeColor = Color.Blue;
                lblAddNoteResults.Text = "Successfully added notes to selected task. ";
                tbxAddNoteNote.Text = "";
                NotesDataList.DataBind();
                //this.RefreshAllDropDownLists();
                NotesDataList.Visible = true;
            }
            else
            {
                lblAddNoteResults.ForeColor = Color.Red;
                lblAddNoteResults.Text = "Unable to add notes to selected task. ";
            }
        }

        /// <summary>
        /// Forces all drop down lists on this page to refresh
        /// </summary>
        private void RefreshAllDropDownLists()
        {
            ddlAddNoteTask.DataBind();
            ddlAddNoteTeam.DataBind();

            ddlARMemTeam.DataBind();
            ddlARMemTask.DataBind();
            

            ddlCreateTaskTeam.DataBind();

            ddlUpdateTask.DataBind();
            ddlUpdateTeam.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnARMemRemoveUser_Click(object sender, EventArgs e)
        {
            // Reset the results label for this view
            lblARMemResults.Text = "";

            // Make sure a team and task is selected
            if (ddlARMemTeam.SelectedValue.Equals("") || ddlARMemTeam.SelectedValue.Equals("0") || ddlARMemTeam.SelectedValue == null
                || ddlARMemTask.SelectedValue.Equals("") || ddlARMemTask.SelectedValue.Equals("0") || ddlARMemTask.SelectedValue == null)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "You must select a team and task before attempting to unassign a user from the task.";
                return;
            }

            // First make sure the current user is the admin of the selected team
            Team selectedTeam = TeamDAL.GetTeamById(Convert.ToInt64(ddlARMemTeam.SelectedValue));
            User userToRemove = UserDAL.GetUserByEmail(tbxARMemEmail.Text);

            // Show error messsage and abort
            if (currentUser.ID != selectedTeam.TeamOwner)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Cannot remove user from a task of a team that you are not the admin of. ";
                return;
            }

            // Make sure the specified user exists in the ConMan database
            if (userToRemove == null)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Cannot remove user. User must have account with ConMan. ";
                return;
            }

            // Make sure the specified user is not already assigned to this task
            Int64 selectedTaskID = Convert.ToInt64(ddlARMemTask.SelectedValue);
            Int64 selectedUserID = userToRemove.ID;
            if (UserTasksDAL.IsTaskAssignedToUser(Convert.ToInt64(ddlARMemTask.SelectedValue), userToRemove.ID) == false)
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Cannot remove user. User is not assigned to this task. ";
                return;
            }

            // No attempt to remove the user
            if (UserTasksDAL.RemoveUserFromTask(selectedTaskID, selectedUserID))
            {
                lblARMemResults.ForeColor = Color.Blue;
                lblARMemResults.Text = "Successfully removed user from this task. ";

                // Refresh the drop down lists
                this.RefreshAllDropDownLists();

                // Reset the add user text box
                tbxARMemEmail.Text = "";
            }
            else
            {
                lblARMemResults.ForeColor = Color.Red;
                lblARMemResults.Text = "Unable to remove user from this task. ";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewTask_Click(object sender, EventArgs e)
        {
            // Make sure the user selected a team and a task
            if (ddlViewTask.SelectedValue.Equals("") || ddlViewTask.SelectedValue.Equals("0") || ddlViewTask.SelectedValue == null
                || ddlViewTeam.SelectedValue.Equals("") || ddlViewTeam.SelectedValue.Equals("0") || ddlViewTeam.SelectedValue == null)
            {
                lblViewTaskResults.ForeColor = Color.Red;
                lblViewTaskResults.Text = "You must first select a team and a task. ";
                return;
            }

            Task task = TaskDAL.GetTaskByID(Convert.ToInt64(ddlViewTask.SelectedValue));
            if (task == null)
            {
                tbxViewTaskName.Text = "Unable to retrieve task details.";
                tbxViewTaskDueDate.Text = "Unable to retrieve task details.";
                tbxViewTaskDescription.Text = "Unable to retrieve task details.";
            }
            else
            {
                tbxViewTaskName.Text = task.Name;
                tbxViewTaskDueDate.Text = task.DueDate.ToString();
                tbxViewTaskDescription.Text = task.Description;
            }

            ViewTaskUsersDataList.DataBind();
            ViewTaskUsersDataList.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewNotesRefresh_Click(object sender, EventArgs e)
        {
            NotesDataList.DataBind();
            NotesDataList.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (ddlDeleteTeam.SelectedValue.Equals("") || ddlDeleteTeam.SelectedValue.Equals("0") || ddlDeleteTeam.SelectedValue == null
                || ddlDeleteTask.SelectedValue.Equals("") || ddlDeleteTask.SelectedValue.Equals("0") || ddlDeleteTask.SelectedValue == null)
            {
                lblDeleteTaskResults.ForeColor = Color.Red;
                lblDeleteTaskResults.Text = "You must first select a team and task to delete a task. ";
                return;
            }

            Task task = TaskDAL.GetTaskByID(Convert.ToInt64(ddlDeleteTask.SelectedValue));
            Team taskTeam = TeamDAL.GetTeamById(task.TeamID);

            // First ensure that the current user, is the admin of the team that this task
            // is associated with
            if (currentUser.ID != taskTeam.TeamOwner)
            {
                lblDeleteTaskResults.ForeColor = Color.Red;
                lblDeleteTaskResults.Text = "Cannot delete a task of a team that you are not the admin of.";
                return;
            }

            // Delete the task and all of its notes and all of its mappings to users
            bool wasTaskDeletionSuccessful = TaskDAL.DeleteTask(task.TaskID);
            bool wasTaskNotesDeletionSuccessful = TaskNotesDAL.DeleteAllTaskNotes(task.TaskID);
            bool wasTaskUsersDeletionSuccessful = UserTasksDAL.DeleteAllTaskUsers(task.TaskID);

            // Signal whether the deletion was successful or not
            if (!wasTaskDeletionSuccessful || !wasTaskNotesDeletionSuccessful || !wasTaskUsersDeletionSuccessful)
            {
                lblDeleteTaskResults.ForeColor = Color.Red;
                lblDeleteTaskResults.Text = "Error occurred while deleting the task. ";
                return;
            }
            else
            {
                lblDeleteTaskResults.ForeColor = Color.Blue;
                lblDeleteTaskResults.Text = "Successfully delete the specified task. ";

                // Force all dropdown lists to refresh
                this.RefreshAllDropDownLists();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateDate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Make sure the current user is the admin of the team
                Team team = TeamDAL.GetTeamById( Convert.ToInt64( ddlUpdateTeam.SelectedValue));
                if (currentUser.ID != team.TeamOwner)
                {
                    lblUpdateTaskResults.ForeColor = Color.Red;
                    lblUpdateTaskResults.Text = "You cannot edit a task of a team that you are not the admin of. ";
                    return;
                }

                // make sure the user selected a team and a task
                if (ddlUpdateTeam.SelectedValue.Equals("") || ddlUpdateTeam.SelectedValue.Equals("0") || ddlUpdateTeam.SelectedValue == null 
                    || ddlUpdateTask.SelectedValue.Equals("") || ddlUpdateTask.SelectedValue.Equals("0") || ddlUpdateTask.SelectedValue == null )
                {
                    lblUpdateTaskResults.ForeColor = Color.Red;
                    lblUpdateTaskResults.Text = "You must first select a team and task. ";
                    return;
                }

                // If updating date was sucessful, tell the user
                if (TaskDAL.UpdateTaskDueDate(Convert.ToInt64(ddlUpdateTask.SelectedValue), Convert.ToDateTime(cdlUpdateDate.SelectedDate)))
                {
                    lblUpdateTaskResults.ForeColor = Color.Blue;
                    lblUpdateTaskResults.Text = "Successfully updated the date. ";
                }
                else
                {
                    lblUpdateTaskResults.ForeColor = Color.Blue;
                    lblUpdateTaskResults.Text = "Unable to update the date. ";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateDescription_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Make sure the current user is the admin of the team
                Team team = TeamDAL.GetTeamById(Convert.ToInt64(ddlUpdateTeam.SelectedValue));
                if (currentUser.ID != team.TeamOwner)
                {
                    lblUpdateTaskResults.ForeColor = Color.Red;
                    lblUpdateTaskResults.Text = "You cannot edit a task of a team that you are not the admin of. ";
                    return;
                }

                // make sure the user selected a team and a task
                if (ddlUpdateTeam.SelectedValue.Equals("") || ddlUpdateTeam.SelectedValue.Equals("0") || ddlUpdateTeam.SelectedValue == null
                    || ddlUpdateTask.SelectedValue.Equals("") || ddlUpdateTask.SelectedValue.Equals("0") || ddlUpdateTask.SelectedValue == null)
                {
                    lblUpdateTaskResults.ForeColor = Color.Red;
                    lblUpdateTaskResults.Text = "You must first select a team and task. ";
                    return;
                }

                // If updating description was sucessful, tell the user
                if (TaskDAL.UpdateTaskDescription(Convert.ToInt64(ddlUpdateTask.SelectedValue), tbxUpdateDescription.Text))
                {
                    lblUpdateTaskResults.ForeColor = Color.Blue;
                    lblUpdateTaskResults.Text = "Successfully updated the description. ";
                    tbxUpdateDescription.Text = "";
                }
                else
                {
                    lblUpdateTaskResults.ForeColor = Color.Blue;
                    lblUpdateTaskResults.Text = "Unable to update the description. ";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateLoadTaskInfo_Click(object sender, EventArgs e)
        {
            // make sure the user selected a team and a task
            if (ddlUpdateTeam.SelectedValue.Equals("") || ddlUpdateTeam.SelectedValue.Equals("0") || ddlUpdateTeam.SelectedValue == null
                || ddlUpdateTask.SelectedValue.Equals("") || ddlUpdateTask.SelectedValue.Equals("0") || ddlUpdateTask.SelectedValue == null)
            {
                lblUpdateTaskResults.ForeColor = Color.Red;
                lblUpdateTaskResults.Text = "You must first select a team and task. ";
                return;
            }

            // Fill in the textboxes that will show the user the tasks orginal information
            Task task = TaskDAL.GetTaskByID(Convert.ToInt64(ddlUpdateTask.SelectedValue));
            tbxUpdateOrigDueDate.Text = task.DueDate.ToString();
            tbxUpdateOrigDescription.Text = task.Description;
        }

        
    }
}