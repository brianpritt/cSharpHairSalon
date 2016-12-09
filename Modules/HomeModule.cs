using Nancy;
using System.Collections.Generic;
using System;
using HairSalon.Objects;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
       List<Stylist> allStylists = Stylist.GetAll();
       return View["index.cshtml", allStylists];
      };
      Get["/add/stylist"] = _ => {
        return View["stylist_form.cshtml"];
      };
      Get["/add/client"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["client_form.cshtml", allStylists];
      };
      Post["/add/client"] = _ => {
        Client newClient = new Client(Request.Form["client_name"], Request.Form["client_notes"], Request.Form["stylist_id"]);
        newClient.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["/index.cshtml", allStylists];
      };
      Post["/add/stylist"] = _ =>{
        Stylist newStylist = new Stylist(Request.Form["stylist_name"], Request.Form["notes"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Get["/stylist/{id}"] = parameters =>{
        var currentStylist = Stylist.Find(parameters.id);
        return View["stylist.cshtml", currentStylist];
      };
      Get["/clients/{id}"] = parameters =>{
        var currentClient = Client.Find(parameters.id);
        return View["client.cshtml", currentClient];
      };
      Get["/modify/stylist/{id}"] = parameters =>{
        var currentStylist = Stylist.Find(parameters.id);
        return View["modify_stylist.cshtml", currentStylist];
      };
      Get["/modify/client/{id}"] = parameters =>{
        var currentClient = Client.Find(parameters.id);
        return View["modify_client.cshtml", currentClient];
      };
      Patch["/modify/stylist/{id}"] = parameters => {
        var currentStylist = Stylist.Find(parameters.id);
        currentStylist.EditName(Request.Form["stylist_name"]);
        currentStylist.EditDetails(Request.Form["stylist_notes"]);
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Patch["/modify/client/{id}"] = parameters => {
        var currentClient = Client.Find(parameters.id);
        currentClient.EditName(Request.Form["client_name"]);
        currentClient.EditDetails(Request.Form["client_notes"]);
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Delete["/delete-stylist/{id}"] = parameters => {
        Stylist currentStylist = Stylist.Find(parameters.id);
        List<Client> currentClients = currentStylist.FindClients();
        foreach (Client client in currentClients)
        {
          client.Delete();
        }
        currentStylist.Delete();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Delete["/delete-client/{id}"] = parameters =>{
        Client currentClient = Client.Find(parameters.id);
        currentClient.Delete();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Delete["/delete-all"] = _ =>{
        Stylist.DeleteAll();
        Client.DeleteAll();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };

    }
  }
}
