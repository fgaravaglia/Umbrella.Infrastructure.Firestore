using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Umbrella.Infrastructure.Firestore.Abstractions;

namespace Umbrella.Infrastructure.Firestore.Tests
{
    public abstract class BaseFirestoreTests<T> where T : IBaseFirestoreData
    {
        protected List<IBaseFirestoreData> _persistedEntities;
        protected CredentialManager _CredentialManager;

        public abstract string CollectionName { get; }

        [SetUp]
        public virtual void Setup()
        {
            this._persistedEntities = new List<IBaseFirestoreData>();
            this._CredentialManager = new CredentialManager();
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (this._persistedEntities == null ||
                (this._persistedEntities != null && !this._persistedEntities.Any()))
                return;

            var repo = InstanceRepostiory();
            foreach (var data in this._persistedEntities)
            {
                try
                {
                    repo.DeleteAsync(data).Wait();
                    Console.WriteLine($"[INF] Entity ${data.GetType().Name} with ID {data.Id} succesfully deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERR] Unexpected error during ${nameof(BaseRepositoryTests)}.TearDown");
                    Console.WriteLine(ex.Message);
                }

            }
        }

        protected BaseRepository<T> InstanceRepostiory(bool autogenerateId = false)
        {
            this._CredentialManager.SetCredentialsForGCP();
            return new BaseRepository<T>(this._CredentialManager.ProjectID, CollectionName, autogenerateId);
        }

        protected static void AssertDatesAreEquals(DateTime actualDate, DateTime expectedData, string message = "")
        {
            Assert.That(actualDate.ToString("YYYY-MM-dd HH:mm:ss fff", CultureInfo.InvariantCulture),
                        Is.EqualTo(expectedData.ToString("YYYY-MM-dd HH:mm:ss fff", CultureInfo.InvariantCulture)),
                        message);
        }
    }
}