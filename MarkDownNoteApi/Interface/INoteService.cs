using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkDownNoteApi.Dtos.NoteDtos;
using MarkDownNoteApi.Models;

namespace MarkDownNoteApi.Interface
{
    public interface INoteService
    {

        Task<NoteContentDto?> GetByIdAsync(int noteId, string userName);
        Task<string?> GetHtml (int noteId, string userName);
        Task<Note?> CreateAsync(IFormFile formFile, string userName);
        Task<Note?> UpdateAsync(int noteId, Note note);
        Task<bool> DeleteAsync(int noteId);
    }
}