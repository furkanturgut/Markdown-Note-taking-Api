using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarkDownNoteApi.Dtos.AccountDtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        
        public string Password { get; set; }
    }
}