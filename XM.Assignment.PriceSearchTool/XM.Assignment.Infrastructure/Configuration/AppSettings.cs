using XM.Assignment.Domain.Models;

namespace XM.Assignment.Infrastructure.Configuration;

public class AppSettings
{
    public IEnumerable<Source> Sources { get; set; }

    public int? DatastoreExpirationTimeHours { get; set; }
}
