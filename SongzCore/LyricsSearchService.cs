using System;
namespace SongzCore
{
    public interface LyricsSearchService
    {
        /// <summary>
        /// Using the API implementation, retrieves all 
        /// words in a track given a artist and title
        /// </summary>
        /// <returns>Each word in the track.</returns>
        /// <param name="artist">Artist of track.</param>
        /// <param name="track">Title of track.</param>
        string[] GetLyrics(string artist, string track);
    }
}
