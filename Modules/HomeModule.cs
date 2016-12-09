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
      Get["/modify/stylist/{id}"] = parameters =>{
        var currentStylist = Stylist.Find(parameters.id);
        return View["modify_stylist.cshtml", currentStylist];
      };
      Patch["/modify/stylist/{id}"] = parameters => {
        var currentStylist = Stylist.Find(parameters.id);
        currentStylist.EditName(Request.Form["stylist_name"]);
        currentStylist.EditDetails(Request.Form["stylist_notes"]);
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Delete["/delete-stylist/{id}"] = parameters => {
        Stylist currentStylist = Stylist.Find(parameters.id);

        currentStylist.Delete();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Delete["/delete-all"] = _ =>{
        Stylist.DeleteAll();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };

    }
  }
}
