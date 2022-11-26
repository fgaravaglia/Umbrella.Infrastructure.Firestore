using System;
using System.Diagnostics.CodeAnalysis;

namespace Umbrella.Infrastructure.Firestore.Extensions
{
    /// <summary>
    /// Extensions for datetime Management
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class FirestoreDateTimeExtensions
    {
        /// <summary>
        /// Gets the Datetime from timestamp value stores on Firestore
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
   