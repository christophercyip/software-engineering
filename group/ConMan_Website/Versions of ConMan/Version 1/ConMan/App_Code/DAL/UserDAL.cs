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
    public class UserDAL
    {

        //*********************************************************************
        // User Authentication
        //*********************************************************************
        /// <summary>
        /// Creates a new User and addes it to the UserInfo Table
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Int64 CreateNewUser(String fname, String lname, String email, String password)
        {
            // Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory\Database.mdf;Integrated Security=True;User Instance=True
            String connectionString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;

            Int64 userID = 0;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String insertCommand = "INSERT INTO UserInfo (first_name, last_name, email, password) VALUES (" +
                                            "@first_name, @last_name, @email, @password);";

                SqlCommand command = new SqlCommand(insertCommand, sqlConn);
                command.Parameters.Add("@first_name", SqlDbType.NVarChar);
                command.Parameters["@first_name"].Value = fname;
                command.Parameters.Add("@last_name", SqlDbType.NVarChar);
                command.Parameters["@last_name"].Value = lname;
                command.Parameters.Add("@email", SqlDbType.NVarChar);
                command.Parameters["@email"].Value = email;
                command.Parameters.Add("@password", SqlDbType.NVarChar);
                command.Parameters["@password"].Value = password;

                try
                {
                    sqlConn.Open();
                    // Execute scalar returns the element at the 1st column of the data inserted( which should be the ID)
                    command.ExecuteNonQuery();
                    sqlConn.Close();

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }

            userID = GetUserByEmail(email).ID;

            return userID;
        }

        //*********************************************************************
        // User Creation
        //*********************************************************************

        //*********************************************************************
        // User Info
        //*********************************************************************
        /// <summary>
        /// Gets the User information associated with an email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User object associated with the email, Null if no such User found</returns>
        public static User GetUserByEmail(String email)
        {
            // Data Source=.\SQLEXPRESS;AttachDbFilename=C:\LuisJr\USDSenior1\Software_Engineering\Test_Sites\App_Data\Database.mdf;Integrated Security=True;User Instance=True
            String connectionString = ConfigurationManager.ConnectionStrings["SqlDatabaseConnString"].ConnectionString;
            User newUser = null;

            // This will be -1 if user was not added or > 0 if the user was added
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String selectCommand = "SELECT * FROM UserInfo WHERE email = @email";

                SqlCommand command = new SqlCommand(selectCommand, sqlConn);
                command.Parameters.Add("@email", SqlDbType.NVarChar);
                command.Parameters["@email"].Value = email;

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = command;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                sqlConn.Open();
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    newUser = new User();
                    newUser.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);
                    newUser.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["first_name"]);
                    newUser.LastName = Convert.ToString(ds.Tables[0].Rows[0]["last_name"]);
                    newUser.Email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
                    newUser.Password = Convert.ToString(ds.Tables[0].Rows[0]["password"]);
                }
                sqlConn.Close();
            }

            return newUser;
        }
    }

}