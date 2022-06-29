using Matroska.Net.Segments;
using NEbml.Core;
using System.Collections.Generic;

namespace Matroska.Net
{
    public class MatroskaDocument
    {
        internal MatroskaDocument(EbmlReader reader, ulong version)
        {
            reader.EnterContainer();
            List<SeekHead>? seekHeads = null;
            List<Cluster>? clusters = null;
            List<Tags>? tags = null;
            while (reader.ReadNext())
            {
                if(reader.ElementId == Elements.SeekHead)
                {
                    if(seekHeads == null)
                    {
                        seekHeads = new List<SeekHead>();
                    }
                    seekHeads.Add(new SeekHead(reader, version));
                }
                else if(reader.ElementId == Elements.Info)
                {
                    Info = new Info(reader, version);
                }
                else if(reader.ElementId == Elements.Cluster)
                {
                    if(clusters == null)
                    {
                        clusters = new List<Cluster>();
                    }
                    clusters.Add(new Cluster(reader, version));
                }
                else if(reader.ElementId == Elements.Tracks)
                {
                    Tracks = new Tracks(reader, version);
                }
                else if(reader.ElementId == Elements.Cues)
                {
                    Cues = new Cues(reader, version);
                }
                else if(reader.ElementId == Elements.Attachments)
                {
                    Attachments = new Attachments(reader, version);
                }
                else if(reader.ElementId == Elements.Chapters)
                {
                    Chapters = new Chapters(reader, version);
                }
                else if(reader.ElementId == Elements.Tags)
                {
                    if(tags == null)
                    {
                        tags = new List<Tags>();
                    }
                    tags.Add(new Tags(reader, version));
                }              
            }
            Tags = tags;
            SeekHeads = seekHeads;
            Clusters = clusters;
        }
        public MatroskaDocument(
            Info info,
            IEnumerable<SeekHead>? seekHeads = null,
            Tracks? tracks = null,
            Chapters? chapters = null,
            IEnumerable<Cluster>? clusters = null,
            Cues? cues = null,
            Attachments? attachments = null,
            IEnumerable<Tags>? tags = null)
        {
            SeekHeads = seekHeads;
            Info = info;
            Tracks = tracks;
            Chapters = chapters;
            Clusters = clusters;
            Cues = cues;
            Attachments = attachments;
            Tags = tags;
        }
        /// <summary>
        /// Contains the Segment Position of other Top-Level Elements.
        /// </summary>
        public IEnumerable<SeekHead>? SeekHeads { get; set; }
        /// <summary>
        /// Contains general information about the Segment.
        /// </summary>
        public Info Info { get; set; }
        /// <summary>
        /// A Top-Level Element of information with many tracks described.
        /// </summary>
        public Tracks? Tracks { get; set; }
        /// <summary>
        /// A system to define basic menus and partition data.
        /// For more detailed information, look at the Chapters explanation in (#chapters).
        /// </summary>
        public Chapters? Chapters { get; set; }
        /// <summary>
        /// The Top-Level Element containing the (monolithic) Block structure.
        /// </summary>
        public IEnumerable<Cluster>? Clusters { get; set; }
        /// <summary>
        /// A Top-Level Element to speed seeking access.
        /// All entries are local to the Segment.
        /// </summary>
        public Cues? Cues { get; set; }
        /// <summary>
        /// Contain attached files.
        /// </summary>
        public Attachments? Attachments { get; set; }
        /// <summary>
        /// Element containing metadata describing Tracks, Editions, Chapters, Attachments, or the Segment as a whole.
        /// A list of valid tags can be found in [@!MatroskaTags].
        /// </summary>
        public IEnumerable<Tags>? Tags { get; set; }

        internal void WriteTo(EbmlWriter writer)
        {
            var segment = writer.StartMasterElement(Elements.Segment);
            if (SeekHeads != null)
            {
                foreach(var seekHead in SeekHeads)
                {
                    seekHead.WriteTo(segment);
                }
            }
            Info.WriteTo(segment);
            if(Tracks != null)
            {
                Tracks.WriteTo(segment);
            }         
            if(Chapters != null)
            {
                Chapters.WriteTo(segment);
            }
            if (Clusters != null)
            {
                foreach(var cluster in Clusters)
                {
                    cluster.WriteTo(segment);
                }
            }
            if(Cues != null)
            {
                Cues.WriteTo(segment);
            }
            if(Attachments != null)
            {
                Attachments.WriteTo(segment);
            }
            if(Tags != null)
            {
                foreach(var tags in Tags)
                {
                    tags.WriteTo(segment);
                }
            }
            segment.Dispose();
        }
    }
}
