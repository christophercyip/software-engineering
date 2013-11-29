using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ConMan.App_Code.DAL;
using System.Drawing;


namespace ConMan
{
    public partial class SignIn : System.Web.UI.Page
    {
        private const int FNAME_MAX_SIZE = 20;
        private const int LNAME_MAX_SIZE = 50;
        private const int EMAIL_MAX_SIZE = 50;
        private const int PASSWORD_MAX_SIZE = 10;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            // Clear results tag
            lblResultsSignIn.Text = "";


            // Make sure the email exist in the db first
            ConMan.App_Code.Entities.User user = UserDAL.GetUserByEmail(tbxEmailSignIn.Text);
            if (user != null)
            {
                // Make sure that the password matches up with the User associated
                // with the email
                if (user.Password.Equals(tbxPasswordSignIn.Text))
                {
                    FormsAuthentication.RedirectFromLoginPage(user.Email, false);
                }
                else
                {
                    lblResultsSignIn.ForeColor = Color.Red;
                    lblResultsSignIn.Text += " Incorrect Password. ";
                }
            }
            else
            {
                lblResultsSignIn.ForeColor = Color.Red;
                lblResultsSignIn.Text += " Email does not exist in ConMain database";
            }
        }



        /// <summary>
        /// Regristration button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Make sure to reset the results label
            this.lblResultsRegister.Text = "";

            if (Page.IsValid && this.IsRegistrationDataValid())
            {
                Int64 userID = UserDAL.CreateNewUser(this.tbxFirstNameRegister.Text, this.tbxLastNameRegister.Text,
                                                        this.tbxEmailRegister.Text, this.tbxPasswordRegister.Text);

                if (userID >= 0)
                {
                    lblResultsRegister.Text += "Successfully created user account (ID = " + userID + ")\n";
                    lblResultsRegister.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lblResultsRegister.Text += "Unable to create user account (ID = " + userID + ")\n";
                    lblResultsRegister.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool IsRegistrationDataValid()
        {
            String fname = tbxFirstNameRegister.Text;
            String lname = tbxLastNameRegister.Text;
            String email = tbxEmailRegister.Text;
            String password = tbxPasswordRegister.Text;
            String confirmPassword = tbxConfirmPasswordRegister.Text;

            bool isDataValid = true;

            if (fname.Trim().Length > FNAME_MAX_SIZE)
            {
                lblResultsRegister.Text += "- First name must have " + FNAME_MAX_SIZE + " or less characters.\n";
                lblResultsRegister.ForeColor = System.Drawing.Color.Red;
                isDataValid = false;
            }

            if (lname.Trim().Length > LNAME_MAX_SIZE)
            {
                lblResultsRegister.Text += "- Last name must have " + LNAME_MAX_SIZE + " or less characters.\n";
                lblResultsRegister.ForeColor = System.Drawing.Color.Red;
                isDataValid = false;
            }

            if (email.Trim().Length > EMAIL_MAX_SIZE)
            {
                lblResultsRegister.Text += "- Email must have " + EMAIL_MAX_SIZE + " or less characters.\n";
                lblResultsRegister.ForeColor = System.Drawing.Color.Red;
                isDataValid = false;
            }

            if (password.Trim().Length > PASSWORD_MAX_SIZE || confirmPassword.Trim().Length > PASSWORD_MAX_SIZE)
            {
                lblResultsRegister.Text += "- Password and Confirm Password must have " + EMAIL_MAX_SIZE + " or less characters.\n";
                lblResultsRegister.ForeColor = System.Drawing.Color.Red;
                isDataValid = false;
            }

            if (password.Equals(confirmPassword) == false)
            {
                lblResultsRegister.Text += "- Password and Confirm Password must be the same\n";
                lblResultsRegister.ForeColor = System.Drawing.Color.Red;
                isDataValid = false;
            }

            if (UserDAL.GetUserByEmail(tbxEmailRegister.Text) != null)
            {
                lblResultsRegister.Text += "- Email is already being used in a ConMan account. Please select a unique email.\n";
                isDataValid = false;
            }

            return isDataValid;
        }
    }
}