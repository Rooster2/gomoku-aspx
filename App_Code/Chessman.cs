using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Base
/// </summary>
public class Chessman
{
    public const string GRID_IMG_NOPE = "~/images/aNopeGrid.png";
    public const string GRID_IMG_BLACK = "~/images/aBlackGrid3.png";
    public const string GRID_IMG_WHITE = "~/images/aWhiteGrid3.png";

    //public const string GRID_NOPE = "grid-nope";
    //public const string GRID_BLACK = "grid-black";
    //public const string GRID_WHITE = "grid-white";
    public const int GRID_TYPE_NOPE = 0;
    public const int GRID_TYPE_WHITE = 1;
    public const int GRID_TYPE_BLACK = 2;

    public string GridImage { get; set; }
    public int GridType { get; set; }
	public Chessman()
	{
        GridImage = GRID_IMG_NOPE;
        GridType = GRID_TYPE_NOPE;
	}

}