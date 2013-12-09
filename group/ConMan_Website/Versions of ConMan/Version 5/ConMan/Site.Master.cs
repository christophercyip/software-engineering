using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ConMan
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: Check if user is signed-in and change the text based on that
            if ( this.isUserSignedIn() == false)
            {
                // Not signed in
                lnkSignInRegOrSignOut.Text = "Sign-In/Register";
            }
            else
            {
                // Signed in
                lnkSignInRegOrSignOut.Text = "Sign Out, " + HttpContext.Current.User.Identity.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSignInRegOrSignOut_Click(object sender, EventArgs e)
        {
            if (this.isUserSignedIn() == true)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/SignIn.aspx");
            }
            else
            {
                // Signed in
                Response.Redirect("~/SignIn.aspx");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool isUserSignedIn()
        {
            // TODO: Check if user is signed-in and change the text based on that
            if (Page.User.Identity.Name.Equals("") || Page.User.Identity.Name == null || Page.User.Identity.IsAuthenticated == false)
            {
                // Not signed in
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
