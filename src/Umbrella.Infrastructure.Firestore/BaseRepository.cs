using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using Umbrella.Infrastructure.Firestore.Abstractions;

namespace Umbrella.Infrastructure.Firestore
{

    /// <summary>
    ///     Represents the base repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IFirestoreDataRepository<T> where T : IBaseFirestoreData
    {
        readonly bool _AutoGenerateID;
        readonly string _CollectionName;
        public readonly FirestoreDb _firestoreDb;

        /// <summary>
        /// default COnstructor
        /// </summary>
        /// <param name="projectId">GCP projetc id where FIrestore has been provisioned</param>
        /// <param name="collectionName">name of collection to save data on FIrestore instance</param>
        /// <param name="autoGenerateId">TRUE to generate id of object; FALSE to use the id set uoside the repo</param>
        public BaseRepository(string projectId, string collectionName, bool autoGenerateID)
        {
            this._AutoGenerateID = autoGenerateID;
            if (String.IsNullOrEmpty(projectId))
                throw new ArgumentNullException(nameof(projectId));
            if (String.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException(nameof(collectionName));
            this._CollectionName = collectionName;
            _firestoreDb = FirestoreDb.Create(projectId); 
        }

        public async Task<List<T>> GetAllAsync()
        {
            Query query = _firestoreDb.Collection(this._CollectionName);
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) 
                    continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) 
                    continue;
                data.SetDocumentId(documentSnapshot.Id);
                list.Add(data);
            }

            return list;
        }

        public async Task<object?> GetAsync(IBaseFirestoreData entity)
        {
            var docRef = _firestoreDb.Collection(this._CollectionName).Document(entity.Id);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var usr = snapshot.ConvertTo<T>();
                usr.SetDocumentId(snapshot.Id);
                return usr;
            }
            return null;
        }

        public async Task<T> AddAsync(T entity) 
        {
            var colRef = _firestoreDb.Collection(this._CollectionName);
            string id = entity.Id;
            if(this._AutoGenerateID)
            {
                var doc = await colRef.AddAsync(entity);
                id = doc.Id;
            }
            else
            {
                await colRef.Document(entity.Id).SetAsync(entity);
            }
            entity.SetDocumentId(id);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity) 
        {
            var recordRef = _firestoreDb.Collection(this._CollectionName).Document(entity.Id);
            await recordRef.SetAsync(entity, SetOptions.MergeAll);
            return entity;
        }

        public async Task DeleteAsync(IBaseFirestoreData entity) 
        {
            var recordRef = _firestoreDb.Collection(this._CollectionName).Document(entity.Id);
            await recordRef.DeleteAsync();
        }

        public CollectionReference GetReference()
        {
            return this._firestoreDb.Collection(this._CollectionName);
        }
        /// <summary>
        /// Queries the colelction
        /// </summary>
        /// <param name="query"></param>
        /// <see url="https://firebase.google.com/docs/firestore/query-data/queries#c"></see>
        /// <returns></returns>
        public async Task<List<T>> QueryRecordsAsync(Query query) 
        {
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) 
                    continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) 
                    continue;
                data.SetDocumentId(documentSnapshot.Id);
                list.Add(data);
            }

            return list;
        }

    }
}