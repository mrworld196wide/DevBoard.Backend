using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.DTOs
{
    public record RegisterRequestDto(string Username, string Email, string Password);
}
