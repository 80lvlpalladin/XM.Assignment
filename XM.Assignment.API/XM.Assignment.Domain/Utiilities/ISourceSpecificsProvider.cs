namespace XM.Assignment.Domain.Utiilities;

public interface ISourceSpecificsProvider
{
    public T GetImplementationOf<T>(string sourceName) where T : class;
}
