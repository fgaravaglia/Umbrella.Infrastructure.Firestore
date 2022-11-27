using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Umbrella.Infrastructure.Firestore.Abstractions;
using Umbrella.Infrastructure.Firestore.Extensions;

namespace Umbrella.Infrastructure.Firestore.Abstractions
{
    [FirestoreData]
    public abstract class FirestoreDocument : IBaseFirestoreData
    {
        [FirestoreDocumentId]
        public string Id { get; protected set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public DateTime CreatedOn { get; set; }

        [FirestoreProperty]
        public DateTime? LastUpdatedOn { get; set; }

        /// <summary>
        /// Empty COnstructor
        /// </summary>
        public FirestoreDocument()
        {
            this.Id = "";
            this.Name = "";
            this.CreatedOn = DateTime.Now.ToFirestoreTimeStamp();
            this.LastUpdatedOn = null;
        }
       
        /// <summary>
        /// Sets the ID
        /// </summary>
        /// <param name="id"></param>
        public virtual void SetDocumentId(string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            this.Id = id;
        }
    }
}