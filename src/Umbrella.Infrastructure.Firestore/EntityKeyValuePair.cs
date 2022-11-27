using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Infrastructure.Firestore
{
    /// <summary>
    /// CLass to map a simple Key Value Pair
    /// </summary>
    public class EntityKeyValuePair
    {
        /// <summary>
        /// Key
        /// </summary>
        /// <value></value>
        public string Id { get; private set; }
        /// <summary>
        /// Creation date
        /// </summary>
        /// <value></value>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Last update date
        /// </summary>
        /// <value></value>
        public DateTime? LastUpdatedOn { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        /// <value></value>
        public string Value { get; set; }


        /// <summary>
        /// Empty Cosntructor
        /// </summary>
        public EntityKeyValuePair()
        {
            this.Id = "";
            this.CreatedOn = DateTime.Now;
            this.LastUpdatedOn = null;
            this.Value = "";
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EntityKeyValuePair(string id, string value) : this()
        {
            this.Id = id;
            this.Value = value;
        }
    }
}