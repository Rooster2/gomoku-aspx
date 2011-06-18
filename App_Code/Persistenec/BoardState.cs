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

    Chessman[,] board;
    String boardId;
    String playerWhiteId;
    String playerBlackId;
    int CurrTurn;

    public BoardState()
	{
	}

	public void ToggleTurn()
	{
		if (CurrTurn == TURN_BLACK)
		{
			CurrTurn = TURN_WHITE;
		}
		else // so: turn == TURN_WHITE
		{
			CurrTurn = TURN_BLACK;
		}
	}
}