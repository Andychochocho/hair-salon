using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Stylists
  {
    private string _name;
    private int _id;

    public Stylists(string name, int id=0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(System.Object otherStylists)
    {
      if(!(otherStylists is Stylists))
      {
        return false;
      }
      else
      {
        Stylists newStylists = (Stylists) otherStylists;
        bool idEquality = this.GetId() == newStylists.GetId();
        bool nameEquality = this.GetName() == newStylists.GetName();
        return(idEquality & nameEquality);
      }
    }

    // public void Update(string newName)
    // {
    //   SqlConnection conn = DB.Connection();
    //   SqlDataReader rdr;
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("UPDATE stylists SET name=@NewName OUTPUT INSERTED.name WHERE id=@StylistsId", conn);
    //
    //   SqlParameter newNameParameter = new SqlParameter();
    //   newNameParameter.ParameterName = "@NewName";
    //   newNameParameter.Value = newName;
    //   cmd.Parameters.Add(newNameParameter);
    //
    //   SqlParameter StylistsIdParameter = new SqlParameter();
    //   StylistsIdParameter.ParameterName = "@StylistsId";
    //   StylistsIdParameter.Value = this.GetId();
    //   cmd.Parameters.Add(StylistsIdParameter);
    //   rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     this._name = rdr.GetString(0);
    //   }
    //   if(rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    //Getters and Setters
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public static List<Stylists> GetAll()
    {
      List<Stylists> allStylists = new List<Stylists> {};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int stylistsId = rdr.GetInt32(0);
        string stylistsName = rdr.GetString(1);
        Stylists newStylists = new Stylists(stylistsName, stylistsId);
        allStylists.Add(newStylists);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStylists;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@stylistsnames);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@stylistsnames";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Stylists Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId", conn);
      SqlParameter StylistsIdParameter = new SqlParameter();
      StylistsIdParameter.ParameterName = "@StylistId";
      StylistsIdParameter.Value = id.ToString();
      cmd.Parameters.Add(StylistsIdParameter);
      rdr = cmd.ExecuteReader();

      int foundStylistsId = 0;
      string foundStylistsName = null;

      while(rdr.Read())
      {
        foundStylistsId = rdr.GetInt32(0);
        foundStylistsName = rdr.GetString(1);
      }
      Stylists foundStylists = new Stylists(foundStylistsName, foundStylistsId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStylists;
    }

    public List<Clients> GetClients()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylists_id=@StylistsId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter ();
      stylistIdParameter.ParameterName = "@StylistsId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      rdr = cmd.ExecuteReader();

      List<Clients> clients = new List<Clients> {};
      while (rdr.Read())
      {
        int ClientsId = rdr.GetInt32(0);
        string ClientsName = rdr.GetString(1);
        int ClientsAge = rdr.GetInt32(2);
        string ClientsPersonalPronoun = rdr.GetString(3);
        int ClientsStylistsId = rdr.GetInt32(4);
        Clients newClients = new Clients(ClientsName, ClientsAge, ClientsPersonalPronoun, ClientsId, ClientsStylistsId);
        clients.Add(newClients);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return clients;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
