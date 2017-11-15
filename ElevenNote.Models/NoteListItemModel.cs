using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteListItemModel
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime? ModifiedUtc { get; set; }
    }
}
