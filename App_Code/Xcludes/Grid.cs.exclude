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
public class Grid_tictac
{
	private Boolean Used; // { get; set; }
	public ImageButton Body; // { get; set; }
	private const String GRID_NORMAL = "~/images/aGrid2.png";
	private const String GRID_BLACK = "~/images/aBlackGrid2.png";
	private const String GRID_WHITE = "~/images/aWhiteGrid2.png";

	public Grid_tictac()
	{
		Body = new ImageButton();
		Body.ImageUrl = GRID_NORMAL;
		Body.Width = 36;
		Body.Height = 36;
		Body.Click += new ImageClickEventHandler(body_Click);
	}

	void body_Click(object sender, ImageClickEventArgs e)
	{
		if (Used == true)
		{
			// TODO: prompt user
			Console.WriteLine("already clicked");
			return;
		}
		Console.WriteLine("not clicked");
		Used = true;
		ImageButton body = (ImageButton)sender;
		if (Status.Turn == Status.TURN_BLACK)
		{
			body.ImageUrl = GRID_BLACK;
		}
		else
		{
			body.ImageUrl = GRID_WHITE;
		}
		Status.ToggleTurn();
	}
}
