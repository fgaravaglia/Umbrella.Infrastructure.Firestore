using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Umbrella.Infrastructure.Firestore
{
    /// <summary>
    /// SImple impelementation of a repository for a collection on Firestore. It can be overrided if needed
    /// </summary>
    /// <typeparam name="T">DTO that map the colletion in your application</typeparam>
    /// <typeparam name="Tdoc">Document representation of DTO on Firestore</typeparam>
    public class ModelEntityRepository<T, Tdoc> : IModelEntityRepository<T>
        where Tdoc : IBaseFirestoreData
    {
        protected readonly ILogger _Logger;
        protected readonly BaseRepository<Tdoc> _Repo;
        protected readonly IFirestoreDocMapper<T, Tdoc> _Mapper;

        /// <summary>
        /// Default COnstructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="projectId">GCP project Id where firestore DB has been provisioned</param>
        /// <param name="dotnetEnv">Code of environment</param>
        /// <param name="autoGenerateId">TRUE to generate id of object; FALSE to use the id set uoside the repo</param>
        /// <param name="collectionName">name of collection to save data on FIrestore instance</param>
        /// <param name="mapper">mapper to translate firestore document to DTO and viceversa</param>
        protected ModelEntityRepository(ILogger logger, string projectId, string dotnetEnv, bool autoGenerateId, string collectionName, IFirestoreDocMapper<T, Tdoc> mapper)
        {
            if (String.IsNullOrEmpty(dotnetEnv))
                throw new ArgumentNullException(nameof(dotnetEnv));
            if (String.IsNullOrEmpty(projectId))
                throw new ArgumentNullException(nameof(projectId));
            if (String.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException(nameof(collectionName));

            this._Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            // fill variable
            var variableValue = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            if(String.IsNullOrEmpty(variableValue))
            {
                throw new InvalidOperationException($"MIssing Environment Variable: GOOGLE_APPLICATION_CREDENTIALS");
            }
            
            this._Logger.LogDebug($"{this.GetType()} : Instance Firestore db...");
            this._Repo = new BaseRepository<Tdoc>(projectId, collectionName, autoGenerateId);
        }

        public virtual IEnumerable<T> GetAll()
        {
            var docs = this._Repo.GetAllAsync().Result as List<Tdoc>;
            return docs.Select(x => this._Mapper.FromFirestoreDoc(x)).ToList();
        }

        public virtual T GetById(string keyValue)
        {
            var doc = this._Repo.GetAsync(FirestoreDataReference.AsBaseFirestoreData(keyValue)).Result;
            if(doc != null)
                return this._Mapper.FromFirestoreDoc((Tdoc)doc);
            else 
                return default(T);
        }

        public virtual string Save(T dto)
        {
            if(dto == null)
                throw new ArgumentNullException(nameof(dto));

            this._Logger.LogDebug($"Converting to Doc the incoming dto {dto.GetType()}");
            var matchDoc = this._Mapper.ToFirestoreDocument(dto);

            this._Logger.LogDebug("Reading Doc " + matchDoc.Id);
            var existing = this._Repo.GetAsync(matchDoc).Result;
            if (existing != null)
                matchDoc = this._Repo.UpdateAsync(matchDoc).Result;
            else
                matchDoc = this._Repo.AddAsync(matchDoc).Result;
            this._Logger.LogDebug($"Document {matchDoc.Id} of type {typeof(T).FullName} succesfully persisted on Firestore");
            return matchDoc.Id;
        }

        public virtual void SaveAll(IEnumerable<T> dtos)
        {
            dtos.ToList().ForEach(m => this.Save(m));
            this._Logger.LogInformation($"Sucesfully persisted {dtos.Count()} Documents on Firestore");

            var existingList = (List<Tdoc>)this._Repo.GetAllAsync().Result;
            var upToDateList = dtos.Select(x => this._Mapper.ToFirestoreDocument(x)).ToList();
            int deleteCounter = 0;
            foreach (var doc in existingList)
            {
                if (!upToDateList.Any(x => x.Id == doc.Id))
                {
                    this._Logger.LogDebug($"Found Obsolete Document: {doc.Id} will be deleted from Firestore");
                    this._Repo.DeleteAsync(doc).Wait();
                    deleteCounter++;
                }
            }
            this._Logger.LogInformation($"Deleted {deleteCounter} Documents from Firestore");
        }

    }
}