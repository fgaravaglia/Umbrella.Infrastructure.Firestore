using System.Collections.Generic;

namespace Umbrella.Infrastructure.Firestore
{
    /// <summary>
    /// Abstraction to Define generic behavior for all repsority stuff. Usefull for external assemblies
    /// </summary>
    /// <typeparam name="T">entity type to sstore</typeparam>
    public interface IModelEntityRepository<T>
    {
        /// <summary>
        /// Gets the complete list of objetcs
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// get by Id
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        T GetById(string keyValue);
        /// <summary>
        /// saves it
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>the Key of the object</returns>
        string Save(T dto);
        /// <summary>
        /// Deletes all objects and persist the input lists
        /// </summary>
        /// <param name="dtos"></param>
        void SaveAll(IEnumerable<T> dtos);
    }
}