using System;
using System.Text.Json.Serialization;

namespace Hydra.NET.EntryPoint
{
    [SupportedClass("EntryPointCollectionLink")]
    public class EntryPointCollectionLink
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public EntryPointCollectionLink() { }

        public EntryPointCollectionLink(Uri id, Uri collectionType, string? iconHint)
        {
            (Id, CollectionType, IconHint) = (id, collectionType, iconHint);
        }

        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        [JsonPropertyName("@type")]
        public string Type => "EntryPointCollectionLink";

        [SupportedProperty(
            "EntryPointCollectionLink/collectionType",
            Rdf.Type,
            Title = "Collection type",
            Description = "The type of the collection.",
            IsWritable = false)
        ]
        [JsonPropertyName("collectionType")]
        public Uri? CollectionType { get; set; }

        [SupportedProperty(
            "EntryPointCollectionLink/iconHint",
            Xsd.String,
            Title = "Icon hint",
            Description = "A hint for UI clients that support rendering icons.",
            IsWritable = false,
            IsRequired = false)
        ]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("iconHint")]
        public string? IconHint { get; set; }
    }
}
