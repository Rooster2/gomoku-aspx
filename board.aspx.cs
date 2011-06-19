using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class board : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Debug.WriteLine("Is Post Back");
        }
        else
        {
            Debug.WriteLine("Is First Run");
        }

        // get board id or redirect
        if (CommonState.CurrBoardId == null)
        {
            Response.Redirect("~/boardlist.aspx");
            return;
        }
        else
        {
            labelDebugBoardId.Text = "board id: " + CommonState.CurrBoardId;
        }
    }
}
