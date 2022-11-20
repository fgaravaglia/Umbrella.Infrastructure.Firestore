using System;

namespace Umbrella.Infrastructure.Firestore.Abstractions
{
    /// <summary>
    /// Represents the base data that will exists on all records.
    /// </summary>
    public interface IBaseFirestoreData 
    {
        /// <summary>
        /// Gets the Id on document
        /// </summary>
         string Id { get; }

        /// <summary>
        /// Gets the name on document
        /// </summary>
         string Name { get; set; }
        /// <summary>
        /// Timestamp for creation of document
        /// </summary>
        /// <value></value>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// Timestamp for update of document
        /// </summary>
        /// <value></value>
        DateTime? LastUpdatedOn { get; set; }
        /// <summary>
        /// Sets the document id on firestore
        /// </summary>
        /// <param name="id"></param>
        void SetDocumentId(string id);
    }

    public class FirestoreDataReference : IBaseFirestoreData
    {
        public string Id {get; private set;}

        public string Name {get; set;}

        public DateTime CreatedOn { get; set; }
        
        public DateTime? LastUpdatedOn { get; set; }

        public FirestoreDataReference(string id)
        {
            if(String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            this.Id = id;
            this.Name = id;    
            this.CreatedOn = DateTime.Now;      
        }

        public void SetDocumentId(string id)
        {
            this.Id = id;
            this.Name = id;
        }

        public static IBaseFirestoreData AsBaseFirestoreData(string id)
        {
            return new FirestoreDataReference(id);
        }
    }
}