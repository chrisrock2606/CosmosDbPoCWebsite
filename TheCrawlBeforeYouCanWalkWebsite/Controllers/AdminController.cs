using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCrawlBeforeYouCanWalkWebsite.Models;
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
            var sqlQueryText = "SELECT * FROM c";
            var persons = await _cosmosDbService.GetItemsAsync(sqlQueryText);
            return View(persons);
        }

        [HttpPost]
        [ActionName("SubmitTestResult")]
        [ValidateAntiForgeryToken]
        public async Task UpdateResult(Result updatedResult)        
        {
            if (updatedResult.PersonId != null)
            {
                var person = await _cosmosDbService.GetItemAsync(updatedResult.PersonId);
                var result = person.Results.FirstOrDefault(x => x.Id == updatedResult.Id);
                if (result == null)
                {
                    person.Results.Add(updatedResult);
                }
                else
                {
                    result.Conclusion = updatedResult.Conclusion;
                }
                await _cosmosDbService.UpdateItemAsync(person.Id, person);
            }
            RedirectToAction("Index", "Admin");
        }
    }
}
