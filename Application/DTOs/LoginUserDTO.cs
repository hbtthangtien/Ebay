using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LoginUserDTO
    {
        public int Id { get; init; }
        public string Email { get; init; } = default!;
        public string? Username { get; set; }
        public string? Role { get; init; }
    }
}
