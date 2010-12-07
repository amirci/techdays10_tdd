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
        /// Poster service to find posters
        /// </summary>
        private readonly IPosterService _posterService;

        private IMovieFactory _factory;

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleMediaLibrary"/> class
        /// </summary>
        /// <param name="critic">Critic to use</param>
        public SimpleMediaLibrary(IMovieCritic critic)
            : this(critic, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleMediaLibrary"/> class
        /// </summary>
        /// <param name="critic">Critic to use</param>
        /// <param name="posterService"></param>
        /// <param name="factory"></param>
        public SimpleMediaLibrary(IMovieCritic critic, 
            IPosterService posterService,
            IMovieFactory factory)
        {
            _critic = critic;
            _posterService = posterService;
            _factory = factory;
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

        /// <summary>
        /// Finds the poster for a movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public string Poster(IMovie movie)
        {
            return this._posterService.FindPoster(movie);
        }

        /// <summary>
        /// Import movies from dictionary
        /// </summary>
        /// <param name="movies"></param>
        public void Import(IDictionary<string, DateTime> movies)
        {
            foreach (var pair in movies)
            {
                this._contents.Add(this._factory.Create(pair.Key, pair.Value));
            }
        }
    }
}