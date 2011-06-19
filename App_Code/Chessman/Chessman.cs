using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Base
/// </summary>
public class Chessman
{
    //public const string GRID_NORMAL = "~/images/aNopeGrid.png";
    //public const string GRID_BLACK = "~/images/aBlackGrid3.png";
    //public const string GRID_WHITE = "~/images/aWhiteGrid3.png";
    public const string GRID_NOPE = "grid-nope";
    public const string GRID_BLACK = "grid-black";
    public const string GRID_WHITE = "grid-white";

    public string GridType { get; set; }
	public Chessman()
	{
        GridType = GRID_NOPE;
	}

}