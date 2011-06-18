using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Base
/// </summary>
public class Chessman
{
    public const string GRID_NORMAL = "~/images/aGrid2.png";
    public const string GRID_BLACK = "~/images/aBlackGrid2.png";
    public const string GRID_WHITE = "~/images/aWhiteGrid2.png";

    public string GridType { get; set; }
	public Chessman()
	{
        GridType = GRID_NORMAL;
	}

}