using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hydra.NET.EntryPoint
{
    [SupportedClass("EntryPoint")]
    public class EntryPoint
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public EntryPoint() { }

        private EntryPoint(Context context, Uri id) => (Context, Id) = (context, id);
        
        [JsonPropertyName("@context")]
        public Context? Context { get; set; }

        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        [JsonPropertyName("@type")]
        public string Type => "EntryPoint";

        [SupportedProperty(
            "EntryPoint/collection",
            "Link",
            Title = "Collection links",
            Description = "Lists links to initial collections on which a client can operate.",
            IsWritable = false)
        ]
        [JsonPropertyName("collection")]
        public List<EntryPointCollectionLink> Collections { get; set; } = 
            new List<EntryPointCollectionLink>();

        /// <summary>
        /// Creates a new entry point.
        /// </summary>
        /// <param name="apiDocumentationContextPrefix">
        /// Context prefix for the API's documentation.
        /// </param>
        /// <param name="apiDocumentationBaseUrl">
        /// Base URL for the API's documentation, typically ending with a "#".
        /// </param>
        /// <param name="id">The entry point's id.</param>
        /// <returns><see cref="EntryPoint"/></returns>
        public static EntryPoint Create(
            string apiDocumentationContextPrefix, Uri apiDocumentationBaseUrl, Uri id)
        {
            // Create the entry point's context
            var context = new Context(new Dictionary<string, Uri>()
            {
                { apiDocumentationContextPrefix, apiDocumentationBaseUrl },
                { "EntryPoint", new Uri($"{apiDocumentationContextPrefix}:EntryPoint") },
                { 
                    "EntryPointCollectionLink",
                    new Uri($"{apiDocumentationContextPrefix}:EntryPointCollectionLink") 
                }
            });

            return new EntryPoint(context, id);
        }

        /// <summary>
        /// Adds a collection of the given type as an initial collection in the entry point.
        /// </summary>
        /// <param name="collectionId">The id of the collection.</param>
        /// <param name="memberType">The type of the collection members.</param>
        /// <returns><see cref="EntryPoint"/>.</returns>
        public EntryPoint AddCollection(Uri id, Uri collectionType, string? iconHint = null)
        {
            Collections.Add(new EntryPointCollectionLink(id, collectionType, iconHint));
            return this;
        }
    }
}
