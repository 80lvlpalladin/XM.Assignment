namespace XM.Assignment.Domain.Models;

public class Source
{
    /// <summary>
    /// Case-insensitive name of the price source
    /// </summary>
    /// <example>Bitstamp</example>
    public string Name { get; set; }

    public Uri Uri { get; set; }
}
