using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlServerCe;

/// <summary>
/// Summary description for Person
/// </summary>
public class Person
{
    static string COL_NAME = "person_name";
    static string COL_ID = "person_id";
    static string COL_LAST = "person_lastactivity";

    public string Id { get; set; }
    public string Name { get; set; }
    public long LastActivity { get; set; }
	
    public static bool IsOnline(string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return false;
        }
        Person p = Person.FindPersonByACol(COL_ID, id);
        if (p != null)
        {
            return IsOnline(p.LastActivity);
        }
        return false;
    }

    public static bool IsOnline(long lastActivity)
    {
        if (lastActivity == null)
        {
            return false;
        }
        if ((lastActivity + Configure.SESSION_TIMEOUT) > CommonState.EpochTime)
        {
            return true;
        }
        return false;
    }

    public static Person FindPersonByName(string name)
    {
        return FindPersonByACol(COL_NAME, name);
    }

    public static Person FindPersonById(string id)
    {
        return FindPersonByACol(COL_ID, id);
    }

    public static string FindNameById(string id)
    {
        Person p = FindPersonById(id);
        if (p != null)
        {
            return p.Name;
        }
        return String.Empty;
    }

    private static Person FindPersonByACol(string colName, string colVal)
    {
        if (String.IsNullOrEmpty(colName) || String.IsNullOrEmpty(colVal))
        {
            return null;
        }
        string query = String.Format("SELECT * FROM [persons] WHERE {0}=@colVal", colName);
        Person p = null;
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@colVal", colVal);
                cmd.Prepare();
                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = new Person();
                        p.Id = (string)reader[COL_ID];
                        p.Name = (string)reader[COL_NAME];
                        p.LastActivity = (long)reader[COL_LAST];
                    }
                }
            }
        }
        return p;
    }

    private static bool DeletePersonByACol(string colName, string colVal)
    {
        string delete = String.Format("DELETE FROM [persons] WHERE {0}=@colVal", colName);
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = delete;
                cmd.Parameters.Add("@colVal", colVal);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                // TODO: check if success
            }
        }
        return true;
    }

    public static bool DeletePersonById(string id)
    {
        return DeletePersonByACol(COL_ID, id);
    }

    public static bool SignUp(string name, string id)
    {
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            string add = String.Format("INSERT INTO [persons] ({0}, {1}, {2}) VALUES (@name, @id, @last)",
                COL_NAME, COL_ID, COL_LAST);
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = add;
                cmd.Parameters.Add("@id", id);
                cmd.Parameters.Add("@name", name);
                cmd.Parameters.Add("@last", CommonState.EpochTime);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                // TODO: check if success
            }
        }
        return true;
    }

}