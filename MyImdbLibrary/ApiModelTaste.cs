using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyImdbLibrary
{
    public class ApiModelTaste
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public string YouTubeUrl { get; set; }

        public ApiModelTaste() { }

        public ApiModelTaste(string title, string plot, string youTubeUrl)
        {
            Title = title;
            Plot = plot;
            YouTubeUrl = youTubeUrl;
        }
    }
}
