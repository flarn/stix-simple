using System.ComponentModel.DataAnnotations.Schema;

namespace Stix.Core.Entities
{
    public class StixEntity2_1 : EntityBase
    {
        //Required common
        //type, spec_version, id, created, modified

        [Column("type")]
        public string Type { get; set; }

        [Column("spec_version")]
        public string SpecVersion => "2.1";

        [Column("created")]
        public DateTimeOffset Created { get; set; }

        [Column("modified")]
        public DateTimeOffset? Modified { get; set; }

        ////Optional common
        ////created_by_ref, revoked, labels, confidence, lang, external_references, object_marking_refs, granular_markings, extensions

        //[Column("created_by_ref")]
        //public string? CreatedByRef { get; set; }

        //[Column("revoked")]
        //public bool? Revoked { get; set; }

        //[Column("labels")]
        //public List<string>? Labels { get; set; }

        //[Column("confidence")]
        //public int? Confidence { get; set; }

        //[Column("lang")]
        //public string? Lang { get; set; }

        //[Column("external_references")]
        //public ExternalReference[]? ExternalReferences { get; set; }

        //[Column("object_marking_refs")]
        //public List<string>? ObjectMarkingRefs { get; set; }

        //[Column("granular_markings")]
        //public GranularMarking[]? GranularMarkings { get; set; }
    }

    //public class ExternalReference
    //{
    //    [Column("id")]
    //    public string Id { get; set; }

    //    [Column("source_name")]
    //    public string SourceName { get; set; }

    //    [Column("description")]
    //    public string? Description { get; set; }

    //    [Column("url")]
    //    public string? Url { get; set; }

    //    [Column("hashes")]
    //    public Dictionary<string, string>? Hashes { get; set; }

    //    [Column("external_id")]
    //    public string? ExternalId { get; set; }
    //}
    //public class GranularMarking
    //{
    //    [Column("id")]
    //    public string Id { get; set; }
    //    [Column("lang")]
    //    public string? Lang { get; set; }

    //    [Column("marking_ref")]
    //    public string? MarkingRef { get; set; }

    //    [Column("selectors")]
    //    public List<string> Selectors { get; set; }

    //}
}