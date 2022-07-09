using System.Diagnostics.CodeAnalysis;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Utiilities;

public class PriceLogEntryEqualityComparer : IEqualityComparer<PriceLogEntry>
{
    public bool Equals(PriceLogEntry? x, PriceLogEntry? y)
    {
        if (x is null && y is null)
            return true;

        if (x is null && y is not null || x is not null && y is null)
            return false;

        return x.Price == y.Price && x.TimeStamp == y.TimeStamp;
    }

    public int GetHashCode([DisallowNull] PriceLogEntry obj)
    {
        return obj.Price.GetHashCode() ^ obj.TimeStamp.GetHashCode();
    }
}
