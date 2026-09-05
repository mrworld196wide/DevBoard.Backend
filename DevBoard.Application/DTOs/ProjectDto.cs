using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.DTOs
{
    public record ProjectDto(Guid Id, string Name, string? Description);
    public record CreateProjectDto(string Name, string? Description);
}
