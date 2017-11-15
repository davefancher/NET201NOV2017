using ElevenNote.Models;
using ElevenNote.Services;
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
        // GET: Notes
        public ActionResult Index()
        {
            var svc = new NoteService();
            var model = svc.GetNotes();
            return View(model);
        }
    }
}