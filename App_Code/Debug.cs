﻿using System;
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
    public const bool IsDebug = true;
    public static void WriteLine(String s)
    {
        if (IsDebug)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG: " + s);
        }
    }
}
