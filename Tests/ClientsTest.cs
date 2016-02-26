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
    public void Dispose()
    {
      Clients.DeleteAll();
    }
  }
}
