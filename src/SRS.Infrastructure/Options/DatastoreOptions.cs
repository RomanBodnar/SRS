using System;
using System.Collections.Generic;
using System.Text;

namespace SRS.Infrastructure.Options
{
    public class DatastoreOptions
    {
        public string DatabaseName { get; set; }
        public string MasterConnection { get; set; }
        public string SrsConnection { get; set; }
    }
}
