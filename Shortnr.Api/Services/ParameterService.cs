using Shortnr.Api.Data;
using Shortnr.Api.Domain;
using Shortnr.Api.Repositories;
using Shortnr.Api.Repositories.Interfaces;
using Shortnr.Api.Services.Interfaces;
using Shortnr.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Services
{
    public class ParameterService : IParameterService
    {
        private IParameterRepository _repository;
        private ApiDataContext _context;

        public ParameterService(ApiDataContext context, IParameterRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        public Parameter Get(string key) => _repository.Get(key);
        public Parameter GenerateNextValue(string key) => _repository.GenerateNextValue(key);
    }
}
