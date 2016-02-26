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
        return View ["stylistClearAll.cshtml"];
      };
    }
  }
}
