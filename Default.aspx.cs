using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {

    }
	protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton1.ImageUrl = "~/images/aBlackGrid2.png";
	}
	protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton2.ImageUrl = "~/images/aWhiteGrid2.png";
	}
	protected void TextBox1_TextChanged(object sender, EventArgs e)
	{
		Label1.Text = TextBox1.Text;
	}
}
