using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkDownNoteApi.Data;
using MarkDownNoteApi.Interface;
using MarkDownNoteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarkDownNoteApi.Repositories
{
    public class NoteRepository: INoteRepository
    {
        private readonly ApplicationDataContext _context;
        public NoteRepository(ApplicationDataContext dataContext)
        {
            this._context =dataContext;
        }

        public async Task<Note> CreateAsync(Note note)
        {
            await _context.AddAsync(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> Delete(Note note)
        {
            _context.Remove(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<List<Note>> GetAllAsync()
        {
           return await _context.Notes.ToListAsync();
             
        }

        public async Task<Note?> GetById(int noteId)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId);
        }

        public async Task<Note?> Update(Note note)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == note.Id);
            if (existingNote == null)
            {
                return null;
            }

            existingNote.FileName= note.FileName;

            _context.Notes.Update(existingNote);
            await _context.SaveChangesAsync();

            return existingNote;
        }
    }
}

