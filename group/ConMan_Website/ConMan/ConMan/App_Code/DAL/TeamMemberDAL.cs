using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConMan.App_Code.DAL
{
    public class TeamMemberDAL
    {
        /// <summary>
        /// Adds a team memeber (With ID of userID) to the team (with ID of teamID).
        /// The new member can either be an admin or a regular member
        /// </summary>
        /// <param name="teamID"></param>
        /// <param name="userID"></param>
        /// <param name="isUserTeamAdmin"></param>
        /// <returns></returns>
        public static bool CreateTeamMember(Int64 teamID, Int64 userID, bool isUserTeamAdmin)
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
                command.Parameters["@mem_admin"].Value = (isUserTeamAdmin == true ? 1 : 0);

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

        /// <summary>
        /// Deletes all records for team with the specified ID
        /// </summary>
        /// <param name="teamID">ID of team which will have all its members deleted</param>
        /// <returns>True if successful, and false otherwise</returns>
        public static bool RemoveAllMembers(Int64 teamID)
        {
            bool wasAddSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String deleteCommand = "DELETE TeamMember WHERE team_mem_id = @team_mem_id";
                SqlCommand command = new SqlCommand(deleteCommand, sqlConn);
                command.Parameters.Add("@team_mem_id", SqlDbType.Int);
                command.Parameters["@team_mem_id"].Value = teamID;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public static bool DeleteTeamMember(Int64 memberID)
        {
            bool wasAddSuccessful = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String deleteCommand = "DELETE TeamMember WHERE user_mem_id = @user_mem_id";
                SqlCommand command = new SqlCommand(deleteCommand, sqlConn);
                command.Parameters.Add("@user_mem_id", SqlDbType.Int);
                command.Parameters["@user_mem_id"].Value = memberID;

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

        /// <summary>
        /// Determines if the user with useID is part of the team with teamID.
        /// </summary>
        /// <param name="teamID">ID of team</param>
        /// <param name="userID">ID of user</param>
        /// <returns>True if specified user is member of the specified team, False otherwise.</returns>
        public static bool IsUserMemberOfTeam(Int64 teamID, Int64 userID)
        {
            bool isPartOfTeam = false;
            String connString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {

                // Create SQL Delete Command for deleting all team memberss of the specified team
                String selectCommand = "SELECT * FROM TeamMember WHERE user_mem_id = @user_mem_id AND team_mem_id = @team_mem_id";
                SqlCommand command = new SqlCommand(selectCommand, sqlConn);
                command.Parameters.Add("@user_mem_id", SqlDbType.Int);
                command.Parameters["@user_mem_id"].Value = userID;
                command.Parameters.Add("@team_mem_id", SqlDbType.Int);
                command.Parameters["@team_mem_id"].Value = teamID;

                try
                {
                    sqlConn.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        if (Convert.ToInt64(dr["user_mem_id"]) == userID)
                        {
                            isPartOfTeam = true;
                        }
                    }
                    dr.Close();
                    sqlConn.Close();

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            return isPartOfTeam;
        }
    }
}