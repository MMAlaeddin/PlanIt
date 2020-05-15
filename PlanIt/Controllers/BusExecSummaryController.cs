using Microsoft.AspNetCore.Mvc;
using PlanIt.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
      // List<BusExecSummary> model = _db.BusExecSummaries.Include(busExecSummaries => busExecSummaries.Category).ToList();
      return View();
    }

    public ActionResult Create()
    {
      // ViewBag.SummaryId = new SelectList(_db.Categories, "SummaryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(BusExecSummary busExecSummary)
    {
      _db.BusExecSummaries.Add(busExecSummary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      BusExecSummary thisBusExecSummary = _db.BusExecSummaries.FirstOrDefault(busExecSummaries => busExecSummaries.SummaryId == id);
      return View(thisBusExecSummary);
    }

    public ActionResult Edit(int id)
    {
      var thisBusExecSummary = _db.BusExecSummaries.FirstOrDefault(busExecSummaries => busExecSummaries.SummaryId == id);
      // ViewBag.SummaryId = new SelectList(_db.Categories, "SummaryId", "Name");
      return View(thisBusExecSummary);
    }

    [HttpPost]
    public ActionResult Edit(BusExecSummary busExecSummary)
    {
      _db.Entry(busExecSummary).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBusExecSummary = _db.BusExecSummaries.FirstOrDefault(busExecSummaries => busExecSummaries.SummaryId == id);
      return View(thisBusExecSummary);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBusExecSummary = _db.BusExecSummaries.FirstOrDefault(items => items.SummaryId == id);
      _db.BusExecSummaries.Remove(thisBusExecSummary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}