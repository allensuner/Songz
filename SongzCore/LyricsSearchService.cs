using System;
using System.Collections.Generic;

namespace SongzCore
{
    public interface LyricsSearchService
    {
        /// <summary>
        /// Using the API implementation, stores all 
        /// words in a track into a set given a artist 
        /// and title
        /// </summary>
        /// <returns>Each word in the track.</returns>
        /// <param name="artist">Artist of track.</param>
        /// <param name="track">Title of track.</param>
        HashSet<string> GetWordSet(string artist, string track);
    }
}
