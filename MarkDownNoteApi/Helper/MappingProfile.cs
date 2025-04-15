using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarkDownNoteApi.Dtos.NoteDtos;
using MarkDownNoteApi.Models;

namespace MarkDownNoteApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Note, NoteDto>().ForMember(dest=> dest.UserName , opt=> opt.MapFrom(opt=> opt.User.UserName));
        }
    }
}