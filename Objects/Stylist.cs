using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylists
  {
    private string _name;
    private int _id;


    public Stylists(string name, id=0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(System.Objects otherStylists)
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

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand(UPDATE stylists SET name="@NewName" OUTPUT INSERTED.name WHERE id="@StylistsId", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter StylistsIdParameter = new SqlParameter();
      StylistsIdParameter.ParameterName = "@StylistsId";
      StylistsIdParameter.Value = this.GetId();
      cmd.Parameters.Add();

      while(rdr.Read())
      {
        this._name = rdr.GetString();
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn.Read())
      {
        conn.Close();
      }
    }
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
      _name = NewName();
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
        Sylists newStylists = new Stylists(stylistsName, stylistsId);
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
  }
}