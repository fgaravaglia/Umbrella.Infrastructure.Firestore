using Umbrella.Infrastructure.Firestore.Abstractions;

namespace Umbrella.Infrastructure.Firestore.Tests
{
    public class TestEntityDocument : IBaseFirestoreData
    {
        public string Id { get; private set; }

        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set;}

        public TestEntityDocument()
        {
            this.Id = "";
            this.Name = "";
            this.CreatedOn = DateTime.Now;
            this.LastUpdatedOn = null;
        }
        public void SetDocumentId(string id)
        {
            this.Id = id;
        }
    }

    public class BaseRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

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

        [Test]
        public void Constructor_ThrowEx_IfAspNetVariableForCredentialIsNull()
        {
            //******* GIVEN
            string projectId = "test";
            string collectionName = "TestEntity";

            //******* WHEN
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => new BaseRepository<TestEntityDocument>(projectId, collectionName, true));
            Assert.That(ex.Message.StartsWith("The Application Default Credentials are not available.", true, System.Globalization.CultureInfo.InvariantCulture), 
                        Is.EqualTo(true));
            Assert.Pass();
        }
    }
}