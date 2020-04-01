using Instagroceries.Data.Interfaces;
using Instagroceries.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagroceries.Data.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
       public IEnumerable<Category> Categories { 
            get
            {
                return new List<Category>
                     {
                         new Category { CategoryName = "Bakery & Dairy", Description = "All bakery and dairy products" },
                         new Category { CategoryName = "Produce", Description = "All fresh farm produces" },
                         new Category { CategoryName = "Refreshments", Description = "All refreshment drinks" },
                         new Category { CategoryName = "Fruits & Vegetables", Description = "All fruits and vegetables" },
                         new Category { CategoryName = "Spices", Description = "All spices" }
                         
                     };
            }
        }
    }
}
