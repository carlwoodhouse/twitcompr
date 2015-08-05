using System.Collections.Generic;
using TwitCompr.Models.Twitter;

namespace TwitCompr.Models
{
    public class SearchTweetsModel
    {
        public List<Status> statuses { get; set; }
        public SearchMetadata search_metadata { get; set; }
    }
}
