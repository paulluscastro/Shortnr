using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortnr.Models;

namespace Shortnr.Controllers
{
    public class RedirecController : Controller
    {
        public async Task<IActionResult> Index(string Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ShortnrApiUrl"));
                var response = await client.GetAsync($"{Id}");
                if (response.StatusCode == HttpStatusCode.Moved)
                    return RedirectPermanent(response.Headers.Location.AbsoluteUri);
                else
                    return RedirectToAction("404", "Home");
            }
        }
    }
}
