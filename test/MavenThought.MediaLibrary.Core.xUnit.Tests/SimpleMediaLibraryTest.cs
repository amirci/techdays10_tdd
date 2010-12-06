using System;
using System.Linq;
using MavenThought.MediaLibrary.Domain;
using MbUnit.Framework;
using Rhino.Mocks;

namespace MavenThought.MediaLibrary.Core.xUnit.Tests
{
    /// <summary>
    /// Test for movie library
    /// </summary>
    [TestFixture]
    public class SimpleMediaLibraryTest
    {
        private MockRepository _mockery;

        private SimpleMediaLibrary _library;

        private IMovieCritic _critic;

        /// <summary>
        /// Initialize the repository and library
        /// </summary>
        [SetUp]
        public void BeforeEachTest()
        {
            this._mockery = new MockRepository();
            this._critic = _mockery.StrictMock<IMovieCritic>();
            this._library = new SimpleMediaLibrary(this._critic);
        }

        /// <summary>
        /// Run after each test
        /// </summary>
        [TearDown]
        public void AfterEachTest()
        {
            this._library.Clear();    
        }

        /// <summary>
        /// Checks when the library is created
        /// </summary>
        [Test]
        public void WhenTheLibraryIsCreatedTheContentShouldBeEmpty()
        {
            Assert.IsEmpty(_library.Contents);
        }

        /// <summary>
        /// Checks the contents
        /// </summary>
        [Test]
        public void WhenAMovieIsAddedThenShouldAppearInTheContents()
        {
            var movie = _mockery.StrictMock<IMovie>();

            using (_mockery.Record())
            {
                // no expectations
            }

            using (_mockery.Playback() )
            {
                var before = _library.Contents.Count();
                _library.Add(movie);
                Assert.Contains(_library.Contents, movie);
                Assert.AreEqual(before + 1, _library.Contents.Count());
            }

        }

        /// <summary>
        /// Checks the exception
        /// </summary>
        [Test]
        [ExpectedException(typeof( NotImplementedException ), "Critic Exception")]
        public void WhenListingNonViolentWeShouldNotGetACriticException()
        {
            this._library = new SimpleMediaLibrary(_critic);

            var movie = _mockery.StrictMock<IMovie>();

            using( _mockery.Record() )
            {
                SetupResult.For(_critic.IsViolent(null))
                    .IgnoreArguments()
                    .Throw(new NotImplementedException("Critic Exception"));
            }

            using( _mockery.Playback())
            {
                _library.Add(movie);

                var actual = _library.ListNonViolent();

                Assert.IsTrue(actual.Count( ) > 0, "The collection should be empty");
            }
        }
    }
}