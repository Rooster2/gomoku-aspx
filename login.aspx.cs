using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            textboxUsername.Text = (string)Session["username"];
        }
    }
    protected void buttonLogin_Click(object sender, EventArgs e)
    {
        if (textboxUsername.Text == String.Empty)
        {
            // TODO
            return;
        }
        Session["username"] = textboxUsername.Text;
        Session["userid"] = Guid.NewGuid().ToString();
        Response.Redirect("~/boardlist.aspx");
    }
}