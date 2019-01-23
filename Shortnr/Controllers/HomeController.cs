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
    public class HomeController : Controller
    {
        [Route("/{shortened}")]
        public async Task<IActionResult> Redir(string shortened)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ShortnrApiUrl"));
                var response = await client.GetAsync($"{shortened}");
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return View("NotFound");
                else if (response.StatusCode == HttpStatusCode.Gone)
                    return View("Expired");
                else
                    return RedirectPermanent(response.RequestMessage.RequestUri.AbsoluteUri);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OriginalGist()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Expired()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
