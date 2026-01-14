using Google.Cloud.AIPlatform.V1;
using Value = Google.Protobuf.WellKnownTypes.Value;

namespace wealthpix.Services
{
    public interface IPredictionClient
    {
        Task<PredictResponse> PredictAsync(EndpointName endpoint, IList<Value> instances, Value parameters);
    }

    public interface IPredictionClientFactory
    {
        IPredictionClient Create(string endpoint);
    }

    /// Thin wrapper over the Google PredictionServiceClient to allow mocking in tests.
    public class GooglePredictionClientFactory : IPredictionClientFactory
    {
        public IPredictionClient Create(string endpoint)
        {
            var builder = new PredictionServiceClientBuilder
            {
                Endpoint = endpoint
            };
            return new GooglePredictionClient(builder.Build());
        }
    }

    internal sealed class GooglePredictionClient : IPredictionClient
    {
        private readonly PredictionServiceClient _inner;

        public GooglePredictionClient(PredictionServiceClient inner)
        {
            _inner = inner;
        }

        public Task<PredictResponse> PredictAsync(EndpointName endpoint, IList<Value> instances, Value parameters)
        {
            return _inner.PredictAsync(endpoint, instances, parameters);
        }
    }
}
