using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MarkDownNoteApi.Models
{
    
    public class AppUser : IdentityUser
    {
        public List<Note>? Notes { get; set; }
    }
}