using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Model
{
    /// <summary>
    /// Domain model that indicates expense information.
    /// </summary>
    public class ExpenseInfo
    {
        private readonly List<ExpenseProperty> properties = new List<ExpenseProperty>();

        /// <summary>
        /// Add an expense property to expense info 
        /// </summary>
        /// <param name="property">expense property</param>
        public void AddProperty(ExpenseProperty property) =>
            properties.Add(property); 

        /// <summary>
        /// Check whether expense info has some specific property.
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Result</returns>
        public bool HasProperty(string name) =>
            properties.Find(p => p.Name == name) == null ? false : true;

        /// <summary>
        /// Get expense property by property name
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>expense property</returns>
        public ExpenseProperty GetProperty(string name) =>
            properties.Find(p => p.Name == name); 

        /// <summary>
        /// All properties.
        /// </summary>
        public IEnumerable<ExpenseProperty> Properties
        {
            get => properties; 
        }

        /// <summary>
        /// Count of properties
        /// </summary>
        public int PropertyCount
        {
            get => properties.Count; 
        }
    }
}
