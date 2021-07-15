using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Hydra.NET.EntryPoint
{
    [SupportedClass("doc:EntryPoint")]
    public class EntryPoint
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public EntryPoint() { }

        private EntryPoint(Context context, Uri id, string type) =>
            (Context, Id, Type) = (context, id, type);
        
        [JsonPropertyName("@context")]
        public Context? Context { get; set; }

        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        [JsonPropertyName("@type")]
        public string? Type { get; set;}

        [SupportedProperty(
            "doc:EntryPoint/collection",
            "Link",
            Title = "Collection links",
            Description = "Lists links to initial collections on which a client can operate.",
            IsWritable = false)
        ]
        [JsonPropertyName("collection")]
        public List<Collection<object>> Collections { get; set; } = new List<Collection<object>>();

        /// <summary>
        /// Creates a new entry point.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="id">Id.</param>
        /// <returns><see cref="EntryPoint"/>.</returns>
        /// <remarks>
        /// This method will set @type to "EntryPoint" without a base URI or prefix.
        /// </remarks>
        public static EntryPoint Create(Context context, Uri id) => 
            new EntryPoint(context, id, "EntryPoint");

        /// <summary>
        /// Creates a new entry point given the base URI of the EntryPoint type.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="id">Id.</param>
        /// <param name="typeBaseUri">
        /// Base URI for the type. For example, providing the URI "https://api.example.com/doc#"
        /// set @type to "https://api.example.com/doc#EntryPoint".
        /// </param>
        /// <returns><see cref="EntryPoint"/>.</returns>
        public static EntryPoint Create(Context context, Uri id, Uri typeBaseUri)
        {
            string type = new Uri(typeBaseUri, "EntryPoint").ToString();
            return new EntryPoint(context, id, type);
        }

        /// <summary>
        /// Creates a new entry point given the prefix of the EntryPoint type.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="id">Id.</param>
        /// <param name="typePrefix">
        /// Prefix for the type. For example, providing the prefix "doc" will set @type to 
        /// "doc:EntryPoint".
        /// </param>
        /// <returns><see cref="EntryPoint"/>.</returns>
        public static EntryPoint Create(Context context, Uri id, string typePrefix)
        {
            string type = $"{typePrefix}:EntryPoint";
            return new EntryPoint(context, id, type);
        }

        /// <summary>
        /// Adds a collection of the given type as an initial collection in the entry point.
        /// </summary>
        /// <param name="collectionId">The id of the collection.</param>
        /// <param name="memberType">The type of the collection members.</param>
        /// <returns><see cref="EntryPoint"/>.</returns>
        public EntryPoint AddCollection<T>(Uri collectionId)
        {
            // Get the type of the class
            Type type = typeof(T);

            // Get the class's SupportedClassAttribute
            SupportedClassAttribute? supportedClassAttribute =
                type.GetCustomAttribute<SupportedClassAttribute>();

            if (supportedClassAttribute == null)
            {
                throw new ArgumentException($"{type.Name} cannot be added to the entry point " +
                    $"because it's not decorated with {nameof(SupportedClassAttribute)}.");
            }

            // Get the class's SupportedCollectionAttribute
            SupportedCollectionAttribute? supportedCollectionAttribute =
                type.GetCustomAttribute<SupportedCollectionAttribute>();

            if (supportedClassAttribute == null)
            {
                throw new ArgumentException($"{type.Name} cannot be added to the entry point " +
                    $"because it's not decorated with {nameof(SupportedCollectionAttribute)}.");
            }

            var noMembers = Enumerable.Empty<object>();

            // Create a member assertion specifying the type of the collection
            var memberAssertion = new MemberAssertion(
                supportedClassAttribute.Id, subject: supportedCollectionAttribute.Id);

            // Add the collection
            Collections.Add(
                new Collection<object>(null, collectionId, memberAssertion, noMembers));

            return this;
        }
    }
}
