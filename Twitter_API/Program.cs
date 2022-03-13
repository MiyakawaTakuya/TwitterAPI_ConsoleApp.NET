using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//using TwitterSharp;
//using TweetSharp;
using Tweetinvi;
using System.Net;
using Tweetinvi.Auth;
using Tweetinvi.Models;

//Tweetinviドキュメント
//https://linvi.github.io/tweetinvi/dist/twitter-api-v1.1/timelines.html?highlight=getusertimelineasync

namespace Twitter_API
{
    class Program 
    {
        static async Task Main(string[] args)
        {
            // or you simply initialize the bearer token of client
            //var consumerOnlyCredentials = new ConsumerOnlyCredentials(Client.CONSUMER_KEY, Client.CONSUMER_SECRET);
            //var appClient = new TwitterClient(consumerOnlyCredentials);
            //await appClient.Auth.InitializeClientBearerTokenAsync();

            // we create a client with your user's credentials
            var userClient = new TwitterClient(Client.CONSUMER_KEY, Client.CONSUMER_SECRET, Client.ACCESS_TOKEN, Client.ACCESS_TOKEN_SECRET);
            // request the user's information from Twitter API
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            Console.WriteLine("Hello "+ user);

            // Get tweets from a specific user
            //var userTimeline = await userClient.Timelines.GetUserTimelineAsync("tweetinviapi");
            //Console.WriteLine(userTimeline);

            // Get the tweets available on the user's home page
            var homeTimeline = await userClient.Timelines.GetHomeTimelineAsync();
            // Get the tweets available on the user's home page
            var homeTimelineTweets = await userClient.Timelines.GetHomeTimelineAsync();
            for (int i = 0; i < 100; i++)
            {
                //Console.WriteLine(homeTimelineTweets[i].FullText);
                Console.WriteLine(removeURLs(homeTimelineTweets[i].FullText));
                if (homeTimelineTweets[i].Entities.Urls.Count > 0)
                {
                    Console.WriteLine(homeTimelineTweets[i].Entities.Urls[0].ExpandedURL);
                }
                Console.WriteLine("by " + homeTimelineTweets[i].CreatedBy.Name + " liked : " + homeTimelineTweets[i].FavoriteCount);
                Console.WriteLine("profileURL : "+ homeTimelineTweets[i].CreatedBy.ProfileImageUrl400x400);
                Console.WriteLine(" - - - - - - - - - -");
            }
        }

        internal static string removeURLs(string text)
        {
            string[] strArray = text.Trim().Split(' ');
            List<string> strList = new List<string>(strArray);
            foreach(string str in strArray)
            {
                if (str.Contains("http")) strList.Remove(str);
            }
            string afterText = string.Join(" ",strList);
            return afterText;
        }

    }
}
