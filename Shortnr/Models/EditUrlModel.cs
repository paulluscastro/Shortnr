using Shortnr.Commons.DTO.Input;
using Shortnr.Commons.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Models
{
    public class EditUrlModel
    {
        public ShortenedUrlOutputDTO Original { get; set; } = new ShortenedUrlOutputDTO();
        public EditUrlInputDTO Edit { get; set; } = new EditUrlInputDTO();
    }
}
