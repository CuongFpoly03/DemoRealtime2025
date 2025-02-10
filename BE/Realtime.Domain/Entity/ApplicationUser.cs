using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Realtime.Domain.Entity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}