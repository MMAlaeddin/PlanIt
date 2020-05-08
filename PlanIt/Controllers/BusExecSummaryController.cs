using Microsoft.AspNetCore.Mvc;
using PlanIt.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace PlanIt.Controllers
{
  public class BusExecSummaryController : Controller
  {
    private readonly PlanItContext _db; 

    public BusExecSummaryController(PlanItContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      // List<Summary> model = _db.BusExecSummary.ToList();
      return View();
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(BusExecSummary summary)
    {
      _db.Summary.Add(summary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}