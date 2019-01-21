using Shortnr.Api.Domain;
using Shortnr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Shortnr.Api.Data;

namespace Shortnr.Api.Repositories.Interfaces
{
    public interface IParameterRepository
    {
        Parameter Get(string key);
        Parameter GenerateNextValue(string key);
    }
}
