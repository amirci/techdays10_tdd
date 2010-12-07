using MavenThought.Commons.Testing;
using MavenThought.MediaLibrary.Domain;
using SharpTestsEx;

namespace MavenThought.MediaLibrary.Core.Tests
{
    /// <summary>
    /// Specification when adding a movie
    /// </summary>
    [Specification]
    public class When_media_library_adds_a_movie : SimpleMediaLibrarySpecification
    {
        private IMovie _movie;

        /// <summary>
        /// Setup the movie
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movie = Mock<IMovie>();
        }

        /// <summary>
        /// Add the movie
        /// </summary>
        protected override void WhenIRun()
        {
            this.Sut.Add(_movie);
        }

        /// <summary>
        /// Checks that movie has been added
        /// </summary>
        [It]
        public void Should_add_the_movie_to_the_library()
        {
            this.Sut.Contents.Should().Have.SameSequenceAs(this._movie);
        }
    }
}