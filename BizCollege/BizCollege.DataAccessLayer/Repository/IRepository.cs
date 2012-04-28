using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizCollege.DataAccessLayer.Repository
{
    public interface IRepository<T,K>
    {
        /// <summary>
        /// Retrieves the item from the database
        /// </summary>
        /// <param name="itemId">The unique id of the item</param>
        /// <returns>The item if found, null otherwise</returns>
        T Get(K itemId);

        /// <summary>
        /// If the item is new, add it to the database.  If the item exists,
        /// merge the changes in and update the item.
        /// </summary>
        /// <param name="item">the unique id of the item</param>
        /// <returns>The copy of the item that was persisted</returns>
        T AddOrUpdate(T item);

        /// <summary>
        /// Removes the item from the database
        /// </summary>
        /// <param name="itemId">The unique id of the item</param>
        void Remove(K itemId);

        /// <summary>
        /// Returns all items from the database
        /// </summary>
        /// <returns>A collection of all items, empty collection if no items exist</returns>
        ICollection<T> GetAllItems();
    }
}
