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
                labelWelcome.Text += name;
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
        // TODO: hack
        try
        {
            Dictionary<string, Board> boardList = CommonState.Boards;
            foreach (KeyValuePair<String, Board> b in boardList)
            {
                if (Board.ListPersonOnBoard(b.Value.Id).Count == 0)
                {
                    boardList.Remove(b.Value.Id);
                    continue;
                }
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
        catch (InvalidOperationException ex)
        {
            UpdatePanel1.ContentTemplateContainer.Controls.Clear();
        }

        if (UpdatePanel1.ContentTemplateContainer.Controls.Count == 0)
        {
            Literal l = new Literal();
            l.Text = ">> no Board availiable, you may start a new one.";
            UpdatePanel1.ContentTemplateContainer.Controls.Add(l);
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

        // TODO: hack
        board.Viewers++;
        Board.KeepPersonAliveOnBoard(board.Id, CommonState.PersonId);

        Dictionary<string, Board> boardList = CommonState.Boards;
        boardList.Add(board.Id, board);
        CommonState.Boards = boardList;
        Response.Redirect("~/board.aspx?id=" + board.Id);
    }

    protected void linkLogout_Click(object sender, EventArgs e)
    {
        // delete it from db, if it long term disconnect and someone
        // may use same name, but the id is diff, so it's safe
        string personId = CommonState.PersonId;
        if (!String.IsNullOrEmpty(personId) &&
            Person.FindPersonById(personId) != null)
        {
            Person.DeletePersonById(personId);
        }
        CommonState.PersonId = null;
        // optional
        Response.Redirect("~/");
    }
}