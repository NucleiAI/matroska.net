namespace Matroska.Net.Segments.TracksElements
{
    public enum TrackType : ulong
    {
        /// <summary>
        /// An image.
        /// </summary>
        Video = 1,
        /// <summary>
        /// Audio samples.
        /// </summary>
        Audio = 2,
        /// <summary>
        /// A mix of different other TrackType. The codec needs to define how the `Matroska Player` should interpret such data.
        /// </summary>
        Complex = 3,
        /// <summary>
        /// An image to be rendered over the video track(s).
        /// </summary>
        Logo = 16,
        /// <summary>
        /// Subtitle or closed caption data to be rendered over the video track(s).
        /// </summary>
        Subtitle = 17,
        /// <summary>
        /// Interactive button(s) to be rendered over the video track(s).
        /// </summary>
        Buttons = 18,
        /// <summary>
        /// Metadata used to control the player of the `Matroska Player`.
        /// </summary>
        Control = 32,
        /// <summary>
        /// Timed metadata that can be passed on to the `Matroska Player`.
        /// </summary>
        Metadata = 33
    }
}
