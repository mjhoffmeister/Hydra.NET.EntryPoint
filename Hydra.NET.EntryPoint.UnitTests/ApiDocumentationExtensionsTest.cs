using System;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.EntryPoint.UnitTests
{
    public class ApiDocumentationExtensionsTest
    {
        [Fact]
        public void AddEntryPoint_StockAndUserCollections_ReturnsExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText(
                "expected-api-documentation-with-entry-point.jsonld");

            var apiDocumentation = new ApiDocumentation(
                new Uri("https://api.example.com/doc"), "doc");

            Uri entryPointUrl = new("https://api.example.com/");

            // Act

            apiDocumentation
                .AddEntryPoint(entryPointUrl)
                .AddSupportedClass<Stock>()
                .AddSupportedClass<User>();

            string jsonLD = JsonSerializer.Serialize(apiDocumentation, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }
    }
}
