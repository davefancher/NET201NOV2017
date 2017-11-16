using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        private NoteEntity GetNoteFromDatabase(ElevenNoteDbContext context, int noteId)
        {
            return
                context
                    .Notes
                    .SingleOrDefault(e => e.NoteId == noteId && e.OwnerId == _userId);
        }

        public IEnumerable<NoteListItemModel> GetNotes()
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                return
                    ctx
                        .Notes
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new NoteListItemModel
                                {
                                    NoteId = e.NoteId,
                                    Title = e.Title,
                                    IsStarred = e.IsStarred,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                })
                        .ToArray();
            }
        }

        public bool CreateNote(NoteCreateModel model)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var entity =
                    new NoteEntity
                    {
                        OwnerId = _userId,
                        Title = model.Title,
                        Content = model.Content,
                        CreatedUtc = DateTime.UtcNow
                    };

                ctx.Notes.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public NoteDetailModel GetNoteById(int id)
        {
            NoteEntity entity;

            using (var ctx = new ElevenNoteDbContext())
            {
                entity = GetNoteFromDatabase(ctx, id);
            }

            if (entity == null) return new NoteDetailModel();

            return
                new NoteDetailModel
                {
                    NoteId = entity.NoteId,
                    Title = entity.Title,
                    Content = entity.Content,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
        }

        public bool UpdateNote(NoteEditModel model)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var entity = GetNoteFromDatabase(ctx, model.NoteId);

                if (entity == null) return false;

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTime.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var entity = GetNoteFromDatabase(ctx, noteId);

                if (entity == null) return false;

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
