using System.Collections.Generic;

namespace TwitCompr.Models.Twitter
{
    public class Hashtag
    {
        public string text { get; set; }
        public List<int> indices { get; set; }
    }
}