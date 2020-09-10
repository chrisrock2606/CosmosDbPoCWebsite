using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using TheCrawlBeforeYouCanWalkWebsite.Models;
using TheCrawlBeforeYouCanWalkWebsite.Services;

namespace TheCrawlBeforeYouCanWalkWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CosmosDbService _cosmosDbService;

        public HomeController(ILogger<HomeController> logger, CosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                // SQL injection alert!
                var sqlQueryText = $"SELECT * FROM c WHERE c.Name = '{person.Name}' AND c.SocialSecurityNo = {person.SocialSecurityNo}";
                var queryResult = await _cosmosDbService.GetItemsAsync(sqlQueryText);

                var personStoredInCosmosDb = queryResult.Any() ? queryResult.First() : await _cosmosDbService.AddItemAsync(person);
                personStoredInCosmosDb.Results.Add(new Result { SampleDate = DateTime.Now });
                await _cosmosDbService.UpdateItemAsync(personStoredInCosmosDb.Id, personStoredInCosmosDb);

                return RedirectToAction("Details", "Result", new { personStoredInCosmosDb.SocialSecurityNo });
            }

            return View("Index");
        }

        public IActionResult Privacy()
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
