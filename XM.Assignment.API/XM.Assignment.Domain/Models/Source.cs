using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models
{
    public class Source
    {
        public Source(string name, Uri uri)
        {
            Name = name;
            Uri = uri;
        }

        public string Name { get; set; }

        public Uri Uri { get; set; }
    }
}
