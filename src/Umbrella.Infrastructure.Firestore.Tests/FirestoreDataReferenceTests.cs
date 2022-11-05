using Umbrella.Infrastructure.Firestore;

namespace Umbrella.Infrastructure.Firestore.Tests
{

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //******* GIVEN
            string documentId = "";

            //******* WHEN
            Assert.Throws<ArgumentNullException>(() => new FirestoreDataReference(documentId));
            Assert.Pass();
        }
    }
}