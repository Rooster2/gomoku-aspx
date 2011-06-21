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
    static string COL_LAST = "person_lastactivity";

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

    public static List<string> ListUsersOnBoard(string boardId)
    {
        string query = String.Format("SELECT * FROM [boards] WHERE {0}=@boardId AND {1} > {2}",
            COL_BID, COL_LAST, CommonState.EpochTime - Configure.CONNECTOIN_TIMEOUT);
        List<string> list = new List<string>();
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@boardId", boardId);
                cmd.Prepare();
                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add((string)reader[COL_PID]);
                    }
                }
            }
        }
        return list;
    }

    public static void KeepPersonAlive(string boardId, string personId)
    {
        string query = String.
            Format("SELECT * FROM [boards] WHERE {0}=@boardId AND {1}=@personId", COL_BID, COL_PID);
        string update = String.
            Format("UPDATE [boards] SET {0}=@last WHERE {1}=@boardId AND {2}=@personId", COL_LAST, COL_BID, COL_PID);
        string add = String.
            Format("INSERT INTO [boards] ({0}, {1}, {2}) VALUES (@boardId, @personId, @last)", COL_BID, COL_PID, COL_LAST);
        //string updatePerson = String.
        //    Format("UPDATE [persons] SET {0}=@last WHERE {1}=@boardId AND {2}=@personId", COL_LAST, COL_BID, COL_PID);
        
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
                        goto UPDATE;
                    }
                    else
                    {
                        goto INSERT;
                    }
                }

            INSERT:
                cmd.CommandText = add;
                //cmd.Parameters.Add("@boardId", boardId);
                //cmd.Parameters.Add("@personId", personId);
                //cmd.Parameters.Add("@last", CommonState.EpochTime);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return;

            UPDATE:
                cmd.CommandText = update;
                //cmd.Parameters.Add("@boardId", boardId);
                //cmd.Parameters.Add("@personId", personId);
                //cmd.Parameters.Add("@last", CommonState.EpochTime);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return;
            }
        }
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
        if (Chessman.GRID_NOPE.Equals(chessboard[i, j].GridType))
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
            chessboard[i, j].GridType = Chessman.GRID_BLACK;
        }
        else
        {
            chessboard[i, j].GridType = Chessman.GRID_WHITE;
        }
    }
}