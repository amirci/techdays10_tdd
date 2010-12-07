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
    public class When_media_library_finds_the_poster : SimpleMediaLibrarySpecification
    {
        /// <summary>
        /// Movie to find the poster for
        /// </summary>
        private IMovie _movie;

        private string _actual;

        /// <summary>
        /// Setup the movie and poster
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movie = Mock<IMovie>();

            Dep<IPosterService>().Stub(s => s.FindPoster(this._movie)).Return("MyPoster");
        }

        /// <summary>
        /// Get the poster
        /// </summary>
        protected override void WhenIRun()
        {
            this._actual = this.Sut.Poster(this._movie);
        }

        /// <summary>
        /// Checks that the poster is the expected
        /// </summary>
        [It]
        public void Should_find_the_poster_given_by_the_service()
        {
            this._actual.Should().Be.EqualTo("MyPoster"); 
        }
    }
}