using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Get
/// </summary>
public class CommonState
{
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

    public static string UserGuid
    {
        get
        {
            string id = (string)HttpContext.Current.Session["userguid"];
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }
        set
        {
            HttpContext.Current.Session["userguid"] = value;
        }
    }

    public static string UserNick
    {
        get
        {
            string id = (string)HttpContext.Current.Session["usernick"];
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }
        set
        {
            HttpContext.Current.Session["usernick"] = value;
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
}