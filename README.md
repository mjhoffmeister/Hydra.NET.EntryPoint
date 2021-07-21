# Hydra.NET.EntryPoint

Hydra.NET.EntryPoint is a module for Hydra.NET that adds an entry point class for use in hypermedia-driven Web APIs. Hydra's [ApiDocumentation](https://www.hydra-cg.com/spec/latest/core/#documenting-a-web-api) has an `entrypoint` property which provides a URL to an API's main entry point. This entry point can be anything, so it's up to API designers to define. This project provides a definition that may be useful for APIs and clients that primarily operate on [Collections](https://www.hydra-cg.com/spec/latest/core/#collections).

## Reference

### Entry point

Hydra.NET.EntryPoint provides a `CollectionEntryPoint` class that advertises initial collections for clients to consume. A UI client could then use those collections to create links in a navigation component.

```csharp
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
```

Given the `Stock` and `User` classes defined in the unit test project, the above example will generate the following JSON-LD when serialized.

```json
{
  "@context": {
    "doc": "https://api.example.com/doc#",
    "apiDocumentation": "http://www.w3.org/ns/hydra/core#apiDocumentation",
    "CollectionEntryPoint": "doc:CollectionEntryPoint",
    "EntryPointCollectionLink": "doc:EntryPointCollectionLink"
  },
  "@id": "https://api.example.com/",
  "@type": "CollectionEntryPoint",
  "apiDocumentation": "https://api.example.com/doc",
  "title": "Stocks app",
  "collection": [
    {
      "@id": "https://api.example.com/stocks",
      "@type": "EntryPointCollectionLink",
      "collectionType": "doc:StockCollection",
      "iconHint": "ShowChart"
    },
    {
      "@id": "https://api.example.com/users",
      "@type": "EntryPointCollectionLink",
      "collectionType": "doc:UserCollection",
      "iconHint": "People"
    }
  ]
}
```

A UI client could look up the collection types in `ApiDocumentation` to do any required UI component setup.

### AddEntryPoint extension method

The extension method `AddEntryPoint` is provided to add the entry point class definitions and entry point URL to `ApiDocumentation`.

```csharp
var apiDocumentation = new ApiDocumentation(
    new Uri("https://api.example.com/doc"), "doc");

Uri entryPointUrl = new("https://api.example.com/");

apiDocumentation
    .AddEntryPoint(entryPointUrl)
    .AddSupportedClass<Stock>()
    .AddSupportedClass<User>();
```
