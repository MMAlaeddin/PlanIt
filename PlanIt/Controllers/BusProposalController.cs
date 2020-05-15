using Microsoft.AspNetCore.Mvc;
using PlanIt.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace PlanIt.Controllers
{
  public class BusProposalController : Controller
  {
    private readonly PlanItContext _db; 

    public BusProposalController(PlanItContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<BusProposal> model = _db.BusProposals.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(BusProposal busProposal)
    {
      _db.BusProposals.Add(busProposal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisBusProposal = _db.BusProposals
        .Include(BusProposal => BusProposal.ExecSummaries)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(BusProposal => BusProposal.BusProposalId == id);
      return View(thisBusProposal);
    }
    // [Authorize]
    public ActionResult Edit(int id)
    {
      BusProposal thisBusProposal = _db.BusProposals.FirstOrDefault(busProposal => busProposal.BusProposalId == id);
      return View(thisBusProposal);
    }
    [HttpPost]
    public ActionResult Edit(BusProposal busProposal)
    {
      _db.Entry(busProposal).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // [Authorize]
    public ActionResult Delete(int id)
    {
      var thisBusProposal = _db.BusProposals.FirstOrDefault(busProposal => busProposal.BusProposalId == id);
      return View(thisBusProposal);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBusProposal = _db.BusProposals.FirstOrDefault(busProposal => busProposal.BusProposalId == id);
      _db.BusProposals.Remove(thisBusProposal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}