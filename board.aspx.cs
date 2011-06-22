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
        if (IsPostBack)
        {
            Debug.WriteLine("Is Post Back");
        }
        else
        {
            Debug.WriteLine("Is First Run");
        }

        Debug.WriteLine("checking if user is online");
        if (Person.IsSessionAlive(CommonState.PersonId))
        {
            string name = Person.FindNameById(CommonState.PersonId);
            if (name != null)
            {
                labelWelcome.Text = "Welcome: " + name;
            }
            else
            {
                Response.Redirect("~/login.aspx");
                return;
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        // keep person alive on this board
        Board.KeepPersonAliveOnBoard(CommonState.CurrBoardId, CommonState.PersonId);

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

        // show game progress
        using (Literal l = new Literal())
        {
            if (board.IsGameOver())
            {
                Debug.WriteLine("Page_Load: game is over");
                l.Text = "Winner is: " + Person.FindNameById(board.WinnerId);
            }
            else
            {
                Debug.WriteLine("show who's turn");
                if (board.CurrTurn.Equals(board.PlayerWhiteId))
                {
                    l.Text = "White's turn";
                }
                else
                {
                    l.Text = "Black's turn";
                }
            }
            WinnerIs.Controls.Add(l);
        }

        // join buttons
        // white
        if (String.IsNullOrEmpty(board.PlayerWhiteId) ||
            !Person.IsPersonOnBoard(board.Id, board.PlayerWhiteId))
        {
            board.PlayerWhiteId = String.Empty;
            Button b = new Button();
            b.ID = "White";
            b.Click +=new EventHandler(JoinGame);
            b.Text = "Participate in";
            if (CommonState.PersonId.Equals(board.PlayerBlackId) ||
                CommonState.PersonId.Equals(board.PlayerWhiteId))
            {
                b.Enabled = false;
            }
            WhiteSideHolder.Controls.Add(b);
        }
        else
        {
            Literal l = new Literal();
            l.Text = Person.FindNameById(board.PlayerWhiteId);
            WhiteSideHolder.Controls.Add(l);
        }
        
        if (String.IsNullOrEmpty(board.PlayerBlackId) ||
            !Person.IsPersonOnBoard(board.Id, board.PlayerBlackId))
        {
            board.PlayerBlackId = String.Empty;
            Button b = new Button();
            b.ID = "Black";
            b.Click +=new EventHandler(JoinGame);
            b.Text = "Participate in";
            if (CommonState.PersonId.Equals(board.PlayerBlackId) ||
                CommonState.PersonId.Equals(board.PlayerWhiteId))
            {
                b.Enabled = false;
            }
            BlackSideHolder.Controls.Add(b);
        }
        else
        {
            Literal l = new Literal();
            l.Text = Person.FindNameById(board.PlayerBlackId);
            BlackSideHolder.Controls.Add(l);
        }
        // end join buttons

        //// TEMP
        //if (String.IsNullOrEmpty(board.PlayerBlackId) &&
        //    !CommonState.PersonId.Equals(board.PlayerWhiteId))
        //{
        //    board.PlayerBlackId = CommonState.PersonId;
        //    board.Players++;
        //}

        GenerateAndRestoreChessboard(board);
        var list = Board.ListUsersOnBoard(CommonState.CurrBoardId);
        ListViewers.DataSource = list;
        ListViewers.DataBind();
    }

    void JoinGame(object sender, EventArgs e)
    {
        string side = ((Control)sender).ID;
        Board board = CommonState.GetBoardById(CommonState.CurrBoardId);
        if (side.Equals("White"))
        {
            board.PlayerWhiteId = CommonState.PersonId;
            // someone disconn dirty hack
            if (board.CurrTurn.Equals(board.PlayerBlackId))
            {
                ;
            }
            else
            {
                board.CurrTurn = board.PlayerWhiteId;
            }
        }
        else if (side.Equals("Black"))
        {
            board.PlayerBlackId = CommonState.PersonId;
            if (board.CurrTurn.Equals(board.PlayerWhiteId))
            {
                ;
            }
            else
            {
                board.CurrTurn = board.PlayerBlackId;
            }
        }

        if ((board.IsBranyNewBoard || board.IsGameOver()) && board.IsReady())
        {
            board.NewGameStartInit();
        }
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
        if (CommonState.PersonId == null)
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        if (board.IsGameOver())
        {
            Debug.WriteLine("game is over, don't touch");
            return;
        }
        if (!board.IsReady())
        {
            Debug.WriteLine("someone is missing");
            return;
        }

        Debug.WriteLine("White is: " + board.PlayerWhiteId);
        Debug.WriteLine("Black is: " + board.PlayerBlackId);
        Debug.WriteLine("Turn is : " + board.CurrTurn);
        Debug.WriteLine("You are : " + CommonState.PersonId);
        // check if the user can move
        if (board.IsTurnOf(CommonState.PersonId))
        {
            // make move
            Debug.WriteLine("making move");
            board.MakeMove(i, j);
            board.EvaluateAt(i, j);
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
