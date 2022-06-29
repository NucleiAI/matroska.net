namespace Matroska.Net.Segments.TagsElements.TagElements
{
    public enum TargetTypeValue : ulong
    {
        /// <summary>
        /// The highest hierarchical level that tags can describe.
        /// </summary>
        Collection = 70,
        /// <summary>
        /// A list of lower levels grouped together.
        /// </summary>
        EditionIssueVolumeOpusSeasonSequel = 60,
        /// <summary>
        /// The most common grouping level of music and video (equals to an episode for TV series).
        /// </summary>
        AlbumOperaConcertMovieEpisode = 50,
        /// <summary>
        /// When an album or episode has different logical parts.
        /// </summary>
        PartSession = 40,
        /// <summary>
        /// The common parts of an album or movie.
        /// </summary>
        TrackSongChapter = 30,
        /// <summary>
        /// Corresponds to parts of a track for audio (like a movement).
        /// </summary>
        SubtrackPartMovementScene = 20,
        /// <summary>
        /// The lowest hierarchy found in music or movies.
        /// </summary>
        Shot = 10
    }
}
