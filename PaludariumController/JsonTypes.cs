using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaludariumController
{
    public class JsonTypes
    {

        public class LightState
        {
            public int brightness { get; set; }
            public LightColor color { get; set; }

            public string state { get; set; }

        }

        public class LightColor
        {
            public int r { get; set; }
            public int g { get; set; }
            public int b { get; set; }

        }

        public class EntityState
        {
            public Attributes Attributes { get; set; }
            public Context Context { get; set; }
            public string EntityId { get; set; }
            public DateTime LastChanged { get; set; }
            public DateTime LastUpdated { get; set; }
            public string State { get; set; }
        }

        public class Attributes
        {
            public string AccessToken { get; set; }
            public string EntityPicture { get; set; }
            public string FriendlyName { get; set; }
            public int SupportedFeatures { get; set; }
        }

        public class Context
        {
            public string Id { get; set; }
            public object ParentId { get; set; }
            public object UserId { get; set; }
        }

    }
}
