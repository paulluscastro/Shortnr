using Shortnr.Api.Data;
using Shortnr.Api.Domain;
using Shortnr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Repositories.Interfaces
{
    public interface IUrlRepository
    {
        bool CheckAvailable(string shortened);
        Url Access(string shortened);
        List<Url> Get(string userId);
        Url Get(string userId, string shortened);
        Url Add(Url url);
        Url Update(Url url);
    }
}
