using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Umbrella.Infrastructure.Firestore.Abstractions;
using Umbrella.Infrastructure.Firestore.Extensions;

namespace Umbrella.Infrastructure.Firestore.Tests.Entities.KeyValuePairExample
{
    [FirestoreData]
    public class TestEntityWIthKeyValuePairListDocument : IBaseFirestoreData
    {
        [FirestoreDocumentId]
        public string Id { get; private set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public DateTime CreatedOn { get; set; }

        [FirestoreProperty]
        public DateTime? LastUpdatedOn { get; set; }

        [FirestoreProperty]
        public int Counter { get; set; }

        [FirestoreProperty]
        public List<FirestoreKeyValuePair> PointsPerRule { get; set; }


        public TestEntityWIthKeyValuePairListDocument()
        {
            this.Id = "";
            this.Name = "";
            this.CreatedOn = DateTime.Now.ToFirestoreTimeStamp();
            this.LastUpdatedOn = null;
            this.PointsPerRule = new List<FirestoreKeyValuePair>();
        }
        public void SetDocumentId(string id)
        {
            this.Id = id;
        }

        public void AddPair(string id, string value)
        {
            var pair = new FirestoreKeyValuePair(id, value);
            pair.SetDocumentId(id);
            this.PointsPerRule.Add(pair);
        }
    }
}