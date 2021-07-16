using System;

namespace Hydra.NET.EntryPoint
{
    public static class ApiDocumentationExtensions
    {
        public static ApiDocumentation AddEntryPoint(
            this ApiDocumentation apiDocumentation,
            Uri entryPointUrl)
        {
            apiDocumentation.EntryPoint = entryPointUrl;
            return apiDocumentation
                .AddSupportedClass<EntryPoint>()
                .AddSupportedClass<EntryPointCollectionLink>();
        }
            
    }
}
