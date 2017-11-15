using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteCreateModel
    {
        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }
    }
}
