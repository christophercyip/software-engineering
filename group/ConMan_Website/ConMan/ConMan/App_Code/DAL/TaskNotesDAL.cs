using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConMan.App_Code.DAL
{
    public class TaskNotesDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userID"></param>
        /// <param name="notes"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static bool AddNotesToTask(Int64 taskID, Int64 userID, String notes, DateTime timestamp)
        {
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            bool successful = false;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                // Create SQL Insert Command for creating new Team Table entry
                // We get the ID of the newly inserted entry by using "OUTPUT INSERTED.team_id"
                String insertCommand = "INSERT INTO TaskNotes (task_id, user_id, note, note_date) " +
                                        "VALUES (@task_id, @user_id, @note, @note_date);";
                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;
                command.Parameters.Add("@user_id", SqlDbType.Int);
                command.Parameters["@user_id"].Value = userID;
                command.Parameters.Add("@note", SqlDbType.NVarChar);
                command.Parameters["@note"].Value = notes;
                command.Parameters.Add("@note_date", SqlDbType.SmallDateTime);
                command.Parameters["@note_date"].Value = timestamp;

                try
                {
                    sqlConn.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    sqlConn.Close();

                    if (numRowsAffected > 0)
                    {
                        successful = true;
                    }

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

            return successful;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static bool DeleteAllTaskNotes(Int64 taskID)
        {
            bool wasDeleteSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String deleteCommand = "DELETE TaskNotes WHERE task_id = @task_id";
                SqlCommand command = new SqlCommand(deleteCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;

                try
                {
                    sqlConn.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    sqlConn.Close();

                    // If we got this far, we were successful
                    wasDeleteSuccessful = true;

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return wasDeleteSuccessful;
        }
    }
}