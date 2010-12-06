using System;
using System.Collections.Generic;
using System.Linq;
using MavenThought.MediaLibrary.Domain;

namespace MavenThought.MediaLibrary.Core
{
    /// <summary>
    /// Simple implementation of media library
    /// </summary>
    public class SimpleMediaLibrary : IMediaLibrary
    {
        /// <summary>
        /// Critic to list movies
        /// </summary>
        private readonly IMovieCritic _critic;

        /// <summary>
        /// Contents to store
        /// </summary>
        private readonly ICollection<IMovie> _contents = new List<IMovie>();

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleMediaLibrary"/> class
        /// </summary>
        /// <param name="critic">Critic to use</param>
        public SimpleMediaLibrary(IMovieCritic critic)
        {
            _critic = critic;
        }

        /// <summary>
        /// Notifies when a movie is added to the library
        /// </summary>
        public event EventHandler<MediaLibraryArgs> Added = delegate { };

        /// <summary>
        /// Adds a new element to the library
        /// </summary>
        /// <param name="movie">New media element to add to the library</param>
        public void Add(IMovie movie)
        {
            this._contents.Add(movie);

            this.Added(this, new MediaLibraryArgs {Movie = movie});
        }

        /// <summary>
        /// Gets the collection of media
        /// </summary>
        public IEnumerable<IMovie> Contents
        {
            get { return this._contents; }
        }

        /// <summary>
        /// Clears the contents of the library
        /// </summary>
        public void Clear()
        {
            this._contents.Clear();
        }

        /// <summary>
        /// Finds the collection of movies that are non violent
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMovie> ListNonViolent()
        {
            if (this._critic == null)
            {
                throw new MissingCriticException();
            }

            return this._contents.Where(m => !this._critic.IsViolent(m));
        }
    }
}