using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ResourceBasedAuthorization.Models;
using ResourceBasedAuthorization.Policies.Handlers;
using ResourceBasedAuthorization.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ResourceBasedAuthorization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthorizationService _authorizationService;
        public HomeController(ILogger<HomeController> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }
        [Authorize]
        public IActionResult Index()
        {
            var name = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            ViewBag.Name = name;
            return View();
        }
        public async Task<IActionResult> Settings()
        {
            //Policy And Resource Based Authoriization
            KeyValuePair<int, string> keyValue = new KeyValuePair<int, string>(5, "raj");
            if((await _authorizationService.AuthorizeAsync(User, keyValue, "EditPolicy")).Succeeded)
            {
                return View();

            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }
        public async Task<IActionResult> Privacy()
        {
            //Resource Based authorization With Operations
            ImageFileDTO imageFile = new ImageFileDTO();
            imageFile.Name = "admin";
            if ((await _authorizationService.AuthorizeAsync(User, imageFile, Operations.Read)).Succeeded)
            {
                var name = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
                var salary = HttpContext.User.FindFirst(c => c.Type == "Salary").Value;
                ViewBag.Name = name;
                ViewBag.Salary = salary;
                return View();
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}