using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Umbrella.Infrastructure.Firestore.Abstractions
{
    /// <summary>
    /// Represents the behaviour ofr a repository of a given data type / collection
    /// </summary>
    public interface IFirestoreDataRepository<T> where T : IBaseFirestoreData
    {
        /// <summary>
        /// Gets the entire collection
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// Gets the referenced document 
        /// </summary>
        /// <param name="entity">enetity to retreive</param>
        /// <returns></returns>
        Task<object?> GetAsync(IBaseFirestoreData entity);
        /// <summary>
        /// Creates a new document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Updates the target document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// Deletes the target document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(IBaseFirestoreData entity);
        /// <summary>
        /// Gets the reference to the target collection
        /// </summary>
        /// <returns></returns>
        CollectionReference GetReference();

        /// <summary>
        /// Queries the colelction
        /// </summary>
        /// <param name="query"></param>
        /// <see url="https://firebase.google.com/docs/firestore/query-data/queries#c"></see>
        /// <returns></returns>
        Task<List<T>> QueryRecordsAsync(Query query);
    }
}