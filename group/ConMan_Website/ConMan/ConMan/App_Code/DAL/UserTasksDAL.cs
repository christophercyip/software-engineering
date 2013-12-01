using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConMan.App_Code.DAL
{
    public class UserTasksDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userID"></param>
        /// <returns>The id of the user that was added to the Task</returns>
        public static Int64 AddUserToTask(Int64 taskID, Int64 userID)
        {
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            Int64 addedUserID = 0;
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                // Create SQL Insert Command for creating new Team Table entry
                // We get the ID of the newly inserted entry by using "OUTPUT INSERTED.team_id"
                String insertCommand = "INSERT INTO UserTasks (task_id, user_id) OUTPUT INSERTED.user_id VALUES (" +
                                            "@task_id, @user_id);";
                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;
                command.Parameters.Add("@user_id", SqlDbType.Int);
                command.Parameters["@user_id"].Value = userID;

                try
                {
                    sqlConn.Open();
                    // Execute scalar returns the element at the 1st column of the data inserted( which should be the ID)
                    addedUserID = Convert.ToInt64(command.ExecuteScalar());
                    sqlConn.Close();

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return addedUserID;
        }

        /// <summary>
        /// Removes the User with the specified ID from the UserTasks ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool RemoveUserFromTask(Int64 taskID, Int64 userID)
        {
            // Flag that signals whether the record has been deleted successfully
            bool successfullyDeleted = false;

            String connectionString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String deleteTeamCommand = "DELETE UserTasks WHERE task_id = @task_id AND user_id = @user_id";

                SqlCommand command = new SqlCommand(deleteTeamCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;
                command.Parameters.Add("@user_id", SqlDbType.Int);
                command.Parameters["@user_id"].Value = userID;

                sqlConn.Open();
                int numRowsAffected = command.ExecuteNonQuery();
                sqlConn.Close();

                // If one row was affected, then we successfully deleted the Team
                if (numRowsAffected > 0)
                {
                    successfullyDeleted = true;
                }
            }

            return successfullyDeleted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool IsTaskAssignedToUser(Int64 taskID, Int64 userID)
        {
            // Data Source=.\SQLEXPRESS;AttachDbFilename=C:\LuisJr\USDSenior1\Software_Engineering\Test_Sites\App_Data\Database.mdf;Integrated Security=True;User Instance=True
            String connectionString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;
            bool isUserAssignedToTask = false;

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String selectCommand = "SELECT * FROM UserTasks WHERE task_id = @task_id AND user_id = @user_id";

                SqlCommand command = new SqlCommand(selectCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;
                command.Parameters.Add("@user_id", SqlDbType.Int);
                command.Parameters["@user_id"].Value = userID;

                try
                {
                    sqlConn.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        isUserAssignedToTask = true;
                    }
                    dr.Close();
                    sqlConn.Close();

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return isUserAssignedToTask;
        }
    }
}