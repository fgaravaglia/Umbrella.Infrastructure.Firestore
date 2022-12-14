using Google.Cloud.Firestore;
using Umbrella.Infrastructure.Firestore.Abstractions;
using Umbrella.Infrastructure.Firestore.Extensions;
using System.Globalization;
using Umbrella.Infrastructure.Firestore.Tests.Entities.KeyValuePairExample;

namespace Umbrella.Infrastructure.Firestore.Tests
{
    [FirestoreData]
    public class TestEntityDocument : IBaseFirestoreData
    {
        [FirestoreDocumentId]
        public string Id { get; private set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public DateTime CreatedOn { get; set; }

        [FirestoreProperty]
        public DateTime? LastUpdatedOn { get; set;}

        [FirestoreProperty]
        public int Counter {get; set;}

        public TestEntityDocument()
        {
            this.Id = "";
            this.Name = "";
            this.CreatedOn = DateTime.Now.ToFirestoreTimeStamp();
            this.LastUpdatedOn = null;
        }
        public void SetDocumentId(string id)
        {
            this.Id = id;
        }
    }

    public class BaseRepositoryTests : BaseFirestoreTests<TestEntityDocument>
    {
        public override string CollectionName { get{ return "TestEntity"; } }

        [Test]
        public void Constructor_ThrowEx_IfProjectIdIsNull()
        {
            //******* GIVEN
            string projectId = "";
            string collectionName = "TestEntity";

            //******* WHEN
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new BaseRepository<TestEntityDocument>(projectId, collectionName, true));
            Assert.That(ex.ParamName, Is.EqualTo("projectId"));
            Assert.Pass();
        }

        [Test]
        public void Constructor_ThrowEx_IfCollectionNameIsNull()
        {
            //******* GIVEN
            string projectId = "test";
            string collectionName = "";

            //******* WHEN
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new BaseRepository<TestEntityDocument>(projectId, collectionName, true));
            Assert.That(ex.ParamName, Is.EqualTo("collectionName"));
            Assert.Pass();
        }

        #region Tests on AddAsync
        [Test]
        public void AddAsync_CreatesNewDocument()
        {
            //******* GIVEN
            var id = Guid.NewGuid().ToString();
            var testEntity = new TestEntityDocument()
            {
                Counter = 22
            };
            testEntity.SetDocumentId(id);
            var repo = InstanceRepostiory();

            //******* WHEN
            var existingEntity =  repo.AddAsync(testEntity).Result as TestEntityDocument;
            this._persistedEntities.Add(existingEntity);

            //******* ASSERT
            Assert.False(existingEntity == null);
            Assert.That(existingEntity.Id, Is.EqualTo(id));
            Assert.Pass();
        }

        [Test]
        public void AddAsync_WithAutoGeneratedId_CreatesNewDocumentWithDIfferentIdEvent_ItIsSet()
        {
            //******* GIVEN
            var id = Guid.NewGuid().ToString();
            var testEntity = new TestEntityDocument()
            {
                Counter = 22
            };
            testEntity.SetDocumentId(id);
            var repo = InstanceRepostiory(autogenerateId: true);

            //******* WHEN
            var existingEntity = repo.AddAsync(testEntity).Result as TestEntityDocument;
            this._persistedEntities.Add(existingEntity);

            //******* ASSE
            Assert.False(existingEntity == null);
            Assert.That(existingEntity.Id, Is.Not.EqualTo(id));
            Assert.Pass();
        }

        [Test]
        public void AddAsync_WithAutoGeneratedId_CreatesNewDocumentWithIdIfNotSet()
        {
            //******* GIVEN
            var testEntity = new TestEntityDocument()
            {
                Counter = 22
            };
            var repo = InstanceRepostiory(autogenerateId: true);

            //******* WHEN
            var existingEntity = repo.AddAsync(testEntity).Result as TestEntityDocument;
            this._persistedEntities.Add(existingEntity);

            //******* ASSE
            Assert.False(existingEntity == null);
            Assert.That(existingEntity.Id, Is.Not.EqualTo(""));
            Assert.Pass();
        }

        [Test]
        public void AddAsync_PersistsDocument_WithExpecetdValues()
        {
            //******* GIVEN
            var id = Guid.NewGuid().ToString();
            var creationDate = DateTime.Now.ToFirestoreTimeStamp();
            var testEntity = new TestEntityDocument()
            {
                Counter = 22,
                Name = "XXX",
                CreatedOn = creationDate
            };
            testEntity.SetDocumentId(id);
            var repo = InstanceRepostiory();

            //******* WHEN
            var existingEntity = repo.AddAsync(testEntity).Result as TestEntityDocument;
            this._persistedEntities.Add(existingEntity);

            //******* ASSE
            Assert.False(existingEntity == null);
            Assert.That(existingEntity.Id, Is.EqualTo(id));
            Assert.That(existingEntity.Name, Is.EqualTo("XXX"));
            Assert.That(existingEntity.Counter, Is.EqualTo(22));
            Assert.That(existingEntity.CreatedOn, Is.EqualTo(creationDate));

            var readEntity = repo.GetAsync(testEntity).Result as TestEntityDocument;
            Assert.False(readEntity == null);
            Assert.That(readEntity.Id, Is.EqualTo(id));
            Assert.That(readEntity.Name, Is.EqualTo("XXX"));
            Assert.That(readEntity.Counter, Is.EqualTo(22));
            AssertDatesAreEquals(readEntity.CreatedOn, creationDate, "Read Entity has unexpected value for Creation Date");
            Assert.Pass();
        }

        #endregion

        #region Tests on UpdateAsync

        [Test]
        public void UpdateAsync_PersistsDocument_WithExpecetdValues()
        {
            //******* GIVEN
            var id = Guid.NewGuid().ToString();
            var creationDate = DateTime.Now.ToFirestoreTimeStamp();
            var lastUpdateDate = DateTime.Now.ToFirestoreTimeStamp();
            var testEntity = new TestEntityDocument()
            {
                Counter = 22,
                Name = "XXX",
                CreatedOn = creationDate
            };
            testEntity.SetDocumentId(id);
            var repo = InstanceRepostiory();
            var existingEntity = repo.AddAsync(testEntity).Result;
            Assert.False(existingEntity == null, "Precondition: Entity Must exist Failed");
            testEntity.Counter = 102;
            testEntity.Name = "YYYY";
            testEntity.LastUpdatedOn = lastUpdateDate;
            this._persistedEntities.Add(testEntity);

            //******* WHEN
            existingEntity = repo.UpdateAsync(testEntity).Result as TestEntityDocument;

            //******* ASSERT
            Assert.False(existingEntity == null);
            Assert.That(existingEntity.Id, Is.EqualTo(id));
            Assert.That(existingEntity.Name, Is.EqualTo("YYYY"));
            Assert.That(existingEntity.Counter, Is.EqualTo(102));
            Assert.That(existingEntity.CreatedOn, Is.EqualTo(creationDate));
            Assert.True(existingEntity.LastUpdatedOn.HasValue);
            Assert.That(existingEntity.LastUpdatedOn.Value, Is.EqualTo(lastUpdateDate));

            var readEntity = repo.GetAsync(testEntity).Result as TestEntityDocument;
            Assert.False(readEntity == null);
            Assert.That(readEntity.Id, Is.EqualTo(id), "Read Entity has unexpected value for id");
            Assert.That(readEntity.Name, Is.EqualTo("YYYY"), "Read Entity has unexpected value for Name");
            Assert.That(readEntity.Counter, Is.EqualTo(102), "Read Entity has unexpected value for Counter");
            Assert.That(readEntity.CreatedOn.ToString("YYYY-MM-dd HH:mm:ss fff", CultureInfo.InvariantCulture), 
                        Is.EqualTo(creationDate.ToString("YYYY-MM-dd HH:mm:ss fff", CultureInfo.InvariantCulture)), 
                        "Read Entity has unexpected value for Creation Date");
            Assert.True(existingEntity.LastUpdatedOn.HasValue, "Read Entity has unexpected null last update date");
            Assert.That(existingEntity.LastUpdatedOn.Value, Is.EqualTo(lastUpdateDate), "Read Entity has unexpected value for Last Update Date");
            Assert.Pass();
        }

        #endregion

        #region Tests on GetAsync

        [Test]
        public void GetAsync_ReturnsNull_IfEntityDoesNotExist()
        {
            //******* GIVEN
            var id = "NotExistingId";
            var testEntity = new TestEntityDocument()
            {
                Counter = 22,
                Name = "XXX",
                CreatedOn = DateTime.Now.ToFirestoreTimeStamp()
            };
            testEntity.SetDocumentId(id);
            var repo = InstanceRepostiory();

            //******* WHEN
            var existingEntity = repo.GetAsync(testEntity).Result as TestEntityDocument;

            //******* ASSERT
            Assert.True(existingEntity == null);
            Assert.Pass();
        }

        #endregion

        [Test]
        public void GetAllAsync_Retreives_All_Documents()
        {
            //******* GIVEN
            var repo = InstanceRepostiory(autogenerateId: true);
            int previousCounter = (repo.GetAllAsync().Result as List<TestEntityDocument>).Count;
            
            var existingEntity = repo.AddAsync(new TestEntityDocument()
            {
                Counter = 22,
                Name = "XXX",
            }).Result;
            this._persistedEntities.Add(existingEntity);
            existingEntity = repo.AddAsync(new TestEntityDocument()
            {
                Counter = 50,
                Name = "1",
            }).Result;
            this._persistedEntities.Add(existingEntity);
            existingEntity = repo.AddAsync(new TestEntityDocument()
            {
                Counter = 100,
                Name = "2",
            }).Result;
            this._persistedEntities.Add(existingEntity);
            existingEntity = repo.AddAsync(new TestEntityDocument()
            {
                Counter = -1200,
                Name = "3",
            }).Result;
            this._persistedEntities.Add(existingEntity);

            //******* WHEN
            var existingEntities = repo.GetAllAsync().Result as List<TestEntityDocument>;

            //******* ASSERT
            Assert.False(existingEntities == null);
            Assert.That(existingEntities.Count,  Is.EqualTo(4 + previousCounter));
            Assert.Pass();
        }

        #region Using Specific Document

        [Test]
        public void AddAsync_PersistsDocument_WithPropertyAsListOfKeyValuePair()
        {
            //******* GIVEN
            var id = Guid.NewGuid().ToString();
            var creationDate = DateTime.Now.ToFirestoreTimeStamp();
            var testEntity = new TestEntityWIthKeyValuePairListDocument()
            {
                Counter = 22,
                Name = "XXX",
                CreatedOn = creationDate
            };
            testEntity.SetDocumentId(id);
            testEntity.AddPair("A", "1222");
            testEntity.AddPair("B", "-1.5");
            testEntity.AddPair("C", "22");
            var repo = InstanceRepositoryForDocument<TestEntityWIthKeyValuePairListDocument>();

            //******* WHEN
            var existingEntity = repo.AddAsync(testEntity).Result as TestEntityWIthKeyValuePairListDocument;
            //this._persistedEntities.Add(existingEntity);

            //******* ASSE
            Assert.False(existingEntity == null, "Unexpected error during Add");
            Assert.That(existingEntity.Id, Is.EqualTo(id));
            Assert.That(existingEntity.Name, Is.EqualTo("XXX"));
            Assert.That(existingEntity.Counter, Is.EqualTo(22));
            Assert.That(existingEntity.CreatedOn, Is.EqualTo(creationDate));

            var readEntity = repo.GetAsync(testEntity).Result as TestEntityWIthKeyValuePairListDocument;
            Assert.False(readEntity == null);
            Assert.That(readEntity.Id, Is.EqualTo(id), "Read entity has unexpected value for ID");
            Assert.That(readEntity.Name, Is.EqualTo("XXX"), "Read entity has unexpected value for Name");
            Assert.That(readEntity.Counter, Is.EqualTo(22), "Read entity has unexpected value for Counter");
            AssertDatesAreEquals(readEntity.CreatedOn, creationDate, "Read Entity has unexpected value for Creation Date");
            Assert.Pass();
        }

        #endregion
    }

}