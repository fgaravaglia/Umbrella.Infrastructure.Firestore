using System;
using System.Globalization;
using Google.Cloud.Firestore;

namespace Umbrella.Infrastructure.Firestore
{
    [FirestoreData]
    public class FirestoreKeyValuePair
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Key{get; set;}

        [FirestoreProperty]
        public string Value {get; set;}

        public double DoubleValue { get => Double.Parse(this.Value, CultureInfo.InvariantCulture); }

        public FirestoreKeyValuePair()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Key = "";
            this.Value = "";
        }

        public FirestoreKeyValuePair(string key, string value) : this()
        {
            if(String.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            this.Key = key;
            this.Value = value;
        }

        public FirestoreKeyValuePair(string key, double value) : this()
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            this.Key = key;
            this.Value = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}