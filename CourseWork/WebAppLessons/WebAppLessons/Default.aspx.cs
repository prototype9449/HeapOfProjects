using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppLessons
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonNext_OnClick(object sender, EventArgs e)
        {
            if (PasswordBox.Text == "123")
            {
                Response.Redirect("ViewGrids.aspx");
            }
        }
    }
}