using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class entrance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CommonState.UserNick != null)
        {
            labelWelcome.Text = "Welcome: " + CommonState.UserNick;
        }
        else
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        Dictionary<string, Board> boardList = CommonState.Boards;
        foreach (KeyValuePair<String, Board> b in boardList)
	    {
            LinkButton link = new LinkButton();
            link.ID = b.Value.Id;
            link.Text = b.Value.Players.ToString() + " Players and " + 
                b.Value.Viewers.ToString() + " Viewers joined board: " + 
                b.Value.Nickname + " (" + b.Value.Id + ")";
            link.Click += new EventHandler(Board_Click);
            UpdatePanel1.ContentTemplateContainer.Controls.Add(link);
            Label newLine = new Label();
            newLine.Text = "<br />";
            UpdatePanel1.ContentTemplateContainer.Controls.Add(newLine);
	    }
    }

    void Board_Click(object sender, EventArgs e)
    {
        string boardId = ((Control)sender).ID;
        System.Diagnostics.Debug.WriteLine("board id is: " + boardId);
        //Response.Redirect(Request.Url.AbsolutePath.ToString() + "test");
        Response.Redirect("~/board.aspx?id=" + boardId);
    }

    protected void buttonStartANewBoard_Click(object sender, EventArgs e)
    {
        Board board = new Board();
        board.Nickname = textboxRoomName.Text;
        // debug only:
        if (CommonState.UserGuid != null)
        {
            board.PlayerWhiteId = CommonState.UserGuid;
            board.CurrTurn = board.PlayerWhiteId;
        }
        else
        {
            Response.Redirect("~/login.aspx");
            return;
        }
        // debug end

        // EPIC, because i new a new one at last in construct
        //Debug.WriteLine("Now board is null? " + board.chessboard[0, 0].GridType);
        Dictionary<string, Board> boardList = CommonState.Boards;
        boardList.Add(board.Id, board);
        CommonState.Boards = boardList;
        Response.Redirect("~/board.aspx?id=" + board.Id);
    }
}