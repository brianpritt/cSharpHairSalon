using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using HairSalon.Objects;

namespace  HairSalon
{
  public class StylistTest : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void StylistDBIsEmpty_true()
    {
      //Arrange
      //Act
       int result = Stylist.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_EqualOverride_True()
    {
      Stylist firstStylist = new Stylist("Terry", "notes");
      Stylist secondStylist = new Stylist("Terry", "notes");
      Assert.Equal(firstStylist, secondStylist);
    }
    [Fact]
    public void StylistSavesTODatabase_true()
    {
      //Arrange
      Stylist newStylist = new Stylist("Terry", "notes");
      //Act
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      //Assert
      Assert.Equal(allStylists[0], newStylist);
    }
    [Fact]
    public void StylistFindsInDatabase_True()
    {
      //Arrange
      Stylist newStylist = new Stylist("Terry", "notes");
      newStylist.Save();
      //Act
      Stylist findStylist = Stylist.Find(newStylist.GetId());
      //Assert
      Assert.Equal(newStylist, findStylist);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {
      Stylist newStylist = new Stylist("Terry", "notes");
      newStylist.Save();
      newStylist.EditName("Jerry");

      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      Assert.Equal("Jerry", foundStylist.GetName());
    }
    [Fact]
    public void Edit_ChangesDetails_true()
    {
      Stylist newStylist = new Stylist("Terry", "notes");
      newStylist.Save();
      newStylist.EditName("Jerry");

      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      Assert.Equal("Jerry", foundStylist.GetName());
    }
    [Fact]
    public void Test_Delete_deleteCategoryFromDB()
    {

    }
  }
}
