namespace MavenThought.MediaLibrary.Domain
{
    /// <summary>
    /// Critic for movies
    /// </summary>
    public interface IMovieCritic
    {
        /// <summary>
        /// Indicates whether a movie is violent
        /// </summary>
        /// <param name="movie">Movie to use</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise</returns>
        bool IsViolent(IMovie movie);
    }
}