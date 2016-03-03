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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void test_StylistsEmptyAtFirst()
    {
      int result = Stylist.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void test_StylistsReturnTrueForSame()
    {
      Stylist firstStylist = new Stylist("Cody");
      Stylist secondStylist = new Stylist("Cody");
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void test_Save_SavesStylistsToDatabase()
    {
      Stylist testStylist = new Stylist("Sammy");
      testStylist.Save();

      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist> {testStylist};
      Assert.Equal(testList, result);
    }

    [Fact]
    public void test_Find_FindsStylistsInDatabase()
    {
      Stylist testStylists = new Stylist("Jimmy");
      testStylists.Save();

      Stylist foundStylists = Stylist.Find(testStylists.GetId());
      Assert.Equal(testStylists, foundStylists);
    }

    [Fact]
    public void test_GetStylists_RetrieveAllClientsWithStylists()
    {
      Stylist testStylists = new Stylist("Harold");
      testStylists.Save();

      Client firstClient = new Client("Spongebob", 10, "sponge", testStylists.GetId(), 1);
      firstClient.Save();
      Client secondClient = new Client("Patrick", 11, "starfish",testStylists.GetId(), 1);
      secondClient.Save();

      List<Client> testClientsList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientsList = testStylists.GetClients();

      Assert.Equal(testClientsList, resultClientsList);
    }

    [Fact]
    public void test_Update_UpdatesStylistsInDatabase()
    {
      string name = "Andrew";
      Stylist testStylists = new Stylist(name);
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
      Stylist testStylistsOne = new Stylist(nameOne);
      testStylistsOne.Save();

      string nameTwo = "Jill Smith";
      Stylist testStylistsTwo = new Stylist(nameTwo);
      testStylistsTwo.Save();

      Client testClientOne = new Client("Donnie", 18, "he", testStylistsOne.GetId(), 1);
      testClientOne.Save();
      Client testClientTwo = new Client("Darko", 50, "she", testStylistsTwo.GetId(), 10);
      testClientTwo.Save();

      testStylistsOne.Delete();
      List<Stylist> resultStylists = Stylist.GetAll();
      List<Stylist> testStylistsList = new List<Stylist> {testStylistsTwo};

      List<Client> resultClients = Client.GetAll();
      List<Client> testClientsList = new List<Client> {testClientTwo};

      Assert.Equal(testStylistsList, resultStylists);
      Assert.Equal(testClientsList, resultClients);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
