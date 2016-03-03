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
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/stylists/deleteAll"] = _ => {
        Stylist.DeleteAll();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/stylists/{id}"] = Parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectStylist = Stylist.Find(Parameters.id);
        List<Client> stylistClient = selectStylist.GetClients();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("stylist", selectStylist);
        model.Add("clients", stylistClient);
        model.Add("stylists", allStylists);
        return View["clients.cshtml", model];
      };
      Post["/stylists/{id}/new"] = Parameters => {
        Client newClient = new Client(Request.Form["name"],Request.Form["age"], Request.Form["personalPronoun"], Request.Form["stylist-id"]);
        newClient.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectStylist = Stylist.Find(Parameters.id);
        List<Client> stylistClient = selectStylist.GetClients();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("stylist", selectStylist);
        model.Add("clients", stylistClient);
        model.Add("stylists", allStylists);
        return View["clients.cshtml", model];
      };
      Get["/stylist/edit/{id}"] = Parameters => {
        Stylist selectStylist = Stylist.Find(Parameters.id);
        return View["stylistEdit.cshtml", selectStylist];
      };
      Patch["/stylist/edit/{id}"] = Parameters => {
        Stylist selectStylist = Stylist.Find(Parameters.id);
        selectStylist.Update(Request.Form["stylist-name"]);

        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Delete["/stylist/delete/{id}"] = Parameters => {
        Stylist selectStylist = Stylist.Find(Parameters.id);
        selectStylist.Delete();

        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/client/edit/{id}"] = Parameters => {
        Client selectClient = Client.Find(Parameters.id);
        return View["clientEdit.cshtml", selectClient];
      };

      Patch["/client/edit/{id}"] = Parameters => {
        Client selectClient = Client.Find(Parameters.id);
        selectClient.Update(Request.Form["client-name"]);
        // selectClient.Update(Request.Form["client-age"]);
        // selectClient.Update(Request.Form["client-personal-pronoun"]);

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectStylist = Stylist.Find(selectClient.GetStylistsId());
        List<Client> stylistClient = selectStylist.GetClients();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("stylist", selectStylist);
        model.Add("clients", stylistClient);
        model.Add("stylists", allStylists);
        return View["clients.cshtml", model];
      };
      Delete["/client/delete/{id}"] = Parameters => {
        Client selectClient = Client.Find(Parameters.id);
        selectClient.Delete();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectStylist = Stylist.Find(selectClient.GetStylistsId());
        List<Client> stylistClient = selectStylist.GetClients();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("stylist", selectStylist);
        model.Add("clients", stylistClient);
        model.Add("stylists", allStylists);
        return View["clients.cshtml", model];
      };
    }
  }
}
