using Microsoft.AspNetCore.Mvc;
using PlanIt.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PlanIt.Controllers
{
  [Authorize]
  public class BusProposalController : Controller
  {
    private readonly PlanItContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 

    public BusProposalController(UserManager<ApplicationUser> userManager, PlanItContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userProposals = _db.BusProposals.Where(entry => entry.User.Id == currentUser.Id);
      return View(userProposals);
    }

    public ActionResult Create()
    {
      ViewBag.ExecSummaryId = new SelectList(_db.ExecSummaries, "ExecSummaryId", "ExecSummaryTitle");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(BusProposal busProposal, int ExecSummaryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      busProposal.User = currentUser;
      _db.BusProposals.Add(busProposal);
      if (ExecSummaryId != 0)
      {
        _db.ExecSummaryBusProposal.Add(new ExecSummaryBusProposal() { ExecSummaryId = ExecSummaryId, BusProposalId = busProposal.BusProposalId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBusProposal = _db.BusProposals
          .Include(busProposal => busProposal.ExecSummaries)
          .ThenInclude(join => join.ExecSummary)
          .FirstOrDefault(busProposal => busProposal.BusProposalId == id);
      return View(thisBusProposal);
    }

    public ActionResult Edit(int id)
    {
      var thisBusProposal = _db.BusProposals.FirstOrDefault(busProposals => busProposals.BusProposalId == id);
      ViewBag.ExecSummaryId = new SelectList(_db.ExecSummaries, "ExecSummaryId", "Name");
      return View(thisBusProposal);
    }

    [HttpPost]
    public ActionResult Edit(BusProposal busProposal, int ExecSummaryId)
    {
      if (ExecSummaryId != 0)
      {
        _db.ExecSummaryBusProposal.Add(new ExecSummaryBusProposal() { ExecSummaryId = ExecSummaryId, BusProposalId = busProposal.BusProposalId });
      }
      _db.Entry(busProposal).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddExecSummary(int id)
    {
      var thisBusProposal = _db.BusProposals.FirstOrDefault(busProposals => busProposals.BusProposalId == id);
      ViewBag.ExecSummaryId = new SelectList(_db.ExecSummaries, "ExecSummaryId", "Name");
      return View(thisBusProposal);
    }

    [HttpPost]
    public ActionResult AddExecSummary(BusProposal busProposal, int ExecSummaryId)
    {
      if (ExecSummaryId != 0)
      {
        _db.ExecSummaryBusProposal.Add(new ExecSummaryBusProposal() { ExecSummaryId = ExecSummaryId, BusProposalId = busProposal.BusProposalId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBusProposal = _db.BusProposals.FirstOrDefault(busProposals => busProposals.BusProposalId == id);
      return View(thisBusProposal);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBusProposal = _db.BusProposals.FirstOrDefault(busProposals => busProposals.BusProposalId == id);
      _db.BusProposals.Remove(thisBusProposal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteProposal(int joinId)
    {
      var joinEntry = _db.ExecSummaryBusProposal.FirstOrDefault(entry => entry.ExecSummaryBusProposalId == joinId);
      _db.ExecSummaryBusProposal.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}