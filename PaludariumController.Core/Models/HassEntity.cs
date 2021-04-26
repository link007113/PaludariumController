using System;
using System.Collections.Generic;
using System.Text;

namespace PaludariumController.Core.Models
{
   public class HassEntity
    {
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
