using System;
using System.Text.Json.Serialization;

namespace Hydra.NET.EntryPoint.UnitTests
{
    [SupportedClass("User", Title = "User", Description = "Represents a user.")]
    [SupportedCollection("UserCollection", Title = "Users", Description = "A list of users")]
    public class User
    {
        [JsonPropertyName("@id")]
        public Uri? Id { get; }

        [SupportedProperty(
            "User/fullName",
            Xsd.String,
            Title = "Full name")]
        [JsonPropertyName("fullName")]
        public string? FullName { get; }

        [SupportedProperty(
            "User/username",
            Xsd.String,
            Title = "Username",
            IsWritable = false)]
        [JsonPropertyName("username")]
        public string? Username { get; }
    }
}
