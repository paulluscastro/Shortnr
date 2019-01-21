using Microsoft.AspNetCore.Identity;
using Shortnr.Commons.Enums;
using Shortnr.Commons.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Domain
{
    public class Url : BaseDomainEntity
    {
        public string UserId { get; protected set; }
        public string OriginalUrl { get; protected set; }
        public string Shortened { get; protected set; }
        public LinkExpiration Expiration { get; protected set; }
        public int Accesses { get; protected set; }
        public DateTime? LastAccess { get; protected set; }

        protected Url() { }
        public Url(string userId, string originalUrl, LinkExpiration expiration)
        {
            UserId = userId;
            OriginalUrl = originalUrl;
            Expiration = expiration;
        }
        public Url(string userId, string originalUrl, string shortened, LinkExpiration duration) : this(userId, originalUrl, duration) => Shortened = shortened;
        public void ChangeShortened(string shortened) => Shortened = shortened;
        public void Edit(string shortened, LinkExpiration duration)
        {
            Shortened = shortened;
            Expiration = duration;
        }
        public void IncreseAccess()
        {
            Accesses++;
            LastAccess = DateTime.Now;
        }
        public bool IsExpired()
        {
            switch (Expiration)
            {
                case LinkExpiration.OneWeek:
                    return Created.AddDays(7) < DateTime.Now;
                case LinkExpiration.OneMonth:
                    return Created.AddMonths(1) < DateTime.Now;
                case LinkExpiration.OneYear:
                    return Created.AddYears(1) < DateTime.Now;
                default:
                    return false;
            }

        }
        public override bool Validate()
        {
            Errors.Clear();
            if (string.IsNullOrEmpty(OriginalUrl))
                AddError(UrlErrors.OriginalUrlNotInformed);
            else if (!Uri.IsWellFormedUriString(OriginalUrl, UriKind.Absolute))
                AddError(UrlErrors.InvalidOriginalUrl);
            if (string.IsNullOrEmpty(UserId))
                AddError(UrlErrors.UrlOwnerNotInformed);
            return Errors.Count == 0;
        }

        private void AddError(object invalidOriginalUrl)
        {
            throw new NotImplementedException();
        }
    }
}
