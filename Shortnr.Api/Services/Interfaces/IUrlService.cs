using Shortnr.Api.Domain;
using Shortnr.Api.Repositories;
using Shortnr.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Services.Interfaces
{
    public interface IUrlService
    {
        Url Access(string shortened);
        List<Url> Get(string userId);
        Url Get(string userId, string shortened);
        Url Shorten(string original, string userId, LinkExpiration duration);
        Url Edit(string userId, string shortened, string newShortened, LinkExpiration duration);
    }
}
