using Microsoft.AspNetCore.Mvc;
using PlanIt.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PlanIt.Controllers
{
  public class ExecSummaryController : Controller
  {
    private readonly PlanItContext _db;

    public ExecSummaryController(PlanItContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<ExecSummary> model = _db.ExecSummaries.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(ExecSummary execSummary)
    {
      _db.ExecSummaries.Add(execSummary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisExecSummary = _db.ExecSummaries
          .Include(execSummary => execSummary.BusProposals)
          .ThenInclude(join => join.Busproposal)
          .FirstOrDefault(execSummary => execSummary.ExecSummaryId == id);
      return View(thisExecSummary);
    }

    public ActionResult Edit(int id)
    {
      var thisExecSummary = _db.ExecSummaries.FirstOrDefault(execSummary => execSummary.ExecSummaryId == id);
      return View(thisExecSummary);
    }

    [HttpPost]
    public ActionResult Edit(ExecSummary execSummary)
    {
      _db.Entry(execSummary).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisExecSummary = _db.ExecSummaries.FirstOrDefault(execSummary => execSummary.ExecSummaryId == id);
      return View(thisExecSummary);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisExecSummary = _db.ExecSummaries.FirstOrDefault(execSummary => execSummary.ExecSummaryId == id);
      _db.ExecSummaries.Remove(thisExecSummary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}