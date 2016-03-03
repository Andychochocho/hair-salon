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
      DBConfiguration.ConnectionString = "Data Source = (localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test; Integrated Security=SSPI;";
    }
    [Fact]
    public void test_DatabaseEmptyInitially()
    {
      int result = Client.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void test_ClientsReturnTrueForSameName()
    {
      Client firstClient = new Client("Andrew", 20, "he", 1);
      Client secondClient = new Client("Andrew", 20, "he", 1);
      Assert.Equal(firstClient, secondClient);
    }
    [Fact]
    public void test_Save_SavesObjectsToDatabase()
    {
      Client testClients = new Client ("Big-Foot", 100, "they", 1);
      testClients.Save();

      List<Client> test = new List<Client> {testClients};
      List<Client> results = Client.GetAll();
      Assert.Equal(results, test);
    }
    [Fact]
    public void test_SaveAssignsIdToObject()
    {
      Client testClients = new Client("Nissa", 300, "planeswalker", 1);
      testClients.Save();

      Client savedClients = Client.GetAll()[0];
      int result = savedClients.GetId();
      int testId = testClients.GetId();
      Assert.Equal(testId, result);
    }
    [Fact]
    public void test_FindClientsInDatabase()
    {
      Client testClients = new Client("Drana", 4000, "vampire", 1);
      testClients.Save();

      Client foundClients = Client.Find(testClients.GetId());
      Assert.Equal(testClients, foundClients);
    }

    [Fact]
    public void test_Update_UpdatesClientsInDatabase()
    {
      string name = "Carl";
      int age = 55;
      string personalPronoun = "he";
      int stylistsId = 1;

      Client testClients = new Client(name, age, personalPronoun, stylistsId);
      testClients.Save();

      string newName = "Caroline";

      testClients.Update(newName);

      Client result = new Client(testClients.GetName(), age, personalPronoun, stylistsId);
      Client newClient = new Client (newName, age, personalPronoun, stylistsId);
      Assert.Equal(newClient, result);
    }
    [Fact]
    public void test_Delete_DeletesClientFromDatabase()
    {
      string name = "Hair Dresser";

      Stylist testStylists1 = new Stylist(name);
      testStylists1.Save();

      Client testClients1 = new Client("Bruce Wayne", 40, "bat", testStylists1.GetId(), 4);
      testClients1.Save();
      Client testClients2 = new Client("Clark Kent", 45, "alien", testStylists1.GetId(), 18);
      testClients2.Save();

      testClients1.Delete();
      List<Client> resultClients = testStylists1.GetClients();
      List<Client> testClientsList = new List<Client> {testClients2};

      Assert.Equal(testClientsList, resultClients);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
