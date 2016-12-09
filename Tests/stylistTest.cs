using Xunit;
using System;
using System.Collections.Generic;
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
    public void StylistSavesTODatabase_true()
    {
      //Arrange
      Stylist newStylist = new Stylist("Terry", "3309627946", "notes");
      //Act
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      //Assert
      Assert.Equal(allStylists[0], newStylist);
    }
  }
}
