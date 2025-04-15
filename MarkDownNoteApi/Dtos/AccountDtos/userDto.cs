using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarkDownNoteApi.Dtos.AccountDtos
{
    public class userDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}