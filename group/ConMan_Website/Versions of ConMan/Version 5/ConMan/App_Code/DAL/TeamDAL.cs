using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ConMan.App_Code.Entities;

namespace ConMan.App_Code.DAL
{
    public class TeamDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="teamOwnerID"></param>
        public static Int64 CreateNewTeam(String teamName, Int64 teamOwnerID)
        {
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            Int64 teamID = 0;   
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                // Create SQL Insert Command for creating new Team Table entry
                // We get the ID of the newly inserted entry by using "OUTPUT INSERTED.team_id"
                String insertCommand = "INSERT INTO Team (team_name, team_owner) OUTPUT INSERTED.team_id VALUES (" +
                                            "@team_name, @team_owner);";
                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@team_name", SqlDbType.NVarChar);
                command.Parameters["@team_name"].Value = teamName;
                command.Parameters.Add("@team_owner", SqlDbType.Int);
                command.Parameters["@team_owner"].Value = teamOwnerID;

                try
                {
                    sqlConn.Open();
                    // Execute scalar returns the element at the 1st column of the data inserted( which should be the ID)
                    teamID = Convert.ToInt64(command.ExecuteScalar());
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
            return teamID;
        }

        /// <summary>
        /// Gets the Team object associated with the given ID
        /// </summary>
        /// <param name="teamID">ID of Team object to return</param>
        /// <returns>Team object associated with the ID, Null if no such Team found</returns>
        public static Team GetTeamById(Int64 teamID)
        {
            // Data Source=.\SQLEXPRESS;AttachDbFilename=C:\LuisJr\USDSenior1\Software_Engineering\Test_Sites\App_Data\Database.mdf;Integrated Security=True;User Instance=True
            String connectionString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            Team team = null;

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String selectCommand = "SELECT * FROM Team WHERE team_id = @team_id";

                SqlCommand command = new SqlCommand(selectCommand, sqlConn);
                command.Parameters.Add("@team_id", SqlDbType.Int);
                command.Parameters["@team_id"].Value = teamID;

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = command;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                sqlConn.Open();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    team = new Team();
                    team.TeamID = Convert.ToInt64(ds.Tables[0].Rows[0]["team_id"]);
                    team.TeamName = Convert.ToString(ds.Tables[0].Rows[0]["team_name"]);
                    team.TeamOwner = Convert.ToInt64(ds.Tables[0].Rows[0]["team_owner"]);
                }
                sqlConn.Close();
            }

            return team;
        }

        /// <summary>
        /// Deletes the team witht the specified ID
        /// </summary>
        /// <param name="teamID">ID of team to delete</param>
        /// <returns></returns>
        public static bool DeleteTeam(Int64 teamID)
        {
            // Flag that signals whether the record has been deleted successfully
            bool successfullyDeleted = false;

            String connectionString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;
            
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String deleteTeamCommand = "DELETE Team WHERE team_id = @team_id";

                SqlCommand command = new SqlCommand(deleteTeamCommand, sqlConn);
                command.Parameters.Add("@team_id", SqlDbType.Int);
                command.Parameters["@team_id"].Value = teamID;

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
    }
}