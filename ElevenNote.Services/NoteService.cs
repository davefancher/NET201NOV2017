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
        public IEnumerable<NoteListItemModel> GetNotes()
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                return
                    ctx
                        .Notes
                        .Select(
                            e =>
                                new NoteListItemModel
                                {
                                    NoteId = e.NoteId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                })
                        .ToArray();
            }
        }
    }
}
