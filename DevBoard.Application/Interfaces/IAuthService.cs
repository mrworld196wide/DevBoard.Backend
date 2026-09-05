using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync(string email, string password);
        Task<AuthResultDto> RefreshAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}
