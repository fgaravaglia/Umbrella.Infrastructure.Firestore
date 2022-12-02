using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Umbrella.Infrastructure.Firestore.Abstractions;

namespace Umbrella.Infrastructure.Firestore.Tests.Entities
{
    /// <summary>
    /// Class that implement repository for TestEntity that hides firestore details to outside.
    /// TestEntity is the representation of Entity inside Domain Model;
    /// TestEntityDocument is the representation of same entity inside Firestore, as a Document
    /// </summary>
    public class TestEntityRepository : ModelEntityRepository<TestEntity, TestEntityDocument>
    {
        public TestEntityRepository(ILogger logger, string projectId, string dotnetEnv, bool autoGenerateId,
                                    string collectionName, IFirestoreDocMapper<TestEntity, TestEntityDocument> mapper)
            : base(logger, projectId, dotnetEnv, autoGenerateId, collectionName, mapper)
        {

        }

        public TestEntityRepository(ILogger logger, string dotnetEnv, 
                                    IFirestoreDocMapper<TestEntity, TestEntityDocument> mapper,
                                    IFirestoreDataRepository<TestEntityDocument> firestoreRepo)
            : base(logger,  dotnetEnv,  mapper, firestoreRepo)
        {

        }

    }
}