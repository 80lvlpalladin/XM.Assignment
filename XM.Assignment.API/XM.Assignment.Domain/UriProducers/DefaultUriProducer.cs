using Flurl;

namespace XM.Assignment.Domain.UriProducers;

public class DefaultUriProducer : IUriProducer
{
    public Uri Produce(Uri uri, string currency) => new(Url.Combine(uri.OriginalString, currency));
}
