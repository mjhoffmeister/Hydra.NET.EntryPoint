using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.EntryPoint.UnitTests
{
    public static class EntryPointTests
    {
        [Fact]
        public static void Serialize_WithStockAndUserCollections_ReturnsExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-entry-point.jsonld");

            Context entryPointContext = new(new Dictionary<string, Uri>()
            {
                { "doc", new("https://api.example.com/doc#") }
            });

            var entryPoint = EntryPoint
                .Create(entryPointContext, new Uri("https://api.example.com/"), "doc")
                .AddCollection<Stock>(new Uri("https://api.example.com/stocks"))
                .AddCollection<User>(new Uri("https://api.example.com/users"));

            // Act

            string jsonLD = JsonSerializer.Serialize(entryPoint, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }
    }
}
