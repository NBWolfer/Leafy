using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Results.UserResults
{
    public class LoginUserQueryResult
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string JWT { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
    }
}
