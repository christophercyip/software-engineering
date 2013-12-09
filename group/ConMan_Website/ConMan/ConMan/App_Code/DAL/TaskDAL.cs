using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ConMan.App_Code.Entities;
using System.Collections;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static Task GetTaskByID(Int64 taskID)
        {
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            Task task = null;
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                // Create SQL Insert Command for creating new Team Table entry
                // We get the ID of the newly inserted entry by using "OUTPUT INSERTED.team_id"
                String insertCommand = "SELECT * FROM Task WHERE task_id = @task_id;";
                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;

                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);

                    sqlConn.Open();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        task = new Task();
                        task.TaskID = Convert.ToInt64(ds.Tables[0].Rows[0]["task_id"]);
                        task.TeamID = Convert.ToInt64(ds.Tables[0].Rows[0]["team_id"]);
                        task.Name = Convert.ToString(ds.Tables[0].Rows[0]["task_name"]);
                        task.DueDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["due_date"]);
                        task.Description = Convert.ToString(ds.Tables[0].Rows[0]["task_description"]);
                    }
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
            return task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static bool DeleteTask(Int64 taskID)
        {
            bool wasDeleteSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String deleteCommand = "DELETE Task WHERE task_id = @task_id";
                SqlCommand command = new SqlCommand(deleteCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;

                try
                {
                    sqlConn.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    sqlConn.Close();

                    // If we got this far it, means that we were successful
                    wasDeleteSuccessful = true;

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return wasDeleteSuccessful;
        }

        /// <summary>
        /// Deletes all of the tasks associated with the team specified
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns></returns>
        public static bool DeleteAllTeamTasks(Int64 teamID)
        {
            bool wasDeleteSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String deleteCommand = "DELETE Task WHERE team_id = @team_id";
                SqlCommand command = new SqlCommand(deleteCommand, sqlConn);
                command.Parameters.Add("@team_id", SqlDbType.Int);
                command.Parameters["@team_id"].Value = teamID;

                try
                {
                    sqlConn.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    sqlConn.Close();
                    if (numRowsAffected > 0)
                    {
                        wasDeleteSuccessful = true;
                    }

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return wasDeleteSuccessful;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns></returns>
        public static List<Int64> GetAllTeamTasks(Int64 teamID)
        {
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            List<Int64> taskIDs = new List<Int64>();
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                String insertCommand = "SELECT Task.task_id FROM Task WHERE team_id = @team_id;";
                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@team_id", SqlDbType.Int);
                command.Parameters["@team_id"].Value = teamID;

                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);

                    sqlConn.Open();
                    IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator();
                    for(int x = 0; enumerator.MoveNext(); x++)
                    {
                        taskIDs.Add( Convert.ToInt64(ds.Tables[0].Rows[x]["task_id"]));
                    }
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
            return taskIDs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="newDueDate"></param>
        /// <returns></returns>
        public static bool UpdateTaskDueDate(Int64 taskID, DateTime newDueDate)
        {
            bool wasUpdateSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String deleteCommand = "UPDATE Task SET due_date = @due_date WHERE task_id = @task_id";
                SqlCommand command = new SqlCommand(deleteCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;
                command.Parameters.Add("@due_date", SqlDbType.SmallDateTime);
                command.Parameters["@due_date"].Value = newDueDate;

                try
                {
                    sqlConn.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    sqlConn.Close();
                    if (numRowsAffected > 0)
                    {
                        wasUpdateSuccessful = true;
                    }

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return wasUpdateSuccessful;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static bool UpdateTaskDescription(Int64 taskID, String description)
        {
            bool wasUpdateSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String deleteCommand = "UPDATE Task SET task_description = @task_description WHERE task_id = @task_id ";
                SqlCommand command = new SqlCommand(deleteCommand, sqlConn);
                command.Parameters.Add("@task_id", SqlDbType.Int);
                command.Parameters["@task_id"].Value = taskID;
                command.Parameters.Add("@task_description", SqlDbType.NVarChar);
                command.Parameters["@task_description"].Value = description;

                try
                {
                    sqlConn.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    sqlConn.Close();
                    if (numRowsAffected > 0)
                    {
                        wasUpdateSuccessful = true;
                    }

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return wasUpdateSuccessful;
        }

    }
}