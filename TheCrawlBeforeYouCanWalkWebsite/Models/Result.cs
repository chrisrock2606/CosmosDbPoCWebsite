using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TheCrawlBeforeYouCanWalkWebsite.Models
{
    public enum TestConclusion {
        [Display(Name = "Afventer")]
        Waiting,
        [Display(Name = "Positiv")]
        Postive,
        [Display(Name = "Negativ")]
        Negative
    };
    public class Result
    {
        public string Id { get; set; }
        public DateTime SampleDate { get; set; }
        public TestConclusion Conclusion { get; set; }

        public Result()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
