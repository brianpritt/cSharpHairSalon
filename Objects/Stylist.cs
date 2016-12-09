using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Stylist
  {
    string _name;
    string _notes;
    int _id;

    public Stylist(string name, string notes, int id = 0)
    {
      _name = name;
      _notes = notes;
      _id = id;
    }

    public string GetName()
    {
      return _name;
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
      bool notesEquality = (this.GetNotes() == newStylist.GetNotes());
      return (nameEquality && notesEquality);
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
    public List<Client> FindClients()
    {
      List<Client> foundClient = new List<Client> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter("@StylistId", this.GetId());
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string notes = rdr.GetString(2);
        int stylist_id = rdr.GetInt32(3);

        Client newClient = new Client(name, notes, stylist_id, id);
        foundClient.Add(newClient);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundClient;
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
        string notes = rdr.GetString(2);
        Stylist newStylist = new Stylist(name, notes, id);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylist (name, notes) OUTPUT INSERTED.id VALUES (@name, @notes);", conn);

      SqlParameter nameParameter = new SqlParameter("@name", this.GetName());
      SqlParameter notesParameter = new SqlParameter("@notes", this.GetNotes());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(notesParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
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
      string foundStylistNotes = null;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
        foundStylistNotes = rdr.GetString(2);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistNotes, foundId);
      Console.WriteLine(foundStylistName);
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
    public void EditName(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylist SET name = @NewName OUTPUT INSERTED.name WHERE id = @stylistId;", conn);

      SqlParameter nameParameter = new SqlParameter("@NewName", newName);
      SqlParameter stylistIdParameter = new SqlParameter("@stylistId", this.GetId());
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(stylistIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public void EditDetails(string newNotes)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylist SET notes = @NewNotes OUTPUT INSERTED.notes WHERE id = @stylistId;", conn);

      SqlParameter notesParameter = new SqlParameter("@NewNotes", newNotes);
      SqlParameter stylistIdParameter = new SqlParameter("@stylistId", this.GetId());
      cmd.Parameters.Add(notesParameter);
      cmd.Parameters.Add(stylistIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._notes = rdr.GetString(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylist WHERE id =@stylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter("@stylistId", this.GetId());

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
