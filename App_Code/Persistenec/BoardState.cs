using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BoardState
/// </summary>
public class BoardState
{
	const int TURN_WHITE = 0x0;
	const int TURN_BLACK = 0x1;

    int[,] board;
    Guid boardId;
    Guid playerWhiteId;
    Guid playerBlackId;
    int Turn;

    public BoardState()
	{
	}

	public void ToggleTurn()
	{
		if (Turn == TURN_BLACK)
		{
			Turn = TURN_WHITE;
		}
		else // so: turn == TURN_WHITE
		{
			Turn = TURN_BLACK;
		}
	}
}