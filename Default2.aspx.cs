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

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		//if (IsPostBack)
		//{
		//    // 1st time
		//}
		//else
		//{
			for (int i = 0; i < 10; i++)
			{
				ImageButton grid = new ImageButton();
				grid.ImageUrl = "~/images/aGrid2.png";
				grid.Width = grid.Height = 36;
				grid.Click += grid_Click;
				Panel1.Controls.Add(grid);
			}
		//}
    }

	void grid_Click(object sender, ImageClickEventArgs e)
	{
		Random rand = new Random();
		ImageButton grid = ((ImageButton)sender);
		if ((rand.Next() % 2) == 0)
		{
			grid.ImageUrl = "~/images/aWhiteGrid2.png";
		}
		else
		{
			grid.ImageUrl = "~/images/aBlackGrid2.png";
		}
		grid.Click -= grid_Click;
	}
}
