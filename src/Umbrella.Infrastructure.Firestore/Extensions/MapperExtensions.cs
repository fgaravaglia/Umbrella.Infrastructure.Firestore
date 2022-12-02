using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbrella.Infrastructure.Firestore.Abstractions;
using Umbrella.Infrastructure.Firestore.Tests.Mappers;

namespace Umbrella.Infrastructure.Firestore.Extensions
{
    /// <summary>
    /// Extensions to support mapping between dfirestore docuemnt and entities
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// Converts the DTO into Firestore Document
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static FirestoreKeyValuePair MapToFirestoreDocument(this EntityKeyValuePair dto)
        {
            if(dto == null)
                throw new ArgumentNullException(nameof(dto));
            var mapper = new EntityKeyValuePairMapper();
            return mapper.ToFirestoreDocument(dto);
        }
        /// <summary>
        /// Converts the Firestore Document into DTO
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static EntityKeyValuePair MapToDTO(this FirestoreKeyValuePair doc)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));
            var mapper = new EntityKeyValuePairMapper();
            return mapper.FromFirestoreDoc(doc);
        }
    }
}