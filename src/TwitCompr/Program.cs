using Microsoft.Azure;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitCompr.Models;
using TwitCompr.Models.Twitter;

namespace TwitCompr
{
    class Program
    {
        private static string token = string.Empty;
        private static string tokenSecret = string.Empty;
        private static string consumerKey = string.Empty;
        private static string consumerSecret = string.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting twitcompr ... hold onto your hats");

            var client = new RestClient("https://api.twitter.com");

            token = CloudConfigurationManager.GetSetting("token");
            tokenSecret = CloudConfigurationManager.GetSetting("tokenSecret");
            consumerKey = CloudConfigurationManager.GetSetting("consumerKey");
            consumerSecret = CloudConfigurationManager.GetSetting("consumerSecret");

            // The OAuth keys/tokens/secrets are retrieved elsewhere
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                consumerKey, consumerSecret, token, tokenSecret
            );

            var statuses = new List<Status>();
            var phrases = new List<string> { "\"rt to win\"" };

            foreach (var phrase in phrases)
            {
                var encodedPhrase = HttpUtility.UrlEncode(phrase);
                var request = new RestRequest("/1.1/search/tweets.json", Method.GET);
                request.AddParameter("q", encodedPhrase, ParameterType.GetOrPost);

                var response = client.Execute<SearchTweetsModel>(request);

                if (response != null 
                    && response.ResponseStatus == ResponseStatus.Completed
                    && response.Data != null && response.Data.statuses != null)
                {
                    statuses.AddRange(response.Data.statuses);
                }
            }

            statuses = statuses.Distinct().ToList();

         
            Console.ReadKey();

        }
    }
}
