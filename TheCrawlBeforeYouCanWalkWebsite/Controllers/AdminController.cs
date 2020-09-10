using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCrawlBeforeYouCanWalkWebsite.Services;

namespace TheCrawlBeforeYouCanWalkWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly CosmosDbService _cosmosDbService;

        public AdminController(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
        public async Task<IActionResult> Index()
        {
            var sqlQueryText = $"SELECT * FROM c";
            var persons = await _cosmosDbService.GetItemsAsync(sqlQueryText);
            return View(persons);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Update([FromBody] object o)
        {
            var type = o.GetType();
            var s = o;
        }
    }
}
