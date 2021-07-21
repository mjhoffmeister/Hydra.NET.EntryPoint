using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.EntryPoint.UnitTests
{
    public static class CollectionEntryPointTests
    {
        [Fact]
        public static void Serialize_WithStockAndUserCollections_ReturnsExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-entry-point.jsonld");

            var entryPoint = CollectionEntryPoint
                .Create(
                    "Stocks app",
                    "doc",
                    new("https://api.example.com/doc"),
                    new("https://api.example.com/"))
                .AddCollection(
                    new Uri("https://api.example.com/stocks"),
                    new Uri("doc:StockCollection"),
                    IconHint.ShowChart)
                .AddCollection(
                    new Uri("https://api.example.com/users"),
                    new Uri("doc:UserCollection"),
                    IconHint.People);

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
