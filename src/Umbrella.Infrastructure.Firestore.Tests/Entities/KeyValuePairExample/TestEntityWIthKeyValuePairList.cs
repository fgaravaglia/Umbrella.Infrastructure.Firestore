using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Infrastructure.Firestore.Tests.Entities.KeyValuePairExample
{
    public class TestEntityWIthKeyValuePairList : TestEntity
    {
        public List<EntityKeyValuePair> PointsPerRule {get; set;}

        public TestEntityWIthKeyValuePairList() : base()
        {
            this.PointsPerRule = new List<EntityKeyValuePair>();
        }

        public void AddPair(string id, string value)
        {
            var pair = new EntityKeyValuePair(id, value);
            this.PointsPerRule.Add(pair);
        }
    }
}