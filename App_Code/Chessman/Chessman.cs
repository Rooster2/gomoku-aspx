using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Base
/// </summary>
public class Chessman
{
    int playerColor;
	public Chessman()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected abstract void canMoveTo(int x, int y)
    {
    }

    protected abstract void moveTo(int x, int y)
    {
    }

}