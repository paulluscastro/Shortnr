using Shortnr.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Commons.DTO.Input
{
    public class OriginalUrlInputDTO
    {
        public string UserId { get; set; }
        [Url(ErrorMessage = "The provided URL is invalid.")]
        public string OriginalUrl { get; set; }
        public LinkExpiration Expiration { get; set; }
    }
}
