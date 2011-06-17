using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Room
/// </summary>
[Serializable]
public class Room
{
    public String Id { get; set; }
    public String Name { get; set; }
    public int Persons { get; set; }
	public Room(String Name)
	{
        Id = Guid.NewGuid().ToString();
        this.Name = Name;
        Persons = 0;
    }
    public Room()
    {
        Id = Guid.NewGuid().ToString();
        Persons = 0;
    }
}