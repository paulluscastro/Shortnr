using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shortnr.Commons.DTO.Input;
using Shortnr.Commons.DTO.Output;
using Shortnr.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shortnr.Controllers
{
    [Authorize]
    public class UrlsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UrlsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult ShortenLink()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string shortenedUrl)
        {
            List<ShortenedUrlOutputDTO> list = null;
            var user = await _userManager.GetUserAsync(User);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ShortnrApiUrl"));
                var response = await client.GetAsync($"api/v1/shorten/{user.Id}");
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return RedirectToAction("404", "Home");
                    else
                        ModelState.AddModelError(string.Empty, "API error. An exception occurred while listing your URLs.");
                }
                else
                {
                    list = await response.Content.ReadAsAsync<List<ShortenedUrlOutputDTO>>();
                    return View(list);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            ShortenedUrlOutputDTO shortened = null;
            var user = await _userManager.GetUserAsync(User);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ShortnrApiUrl"));
                var response = await client.GetAsync($"api/v1/shorten/{user.Id}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "API error. This shortened URL was not found.");
                }
                else
                {
                    shortened = await response.Content.ReadAsAsync<ShortenedUrlOutputDTO>();
                }
            }
            return View(shortened);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            EditUrlModel shortenedModel = null;
            var user = await _userManager.GetUserAsync(User);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ShortnrApiUrl"));
                var response = await client.GetAsync($"api/v1/shorten/{user.Id}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "API error. This shortened URL was not found.");
                }
                else
                {
                    ShortenedUrlOutputDTO dto = await response.Content.ReadAsAsync<ShortenedUrlOutputDTO>();
                    shortenedModel = new EditUrlModel
                    {
                        Original = dto,
                        Edit = new EditUrlInputDTO()
                        {
                            NewShortened = dto.ShortenedUrl,
                            Expiration = dto.Expiration
                        }
                    };
                }
            }
            return View(shortenedModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUrlModel model)
        {
            ShortenedUrlOutputDTO shortened = null;
            var user = await _userManager.GetUserAsync(User);
            using (var client = new HttpClient())
            {
                Debug.WriteLine($"==> DESTINO: {Environment.GetEnvironmentVariable("ShortnrApiUrl")}api/v1/shorten/{user.Id}/{id}");
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ShortnrApiUrl"));
                model.Edit.UserId = user.Id;
                EditUrlInputDTO dto = model.Edit;
                var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"api/v1/shorten/{user.Id}/{id}", content);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "API error. An error occurred while editing your URL.");
                    return View();
                }
                else
                {
                    shortened = await response.Content.ReadAsAsync<ShortenedUrlOutputDTO>();
                    return RedirectToAction("Details", "Urls", new { @Id = shortened.ShortenedUrl });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> ShortenLink(OriginalUrlInputDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.GetUserAsync(User);
            using (var client = new HttpClient())
            {
                OriginalUrlInputDTO dto = new OriginalUrlInputDTO()
                {
                    UserId = user.Id,
                    OriginalUrl = model.OriginalUrl,
                    Expiration = model.Expiration
                };
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ShortnrApiUrl"));
                var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/v1/shorten", content);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "API error. An error occurred during URL shortening.");
                    return View();
                }
                else
                {
                    ShortenedUrlOutputDTO shortened = await response.Content.ReadAsAsync<ShortenedUrlOutputDTO>();
                    return RedirectToAction("Details", "Urls", new { @Id = shortened.ShortenedUrl });
                }
            }
        }
    }
}
