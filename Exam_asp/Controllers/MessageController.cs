using Exam_asp.Data;
using Exam_asp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Exam_asp.Controllers;

public class MessageController : Controller {
    private readonly AppDbContext _context;
    private readonly PasswordHasher<string> _hasher = new();

    public MessageController(AppDbContext context) {
        _context = context;
    }

    public IActionResult Index() => View();

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(string message, string password) {
        var entity = new SecretMessage {
            Id = Guid.NewGuid(),
            Message = message,
            PasswordHash = _hasher.HashPassword(null, password)
        };

        _context.SecretMessages.Add(entity);
        _context.SaveChanges();

        return RedirectToAction("Link", new { id = entity.Id });
    }

    public IActionResult Link(Guid id) {
        ViewBag.Link = Url.Action("EnterPassword", "Message", new { id }, Request.Scheme);
        return View();
    }


    public IActionResult EnterPassword(Guid id) {
        return View(id);
    }

    [HttpPost]
    public IActionResult EnterPassword(Guid id, string password) {
        var msg = _context.SecretMessages.Find(id);
        if (msg == null)
            return RedirectToAction("Index");

        var result = _hasher.VerifyHashedPassword(null, msg.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed) {
            _context.SecretMessages.Remove(msg);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        _context.SecretMessages.Remove(msg);
        _context.SaveChanges();

        return View("Show", msg.Message);
    }
}