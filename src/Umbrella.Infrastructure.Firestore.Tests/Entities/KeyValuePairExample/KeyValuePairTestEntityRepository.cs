using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Umbrella.Infrastructure.Firestore.Abstractions;
using Umbrella.Infrastructure.Firestore.Tests.Entities.KeyValuePairExample;

namespace Umbrella.Infrastructure.Firestore.Tests.Entities.KeyValuePairExample
{
    /// <summary>
    /// Class that implement repository for TestEntity that hides firestore details to outside.
    /// TestEntity is the representation of Entity inside Domain Model;
    /// TestEntityDocument is the representation of same entity inside Firestore, as a Document
    /// </summary>
    public class KeyValuePairTestEntityRepository : ModelEntityRepository<TestEntityWIthKeyValuePairList, TestEntityWIthKeyValuePairListDocument>
    {
        public KeyValuePairTestEntityRepository(ILogger logger, string projectId, string dotnetEnv, bool autoGenerateId,
                                    string collectionName, IFirestoreDocMapper<TestEntityWIthKeyValuePairList, TestEntityWIthKeyValuePairListDocument> mapper)
            : base(logger, projectId, dotnetEnv, autoGenerateId, collectionName, mapper)
        {

        }

        public KeyValuePairTestEntityRepository(ILogger logger, string dotnetEnv, 
                                    IFirestoreDocMapper<TestEntityWIthKeyValuePairList, TestEntityWIthKeyValuePairListDocument> mapper,
                                    IFirestoreDataRepository<TestEntityWIthKeyValuePairListDocument> firestoreRepo)
            : base(logger,  dotnetEnv,  mapper, firestoreRepo)
        {

        }

    }
}