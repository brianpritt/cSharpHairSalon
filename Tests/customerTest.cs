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
    [Fact]
    public void ClientFindsInDatabase_True()
    {
      //Arrange
      Client newClient = new Client("Terry", "notes", 0);
      newClient.Save();
      //Act
      Client findClient = Client.Find(newClient.GetId());
      //Assert
      Assert.Equal(newClient, findClient);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {
      Client newClient = new Client("Terry", "notes", 0);
      newClient.Save();
      newClient.EditName("Jerry");

      Client foundClient = Client.Find(newClient.GetId());

      Assert.Equal("Jerry", foundClient.GetName());
    }
    [Fact]
    public void Edit_ChangesDetails_true()
    {
      Client newClient = new Client("Terry", "notes", 0);
      newClient.Save();
      newClient.EditName("Jerry");

      Client foundClient = Client.Find(newClient.GetId());

      Assert.Equal("Jerry", foundClient.GetName());
    }
    [Fact]
    public void Test_Delete_deleteClientFromDB()
    {
      //Arrange
      Client client1 = new Client("Jerry", "notes",0);
      Client client2 = new Client("Samantha","more notes", 0);
      client1.Save();
      client2.Save();
      //Act
      client1.Delete();
      List<Client> resultClient = Client.GetAll();
      List<Client> testClient = new List<Client> {client2};
      //Assert
      Assert.Equal(testClient, resultClient);
    }
    [Fact]
    public void Test_returnCustomerDataUnderStylist()
    {
      Stylist newStylist = new Stylist("Terry", "notes");
      newStylist.Save();
      int newStylistId = newStylist.GetId();
      Client newClient = new Client("Jerry", "notes", newStylistId);
      newClient.Save();

      Stylist findStylist = Stylist.Find(newStylist.GetId());
      Client findClient = Client.Find(newClient.GetId());
      int stylistId = findStylist.GetId();
      int clientStylistId = findClient.GetStylistId();

      Assert.Equal(stylistId, clientStylistId);

    }
  }
}
