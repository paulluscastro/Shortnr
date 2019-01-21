using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Domain
{
    public interface IBaseDomainEntity
    {
        Guid Id { get; }
        DateTime Created { get; }
        DateTime LastUpdated { get; }
        List<string> Errors { get; }
        bool Validate();
    }
}
