using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _age;
    private string _personal_pronoun;
    private int _stylists_id;

    public Client(string name, int age, string personal_pronoun, int stylists_id, int id = 0)
    {
      _id = id;
      _name = name;
      _age = age;
      _personal_pronoun = personal_pronoun;
      _stylists_id = stylists_id;
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
        bool idEquality = this.GetId() == newClient.GetId();
        bool idStylistEquality = this.GetStylistsId() == newClient.GetStylistsId();
        bool nameEquality = this.GetName() == newClient.GetName();
        bool ageEquality = this.GetAge() == newClient.GetAge();
        bool personalPronounEquality = this.GetPersonalPronoun() == newClient.GetPersonalPronoun();
        return (idEquality && nameEquality && ageEquality && personalPronounEquality);
      }
    }

    // Getters and Setters
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
    public int GetAge()
    {
      return _age;
    }
    public void SetAge(int newAge)
    {
      _age = newAge;
    }
    public string GetPersonalPronoun()
    {
      return _personal_pronoun;
    }
    public void SetPersonalPronounc(string newPersonalPronoun)
    {
      _personal_pronoun = newPersonalPronoun;
    }
    public int GetStylistsId()
    {
      return _stylists_id;
    }
    public void SetStylistId(int newStylistId)
    {
      _stylists_id = newStylistId;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClient = new List<Client>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients ORDER BY name desc", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientName = rdr.GetString(1);
        int ClientAge = rdr.GetInt32(2);
        string ClientPersonalPronoun = rdr.GetString(3);
        int ClientStylistsId= rdr.GetInt32(4);

        Client newClient = new Client(ClientName, ClientAge, ClientPersonalPronoun, ClientStylistsId, ClientId);
        allClient.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allClient;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, age, personal_pronoun, stylist_id) OUTPUT INSERTED.id VALUES (@ClientsName, @ClientsAge, @ClientsPersonalPronoun, @StylistsId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientsName";
      nameParameter.Value = this.GetName();

      SqlParameter ageParameter = new SqlParameter();
      ageParameter.ParameterName = "@ClientsAge";
      ageParameter.Value = this.GetAge();

      SqlParameter personalPronounParameter = new SqlParameter();
      personalPronounParameter.ParameterName = "@ClientsPersonalPronoun";
      personalPronounParameter.Value = this.GetPersonalPronoun();

      SqlParameter stylistsIdParameter = new SqlParameter();
      stylistsIdParameter.ParameterName = "@StylistsId";
      stylistsIdParameter.Value = this.GetStylistsId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(ageParameter);
      cmd.Parameters.Add(personalPronounParameter);
      cmd.Parameters.Add(stylistsIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients where id=@ClientsId;", conn);
      SqlParameter clientsIdParameter = new SqlParameter();
      clientsIdParameter.ParameterName = "@ClientsId";
      clientsIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientsIdParameter);
      rdr = cmd.ExecuteReader();

      int foundClientsId  = 0;
      string foundClientsName = null;
      int foundClientsAge = 0;
      string foundClientsPersonalPronoun = null;
      int foundStylistsId = 0;

      while(rdr.Read())
      {
        foundClientsId = rdr.GetInt32(0);
        foundClientsName = rdr.GetString(1);
        foundClientsAge = rdr.GetInt32(2);
        foundClientsPersonalPronoun = rdr.GetString(3);
        foundStylistsId = rdr.GetInt32(4);
      }
      Client foundClients = new Client(foundClientsName, foundClientsAge, foundClientsPersonalPronoun, foundStylistsId, foundClientsId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundClients;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name=@NewName OUTPUT INSERTED.name WHERE id=@ClientsId", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter ClientsIdParameter = new SqlParameter();
      ClientsIdParameter.ParameterName = "@ClientsId";
      ClientsIdParameter.Value = this.GetId();
      cmd.Parameters.Add(ClientsIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);

      SqlParameter clientsIdParameter = new SqlParameter();
      clientsIdParameter.ParameterName = "@ClientId";
      clientsIdParameter.Value = this.GetId();

      cmd.Parameters.Add(clientsIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
