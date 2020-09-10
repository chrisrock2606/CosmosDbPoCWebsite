using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCrawlBeforeYouCanWalkWebsite.Models;
using TheCrawlBeforeYouCanWalkWebsite.Services;

namespace TheCrawlBeforeYouCanWalkWebsite.Controllers
{
    public class ResultController : Controller
    {
        private readonly CosmosDbService _cosmosDbService;

        public ResultController(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(long socialSecurityNo)
        {
            Person person;
            var sqlQueryText = $"SELECT * FROM c WHERE c.SocialSecurityNo = {socialSecurityNo}";
            var queryResult = await _cosmosDbService.GetItemsAsync(sqlQueryText);
            if (queryResult.Any())
                person = queryResult.First();
            else
                person = new Person();

            return View(person);
        }
    }
}
