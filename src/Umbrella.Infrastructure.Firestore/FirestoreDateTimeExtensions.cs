using System;

namespace Umbrella.Infrastructure.Firestore
{ 
    /// <summary>
    /// Extensions for datetime Management
    /// </summary>
    public static class FirestoreDateTimeExtensions
    {
        /// <summary>
        /// Gets the Datetime from timestamp value stores on FIrestore
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToFirestoreTimeStamp(this DateTime timestamp)
        {
            return new DateTime(timestamp.Ticks, DateTimeKind.Utc);
        }
        /// <summary>
        /// Gets the Datetime from timestamp value stores on FIrestore
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime? ToFirestoreNullableTimeStamp(this DateTime? timestamp)
        {
            if(timestamp.HasValue)
                return timestamp.Value.ToFirestoreTimeStamp();
            return null;
        }

    }
}
   