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
/// Debug 的摘要说明
/// </summary>
public class Debug
{
	public Debug()
	{
	}

    public static void WriteLine(String s)
    {
        System.Diagnostics.Debug.WriteLine("DEBUG: " + s);
    }
}
