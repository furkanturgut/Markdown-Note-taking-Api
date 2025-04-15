using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using MarkDownNoteApi.Dtos.NoteDtos;
using MarkDownNoteApi.Extensions;
using MarkDownNoteApi.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarkDownNoteApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;

        public NoteController(INoteService noteService, IMapper mapper)
        {
            this._noteService = noteService;
            this._mapper= mapper;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(IFormFile file)
        {

            try 
            {
                var userName = User.GetUserName();
                var note= await _noteService.CreateAsync(file, userName);
                return Ok(_mapper.Map<NoteDto>(note));
            }
            catch (Exception ex)
            {
                return StatusCode(500 , ex.ToString());
            }
        }
        [HttpGet]
        [Route("{noteId:int}")]
        [Authorize]

        public async Task<IActionResult> GetMd (int noteId)
        {
            try
            {
                var userName = User.GetUserName();
                var note = await _noteService.GetByIdAsync(noteId, userName); 
                return Ok(note);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(404, "File Not Found");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, "You don't have permission to acces this note");
            }
            catch (Exception ex)
            {
                return StatusCode (500, "An error occured while processing your request");
            }
        }
        [HttpGet]
        [Route("{noteId:int}/html")]
        [Authorize]
        public async Task<IActionResult> GetHtml (int noteId)
        {
            try
            {
                var userName = User.GetUserName();
                var note =await  _noteService.GetHtml(noteId, userName);
                return Ok(note);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(404, "File Not Found");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, "You don't have permission to acces this note");
            }
            catch (Exception ex)
            {
                return StatusCode (500, "An error occured while processing your request");
            }
        }
    }
}