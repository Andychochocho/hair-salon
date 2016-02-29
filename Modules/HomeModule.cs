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
        return View ["index.cshtml"];
      };
      Get["/stylists"] = _ => {
        List<Stylists> allStylists = Stylists.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Post["/stylists/new"] = _ => {
        Stylists newStylist = new Stylists(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylists> allStylists = Stylists.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/stylists/deleteAll"] = _ => {
        Stylists.DeleteAll();
        List<Stylists> allStylists = Stylists.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/stylists/{id}"] = Parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylists selectStylist = Stylists.Find(Parameters.id);
        List<Clients> stylistClient = selectStylist.GetClients();
        List<Stylists> allStylists = Stylists.GetAll();
        model.Add("stylist", selectStylist);
        model.Add("clients", stylistClient);
        model.Add("stylists", allStylists);
        return View["clients.cshtml", model];
      };
      Post["/stylists/{id}/new"] = Parameters => {
        Clients newClient = new Clients(Request.Form["name"],Request.Form["age"], Request.Form["personalPronoun"], Request.Form["stylist-id"]);
        newClient.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylists selectStylist = Stylists.Find(Parameters.id);
        List<Clients> stylistClient = selectStylist.GetClients();
        List<Stylists> allStylists = Stylists.GetAll();
        model.Add("stylist", selectStylist);
        model.Add("clients", stylistClient);
        model.Add("stylists", allStylists);
        return View["clients.cshtml", model];
      };
      Get["/stylist/edit/{id}"] = Parameters => {
        Stylists selectStylist = Stylists.Find(Parameters.id);
        return View["stylistEdit.cshtml", selectStylist];
      };
      Patch["/stylist/edit/{id}"] = Parameters => {
        Stylists selectStylist = Stylists.Find(Parameters.id);
        selectStylist.Update(Request.Form["stylist-name"]);

        List<Stylists> allStylists = Stylists.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Delete["/stylist/delete/{id}"] = Parameters => {
        Stylists selectStylist = Stylists.Find(Parameters.id);
        selectStylist.Delete();

        List<Stylists> allStylists = Stylists.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/client/edit/{id}"] = Parameters => {
        Clients selectClient = Clients.Find(Parameters.id);
        return View["clientEdit.cshtml", selectClient];
      };

      Patch["/client/edit/{id}"] = Parameters => {
        Clients selectClient = Clients.Find(Parameters.id);
        selectClient.Update(Request.Form["client-name"]);
        // selectClient.Update(Request.Form["client-age"]);
        // selectClient.Update(Request.Form["client-personal-pronoun"]);

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylists selectStylist = Stylists.Find(selectClient.GetStylistsId());
        List<Clients> stylistClient = selectStylist.GetClients();
        List<Stylists> allStylists = Stylists.GetAll();
        model.Add("stylist", selectStylist);
        model.Add("clients", stylistClient);
        model.Add("stylists", allStylists);
        return View["clients.cshtml", model];
      };
    }
  }
}
