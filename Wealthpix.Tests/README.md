Wealthpix.Tests setup steps

- Created xUnit test project: `dotnet new xunit -n Wealthpix.Tests`
- Added packages: `dotnet add package Moq`; `dotnet add package FluentAssertions`
- Added project reference to main app: `dotnet add reference ../wealthpix.csproj`
- Excluded test sources from app build via `wealthpix.csproj` `Compile/None/EmbeddedResource Remove` entries
- Added test seams in app: `IPredictionClient`/factory wrapper, `IExamplesProvider`
- Registered seams in `Program.cs` and injected into `VertexAiService`
- Wrote controller tests for `HomeController` and `wealthpixChatController`
- Wrote `VertexAiService` unit tests covering empty prompt, normal predict path, and repeated intro prompt logic
