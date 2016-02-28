using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HairSalon
{
  public class StylistsTest : IDisposable
  {
    public StylistsTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hairSalon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void test_StylistsEmptyAtFirst()
    {
      int result = Stylists.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void test_StylistsReturnTrueForSame()
    {
      Stylists firstStylist = new Stylists("Cody");
      Stylists secondStylist = new Stylists("Cody");
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void test_Save_SavesStylistsToDatabase()
    {
      Stylists testStylist = new Stylists("Sammy");
      testStylist.Save();

      List<Stylists> result = Stylists.GetAll();
      List<Stylists> testList = new List<Stylists> {testStylist};
      Assert.Equal(testList, result);
    }

    [Fact]
    public void test_Find_FindsStylistsInDatabase()
    {
      Stylists testStylists = new Stylists("Jimmy");
      testStylists.Save();

      Stylists foundStylists = Stylists.Find(testStylists.GetId());
      Assert.Equal(testStylists, foundStylists);
    }

    [Fact]
    public void test_GetStylists_RetrieveAllClientsWithStylists()
    {
      Stylists testStylists = new Stylists("Harold");
      testStylists.Save();

      Clients firstClient = new Clients("Spongebob", 10, "sponge", testStylists.GetId(), 1);
      firstClient.Save();
      Clients secondClient = new Clients("Patrick", 11, "starfish",testStylists.GetId(), 1);
      secondClient.Save();

      List<Clients> testClientsList = new List<Clients> {firstClient, secondClient};
      List<Clients> resultClientsList = testStylists.GetClients();

      Assert.Equal(testClientsList, resultClientsList);
    }

    [Fact]
    public void test_Update_UpdatesStylistsInDatabase()
    {
      string name = "Andrew";
      Stylists testStylists = new Stylists(name);
      testStylists.Save();
      string newName = "Andy";

      testStylists.Update(newName);

      string result = testStylists.GetName();
      Assert.Equal(newName, result);
    }

    [Fact]
    public void test_Delete_DeletesStylistFromDatabase()
    {
      string nameOne = "John Smith";
      Stylists testStylistsOne = new Stylists(nameOne);
      testStylistsOne.Save();

      string nameTwo = "Jill Smith";
      Stylists testStylistsTwo = new Stylists(nameTwo);
      testStylistsTwo.Save();

      Clients testClientOne = new Clients("Donnie", 18, "he", testStylistsOne.GetId(), 1);
      testClientOne.Save();
      Clients testClientTwo = new Clients("Darko", 50, "she", testStylistsTwo.GetId(), 10);
      testClientTwo.Save();

      testStylistsOne.Delete();
      List<Stylists> resultStylists = Stylists.GetAll();
      List<Stylists> testStylistsList = new List<Stylists> {testStylistsTwo};

      List<Clients> resultClients = Clients.GetAll();
      List<Clients> testClientsList = new List<Clients> {testClientTwo};

      Assert.Equal(testStylistsList, resultStylists);
      Assert.Equal(testClientsList, resultClients);
    }

    public void Dispose()
    {
      Clients.DeleteAll();
      Stylists.DeleteAll();
    }
  }
}
