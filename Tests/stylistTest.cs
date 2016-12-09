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
      Stylist firstStylist = new Stylist("Terry", "3309627946", "notes");
      Stylist secondStylist = new Stylist("Terry", "3309627946", "notes");
      Assert.Equal(firstStylist, secondStylist);
    }
    [Fact]
    public void StylistSavesTODatabase_true()
    {
      //Arrange
      Stylist newStylist = new Stylist("Terry", "3309627946", "notes");
      //Act
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      //Assert
      // Console.WriteLine(allStylists[0].GetId() + " " + allStylists[0].GetName()+ " " + allStylists[0].GetPhone() + " " +allStylists[0].GetNotes());
      Assert.Equal(allStylists[0], newStylist);
    }
    [Fact]
    public void StylistFindsInDatabase_True()
    {
      //Arrange
      Stylist newStylist = new Stylist("Terry", "3309627946", "notes");
      newStylist.Save();
      //Act
      Stylist findStylist = Stylist.Find(newStylist.GetId());
      //Assert
      Assert.Equal(newStylist, findStylist);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {

    }
    [Fact]
    public void Test_Delete_deleteCategoryFromDB()
    {

    }
  }
}
