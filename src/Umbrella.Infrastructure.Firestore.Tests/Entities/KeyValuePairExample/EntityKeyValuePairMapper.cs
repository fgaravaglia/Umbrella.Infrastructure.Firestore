using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbrella.Infrastructure.Firestore.Abstractions;
using Umbrella.Infrastructure.Firestore.Extensions;

namespace Umbrella.Infrastructure.Firestore.Tests.Entities.KeyValuePairExample
{
    public class EntityKeyValuePairMapper : IFirestoreDocMapper<TestEntityWIthKeyValuePairList, TestEntityWIthKeyValuePairListDocument>
    {
        public TestEntityWIthKeyValuePairList FromFirestoreDoc(TestEntityWIthKeyValuePairListDocument doc)
        {
            if(doc == null)
            return null;

            var dto = new TestEntityWIthKeyValuePairList();
            dto.ID = Guid.Parse(doc.Id);
            dto.Name = doc.Name;
            dto.CreatedOn = doc.CreatedOn;
            dto.LastUpdatedOn = doc.LastUpdatedOn;
            dto.Counter = doc.Counter;

            dto.PointsPerRule.Clear();
            foreach (var docPair in doc.PointsPerRule)
            {
                var pair = new EntityKeyValuePair(docPair.Id, docPair.Value);
                pair.CreatedOn = docPair.CreatedOn;
                pair.LastUpdatedOn = docPair.LastUpdatedOn;
                dto.PointsPerRule.Add(pair);
            }
            return dto;
        }

        public TestEntityWIthKeyValuePairListDocument ToFirestoreDocument(TestEntityWIthKeyValuePairList dto)
        {
            if(dto == null)
                return null;

            var doc = new TestEntityWIthKeyValuePairListDocument();
            doc.SetDocumentId(dto.ID.ToString());
            doc.Name = dto.Name;
            doc.CreatedOn = dto.CreatedOn.ToFirestoreTimeStamp();
            doc.LastUpdatedOn = dto.LastUpdatedOn.ToFirestoreNullableTimeStamp();
            doc.Counter = dto.Counter;

            doc.PointsPerRule.Clear();
            foreach(var pair in dto.PointsPerRule)
            {
                var docPair = new FirestoreKeyValuePair(pair.Id, pair.Value);
                docPair.CreatedOn = pair.CreatedOn.ToFirestoreTimeStamp();
                docPair.LastUpdatedOn = pair.LastUpdatedOn.ToFirestoreNullableTimeStamp();
                doc.PointsPerRule.Add(docPair);
            }

            return doc;
        }
    }
}