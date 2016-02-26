using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Xunit;

namespace HairSalon
{
  public class HairSalonTest : IDisposable
  {
    public HairSalonTest()
    {
      DBConfiguration.ConnectionString = "Data Source = (localdb)\\mssqllocaldb;Initial Catalog=hairSalon_test; Integrated Security=SSPI;";
    }
    [Fact]
    public void test_DatabaseEmptyInitially()
    {
      int result = Clients.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void test_ClientsReturnTrueForSameName()
    {
      Clients firstClient = new Clients("Andrew", 20, "he", 1);
      Clients secondClient = new Clients("Andrew", 20, "he", 1);
      Assert.Equal(firstClient, secondClient);
    }
    [Fact]
    public void test_Save_SavesObjectsToDatabase()
    {
      Clients testClients = new Clients ("Big-Foot", 100, "they", 1);
      testClients.Save();

      List<Clients> results = Clients.GetAll();
      List<Clients> test = new List<Clients> {testClients};
      Assert.Equal(results, test);
    }
    [Fact]
    public void test_SaveAssignsIdToObject()
    {
      Clients testClients = new Clients("Nissa", 300, "planeswalker", 1);
      testClients.Save();

      Clients savedClients = Clients.GetAll()[0];
      int result = savedClients.GetId();
      int testId = testClients.GetId();
      Assert.Equal(testId, result);
    }
    [Fact]
    public void test_FindClientsInDatabase()
    {
      Clients testClients = new Clients("Drana", 4000, "vampire", 1);
      testClients.Save();

      Clients foundClients = Clients.Find(testClients.GetId());
      // foundClients.Save();
      Console.WriteLine("TEST CLIENT: " + testClients.GetId());
      Console.WriteLine("FOUND CLIENT: " + foundClients.GetId());
      Assert.Equal(testClients, foundClients);
    }
    public void Dispose()
    {
      Clients.DeleteAll();
    }
  }
}
