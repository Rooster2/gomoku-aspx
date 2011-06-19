using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// handle grid's behavior
/// </summary>
public class Grid
{
	private Boolean Used; // { get; set; }
	public ImageButton Body; // { get; set; }
	private const String GRID_NORMAL = "~/images/aGrid2.png";
	private const String GRID_BLACK = "~/images/aBlackGrid2.png";
	private const String GRID_WHITE = "~/images/aWhiteGrid2.png";

	public Grid()
	{
		Body = new ImageButton();
		Body.ImageUrl = GRID_NORMAL;
		Body.Width = 36;
		Body.Height = 36;
		Body.Click += new ImageClickEventHandler(body_Click);
	}

	void body_Click(object sender, ImageClickEventArgs e)
	{
		if (Status.Turn == Status.TURN_BLACK)
		{
			Body.ImageUrl = GRID_BLACK;
		}
		else
		{
			Body.ImageUrl = GRID_WHITE;
		}
		Status.ToggleTurn();
	}
}
