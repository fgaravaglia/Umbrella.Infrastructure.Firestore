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
    public class FirestoreKeyValuePair : FirestoreDocument
    {
        
        [FirestoreProperty]
        public string Value { get; set; }

        public double DoubleValue
        {
            get
            {
                double output;
                if (Double.TryParse(this.Value, out output))
                    return output;
                return 0.0;
            }
        }

        /// <summary>
        /// Empty COnstructor
        /// </summary>
        public FirestoreKeyValuePair() : base()
        {
            this.Value = "";
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public FirestoreKeyValuePair(string id, string value) : this()
        {
            SetDocumentId(id);
            this.Value = value;
        }
        /// <summary>
        /// Sets the ID
        /// </summary>
        /// <param name="id"></param>
        public override void SetDocumentId(string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            this.Id = id;
            this.Name = id;
        }
    }
}