using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class board : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CommonState.UserNick == null)
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        if (IsPostBack)
        {
            Debug.WriteLine("Is Post Back");
        }
        else
        {
            Debug.WriteLine("Is First Run");
        }

        // test if board exists or redirect
        if (CommonState.CurrBoardId == null)
        {
            Response.Redirect("~/boardlist.aspx");
            return;
        }

        // get Board object
        Board board = CommonState.GetBoardById(CommonState.CurrBoardId);
        if (board == null)
        {
            // TODO
            Response.Redirect("~/boardlist.aspx");
            return;
        }
        this.Title = String.Format("Board: {0} ({1})", board.Nickname, board.Id);

        // TEMP
        if (String.IsNullOrEmpty(board.PlayerBlackId) &&
            !CommonState.UserGuid.Equals(board.PlayerWhiteId))
        {
            board.PlayerBlackId = CommonState.UserGuid;
        }

        GenerateAndRestoreChessboard(board);
    }

    void GenerateAndRestoreChessboard(Board board)
    {
        Table chessboard = new Table();
        for (int i = 0; i < Configure.ROWS; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < Configure.COLS; j++)
            {
                TableCell gridHolder = new TableCell();
                gridHolder.ID = String.Format("cell{0:X}{1:X}", i + 1, j + 1);
                gridHolder.CssClass = "grid-bg";
                //gridHolder.Style["background-image"] = "url(aGrid2.png)";
                //gridHolder.Style["background-size"] = String.Format("{0}px {0}px", SIZE);

                ImageButton grid = new ImageButton();
                grid.Click += new ImageClickEventHandler(Grid_Click);
                grid.Height = 32;
                grid.Width = 32;
                grid.ImageUrl = board.chessboard[i, j].GridType;
                //Button grid = new Button();
                //grid.Click += new EventHandler(Grid_Click);
                //grid.BorderStyle = BorderStyle.None;
                //grid.CssClass = board.chessboard[i, j].GridType;
                // ----
                grid.ID = String.Format("grid{0:X}{1:X}", i + 1, j + 1);
                
                gridHolder.Controls.Add(grid);
                tr.Controls.Add(gridHolder);
            }
            chessboard.Controls.Add(tr);
        }
        ChessboardHolder.Controls.Add(chessboard);
    }

    //void Grid_Click(object sender, EventArgs e)
    void Grid_Click(object sender, ImageClickEventArgs e)
    {
        // locate the position of clilcked grid
        ImageButton grid = ((ImageButton)sender);
        //Button grid = ((Button)sender);
        int i = Int32.Parse(grid.ID.Substring(4, 1), NumberStyles.HexNumber) - 1;
        int j = Int32.Parse(grid.ID.Substring(5, 1), NumberStyles.HexNumber) - 1;
        Debug.WriteLine("Clicked grid: " + i.ToString() + " " + j.ToString());

        // get the current Board
        Board board = CommonState.GetBoardById(CommonState.CurrBoardId);
        if (board == null)
        {
            Response.Redirect("~/boardlist.aspx");
            return;
        }

        // is user logged in?
        if (CommonState.UserGuid == null)
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        Debug.WriteLine("White is: " + board.PlayerWhiteId);
        Debug.WriteLine("Black is: " + board.PlayerBlackId);
        Debug.WriteLine("Turn is : " + board.CurrTurn);
        Debug.WriteLine("You are : " + CommonState.UserGuid);
        // check if the user can move
        if (board.IsTurnOf(CommonState.UserGuid))
        {
            // make move
            Debug.WriteLine("making move");
            board.MakeMove(i, j);
            board.ToggleTurn();
            // TODO
            //CommonState.SaveBoard(board);
            //Response.Redirect(Request.Url.ToString());
            //Page_Load(null, null);
            Debug.WriteLine("now is turn of: " + board.CurrTurn);
        }
        else
        {
            // TODO
            Debug.WriteLine("you are not eligible to make move");
            return;
        }
    }

    protected void linkBackToBoardList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/boardlist.aspx");
    }
}
