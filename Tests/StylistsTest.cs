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

    // THIS WILL BE WHERE THE RETRIEVE TEST GOES AFTER CLIENTS CS IS FINISHED

    public void Dispose()
    {
      // Clients.DeleteAll();
      Stylists.DeleteAll();
    }
  }
}
