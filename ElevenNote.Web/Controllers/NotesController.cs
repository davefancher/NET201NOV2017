using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new NoteService(userId);

            return svc;
        }

        // GET: Notes
        public ActionResult Index()
        {
            var model = CreateNoteService().GetNotes();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new NoteCreateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (!CreateNoteService().CreateNote(model))
            {
                ModelState.AddModelError("", "Unable to create note");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = CreateNoteService().GetNoteById(id);

            return View(model);
        }
    }
}