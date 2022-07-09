using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models;

/// <summary>
/// Price history filtering options. Filter value is a value the filtering process will be based on.
/// Deserialized differently depending on the FilterBy enumeratio
/// </summary>
public class FilteringOptions
{
    /// <summary>
    /// A value the filtering process will be based on. 
    /// Deserialized differently depending on the FilterBy enumeration.
    /// For FilterBy = 0 (Source) it will be treated as if it is a string.
    /// For FilterBy = 1 (Date) it will be treated as a date in form of Unix timestamp 
    /// </summary>
    /// <example>"'bitfinex' for FilterBy = 0"</example>
    /// <example>"'1657382481' for FilterBy = 1"</example>
    public string FilterValue { get; set; }

    /// <example>"0 for Source filtering"</example>
    /// <example>"1 for Date filtering"</example>
    public FilterBy FilterBy { get; set; }
}
