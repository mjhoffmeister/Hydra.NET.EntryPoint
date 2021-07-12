using System;
using System.Text.Json.Serialization;

namespace Hydra.NET.EntryPoint.UnitTests
{
    [SupportedClass("doc:User", Title = "User", Description = "Represents a user.")]
    [SupportedCollection("doc:UserCollection", Title = "Users", Description = "A list of users")]
    public class User
    {
        [JsonPropertyName("@id")]
        public Uri Id { get; }

        [SupportedProperty(
            "doc:User/fullName",
            Xsd.String,
            Title = "Full name")]
        [JsonPropertyName("fullName")]
        public string FullName { get; }

        [SupportedProperty(
            "doc:User/username",
            Xsd.String,
            Title = "Username",
            IsWritable = false)]
        [JsonPropertyName("username")]
        public string Username { get; }
    }
}
