using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shortnr.Api.Domain;
using Shortnr.Api.Services;

namespace Shortnr.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        UrlService _service;
        public RedirectController(UrlService service) => _service = service;
        // GET /{shortened}
        [HttpGet]
        [Route("{shortened}")]
        public  IActionResult Get(string shortened)
        {
            Url url = _service.Access(shortened);
            if (url == null)
                return NotFound();
            else if (url.IsExpired())
                return new StatusCodeResult((int)HttpStatusCode.Gone);
            else
                return RedirectPermanent(url.OriginalUrl);
        }
    }
}
