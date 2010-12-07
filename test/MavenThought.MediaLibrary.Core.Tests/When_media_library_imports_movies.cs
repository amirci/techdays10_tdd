using System;
using System.Collections.Generic;
using System.Linq;
using MavenThought.MediaLibrary.Domain;
using Rhino.Mocks;
using MavenThought.Commons.Testing;
using SharpTestsEx;

namespace MavenThought.MediaLibrary.Core.Tests
{
    /// <summary>
    /// Specification when ...
    /// </summary>
    [Specification]
    public class When_media_library_imports_movies : SimpleMediaLibrarySpecification
    {
        private IDictionary<string, DateTime> _movies;
        private ICollection<IMovie> _expected;

        /// <summary>
        /// Setup movies to import
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movies = new Dictionary<string, DateTime>()
                               {
                                   {"Young Frankenstein", new DateTime(1972)},
                                   {"Spaceballs", new DateTime(1986)},
                                   {"Blazing Saddles", new DateTime(1974)}
                               };

            this._expected = new List<IMovie>();

            foreach (var pair in _movies)
            {
                var m = Mock<IMovie>();

                Dep<IMovieFactory>().Stub(f => f.Create(pair.Key, pair.Value)).Return(m);

                this._expected.Add(m);
            }
        }

        /// <summary>
        /// Import the movies
        /// </summary>
        protected override void WhenIRun()
        {
            this.Sut.Import(_movies);
        }

        /// <summary>
        /// Checks that contain all movies with the same names and dates
        /// </summary>
        [It]
        public void Should_contain_all_the_movies_from_the_dictionary()
        {
            this.Sut.Contents.Should().Have.SameSequenceAs(this._expected);
        }
    }
}