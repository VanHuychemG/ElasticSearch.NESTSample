using System;

using Newtonsoft.Json;

namespace ElasticSearch.NESTSample.Domain
{
    [JsonObject("newitem")]
    public class NewsItem
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("publishDateTime")]
        public string PublishDateTime { get; set; }

        [JsonProperty("publishDate")]
        public string PublishDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lead")]
        public string Lead { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonIgnore]
        public string TargetIndex { get; set; }
    }
}
