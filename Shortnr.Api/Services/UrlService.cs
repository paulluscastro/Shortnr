using Shortnr.Api.Constants;
using Shortnr.Api.Domain;
using Shortnr.Api.Repositories;
using Shortnr.Api.Repositories.Interfaces;
using Shortnr.Api.Services.Interfaces;
using Shortnr.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Services
{
    public class UrlService : IUrlService
    {
        private IUrlRepository _repository;
        private IParameterService _parameterService;

        public UrlService(UrlRepository repository, IParameterService parameterService)
        {
            _repository = repository;
            _parameterService = parameterService;
        }
        public Url Access(string shortened)
        {
            Url url = _repository.Access(shortened);
            if (url == null) return null;
            url.IncreseAccess();
            _repository.Update(url);
            return url;
        }
        public List<Url> Get(string userId) => _repository.Get(userId);
        public Url Get(string userId, string shortened) => _repository.Get(userId, shortened);
        public Url Shorten(string original, string userId, LinkExpiration duration)
        {
            Url url = new Url(userId, original, duration);
            string generated = _parameterService.GenerateNextValue(ParameterConstants.LastCreated).Value;
            while (!_repository.CheckAvailable(generated))
                generated = _parameterService.GenerateNextValue(ParameterConstants.LastCreated).Value;
            url.ChangeShortened(generated);
            _repository.Add(url);
            return url;
        }
        public Url Edit(string userId, string shortened, string newShortened, LinkExpiration duration)
        {
            Url url = Get(userId, shortened);
            if (url == null) return null;
            if (_repository.CheckAvailable(newShortened))
                url.Edit(newShortened, duration);
            else
                url.Edit(url.Shortened, duration);
            _repository.Update(url);
            return url;
        }
    }
}
