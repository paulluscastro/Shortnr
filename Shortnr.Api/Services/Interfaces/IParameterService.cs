using Shortnr.Api.Domain;
using Shortnr.Api.Repositories;
using Shortnr.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Services.Interfaces
{
    public interface IParameterService
    {
        Parameter Get(string key);
        Parameter GenerateNextValue(string key);
    }
}
