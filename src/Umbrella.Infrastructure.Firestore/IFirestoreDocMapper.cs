namespace Umbrella.Infrastructure.Firestore
{
    /// <summary>
    /// Abstraction for mapper from Firestore document to a DTO class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Tdoc"></typeparam>
    public interface IFirestoreDocMapper<T, Tdoc> where Tdoc : IBaseFirestoreData
    {
        /// <summary>
        /// Maps the origina DTO object into FirestoreDocument
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Tdoc ToFirestoreDocument(T dto);
        /// <summary>
        /// Rebuilds the original doc from firestore persistence
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        T FromFirestoreDoc(Tdoc doc);
    }
}