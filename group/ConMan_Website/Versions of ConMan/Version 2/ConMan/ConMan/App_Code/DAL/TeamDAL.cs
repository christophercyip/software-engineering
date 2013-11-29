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
        /// 
        /// </summary>
        /// <param name="teamID"></param>
        /// <param name="userID"></param>
        /// <param name="isUserTeamAdmin"></param>
        /// <returns></returns>
        public static bool AddTeamMember(Int64 teamID, Int64 userID, bool isUserTeamAdmin)
        {
            bool wasAddSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Insert Command for creating new Team Member Table Entry
                String insertCommand = "INSERT INTO TeamMember (team_mem_id, user_mem_id, mem_admin) VALUES (" +
                                            "@team_mem_id, @user_mem_id, @mem_admin);";
                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@team_mem_id", SqlDbType.Int);
                command.Parameters["@team_mem_id"].Value = teamID;
                command.Parameters.Add("@user_mem_id", SqlDbType.Int);
                command.Parameters["@user_mem_id"].Value = userID;
                command.Parameters.Add("@mem_admin", SqlDbType.Int);
                command.Parameters["@mem_admin"].Value = (isUserTeamAdmin == true ? 1 : 0 );

                try
                {
                    sqlConn.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    sqlConn.Close();
                    if (numRowsAffected > 0)
                    {
                        wasAddSuccessful = true;
                    }

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return wasAddSuccessful;
        }
    }
}