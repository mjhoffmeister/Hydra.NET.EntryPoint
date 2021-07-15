# Hydra.NET.EntryPoint

Hydra.NET.EntryPoint is a module for Hydra.NET that adds an entry point class for use in hypermedia-driven Web APIs. Hydra's [ApiDocumentation](https://www.hydra-cg.com/spec/latest/core/#documenting-a-web-api) has an `entrypoint` property which provides a URL to an API's main entry point. This entry point can be anything, so it's up to API designers to define. This project provides a definition that may be useful for APIs and clients that primarily operate on [Collections](https://www.hydra-cg.com/spec/latest/core/#collections).

## Reference

### Entry point

Hydra.NET.EntryPoint provides an `EntryPoint` class that advertises initial collections for clients to consume. A UI client could then use those collections to create links in a navigation bar.

```csharp
Context entryPointContext = new(new Dictionary<string, Uri>()
{
    { "doc", new("https://api.example.com/doc#") }
});

var entryPoint = EntryPoint
    .Create(entryPointContext, new Uri("https://api.example.com/"), "doc")
    .AddCollection<Stock>(new Uri("https://api.example.com/stocks"))
    .AddCollection<User>(new Uri("https://api.example.com/users"));
```

The `Create` method has several overloads that allow you to configure the `EntryPoint` type relative to your context. `AddCollection` adds collections to the list of initial collections. Classes must be decorated with both `[SupportedClass]` and `[SupportedCollection]`, or an `ArgumentException` will be thrown.

Given the `Stock` and `User` classes defined in the unit test project, the above example will generate the following JSON-LD when serialized.

```json
{
  "@context": {
    "doc": "https://api.example.com/doc#"
  },
  "@id": "https://api.example.com/",
  "@type": "doc:EntryPoint",
  "collection": [
    {
      "@id": "https://api.example.com/stocks",
      "@type": "Collection",
      "memberAssertion": {
        "property": "doc:Stock",
        "subject": "doc:StockCollection"
      },
      "member": []
    },
    {
      "@id": "https://api.example.com/users",
      "@type": "Collection",
      "memberAssertion": {
        "property": "doc:User",
        "subject": "doc:UserCollection"
      },
      "member": []
    }
  ]
}
```

A UI client could use the collections' `memberAssertion`s to look up the collections in the `ApiDocumentation` to do any required UI component setup. `member`s aren't included as it's expected that the client will make a `GET` call to retrieve items when a user navigates to a collection.

### AddEntryPoint extension method

The extension method `AddEntryPoint` is provided to add the `EntryPoint` class definition and entry point URL to `ApiDocumentation`.

```csharp
ApiDocumentation apiDocumentation = new(new Uri("https://api.example.com/doc"));
apiDocumentation.Context.TryAddMapping("doc", new Uri("https://api.example.com/doc#"));

Uri entryPointUrl = new("https://api.example.com/");

apiDocumentation
    .AddEntryPoint(entryPointUrl)
    .AddSupportedClass<Stock>()
    .AddSupportedClass<User>();
```

Currently, the URL specified in the API documentation isn't linked to the one provided to the `EntryPoint` constructor, though this may be changed in future versions so that it doesn't have to be set twice.
