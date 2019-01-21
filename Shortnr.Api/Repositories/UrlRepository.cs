using Shortnr.Api.Data;
using Shortnr.Api.Domain;
using Shortnr.Api.Repositories.Interfaces;
using Shortnr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        ApiDataContext _context;
        public UrlRepository(ApiDataContext context) => _context = context;
        public bool CheckAvailable(string shortened) => !_context.Urls.Any(u => u.Shortened == shortened);
        public Url Access(string shortened) => _context.Urls.Where(u => u.Shortened == shortened).FirstOrDefault();
        public List<Url> Get(string userId) => _context.Urls.Where(u => u.UserId == userId).ToList();
        public Url Get(string userId, string shortened) => _context.Urls.Where(u => u.UserId == userId && u.Shortened == shortened).FirstOrDefault();
        public Url Add(Url url)
        {
            _context.Urls.Add(url);
            _context.SaveChanges();
            return url;
        }
        public Url Update(Url url)
        {
            _context.Urls.Update(url);
            _context.SaveChanges();
            return url;
        }
    }
}
