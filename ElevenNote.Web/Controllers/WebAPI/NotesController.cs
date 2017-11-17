using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.Web.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Notes")]
    public class NotesController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            var detail = service.GetNoteById(noteId);

            var updatedNote =
                new NoteEditModel
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content,
                    IsStarred = newState
                };

            return service.UpdateNote(updatedNote);
        }

        [Route("{id}/Star")]
        public bool Put(int id)
        {
            return SetStarState(id, true);
        }

        [Route("{id}/Star")]
        public bool Delete(int id)
        {
            return SetStarState(id, false);
        }
    }
}
