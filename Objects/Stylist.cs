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


    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
      Stylist newStylist = (Stylist) otherStylist;
      // bool idEquality = (this.GetId() == newStylist.GetId());
      bool nameEquality = (this.GetName() == newStylist.GetName());
      bool phoneEqulity = (this.GetPhone() == newStylist.GetPhone());
      bool notesEquality = (this.GetNotes() == newStylist.GetNotes());
      Console.WriteLine(nameEquality);
      return (nameEquality && phoneEqulity && notesEquality);
      }
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
      List<Stylist> allStylists = new List<Stylist> {};
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
        Console.WriteLine(id + " " + name + " " + phone + " " + notes);
        Console.WriteLine(allStylists[0 ].GetId());
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylist (name, phone, notes) OUTPUT INSERTED.id VALUES (@name, @phone, @notes);", conn);

      SqlParameter nameParameter = new SqlParameter("@name", this.GetName());
      SqlParameter phoneParameter = new SqlParameter ("@phone", this.GetPhone());
      SqlParameter notesParameter = new SqlParameter("@notes", this.GetNotes());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(phoneParameter);
      cmd.Parameters.Add(notesParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylist WHERE id = @stylist_id;", conn);
      SqlParameter stylistIdParameter = new SqlParameter("@stylist_Id", id.ToString());

      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundStylistName = null;
      string foundStylistPhone = null;
      string foundStylistNotes = null;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
        foundStylistPhone = rdr.GetString(2);
        foundStylistNotes = rdr.GetString(3);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistPhone, foundStylistNotes, foundId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }
  }
}
