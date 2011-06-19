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
        if (CommonState.UserNick != null)
        {
            textboxUsername.Text = CommonState.UserNick;
        }
    }
    protected void buttonLogin_Click(object sender, EventArgs e)
    {
        if (String.Empty.Equals(textboxUsername.Text))
        {
            // TODO
            return;
        }
        CommonState.UserNick = textboxUsername.Text;
        CommonState.UserGuid = Guid.NewGuid().ToString();
        Response.Redirect("~/boardlist.aspx");
    }
}