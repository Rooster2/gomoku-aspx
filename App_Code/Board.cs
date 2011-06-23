using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlServerCe;

/// <summary>
/// Summary description for Room
/// </summary>
public class Board
{
    static string COL_BID = "board_id";
    static string COL_PID = "person_id";
    static string COL_PNAME = "person_name";
    static string COL_LAST = "person_lastactivity";

    public string Id { get; set; }
    public string Nickname { get; set; }
    public int Players { get; set; }
    public int Viewers { get; set; }

    public Chessman[,] chessboard { get; set; }
    public string CurrTurn { get; set; }
    public string PlayerWhiteId { get; set; }
    public string PlayerBlackId { get; set; }
    public string WinnerId { get; set; }
    public bool IsGameOver { get; set; }

    public Board()
    {
        CurrTurn = String.Empty;
        PlayerWhiteId = String.Empty;
        PlayerBlackId = String.Empty;
        WinnerId = String.Empty;
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
        IsGameOver = true;
    }

    public void ClearForANewGame()
    {
        CurrTurn = String.Empty;
        PlayerWhiteId = String.Empty;
        PlayerBlackId = String.Empty;
        IsGameOver = true;
    }

    public void NewGameStartInit()
    {
        IsGameOver = false;
        WinnerId = String.Empty;
        CurrTurn = PlayerWhiteId;
        chessboard = new Chessman[Configure.ROWS, Configure.COLS];
        for (int i = 0; i < Configure.ROWS; i++)
        {
            for (int j = 0; j < Configure.COLS; j++)
            {
                chessboard[i, j] = new Chessman();
            }
        }
    }

    public static List<string> ListPersonOnBoard(string boardId)
    {
        string query = String.Format("SELECT * FROM [boards] WHERE {0}=@boardId AND {1} > {2}",
            COL_BID, COL_LAST, CommonState.EpochTime - Configure.CONNECTOIN_TIMEOUT);
        string query2 =
            "SELECT persons.person_name " +
            "FROM persons INNER JOIN " +
            "         boards ON boards.person_id = persons.person_id " +
            "WHERE boards.board_id=@boardId " +
            "         AND boards.person_lastactivity > " +
            (CommonState.EpochTime - Configure.CONNECTOIN_TIMEOUT).ToString();
        List<string> list = new List<string>();
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = query2;
                cmd.Parameters.Add("@boardId", boardId);
                cmd.Prepare();
                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add((string)reader[COL_PNAME]);
                    }
                }
            }
        }
        return list;
    }

    public static void KeepPersonAliveOnBoard(string boardId, string personId)
    {
        string query = String.
            Format("SELECT * FROM [boards] WHERE {0}=@boardId AND {1}=@personId", COL_BID, COL_PID);
        string update = String.
            Format("UPDATE [boards] SET {0}=@last WHERE {1}=@boardId AND {2}=@personId", COL_LAST, COL_BID, COL_PID);
        string add = String.
            Format("INSERT INTO [boards] ({0}, {1}, {2}) VALUES (@boardId, @personId, @last)", COL_BID, COL_PID, COL_LAST);
        string updatePerson = String.
            Format("UPDATE [persons] SET {0}=@last WHERE {1}=@personId", COL_LAST, COL_PID);
        
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@boardId", boardId);
                cmd.Parameters.Add("@personId", personId);
                cmd.Parameters.Add("@last", CommonState.EpochTime);
                cmd.Prepare();
                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cmd.CommandText = update;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = add;
                        cmd.ExecuteNonQuery();
                    }
                }
                
                // now update person table
                cmd.CommandText = updatePerson;
                cmd.ExecuteNonQuery();
            }
        }
    }

    public bool IsReady()
    {
        if (!String.IsNullOrEmpty(PlayerWhiteId) &&
            !String.IsNullOrEmpty(PlayerBlackId))
        {
            return true;
        }
        return false;
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
        if (Chessman.GRID_IMG_NOPE.Equals(chessboard[i, j].GridImage))
        {
            ;
        }
        else
        {
            Debug.WriteLine("Chessman existed at this position");
            return;
        }
        if (CurrTurn.Equals(PlayerBlackId))
        {
            chessboard[i, j].GridImage = Chessman.GRID_IMG_BLACK;
            chessboard[i, j].GridType = Chessman.GRID_TYPE_BLACK;
        }
        else
        {
            chessboard[i, j].GridImage = Chessman.GRID_IMG_WHITE;
            chessboard[i, j].GridType = Chessman.GRID_TYPE_WHITE;
        }
    }

    public void EvaluateAt(int row, int col)
    {
        int theChessmanType = chessboard[row, col].GridType;
        int r, c, consCount;

        // horiz, left
        r = row;
        c = col - 1;
        consCount = 0;
        while (c >= 0 && c >= col - 4 &&
            chessboard[row, c].GridType == theChessmanType)
        {
            consCount++;
            c--;
        }
        // horiz right
        c = col + 1;
        while (c < Configure.COLS && c <= col + 4 &&
            chessboard[row, c].GridType == theChessmanType)
        {
            consCount++;
            c++;
        }
        if (consCount >= 4)
            goto GAMEOVER;

        //vertical up
        r = row - 1;
        c = col;
        consCount = 0;
        while (r >= 0 && r >= row - 4 &&
            chessboard[r, col].GridType == theChessmanType)
        {
            consCount++;
            r--;
        }
        // vertical down
        r = row + 1;
        while (r < Configure.ROWS && r <= row + 4 &&
            chessboard[r, col].GridType == theChessmanType)
        {
            consCount++;
            r++;
        }
        if (consCount >= 4)
            goto GAMEOVER;

        // \\\\\\\\ upper-left
        r = row - 1;
        c = col - 1;
        consCount = 0;
        while (r >= 0 && c >= 0 && r >= row - 4 &&
            chessboard[r, c].GridType == theChessmanType)
        {
            consCount++;
            r--;
            c--;
        }
        // \\\\\\\\ lower-right
        r = row + 1;
        c = col + 1;
        while (r < Configure.ROWS && c < Configure.COLS && r <= row + 4 &&
            chessboard[r, c].GridType == theChessmanType)
        {
            consCount++;
            r++;
            c++;
        }
        if (consCount >= 4)
            goto GAMEOVER;

        // //////// upper-right
        r = row - 1;
        c = col + 1;
        consCount = 0;
        while (r >= 0 && c < Configure.COLS && r >= row - 4 &&
            chessboard[r, c].GridType == theChessmanType)
        {
            consCount++;
            r--;
            c++;
        }
        // //////// lower-left-
        r = row + 1;
        c = col - 1;
        while (r < Configure.ROWS && c >= 0 && r <= row + 4 &&
            chessboard[r, c].GridType == theChessmanType)
        {
            consCount++;
            r++;
            c--;
        }
        if (consCount >= 4)
            goto GAMEOVER;

        // nope
        return;

        // found a winner, congrets!
    GAMEOVER:
        WinnerId = CurrTurn;
        ClearForANewGame();
    }

    public static void KillPersonOnBoard(string boardId, string personId)
    {
        if (String.IsNullOrEmpty(boardId) || String.IsNullOrEmpty(personId))
            return;

        string query = String.
            Format("SELECT * FROM [boards] WHERE {0}=@boardId AND {1}=@personId", COL_BID, COL_PID);
        string delete = String.
            Format("DELETE FROM [boards] WHERE {0}=@boardId AND {1}=@personId", COL_BID, COL_PID);
        
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@boardId", boardId);
                cmd.Parameters.Add("@personId", personId);
                cmd.Prepare();
                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cmd.CommandText = delete;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    } // end method KillPersonOnBoard
}