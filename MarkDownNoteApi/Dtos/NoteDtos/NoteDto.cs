using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkDownNoteApi.Dtos.NoteDtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string UserName {get;set;}
    }
}