using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Client
  {
    string _name;
    string _notes;
    int _stylist_id;
    int _id;

    public Client(string name, string notes, int stylist_id, int id = 0)
    {
      _name = name;
      _notes = notes;
      _stylist_id = stylist_id;
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

    public int GetStylistId()
    {
      return _stylist_id;
    }

    public int GetId()
    {
      return _id;
    }


    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
      Client newClient = (Client) otherClient;
      bool idEquality = (this.GetStylistId() == newClient.GetStylistId());
      bool nameEquality = (this.GetName() == newClient.GetName());
      bool notesEquality = (this.GetNotes() == newClient.GetNotes());
      return (idEquality && nameEquality && notesEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string notes = rdr.GetString(2);
        int stylist_id = rdr.GetInt32(3);
        Client newClient = new Client(name, notes,stylist_id, id);
        allClients.Add(newClient);
      }
        if(rdr != null)
        {
          rdr.Close();
        }
        if(conn != null)
        {
          conn.Close();
        }
      return allClients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, notes, stylist_id) OUTPUT INSERTED.id VALUES (@name, @notes, @stylist_id);", conn);

      SqlParameter nameParameter = new SqlParameter("@name", this.GetName());
      SqlParameter notesParameter = new SqlParameter("@notes", this.GetNotes());
      SqlParameter stylistIdParameter = new SqlParameter("@stylist_id", this.GetStylistId());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(notesParameter);
      cmd.Parameters.Add(stylistIdParameter);

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

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @client_id;", conn);
      SqlParameter clientIdParameter = new SqlParameter("@client_Id", id.ToString());

      cmd.Parameters.Add(clientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundClientName = null;
      string foundClientNotes = null;
      int foundStylistId = 0;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundClientNotes = rdr.GetString(2);
        foundStylistId = rdr.GetInt32(3);
      }
      Client foundClient = new Client(foundClientName, foundClientNotes, foundStylistId, foundId);

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
    public void EditName(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName OUTPUT INSERTED.name WHERE id = @clientId;", conn);

      SqlParameter nameParameter = new SqlParameter("@NewName", newName);
      SqlParameter clientIdParameter = new SqlParameter("@clientId", this.GetId());
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(clientIdParameter);

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

      SqlCommand cmd = new SqlCommand("UPDATE clients SET notes = @NewNotes OUTPUT INSERTED.notes WHERE id = @clientId;", conn);

      SqlParameter notesParameter = new SqlParameter("@NewNotes", newNotes);
      SqlParameter clientIdParameter = new SqlParameter("@clientId", this.GetId());
      cmd.Parameters.Add(notesParameter);
      cmd.Parameters.Add(clientIdParameter);

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

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id =@clientId;", conn);
      SqlParameter clientIdParameter = new SqlParameter("@clientId", this.GetId());

      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
