using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkDownNoteApi.Models;

namespace MarkDownNoteApi.Interface
{
    public interface INoteRepository
    {
        Task<Note> CreateAsync (Note note);
        Task<List<Note>> GetAllAsync ();
        Task<Note?> GetById (int noteId);
        Task<Note?> Update (Note note);
        Task<Note> Delete (Note note);
    }
}