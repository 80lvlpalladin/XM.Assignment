namespace XM.Assignment.Domain.UriProducers;

public interface IUriProducer
{
    public Uri Produce(Uri uri, string currency);
}
