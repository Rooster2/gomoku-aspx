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
        RefreshBoardList();
    }

    void RefreshBoardList()
    {
        Dictionary<string, Board> boardList = CommonState.Boards;
        foreach (KeyValuePair<String, Board> b in boardList)
	    {
            LinkButton link = new LinkButton();
            link.ID = b.Value.Id;
            link.Click += new EventHandler(Board_Click);
            link.Text = ">> " + b.Value.Nickname +
                " (" + b.Value.Players.ToString() + " Players, " +
                b.Value.Viewers.ToString() + " Viewers)";
            if (Debug.IsDebug)
            {
                link.Text += " (" + b.Value.Id + ")";
            }
            Literal newLine = new Literal();
            newLine.Text = "<br />";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(link);
            UpdatePanel1.ContentTemplateContainer.Controls.Add(newLine);
	    }
    }

    void Board_Click(object sender, EventArgs e)
    {
        string boardId = ((Control)sender).ID;
        Debug.WriteLine("clicked board: " + boardId);
        Response.Redirect("~/board.aspx?id=" + boardId);
    }

    protected void buttonStartANewBoard_Click(object sender, EventArgs e)
    {
        Board board = new Board();
        board.Nickname = textboxBoardNick.Text;
        //// debug only:
        //if (CommonState.PersonId != null)
        //{
        //    // TEMP
        //    board.PlayerWhiteId = CommonState.PersonId;
        //    board.CurrTurn = board.PlayerWhiteId;
        //    board.Players++;
        //}
        //else
        //{
        //    Response.Redirect("~/login.aspx");
        //    return;
        //}
        //// debug end

        Dictionary<string, Board> boardList = CommonState.Boards;
        boardList.Add(board.Id, board);
        CommonState.Boards = boardList;
        Response.Redirect("~/board.aspx?id=" + board.Id);
    }

    protected void linkLogout_Click(object sender, EventArgs e)
    {
        string personId = CommonState.PersonId;
        CommonState.PersonId = null;
        // delete it from db, if it long term disconnect and someone
        // is using same name, but the id is diff, so it's safe
        if (!String.IsNullOrEmpty(personId) &&
            Person.FindPersonById(personId) != null)
        {
            Person.DeletePersonById(personId);
        }
        // optional
        Response.Redirect("~/");
    }
}