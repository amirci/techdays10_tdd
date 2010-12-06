using System;
using MavenThought.MediaLibrary.Domain;

namespace MavenThought.MediaLibrary.Core
{
 
    /// <summary>
    /// Events for adding movies
    /// </summary>
    public class MediaLibraryArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the movie added
        /// </summary>
        public IMovie Movie { get; set; }
    }
}