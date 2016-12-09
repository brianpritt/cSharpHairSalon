using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Stylist
  {
    string _name;
    string _phone;
    string _notes;
    int _id;

    public Stylist(string name, string phone, string notes, int id = 0)
    {
      _name = name;
      _phone = phone;
      _notes = notes;
      _id = id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetPhone()
    {
      return _phone;
    }
    public string GetNotes()
    {
      return _notes;
    }
    public int GetId()
    {
      return _id;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylist;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylist;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string phone = rdr.GetString(2);
        string notes = rdr.GetString(3);
        Stylist newStylist = new Stylist(name, phone, notes, id);
        allStylists.Add(newStylist);
      }
        if(rdr != null)
        {
          rdr.Close();
        }
        if(conn != null)
        {
          conn.Close();
        }
      return allStylists;
    }

  }
}
