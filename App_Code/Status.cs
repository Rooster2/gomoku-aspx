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
/// Summary description for Status
/// </summary>
public class Status
{
	public static int Turn; // { get; set; }
	public const int TURN_BLACK = 0x0;
	public const int TURN_WHITE = 0x1;
	public Status()
	{
	}
	public static void ToggleTurn()
	{
		if (Turn == TURN_BLACK)
		{
			Turn = TURN_WHITE;
		}
		else // so: turn == TURN_WHITE
		{
			Turn = TURN_BLACK;
		}
	}
}
