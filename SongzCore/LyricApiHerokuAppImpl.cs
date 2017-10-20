using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using JsonUtils;

namespace SongzCore
{
    public class LyricApiHerokuAppImpl : LyricsSearchService
    {

        const string LyricApiBaseUri = "lyric-api.herokuapp.com/api/find";
        const string WordRegex = @"\b(?:[a-z]{2,}|[ai])\b";

        public HashSet<string> GetWordSet(string artist, string track)
        {
            HashSet<string> wordSet = new HashSet<string>();

            // make the request
            dynamic response = MakeRequest(artist, track);
            string responseLyrics = response.lyric;

            // extract the words from the response
            char[] punctuation = responseLyrics.Where(char.IsPunctuation).Distinct().ToArray();
            IEnumerable<string> words = responseLyrics.Split().Select(x => x.Trim(punctuation));

            // add each word in the set
            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                    wordSet.Add(word.Trim().ToLower());
            }

            return wordSet;
        }

        dynamic MakeRequest(string artist, string track)
        {
            string url = string.Format("http://{0}/{1}/{2}", LyricApiBaseUri, artist, track);
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                WebResponse response = request.GetResponse();
                string reply;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    reply = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(reply))
                        return null;
                }
                return JsonObject.GetDynamicJsonObject(reply);
            }
            catch (WebException we)
            {
                string format = "WebException encountered. status => {0} message => {1} stackTrack => {2}";
                string message = string.Format(format, we.Status.ToString(), we.Message, we.StackTrace);
                Console.WriteLine(message);
                throw we;
            }
        }
    }
}
