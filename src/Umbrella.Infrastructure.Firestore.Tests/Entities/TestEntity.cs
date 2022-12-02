using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Infrastructure.Firestore.Tests.Entities
{
    public class TestEntity
    {
        public Guid ID { get; set; }
        public int Counter { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public TestEntity()
        {
            this.Name = "";
            this.CreatedOn = DateTime.Now;
            this.LastUpdatedOn = null;
        }
    }
}