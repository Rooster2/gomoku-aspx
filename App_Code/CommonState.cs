using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Get
/// </summary>
public class CommonState
{
    public static string ConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.
                ConnectionStrings["persons"].ConnectionString;
        }
    }

    public static long EpochTime
    {
        get
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }

    public static Dictionary<string, Board> Boards
    {
        get
        {
            Dictionary<string, Board> rms = 
                (Dictionary<string, Board>)HttpContext.Current.Application["boardlist"];
            if (rms == null)
            {
                rms = new Dictionary<string, Board>();
            }
            return rms;
        }
        set
        {
            HttpContext.Current.Application["boardlist"] = value;
        }
    }

    public static string PersonId
    {
        get
        {
            string id = (string)HttpContext.Current.Session["id"];
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }
        set
        {
            HttpContext.Current.Session["id"] = value;
        }
    }

    public static string PersonName
    {
        get
        {
            string id = (string)HttpContext.Current.Session["name"];
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }
        set
        {
            HttpContext.Current.Session["name"] = value;
        }
    }

    public static string CurrBoardId
    {
        get
        {
            string id = HttpContext.Current.Request.Params["id"];
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }
    }

    public static Board GetBoardById(string id)
    {
        if (!String.IsNullOrEmpty(id) && Boards.ContainsKey(id))
        {
            return Boards[id];
        }
        return null;
    }

    public static void SaveBoard(Board board)
    {
        Dictionary<string, Board> boardlist = Boards;
        boardlist[board.Id] = board;
    }
}