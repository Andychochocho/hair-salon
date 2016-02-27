using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get ["/"] = _ => {
        List<Stylists> allStylists = Stylists.GetAll();
        return View ["index.cshtml", allStylists];
      };
      Get ["/stylists/new"] = _ =>{
        return View ["stylist_info.cshtml"];
      };
      Post["/"] = _ => {
        Stylists newStylists = new Stylists(Request.Form["stylist-name"]);
        newStylists.Save();
        List<Stylists> allStylists = Stylists.GetAll();
        return View["index.cshtml", allStylists];
      };
      Get["/stylist/ClearAll"] = _ => {
        Stylists.DeleteAll();
        return View ["index.cshtml"];
      };
      Get["/stylists/{id}"] =Parameters=> {
        Dictionary <string, object> model = new Dictionary <string, object>();
        var selectedStylist = Stylists.Find(Parameters.id);
        var StylistClient = selectedStylist.GetClients();
        model.Add ("stylist", selectedStylist);
        model.Add("client", StylistClient);
        return View["stylist.cshtml", model];
      };
      Get["/stylists/clientAdd"] = _ => {
        List<Stylists> AllStylists = Stylists.GetAll();
        return View["addClient.cshtml", AllStylists];
      };
      Post["/stylists/viewClients"] = Parameters => {
        Clients newClients = new Clients(Request.Form["name"], Request.Form["age"], Request.Form["personalPronoun"]);
        newClients.Save();
        return View["success.cshtml", newClients];
      };
    }
  }
}
