using BCrypt.Net;
using DevBoard.Application.DTOs;
using DevBoard.Application.Interfaces;
using DevBoard.Domain.Entities;
using Microsoft.Extensions.Configuration;


namespace DevBoard.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, ITokenService tokenService, IConfiguration config)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _config = config;
        }

        public async Task<AuthResultDto> LoginAsync(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return await IssueTokensAsync(user);
        }

        public async Task<AuthResultDto> RefreshAsync(string refreshToken)
        {
            var stored = await _userRepo.GetRefreshTokenAsync(refreshToken);
            if (stored is null || stored.IsRevoked || stored.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            stored.IsRevoked = true; // rotation: old one dies immediately
            await _userRepo.SaveChangesAsync();

            return await IssueTokensAsync(stored.User);
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var stored = await _userRepo.GetRefreshTokenAsync(refreshToken);
            if (stored is not null)
            {
                stored.IsRevoked = true;
                await _userRepo.SaveChangesAsync();
            }
        }

        private async Task<AuthResultDto> IssueTokensAsync(User user)
        {
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddDays(double.Parse(_config["Jwt:RefreshTokenDays"]!));

            await _userRepo.AddRefreshTokenAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = refreshToken,
                ExpiresAt = expiresAt,
                UserId = user.Id
            });
            await _userRepo.SaveChangesAsync();

            return new AuthResultDto(accessToken, refreshToken, expiresAt);
        }
    }
}
