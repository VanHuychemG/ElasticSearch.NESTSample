﻿using System;
using System.Collections.Generic;

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

        [JsonProperty("authors")]
        public List<Author> Authors { get; set; }

        [JsonIgnore]
        public string TargetIndex { get; set; }
    }

    public class Author
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string Firstname { get; set; }

        [JsonProperty("lastName")]
        public string Lastname { get; set; }
    }
}
