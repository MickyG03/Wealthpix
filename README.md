# Wealthpix
---

Wealthpix is a fintech chatbot developed using ASP.NET MVC framework with C# and follows the MVC (Model-View-Controller) architecture pattern. The chatbot leverages Google Gemini's Vertex AI for its backend infrastructure and is trained using few-shot prompt training techniques.

## Features

- Fintech Services: Offers a range of fintech services including account management, investment advice, budget tracking, and more.
- ASP.NET MVC Architecture: Built on the ASP.NET MVC framework, ensuring a scalable and maintainable codebase following the MVC architectural pattern.
- Natural Language Processing (NLP): Utilizes advanced NLP capabilities to understand and respond to user queries in natural language.
- Vertex AI Integration: Seamlessly integrates with Google Gemini's Vertex AI for robust backend infrastructure and machine learning capabilities.
- Few-Shot Prompt Training: Employs few-shot prompt training methodology for training the chatbot, enabling it to learn and adapt quickly to new tasks and scenarios.
- Responsive Frontend: Features a responsive and user-friendly frontend design powered by Bootstrap, ensuring a seamless experience across devices.
- jQuery and JavaScript: Enhances user interactivity and dynamic content generation with jQuery and JavaScript.

## Getting Started
To get started with Wealthpix, follow these steps:

#### 1. Clone repository: 
```sh
git clone https://github.com/MickyG03/WealthPix
```
#### 2. Install Dotnet: 
Download .Net framework from [.Net Framework]

#### 3. Setup Google Vertex AI: 
- To setup and know about Google's Generative AI libraries follow [Google Generative AI]
- Enable "Generative Language API" and "Vertex AI API" on [Google Cloud Console]
- Go to "Iam and Admin" on [Google Cloud Console] to setup access token and download the JSON File. 
- To try hyper-parameters, you can play with [Vertex AI Language Model]
- Get the following paramters and replace it in App config PalmAPi configuration in appsettings : 
-- Project-ID
-- Location
-- Model
-- RegionEndPoint
- You can change the parameter config based on the tests on [Vertex AI Language Model]

#### 4. Run application locally:
```sh
dotnet run
```

#### 5. Usage:
- Once the application is up and running, users can interact with Wealthpix through the chat interface. Simply type your queries or commands, and the chatbot will respond accordingly.

## Tech
Wealthpix uses a number of open source projects to work properly:

- [.Net Framework 8.0] - A web application framework developed by Microso. for building dynamic web sites with C#.
- [Bootstrap 5] -  A popular front-end framework for building responsive and mobile-first websites.
- [jQuery] - A fast, small, and feature-rich JavaScript library for DOM manipulation.
- [JavaScript] - A programming language that enables dynamic content and interactivity on web pages.
- [Google Vertex AI] - A platform that provides machine learning infrastructure and tools for building and deploying AI models.
- Natural Language Processing (NLP) - Few-shot prompt training - A technique for training machine learning models with limited data by providing a small number of examples.

## Development

Want to contribute? Great!
Contributions are welcome! If you have ideas for new features, improvements, or bug fixes, feel free to open an issue or submit a pull request.

## References

1. https://www.udemy.com/course/generative-ai-for-dotnet-developers-with-google-palm-api/learn/lecture/40576658?start=0
2. Matrix - Particles.js
3. SliderJS - Ettrics
4. Google Fonts
5. https://www.pexels.com/photo/aerial-view-and-grayscale-photography-of-high-rise-buildings-1105766/
6. https://codepen.io/matth12377/pen/NrdorR?editors=1000
---

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [.Net Framework ]: <https://dotnet.microsoft.com/en-us/download>
   [.Net Framework 8.0 ]: <https://dotnet.microsoft.com/en-us/download>
   [Google Generative AI]: <https://cloud.google.com/vertex-ai/generative-ai/docs/learn/overview>
   [Google Cloud Console]: <https://console.cloud.google.com/>
   [Vertex AI Language Model]: <https://console.cloud.google.com/projectselector2/vertex-ai/generative/language>
   [Google Vertex AI]: <https://cloud.google.com/vertex-ai?hl=en>
   [jQuery]: <http://jquery.com>
   [Bootstrap 5]: <https://getbootstrap.com/docs/5.0/getting-started/introduction/>
   [Javascript]: <https://www.javascript.com/>
