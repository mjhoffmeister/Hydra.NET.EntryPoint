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


            var apiDocumentation = new ApiDocumentation(new Uri("https://api.example.com/doc"));
            apiDocumentation.Context.TryAddMapping("doc", new Uri("https://api.example.com/doc#"));

            apiDocumentation
                .AddSupportedClass<Stock>()
                .AddSupportedClass<User>();

            // Act

            apiDocumentation.AddEntryPoint();

            // Assert

            string jsonLD = JsonSerializer.Serialize(apiDocumentation, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }
    }
}
