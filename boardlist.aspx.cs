using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class entrance : System.Web.UI.Page
{
    private Dictionary<string, Room> Rooms
    {
        get
        {
            Dictionary<string, Room> rms = (Dictionary<string, Room>)Application["rooms"];
            if (rms == null)
            {
                rms = new Dictionary<string, Room>();
            }
            return rms;
        }
        set
        {
            Application["rooms"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            labelWelcome.Text = "Welcome: " + (string)Session["username"];
        }
        else
        {
            Response.Redirect("~/login.aspx");
            return;
        }

        Dictionary<string, Room> rooms = Rooms;
        ////Dictionary<string, string> roomToId = new Dictionary<string,string>();
        ////int index = 0;
        foreach (KeyValuePair<String, Room> r in rooms)
	    {
            ////index++;
            LinkButton link = new LinkButton();
            ////link.ID = "room" + index.ToString();
            link.ID = r.Value.Id;
            link.Text = r.Value.Persons + " Persons in room: " + r.Value.Name + " (" + r.Value.Id + ")";
            link.Click += new EventHandler(room_Click);
            UpdatePanel1.ContentTemplateContainer.Controls.Add(link);
            Label newLine = new Label();
            newLine.Text = "<br />";
            UpdatePanel1.ContentTemplateContainer.Controls.Add(newLine);

            ////roomToId.Add(link.ID, r.Value.Id);
	    }
        ////ViewState["roomToId"] = roomToId;
    }

    void room_Click(object sender, EventArgs e)
    {
        ////if (ViewState["roomToId"] == null)
        ////{
        ////    // TODO
        ////    return;
        ////}
        ////string roomNo = ((Control)sender).ID;
        ////Dictionary<string, string> roomToId = (Dictionary<string, string>)ViewState["roomToId"];
        ////if (!roomToId.ContainsKey(roomNo) {
        ////    // TODO
        ////    return;
        ////}
        ////string roomId = roomToId[roomNo];
        string roomId = ((Control)sender).ID;
        System.Diagnostics.Debug.WriteLine("room id is: " + roomId);
        //Response.Redirect(Request.Url.AbsolutePath.ToString() + "test");
        Response.Redirect("~/board.aspx?id=" + roomId);
    }

    protected void buttonBuildNewRoom_Click(object sender, EventArgs e)
    {
        Room r = new Room();
        r.Name = textboxRoomName.Text;
        Dictionary<string, Room> rooms = Rooms;

        //if (rooms.ContainsKey(r.Id))
        //{
        //    // TODO: tell user that this room already exists
        //    return;
        //}
        rooms.Add(r.Id, r);
        Rooms = rooms;
    }
}