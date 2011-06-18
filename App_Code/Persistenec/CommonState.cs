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

    public static string UserId
    {
        get
        {
            string id = (string)HttpContext.Current.Session["userid"];
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }
    }

    //private static Dictionary<string, BoardState> Boards
    //{
    //    get
    //    {
    //        string boardId = HttpContext.Current.Request.Params["id"];
    //        Dictionary<string, BoardState> brds =
    //            (Dictionary<string, BoardState>)HttpContext.Current.Application["boards"];
    //        if (boardId == null)
    //        {
    //            return null;
    //        }
    //        if (brds == null)
    //        {
    //            brds = (Dictionary<string, BoardState>)HttpContext.Current.Application["boards"];
    //        }
    //        return brds;
    //    }
    //    set
    //    {
    //        HttpContext.Current.Application["boards"] = value;
    //    }
    //}

    public static Board GetBoardById(string id)
    {
        if (!String.IsNullOrEmpty(id) && Boards.ContainsKey(id))
        {
            return Boards[id];
        }
        return null;
    }
}