using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.DTOs
{
    public record TaskDto(Guid Id, string Title, string? Description, string Status, Guid ProjectId);
    public record CreateTaskDto(string Title, string? Description, Guid ProjectId);
}
