using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheCrawlBeforeYouCanWalkWebsite.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]

        public string Id { get; set; }

        [Required, Display(Name = "Navn")]
        public string Name { get; set; }

        [Required, Display(Name = "CPR-nummer")]
        public long SocialSecurityNo { get; set; }
        public List<Result> Results { get; set; } = new List<Result>();

        public Person()
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }
}
