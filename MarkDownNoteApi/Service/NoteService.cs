using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markdig;
using MarkDownNoteApi.Dtos.NoteDtos;
using MarkDownNoteApi.Interface;
using MarkDownNoteApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarkDownNoteApi.Service
{
    public class NoteService : INoteService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly INoteRepository _noteRepo;

        public NoteService(UserManager<AppUser> userManager, INoteRepository noteRepository)
        {
            this._userManager = userManager;
            this._noteRepo = noteRepository;
        }

        public async Task<Note?> CreateAsync(IFormFile formFile, string userName)
        {
            try{
                var extension = Path.GetExtension(formFile.FileName);
                var validExtensions = new List<String>() {".md"};
                if (!validExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Invalid file extension. Only .md files are allowed.");
                }
                var fileName = Guid.NewGuid().ToString() + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                var filePath = Path.Combine(path, fileName);
        
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                var user = _userManager.Users.FirstOrDefault(n=> n.UserName==userName);
                if (user==null)
                {
                    throw new InvalidOperationException($"User with name '{userName}' not found.");
                }
                var note  = await  _noteRepo.CreateAsync(new Note 
                {
                    FileName = fileName,
                    User = user
                });
                return note;
                 

            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while processing the file.", e);
            }
        }

       

        public async Task<NoteContentDto?> GetByIdAsync(int noteId, string userName)
        {
            try {
                var note = await _noteRepo.GetById(noteId);
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                
                if (note == null)
                {
                    throw new FileNotFoundException("Note not found");
                }
                
                if (note.User?.UserName != userName)
                {
                    throw new UnauthorizedAccessException("You don't have permission to access this note");
                }
                
                var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", note.FileName);
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Note file not found");
                }
                
                var content = await File.ReadAllTextAsync(path);
                
                var fileInfo = new FileInfo(path);
                
                return new NoteContentDto
                {
                    Id = note.Id,
                    Content = content,
                    FileName = note.FileName,
                };
            }
            catch (FileNotFoundException ex)
            {
            
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the note", ex);
            }
        }

        public async Task<string?> GetHtml(int noteId, string userName)
        {
            try
            {
                var note = await _noteRepo.GetById(noteId);
                if (note==null)
                {
                    throw new FileNotFoundException();
                }
                var user = await _userManager.Users.FirstOrDefaultAsync(u=> u.UserName== userName);
                if (note.User.UserName != user.UserName)
                {
                    throw new UnauthorizedAccessException();
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", note.FileName);
                var content = await  File.ReadAllTextAsync(path);
                var contentHtml = Markdown.ToHtml(content).ToString();
                return contentHtml;
            }
            catch (FileNotFoundException ex)
            {
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the note", ex);
            }
            

        }
        

        public Task<Note?> UpdateAsync(int noteId, Note note)
        {
            throw new NotImplementedException();
        }

         public Task<bool> DeleteAsync(int noteId)
        {
            throw new NotImplementedException();
        }

      
    }
}