using System;
using System.IO;
using System.Net;
using System.Net.Http;
using JsonUtils;

namespace SongzCore
{
    public class LyricApiHerokuAppImpl : LyricsSearchService
    {

        const string LyricApiBaseUri = "lyric-api.herokuapp.com/api/find";

        public string[] GetLyrics(string artist, string track)
        {
            string[] lyrics = null;

            var response = MakeRequest(artist, track);
            string responseLyrics = response.lyric;

            return lyrics;
        }

        dynamic MakeRequest(string artist, string track)
        {
            string url = string.Format("{0}/{1}/{2}", LyricApiBaseUri, artist, track);
			try
			{
				HttpWebRequest request = HttpWebRequest.CreateHttp(url);
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
                string weMessage = string.Format(format, we.Status.ToString(), we.Message, we.StackTrace);
				Console.WriteLine(weMessage);
				throw we;
			}
            catch (Exception e)
            {
                string format = "Unknown exception encountered. message => {0} stackTrack => {1}";
                string eMessage = string.Format(format, e.Message, e.StackTrace);
                Console.WriteLine(eMessage);
                throw e;
            }
        }
    }
}
