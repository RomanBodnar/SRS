using System;
using System.Collections.Generic;
using System.Text;

namespace SRS.Infrastructure.Options
{
    public class DatastoreOptions
    {
        public string Connection { get; set; }
        public string InitialCatalog { get; set; }
        public string DatabaseName { get; set; }
    }
}
