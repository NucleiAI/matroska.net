namespace Matroska.Net.Segments.TagsElements.TagElements
{
    public class TargetType
    {
        internal TargetType(string type)
        {
            Type = type;
        }
        internal string Type { get; set; }
        public static TargetType Collection => new TargetType("COLLECTION");
        public static TargetType Edition => new TargetType("EDITION");
        public static TargetType ISSUE => new TargetType("ISSUE");
        public static TargetType Volume => new TargetType("VOLUME");
        public static TargetType Opus => new TargetType("OPUS");
        public static TargetType Season => new TargetType("SEASON");
        public static TargetType Sequel => new TargetType("SEQUEL");
        public static TargetType Album => new TargetType("ALBUM");
        public static TargetType Opera => new TargetType("OPERA");
        public static TargetType Concert => new TargetType("CONCERT");
        public static TargetType Movie => new TargetType("MOVIE");
        public static TargetType Episode => new TargetType("EPISODE");
        public static TargetType Part => new TargetType("PART");
        public static TargetType Session => new TargetType("SESSION");
        public static TargetType Track => new TargetType("TRACK");
        public static TargetType Song => new TargetType("SONG");
        public static TargetType Chapter => new TargetType("CHAPTER");
        public static TargetType Subtrack => new TargetType("SUBTRACK");
        public static TargetType Movement => new TargetType("MOVEMENT");
        public static TargetType Scene => new TargetType("SCENE");
        public static TargetType Shot => new TargetType("SHOT");
    }
}
