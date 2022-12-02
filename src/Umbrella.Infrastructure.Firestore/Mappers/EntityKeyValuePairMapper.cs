using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbrella.Infrastructure.Firestore.Abstractions;
using Umbrella.Infrastructure.Firestore.Extensions;

namespace Umbrella.Infrastructure.Firestore.Tests.Mappers
{
    public class EntityKeyValuePairMapper : IFirestoreDocMapper<EntityKeyValuePair, FirestoreKeyValuePair>
    {
        public EntityKeyValuePair FromFirestoreDoc(FirestoreKeyValuePair doc)
        {
            if (doc == null)
                return null;


            var pair = new EntityKeyValuePair(doc.Id, doc.Value);
            pair.CreatedOn = doc.CreatedOn;
            pair.LastUpdatedOn = doc.LastUpdatedOn;

            return pair;
        }

        public FirestoreKeyValuePair ToFirestoreDocument(EntityKeyValuePair dto)
        {
            if (dto == null)
                return null;

            var docPair = new FirestoreKeyValuePair(dto.Id, dto.Value);
            docPair.CreatedOn = dto.CreatedOn.ToFirestoreTimeStamp();
            docPair.LastUpdatedOn = dto.LastUpdatedOn.ToFirestoreNullableTimeStamp();

            return docPair;
        }
    }
}