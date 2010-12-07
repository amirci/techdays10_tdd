using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var pair in _movies)
            {
                this.Sut.Contents
                    .FirstOrDefault(m => m.Title == pair.Key && m.ReleaseDate == pair.Value)
                    .Should()
                    .Not
                    .Be
                    .Null();
            }
        }
    }
}