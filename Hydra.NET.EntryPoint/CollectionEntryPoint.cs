using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hydra.NET.EntryPoint
{
    [SupportedClass("CollectionEntryPoint")]
    public class CollectionEntryPoint
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public CollectionEntryPoint() { }

        private CollectionEntryPoint(Context context, Uri id, string title, Uri apiDocumentationUrl)
        {
            (Context, Id, Title, ApiDocumentation) = (context, id, title, apiDocumentationUrl);
        }
        
        [JsonPropertyName("@context")]
        public Context? Context { get; set; }

        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        [JsonPropertyName("@type")]
        public string Type => "CollectionEntryPoint";

        [JsonPropertyName("apiDocumentation")]
        public Uri? ApiDocumentation { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [SupportedProperty(
            "CollectionEntryPoint/collection",
            "EntryPointCollectionLink",
            AddApiDocumentationPrefixToRange = true,
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
        /// <param name="title">The title of the app.</param>
        /// <param name="apiDocumentationContextPrefix">
        /// Context prefix for the API's documentation.
        /// </param>
        /// <param name="apiDocumentationBaseUrl">
        /// Base URL for the API's documentation, typically ending with a "#".
        /// </param>
        /// <param name="id">The entry point's id.</param>
        /// <returns><see cref="CollectionEntryPoint"/></returns>
        public static CollectionEntryPoint Create(
            string title, string apiDocumentationContextPrefix, Uri apiDocumentationUrl, Uri id)
        {
            // Create the entry point's context
            var context = new Context(new Dictionary<string, Uri>()
            {
                { apiDocumentationContextPrefix, new Uri(apiDocumentationUrl, "#") },
                { "apiDocumentation", new Uri("http://www.w3.org/ns/hydra/core#apiDocumentation") },
                { 
                    "CollectionEntryPoint",
                    new Uri($"{apiDocumentationContextPrefix}:CollectionEntryPoint") },
                { 
                    "EntryPointCollectionLink",
                    new Uri($"{apiDocumentationContextPrefix}:EntryPointCollectionLink") 
                }
            });

            return new CollectionEntryPoint(context, id, title, apiDocumentationUrl);
        }

        /// <summary>
        /// Adds a collection of the given type as an initial collection in the entry point.
        /// </summary>
        /// <param name="collectionId">The id of the collection.</param>
        /// <param name="memberType">The type of the collection members.</param>
        /// <returns><see cref="CollectionEntryPoint"/>.</returns>
        public CollectionEntryPoint AddCollection(
            Uri id, Uri collectionType, string? iconHint = null)
        {
            Collections.Add(new EntryPointCollectionLink(id, collectionType, iconHint));
            return this;
        }
    }
}
