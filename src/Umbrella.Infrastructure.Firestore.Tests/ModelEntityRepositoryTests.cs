using Microsoft.Extensions.Logging;
using Moq;
using Umbrella.Infrastructure.Firestore;

namespace Umbrella.Infrastructure.Firestore.Tests
{
    public class TestEntity
    {

        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set;}

        public TestEntity()
        {
            this.Name = "";
            this.CreatedOn = DateTime.Now;
            this.LastUpdatedOn = null;
        }
    }

    public class TestEntityRepository : ModelEntityRepository<TestEntity, TestEntityDocument>
    {
        public TestEntityRepository(ILogger logger, string projectId, string dotnetEnv, bool autoGenerateId, 
                                    string collectionName, IFirestoreDocMapper<TestEntity, TestEntityDocument> mapper)
            : base(logger, projectId, dotnetEnv, autoGenerateId, collectionName, mapper)
        {

        }

    }

    public class ModelEntityRepositoryTests
    {
        ILogger _Logger;
        IFirestoreDocMapper<TestEntity, TestEntityDocument> _Mapper;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger>();
            this._Logger = logger.Object;

            var mapper = new Mock<IFirestoreDocMapper<TestEntity, TestEntityDocument>>();
            this._Mapper = mapper.Object;
        }

        [Test]
        public void Constructor_ThrowEx_IfProjectIdIsNull()
        {
            //******* GIVEN
            string projectId = "";
            string collectionName = "TestEntity";
            string dotnetEnv = "localhost";
            bool autoGenerateId = true;
            Func<ModelEntityRepository<TestEntity, TestEntityDocument>> factory = () =>
            {
                return new TestEntityRepository(
                                                _Logger, 
                                                projectId, 
                                                dotnetEnv, 
                                                autoGenerateId, 
                                                collectionName,
                                                this._Mapper);
            };

            //******* WHEN
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => factory.Invoke());
            Assert.That(ex.ParamName, Is.EqualTo("projectId"));
            Assert.Pass();
        }

        [Test]
        public void Constructor_ThrowEx_IfDotNetEnvIsNull()
        {
            //******* GIVEN
            string projectId = "projectId";
            string collectionName = "TestEntity";
            string dotnetEnv = "";
            bool autoGenerateId = true;
            Func<ModelEntityRepository<TestEntity, TestEntityDocument>> factory = () =>
            {
                return new TestEntityRepository(
                                                _Logger,
                                                projectId,
                                                dotnetEnv,
                                                autoGenerateId,
                                                collectionName,
                                                this._Mapper);
            };

            //******* WHEN
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => factory.Invoke());
            Assert.That(ex.ParamName, Is.EqualTo("dotnetEnv"));
            Assert.Pass();
        }

        [Test]
        public void Constructor_ThrowEx_IfCollectionNameIsNull()
        {
            //******* GIVEN
            string projectId = "projectId";
            string collectionName = "";
            string dotnetEnv = "localhost";
            bool autoGenerateId = true;
            Func<ModelEntityRepository<TestEntity, TestEntityDocument>> factory = () =>
            {
                return new TestEntityRepository(
                                                _Logger,
                                                projectId,
                                                dotnetEnv,
                                                autoGenerateId,
                                                collectionName,
                                                this._Mapper);
            };

            //******* WHEN
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => factory.Invoke());
            Assert.That(ex.ParamName, Is.EqualTo("collectionName"));
            Assert.Pass();
        }

        [Test]
        public void Constructor_ThrowEx_IfLoggerIsNull()
        {
            //******* GIVEN
            string projectId = "projectId";
            string collectionName = "TestEntity";
            string dotnetEnv = "localhost";
            bool autoGenerateId = true;
            Func<ModelEntityRepository<TestEntity, TestEntityDocument>> factory = () =>
            {
                return new TestEntityRepository(
                                                null,
                                                projectId,
                                                dotnetEnv,
                                                autoGenerateId,
                                                collectionName,
                                                this._Mapper);
            };

            //******* WHEN
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => factory.Invoke());
            Assert.That(ex.ParamName, Is.EqualTo("logger"));
            Assert.Pass();
        }

        [Test]
        public void Constructor_ThrowEx_IfMapperIsNull()
        {
            //******* GIVEN
            string projectId = "projectId";
            string collectionName = "TestEntity";
            string dotnetEnv = "localhost";
            bool autoGenerateId = true;
            Func<ModelEntityRepository<TestEntity, TestEntityDocument>> factory = () =>
            {
                return new TestEntityRepository(
                                                _Logger,
                                                projectId,
                                                dotnetEnv,
                                                autoGenerateId,
                                                collectionName,
                                                null);
            };

            //******* WHEN
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => factory.Invoke());
            Assert.That(ex.ParamName, Is.EqualTo("mapper"));
            Assert.Pass();
        }
    }
}