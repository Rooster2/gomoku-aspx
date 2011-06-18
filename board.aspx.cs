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
    string BoardId
    {
        get
        {
            string id = Request.Params["id"];
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }
    }

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
        if (BoardId == null)
        {
            Response.Redirect("~/boardlist.aspx");
            return;
        }
        else
        {
            labelDebugBoardId.Text = "board id: " + BoardId;
        }

        // get Board object
        Board board = CommonState.GetBoardById(BoardId);
        if (board == null)
        {
            // TODO
            Response.Redirect("~/boardlist.aspx");
            return;
        }

        // restoring grid images of board
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                string controlId = String.Format("img{0}{1}", i + 1, j + 1);
                Debug.WriteLine("restoring control: " + controlId);
                ImageButton grid = (ImageButton)FindControl(controlId);
                grid.ImageUrl = board.chessboard[i, j].GridType;
            }
        }

        // we are done
    }
    
    protected void Grid_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton grid = ((ImageButton)sender);
        int i = Int32.Parse(grid.ID.Substring(3, 1)) - 1;
        int j = Int32.Parse(grid.ID.Substring(4, 1)) - 1;
        Debug.WriteLine("Clicked grid: " + i.ToString() + " " + j.ToString());

        Board board = CommonState.GetBoardById(BoardId);
        if (board == null)
        {
            Response.Redirect("~/boardlist.aspx");
            return;
        }

        if (CommonState.UserId == null)
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        if (board.IsTurnOf(CommonState.UserId))
        {
            // make move
            Debug.WriteLine("making move");
            board.MakeMove(i, j);
            board.ToggleTurn();
        }
        else
        {
            // TODO
            Debug.WriteLine("you are not eligible to make move");
            return;
        }
    }
}
