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
    protected Boolean[,] clicked;
    protected int[,] brd;
    protected Status_o status;// = new Status_o();
    protected const String GRID_NORMAL = "~/images/aGrid2.png";
    protected const String GRID_BLACK = "~/images/aBlackGrid2.png";
    protected const String GRID_WHITE = "~/images/aWhiteGrid2.png";
    protected void Page_Load(object sender, EventArgs e)
    {
        //HttpContext.Current.Res
        if (IsPostBack)
        {
            Debug.WriteLine("isPostBack non-1st run");
            //img1.ImageUrl = "~";
        }
        else
        {
            Debug.WriteLine("1st run");
        }
        brd = (int[,])HttpContext.Current.Session["board"];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                String controlId = String.Format("img{0}{1}", i + 1, j + 1);
                Debug.WriteLine("restoring control: " + controlId);
                ImageButton ib = (ImageButton) FindControl(controlId);
                int gridStatus = brd[i, j];
                if (gridStatus == 0)
                {
                    ib.ImageUrl = GRID_NORMAL;
                }
                else if (gridStatus == 1)
                {
                    ib.ImageUrl = GRID_BLACK;
                }
                else
                {
                    ib.ImageUrl = GRID_WHITE;
                }
            }
        }
        //status = new Status_o();
        //status.Turn = Status_o.TURN_WHITE;
    }

    protected void img_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = ((ImageButton)sender);
        int i = Int32.Parse(ib.ID.Substring(3, 1)) - 1;
        int j = Int32.Parse(ib.ID.Substring(4, 1)) - 1;
        Debug.WriteLine("clicked ImageButton: " + i.ToString() + " " + j.ToString());

        brd = (int[,])HttpContext.Current.Session["board"];
        if (brd[i, j] != 0)
        {
            Debug.WriteLine("img_Click: this button was already clicked");
            return;
        }

        //status.Turn = Status_o.TURN_WHITE;
        if (Status.Turn == Status.TURN_BLACK)
        {
            ib.ImageUrl = GRID_BLACK;
            brd[i, j] = 1;
        }
        else
        {
            ib.ImageUrl = GRID_WHITE;
            brd[i, j] = 2;
        }
        Status.ToggleTurn();

        ////Point gridLoc = new Point();
        ////gridLoc.X = 
        //String isClicked = ib.Attributes["IsClicked"];
        //if (isClicked != null && isClicked.Equals("true"))
        //{
        //    Debug.WriteLine("already clicked!");
        //    return;
        //}

        //Debug.WriteLine("clicking");
        //ib.Attributes.Add("IsClicked", "true");


    }

	protected void Page_Init()
	{
		//if (!IsPostBack)
		//{
            //grids = new Grid[9];
            //for (int i = 0; i < grids.Length; i++)
            //{
            //    grids[i] = new Grid();
            //    Panel1.Controls.Add(grids[i].Body);
            //}
		//}
        //for (int i = 0; i < 10; i++)
        //{
        //    ;
        //}
	}


    protected void Button1_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session["board"] = new int[3, 3];
    }
}
