using Shortnr.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shortnr.Commons.DTO.Output
{
    public class ShortenedUrlOutputDTO
    {
        [Display(Name = "Shortened")]
        public string ShortenedUrl { get; set; }
        [Display(Name = "Original")]
        public string OriginalUrl { get; set; }
        public LinkExpiration Expiration { get; set; }
        [Display(Name = "Expires on")]
        public string ExpiresOn
        {
            get
            {
                switch (Expiration)
                {
                    case LinkExpiration.OneWeek:
                        return Created.AddDays(7).ToString("MMM dd, yyyy");
                    case LinkExpiration.OneMonth:
                        return Created.AddMonths(1).ToString("MMM dd, yyyy");
                    case LinkExpiration.OneYear:
                        return Created.AddYears(1).ToString("MMM dd, yyyy");
                    case LinkExpiration.Never:
                        return "Never expires";
                    default:
                        return "Not listed";
                }
            }
        }
        [Display(Name = "Created on")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy (HH:mm:ss)}")]
        public DateTime Created { get; set; }
        [Display(Name = "Last updated on")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy (HH:mm:ss)}")]
        public DateTime LastUpdated { get; set; }
        [Display(Name = "Number of accesses")]
        public int Accesses { get; set; }
        [Display(Name = "Last accessed on")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy (HH:mm:ss)}")]
        public DateTime? LastAccess { get; set; }
    }
}
