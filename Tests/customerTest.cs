using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HairSalon.Objects;

namespace  HairSalon
{
  public class ClientTest : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
    }
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void ClientDBIsEmpty_true()
    {
      //Arrange
      //Act
       int result = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_EqualOverride_True()
    {
      Client firstClient = new Client("Terry", "notes", 0);
      Client secondClient = new Client("Terry", "notes", 0);
      Assert.Equal(firstClient, secondClient);
    }
    [Fact]
    public void ClientSavesTODatabase_true()
    {
      //Arrange
      Client newClient = new Client("Terry", "notes", 0);
      //Act
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      //Assert
      Assert.Equal(allClients[0], newClient);
    }
    // [Fact]
    // public void StylistFindsInDatabase_True()
    // {
    //   //Arrange
    //   Stylist newStylist = new Stylist("Terry", "notes");
    //   newStylist.Save();
    //   //Act
    //   Stylist findStylist = Stylist.Find(newStylist.GetId());
    //   //Assert
    //   Assert.Equal(newStylist, findStylist);
    // }
    // [Fact]
    // public void Edit_ChangesName_true()
    // {
    //   Stylist newStylist = new Stylist("Terry", "notes");
    //   newStylist.Save();
    //   newStylist.EditName("Jerry");
    //
    //   Stylist foundStylist = Stylist.Find(newStylist.GetId());
    //
    //   Assert.Equal("Jerry", foundStylist.GetName());
    // }
    // [Fact]
    // public void Edit_ChangesDetails_true()
    // {
    //   Stylist newStylist = new Stylist("Terry", "notes");
    //   newStylist.Save();
    //   newStylist.EditName("Jerry");
    //
    //   Stylist foundStylist = Stylist.Find(newStylist.GetId());
    //
    //   Assert.Equal("Jerry", foundStylist.GetName());
    // }
    // [Fact]
    // public void Test_Delete_deleteCategoryFromDB()
    // {
    //   //Arrange
    //   Stylist stylist1 = new Stylist("Jerry", "notes");
    //   Stylist stylist2 = new Stylist("Samantha","more notes");
    //   stylist1.Save();
    //   stylist2.Save();
    //   //Act
    //   stylist1.Delete();
    //   List<Stylist> resultStylist = Stylist.GetAll();
    //   List<Stylist> testStylist = new List<Stylist> {stylist2};
    //   //Assert
    //   Assert.Equal(testStylist, resultStylist);
    // }
  }
}
