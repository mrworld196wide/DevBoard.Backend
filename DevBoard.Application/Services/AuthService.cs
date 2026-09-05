using DevBoard.Application.DTOs;
using DevBoard.Application.Interfaces;
using BCrypt.Net;


namespace DevBoard.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo) => _userRepo = userRepo;

        public async Task<AuthResultDto> LoginAsync(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user is null || !VerifyPassword(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            // Token generation logic comes in Phase E (JWT)
            throw new NotImplementedException("Implemented in Phase E");
        }

        public async Task<AuthResultDto> RefreshAsync(string refreshToken) =>
            throw new NotImplementedException("Implemented in Phase E");

        public async Task LogoutAsync(string refreshToken) =>
            throw new NotImplementedException("Implemented in Phase E");

        private bool VerifyPassword(string password, string hash) =>
            BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
