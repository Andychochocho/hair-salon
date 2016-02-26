using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Clients
  {
    private int _id;
    private string _name;
    private int _age;
    private string _personal_pronoun;
    private int _stylists_id;


    public Clients(string name, int age, string personal_pronoun, int id = 0, int stylists_id =0)
    {
      _id = id;
      _name = name;
      _age = age;
      _personal_pronoun = personal_pronoun;
      _stylists_id = stylists_id;
    }

    // public override bool Equals(System.Object otherClients)
    // {
    //   if (!(otherClients is Clients))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Clients newClients = (Clients) otherClients;
    //     bool idEquality = this.GetId() == newClients.GetId();
    //   }
    // }

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

    public static List<Clients> GetAll()
    {
      List<Clients> allClients = new List<Clients>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients ORDER BY name desc", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int ClientsId = rdr.GetInt32(0);
        string ClientsName = rdr.GetString(1);
        int ClientsAge = rdr.GetInt32(2);
        string ClientsPersonalPronoun = rdr.GetString(3);
        int ClientsStylistsId= rdr.GetInt32(4);

        Clients newClients = new Clients(ClientsName, ClientsAge, ClientsPersonalPronoun, ClientsId,  ClientsStylistsId);
        allClients.Add(newClients);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allClients;
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
