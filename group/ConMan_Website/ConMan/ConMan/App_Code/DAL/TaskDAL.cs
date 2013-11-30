using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ConMan.App_Code.DAL
{
    public class TaskDAL
    {
        /// <summary>
        /// Creates a new Task in the ConMan database
        /// </summary>
        /// <param name="taskName">Name of new task</param>
        /// <param name="taskDueDate">Due date of the task</param>
        /// <param name="taskDescription">New tasks description</param>
        /// <param name="teamID">Team ID that this task will be associated with</param>
        /// <returns></returns>
        public static Int64 CreateNewTask(String taskName, DateTime taskDueDate, String taskDescription, Int64 teamID)
        {
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            Int64 taskID = 0;
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                // Create SQL Insert Command for creating new Team Table entry
                // We get the ID of the newly inserted entry by using "OUTPUT INSERTED.team_id"
                String insertCommand = "INSERT INTO Task (team_id, task_name, due_date, task_description) OUTPUT INSERTED.task_id VALUES (" +
                                            "@team_id, @task_name, @due_date, @task_description);";
                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@team_id", SqlDbType.Int);
                command.Parameters["@team_id"].Value = teamID;
                command.Parameters.Add("@task_name", SqlDbType.NVarChar);
                command.Parameters["@task_name"].Value = taskName;
                command.Parameters.Add("@due_date", SqlDbType.SmallDateTime);
                command.Parameters["@due_date"].Value = taskDueDate;
                command.Parameters.Add("@task_description", SqlDbType.NVarChar);
                command.Parameters["@task_description"].Value = taskDescription;

                try
                {
                    sqlConn.Open();
                    // Execute scalar returns the element at the 1st column of the data inserted( which should be the ID)
                    taskID = Convert.ToInt64(command.ExecuteScalar());
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
            return taskID;
        }
    }
}