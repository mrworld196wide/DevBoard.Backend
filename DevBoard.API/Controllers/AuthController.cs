using DevBoard.Application.DTOs;
using DevBoard.Application.Interfaces;
using DevBoard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepo;

        public AuthController(IAuthService authService, IUserRepository userRepo)
        {
            _authService = authService;
            _userRepo = userRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var existing = await _userRepo.GetByEmailAsync(dto.Email);
            if (existing is not null) return BadRequest("Email already registered");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Member"
            };
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResultDto>> Login(LoginRequestDto dto) =>
            Ok(await _authService.LoginAsync(dto.Email, dto.Password));

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResultDto>> Refresh([FromBody] string refreshToken) =>
            Ok(await _authService.RefreshAsync(refreshToken));

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            await _authService.LogoutAsync(refreshToken);
            return Ok();
        }
    }
}
