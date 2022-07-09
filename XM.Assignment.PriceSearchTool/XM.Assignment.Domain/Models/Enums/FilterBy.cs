namespace XM.Assignment.Domain.Models.Enums;

/// <summary>
/// Source = 0,
/// Date = 1
/// </summary>
public enum FilterBy
{
    /// <summary>
    /// This option will return all the price history for the source name given in FilterValue field
    /// </summary>
    Source = 0,

    /// <summary>
    /// This option will return all the price history that is later than the date given in FilterValue field
    /// </summary>
    Date = 1
}
