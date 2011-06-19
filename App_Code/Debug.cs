using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Debug Methods
/// </summary>
public class Debug
{
    private static bool _debug = true;
    public static void WriteLine(String s)
    {
        if (_debug)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG: " + s);
        }
    }
}
