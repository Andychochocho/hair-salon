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
    public void Dispose()
    {
      Clients.DeleteAll();
    }
  }
}
