using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shortnr.Api.Domain;
using Shortnr.Api.Services.Interfaces;
using Shortnr.Commons.DTO.Input;
using Shortnr.Commons.DTO.Output;
using Shortnr.Commons.Enums;

namespace Shortnr.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShortenController : ControllerBase
    {
        private IUrlService _service;
        public ShortenController(IUrlService service) => _service = service;

        private List<ShortenedUrlOutputDTO> Convert(List<Url> urls)
        {
            List<ShortenedUrlOutputDTO> result = new List<ShortenedUrlOutputDTO>();
            foreach (var url in urls)
                result.Add(Convert(url));
            return result;
        }

        private ShortenedUrlOutputDTO Convert(Url url) => new ShortenedUrlOutputDTO()
        {
            ShortenedUrl = url.Shortened,
            OriginalUrl = url.OriginalUrl,
            Created = url.Created,
            LastUpdated = url.LastUpdated,
            Expiration = url.Expiration,
            Accesses = url.Accesses,
            LastAccess = url.LastAccess
        };

        [HttpGet]
        [Route("{userId}/{shortened}")]
        public ActionResult<ShortenedUrlOutputDTO> Get(string userId, string shortened)
        {
            try
            {
                Url url = _service.Get(userId, shortened);
                return url == null ? null : Ok(Convert(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during URL shortening. Message: {ex.Message}");
                throw ex;
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<List<ShortenedUrlOutputDTO>> Get(string userId)
        {
            try
            {
                List<Url> urls = _service.Get(userId);
                return urls == null ? null : Ok(Convert(urls));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during URL shortening. Message: {ex.Message}");
                throw ex;
            }
        }

        [HttpPost]
        [Route("{userId}/{shortened}")]
        public ActionResult<Url> Post(string userId, string shortened, [FromBody] EditUrlInputDTO input)
        {
            try
            {
                Url url = _service.Edit(input.UserId, shortened, input.NewShortened, input.Expiration);
                return Ok(Convert(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during URL shortening. Message: {ex.Message}");
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult<Url> Post([FromBody] OriginalUrlInputDTO input)
        {
            try
            {
                Url url = _service.Shorten(input.OriginalUrl, input.UserId, input.Expiration);
                return Ok(Convert(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during URL shortening. Message: {ex.Message}");
                throw ex;
            }
        }
    }
}
