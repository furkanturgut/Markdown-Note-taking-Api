using System;

namespace MarkDownNoteApi.Dtos.NoteDtos
{
    public class NoteContentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        
    }
}