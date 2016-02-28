using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Xunit;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
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

      List<Clients> test = new List<Clients> {testClients};
      List<Clients> results = Clients.GetAll();
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
      Assert.Equal(testClients, foundClients);
    }

    [Fact]
    public void test_Update_UpdatesClientsInDatabase()
    {
      string name = "Carl";
      int age = 55;
      string personalPronoun = "he";
      int stylistsId = 1;

      Clients testClients = new Clients(name, age, personalPronoun, stylistsId);
      testClients.Save();

      string newName = "Caroline";

      testClients.Update(newName);

      Clients result = new Clients(testClients.GetName(), age, personalPronoun, stylistsId);
      Clients newClient = new Clients (newName, age, personalPronoun, stylistsId);
      Assert.Equal(newClient, result);
    }
    [Fact]
    public void test_Delete_DeletesClientFromDatabase()
    {
      string name = "Hair Dresser";

      Stylists testStylists1 = new Stylists(name);
      testStylists1.Save();

      Clients testClients1 = new Clients("Bruce Wayne", 40, "bat", testStylists1.GetId(), 4);
      testClients1.Save();
      Clients testClients2 = new Clients("Clark Kent", 45, "alien", testStylists1.GetId(), 18);
      testClients2.Save();

      testClients1.Delete();
      List<Clients> resultClients = testStylists1.GetClients();
      List<Clients> testClientsList = new List<Clients> {testClients2};

      Assert.Equal(testClientsList, resultClients);
    }

    public void Dispose()
    {
      Clients.DeleteAll();
      Stylists.DeleteAll();
    }
  }
}
