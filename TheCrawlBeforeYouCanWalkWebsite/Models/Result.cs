using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TheCrawlBeforeYouCanWalkWebsite.Models
{
    public enum TestConclusion {
        [Display(Name = "Afventer")]
        Waiting = 0,
        [Display(Name = "Positiv")]
        Postive = 1,
        [Display(Name = "Negativ")]
        Negative = 2
    };
    public class Result
    {
        public string Id { get; set; }
        public DateTime SampleDate { get; set; }

        [Display(Name = "Testresultat")]
        [EnumDataType(typeof(TestConclusion))]
        public TestConclusion Conclusion { get; set; }
        public string PersonId { get; set; }

        public Result()
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }
}
