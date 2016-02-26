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
    public void Dispose()
    {
      // Clients.DeleteAll();
      Stylists.DeleteAll();
    }
  }
}
