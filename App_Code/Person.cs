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
	public Person()
	{
	}

    public static bool IsExisted(string name)
    {
        if (FindByName(name) == null)
        {
            return false;
        }
        return true;
    }

    public static bool IsOnline(string name)
    {
        Person p = Person.FindByName(name);
        if (p != null &&
            (p.LastActivity + Configure.SESSION_TIMEOUT) > CommonState.EpochTime)
        {
            return true;
        }
        return false;
    }

    public static Person FindByName(string name)
    {
        string query = String.Format("SELECT * FROM [persons] WHERE {0}=@name", COL_NAME);
        Person p = null;
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            //var a = new SqlCeEngine();
            //a.Upgrade(CommonState.ConnectionString);
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = query;
                cmd.Parameters.Add("@name", name);
                cmd.Prepare();
                SqlCeDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    p = new Person();
                    p.Id = (string)reader[COL_ID];
                    p.Name = (string)reader[COL_NAME];
                    p.LastActivity = (long)reader[COL_LAST];
                }
            }
        }
        return p;
    }

    public static bool KillByName(string name)
    {
        string delete = String.Format("DELETE FROM [persons] WHERE {0}=@name", COL_NAME);
        using (SqlCeConnection conn = new SqlCeConnection())
        {
            conn.ConnectionString = CommonState.ConnectionString;
            conn.Open();
            using (SqlCeCommand cmd = new SqlCeCommand(null, conn))
            {
                cmd.CommandText = delete;
                cmd.Parameters.Add("@name", name);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                // TODO: check if success
            }
        }
        return true;
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