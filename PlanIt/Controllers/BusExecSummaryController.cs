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
      List<BusExecSummary> model = _db.Summary.Include(summary => summary.SummaryId).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      // ViewBag.CategoryId = new SelectList(_db.BusExecSummary, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(BusExecSummary summary)
    {
      _db.Summary.Add(summary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      BusExecSummary thisSummary = _db.Summary.FirstOrDefault(summary => summary.SummaryId == id);
      return View(thisSummary);
    }

    public ActionResult Edit(int id)
    {
      var thisSummary = _db.Summary.FirstOrDefault(summary => summary.SummaryId == id);
      // ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisSummary);
    }

    [HttpPost]
    public ActionResult Edit(BusExecSummary summary)
    {
      _db.Entry(summary).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisSummary = _db.Summary.FirstOrDefault(summary => summary.SummaryId == id);
      return View(thisSummary);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisSummary = _db.Summary.FirstOrDefault(summary => summary.SummaryId == id);
      _db.Summary.Remove(thisSummary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}