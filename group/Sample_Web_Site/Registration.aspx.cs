using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    //*************************************************************************
    // Fields
    //*************************************************************************
    private const int FNAME_MAX_SIZE = 20;
    private const int LNAME_MAX_SIZE = 50;
    private const int EMAIL_MAX_SIZE = 50;
    private const int PASSWORD_MAX_SIZE = 10;

    //*************************************************************************
    // Methods
    //*************************************************************************
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Make sure to reset the results label
        this.lblResults.Text = "";

        if (Page.IsValid && this.IsDataValid() )
        {
            Int64 userID = DAL.UserDal.CreateNewUser(this.tbxFirstName.Text, this.tbxLastName.Text, 
                                                    this.tbxEmail.Text, this.tbxPassword.Text);

            if (userID > 0)
            {
                lblResults.Text += "Successfully created user account (ID = " + userID + ")\n";
                lblResults.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblResults.Text += "Unable to create user account (ID = " + userID + ")\n";
                lblResults.ForeColor = System.Drawing.Color.Blue;
            }
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool DoesEmailExistInDB(String email)
    {
        bool recordExistsWithSameEmail = false;

        String connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            String selectQuery = "SELECT * FROM UserInfo " +
                                    "WHERE email = '" + email + "'";

            SqlCommand command = new SqlCommand(selectQuery, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader(); 
            

            if (dataReader.HasRows == true)
            {
                recordExistsWithSameEmail = true;
            }
            
            connection.Close();
        }

        return recordExistsWithSameEmail;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected bool IsDataValid()
    {
        String fname = tbxFirstName.Text;
        String lname = tbxLastName.Text;
        String email = tbxEmail.Text;
        String password = tbxPassword.Text;
        String confirmPassword = tbxConfirmPassword.Text;

        bool isDataValid = true;

        if (fname.Trim().Length > FNAME_MAX_SIZE)
        {
            lblResults.Text += "- First name must have " + FNAME_MAX_SIZE + " or less characters.\n";
            lblResults.ForeColor = System.Drawing.Color.Red;
            isDataValid = false;
        }

        if (lname.Trim().Length > LNAME_MAX_SIZE)
        {
            lblResults.Text += "- Last name must have " + LNAME_MAX_SIZE + " or less characters.\n";
            lblResults.ForeColor = System.Drawing.Color.Red;
            isDataValid = false;
        }

        if (email.Trim().Length > EMAIL_MAX_SIZE)
        {
            lblResults.Text += "- Email must have " + EMAIL_MAX_SIZE + " or less characters.\n";
            lblResults.ForeColor = System.Drawing.Color.Red;
            isDataValid = false;
        }

        if (password.Trim().Length > PASSWORD_MAX_SIZE || confirmPassword.Trim().Length > PASSWORD_MAX_SIZE)
        {
            lblResults.Text += "- Password and Confirm Password must have " + EMAIL_MAX_SIZE + " or less characters.\n";
            lblResults.ForeColor = System.Drawing.Color.Red;
            isDataValid = false;
        }

        if (password.Equals(confirmPassword) == false)
        {
            lblResults.Text += "- Password and Confirm Password must be the same\n";
            lblResults.ForeColor = System.Drawing.Color.Red;
            isDataValid = false;
        }

        if (this.DoesEmailExistInDB(email) == true)
        {
            lblResults.Text += "- Email is already being used in a ConMan account. Please select a unique email.\n";
            isDataValid = false;
        }

        return isDataValid;
    }

    /// <summary>
    /// 
    /// </summary>
    protected void ClearFields()
    {
        tbxFirstName.Text = "";
        tbxLastName.Text = "";
        tbxEmail.Text = "";
        tbxPassword.Text = "";
        tbxConfirmPassword.Text = "";
    }
}
