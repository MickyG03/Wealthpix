## Wealthpix Unit Testing

Added a dedicated xUnit test project (Wealthpix.Tests) with Moq and FluentAssertions, introduced mockable seams for the Google prediction client and examples source, registered them in DI, and implemented unit tests for controllers and VertexAiService to validate view/model wiring, history handling, and prompt edge cases.


### Steps taken to add these tests
#

#### 1. Created xUnit test project: 
I picked xUnit because it’s the .NET 8 default template. Its widely used and unlike NUnit and MS tests xUnit keep its simpler, and doesn't require special async wrapper or addtional attributes.
```sh
dotnet new xunit -n Wealthpix.Tests
```

#### 2. Added packages:
Moq: lets us isolate external dependencies (prediction client, examples provider, controller services) by mocking interfaces, control their return values, and verify they were called as expected. That keeps tests fast and focused without hitting Google APIs or the file system.  
  
FluentAssertions: provides clear, readable assertions and better failure messages than raw Assert, reducing boilerplate and making intent obvious.
```sh 
dotnet add package Moq; dotnet add package FluentAssertions
```

#### 3. Added project reference to main app: 
Using a separate test project is the standard .NET approach. It keeps test-only dependencies (xUnit/Moq/FluentAssertions) out of the runtime app and its publish output. It also prevents test code from being compiled into or accidentally exposed by the web app.
```sh
dotnet add reference ../wealthpix.csproj
```

#### 4. Excluded test sources from app build:
This keeps test code and test-only packages out of the app build/publish output

#### 5. Added test seams in app: 
"Seams" are places we can substitute real dependencies with fakes/mocks in tests. These seams isolate external effects, make unit tests fast/deterministic, and let us assert calls/inputs without relying on network or files. I have added two:  
  
IPredictionClient + factory wrapper: instead of constructing PredictionServiceClient directly, I resolve an interface from DI. In tests, I supply a mock that returns canned responses and not the real Google API calls.

IExamplesProvider: replaces direct File.ReadAllText("examples.json"). In tests, I mock it to return in-memory examples and not the filesystem access.

#### 6. Registered seams in `Program.cs` and injected into `VertexAiService`:
I hooked up the new interfaces so the app can use them:
  
In Program.cs, I register IPredictionClientFactory and IExamplesProvider with their real implementations. That tells DI how to supply them.

In VertexAiService, instead of new-ing the Google client or reading the file directly, I accept those interfaces in the constructor. At runtime DI injects the real ones; in tests I inject mocks.

#### 7. Wrote controller tests for `HomeController` and `wealthpixChatController`:
HomeControllerTests.Index_returns_view_with_home_model_from_config: verifies that Index returns a ViewResult with a homeModel populated from configuration (BotName and Slogan).  
  
WealthpixChatControllerTests.Intro_calls_chat_with_default_prompt_and_returns_view: verifies Intro delegates to the service with the default prompt and returns the wealthpixChat view with the service's model.  
  
WealthpixChatControllerTests.Chat_returns_view_with_model_from_service: verifies Chat calls the service with the provided prompt, returns the wealthpixChat view, and passes through the model from the service.

#### 8. Wrote `VertexAiService` unit tests covering empty prompt, normal predict path, and repeated intro prompt logic:
PredictAsync_empty_prompt_returns_fallback_and_skips_prediction: empty prompt returns the fallback message, does not call the prediction client, and records the user/bot history entries.
  
PredictAsync_normal_prompt_calls_prediction_and_builds_history: verifies the endpoint/instances/parameters are sent to the mocked prediction client, examples are fetched, and chat history contains user prompt + bot reply.
  
PredictAsync_repeated_intro_prompt_does_not_duplicate_history: after an initial prompt, a repeated intro prompt uses the special branch and does not add extra history entries.
