using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkDownNoteApi.Models;

namespace MarkDownNoteApi.Interface
{
    public interface ITokenService
    {
        string CreateToken (AppUser user);
    }
}