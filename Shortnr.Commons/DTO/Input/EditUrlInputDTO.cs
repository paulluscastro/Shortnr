using Shortnr.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Commons.DTO.Input
{
    public class EditUrlInputDTO
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Provide the new shortened URL")]
        public string NewShortened { get; set; }
        public LinkExpiration Expiration { get; set; }
    }
}
