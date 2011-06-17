using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Base
/// </summary>
public abstract class Chessman
{
    int playerColor;
	public Chessman()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected abstract void canMoveTo();

    protected abstract void moveTo();

}