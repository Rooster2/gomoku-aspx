using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Room
/// </summary>
public class Board
{
    public string Id { get; set; }
    public string Nickname { get; set; }
    public int Players { get; set; }
    public int Viewers { get; set; }

    public Chessman[,] chessboard { get; set; }
    public string CurrTurn { get; set; }
    public string PlayerWhiteId { get; set; }
    public string PlayerBlackId { get; set; }

    public Board()
    {
        CurrTurn = String.Empty;
        PlayerWhiteId = String.Empty;
        PlayerBlackId = String.Empty;
        chessboard = new Chessman[Configure.ROWS, Configure.COLS];
        for (int i = 0; i < Configure.ROWS; i++)
        {
            for (int j = 0; j < Configure.COLS; j++)
            {
                chessboard[i, j] = new Chessman();
            }
        }
        Id = Guid.NewGuid().ToString();
        Players = 0;
        Viewers = 0;
    }

    public bool IsTurnOf(string playerId)
    {
        if (CurrTurn.Equals(playerId))
        {
            return true;
        }
        return false;
    }
    
	public void ToggleTurn()
	{
		if (CurrTurn.Equals(PlayerBlackId))
		{
			CurrTurn = PlayerWhiteId;
		}
		else // so: turn == TURN_WHITE
		{
			CurrTurn = PlayerBlackId;
		}
	}

    public void MakeMove(int i, int j)
    {
        if (CurrTurn.Equals(PlayerBlackId))
        {
            chessboard[i, j].GridType = Chessman.GRID_BLACK;
        }
        else
        {
            chessboard[i, j].GridType = Chessman.GRID_WHITE;
        }
    }
}