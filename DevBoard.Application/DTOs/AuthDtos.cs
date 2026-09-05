using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.DTOs
{
    public record LoginRequestDto(string Email, string Password);
    public record AuthResultDto(string AccessToken, string RefreshToken, DateTime ExpiresAt);
}
