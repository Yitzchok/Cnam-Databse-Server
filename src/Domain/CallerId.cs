using System;

namespace Domain
{
    public class CallerId
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual string PhoneNumber { get; set; }
    }
}
