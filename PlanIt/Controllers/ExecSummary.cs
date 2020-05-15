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
  public class ExecSummary : Controller
  {
    private readonly PlanItContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ExecSummary(UserManager<ApplicationUser> UserManager, PlanItContext db)
    {
      _userManager = UserManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userExecSummary = _db.ExecSummaries.Where(entry => entry.User.Id == currentUser.Id);
      return View(userExecSummary);
    }
    public ActionResult Create()
    {
      ViewBag.BusProposalId = new SelectList(_db.BusProposals, "BusProposalId", "TreatType");
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(ExecSummary execSummary, int BusProposalId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      execSummary.User = currentUser;
      _db.ExecSummaries.Add(flavor);
      if (BusProposalId != 0)
      {
        _db.BusProposalExecSummary.Add(new BusProposalExecSummary() { BusProposalId = BusProposalId, ExecSummaryId = execSummary.ExecSummaryId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisExecSummary = _db.ExecSummaries
        .Include(execSummary => execSummary.BusProposals)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(execSummary => execSummary.ExecSummaryId == id);
      return View(thisExecSummary);
    }
    public ActionResult Edit(int id)
    {
      var thisExecSummary = _db.ExecSummaries.FirstOrDefault(execSummaries => execSummaries.ExecSummaryId == id);
      ViewBag.BusProposalId = new SelectList(_db.BusProposals, "BusProposalId", "TreatType");
      return View(thisExecSummary);
    }
    [HttpPost]
    public ActionResult Edit(ExecSummary execSummary, int BusProposalId)
    {
      if (BusProposalId != 0)
      {
        _db.BusProposalExecSummary.Add(new BusProposalExecSummary() { BusProposalId = BusProposalId, ExecSummaryId = execSummary.ExecSummaryId });
      }
      _db.Entry(execSummary).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddProposal(int id)
    {
      var thisExecSummary = _db.ExecSummaries.FirstOrDefault(flavors => execSummaries.ExecSummaryId == id);
      ViewBag.BusProposalId = new SelectList(_db.BusProposals, "BusProposalId", "BusProposalTitle");
      return View(thisExecSummary);
    }
    [HttpPost]
    public ActionResult AddTreat(ExecSummary execSummary, int BusProposalId)
    {
      if (BusProposalId != 0)
      {
        _db.BusProposalExecSummary.Add(new BusProposalExecSummary() { BusProposalId = BusProposalId, ExecSummaryId = execSummary.ExecSummaryId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisExecSummary = _db.ExecSummaries.FirstOrDefault(execSummaries => execSummaries.ExecSummaryId == id);
      return View(thisExecSummary);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisExecSummary = _db.ExecSummaries.FirstOrDefault(execSummaries => execSummaries.ExecSummaryId == id);
      _db.ExecSummaries.Remove(thisExecSummary);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      var joinEntry = _db.BusProposalExecSummary.FirstOrDefault(entry => entry.TreatExecSummaryId == joinId);
      _db.BusProposalExecSummary.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}