using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Grocery.Core.Data.Repositories
{
    public class GroceryListItemsRepository : IGroceryListItemsRepository
    {
        private readonly AppDbContext _context;

        public GroceryListItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<GroceryListItem> GetAll()
        {
            return _context.GroceryListItems
                .Include(x => x.Product)
                .Include(x => x.GroceryList)
                .ToList();
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            return _context.GroceryListItems
                .Where(x => x.GroceryListId == groceryListId)
                .Include(x => x.Product)
                .ToList();
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            _context.GroceryListItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            _context.GroceryListItems.Remove(item);
            _context.SaveChanges();
            return item;
        }

        public GroceryListItem? Get(int id)
        {
            return _context.GroceryListItems
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            _context.GroceryListItems.Update(item);
            _context.SaveChanges();
            return item;
        }
    }
}