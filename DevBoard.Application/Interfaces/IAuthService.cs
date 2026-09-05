using DevBoard.Application.DTOs;


namespace DevBoard.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync(string email, string password);
        Task<AuthResultDto> RefreshAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}
