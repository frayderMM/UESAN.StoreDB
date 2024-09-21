using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.StoreDB.DOMAN.Infrastucture.Data;
using UESAN.StoreDB.DOMAN.Infrastucture.Data;


namespace UESAN.StoreDB.DOMAN.Infrastucture.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDbContext _dbContext;
        public CategoryRepository(StoreDbContext _dbContext)
        {
            _dbContext = _dbContext;


        }
        //Método Sincrono
        // List o IEnumerable --> difere3ncia que no se va poder modificar
        //public IEnumerable<Category> GetCategories()
        //{
        //    var categories = _dbContext.Category.ToList();
        //    return categories;
        //}


        //Método Asincrono
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _dbContext.Category.ToListAsync();
            return categories;
        }

        //Get Category by ID
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _dbContext
                .Category
                .Where(c => c.Id == id && c.IsActive == true)
                .FirstOrDefaultAsync();
                return category;
        }

        //Create category 
        public async Task<int> Insert(Category category)
        {
            await _dbContext.Category.AddAsync(category);//Lo mas importante ---> es donde se inserta la categoria
            int rows = await _dbContext.SaveChangesAsync();//confima la operacion, como completar la insertcion
            
            return (rows>0) ? category.Id : -1; //Operador ternario

        }

        //Actualizar 
        public async Task<Boolean> Actualizar(Category category)
        {
            _dbContext.Category.Update(category); // directamente no hay un async 
            int rowsm= await _dbContext.SaveChangesAsync(); // XD
            return rowsm > 0;
        }

        //Delete Category
        public async Task<Boolean> Delete(int id)
        {
            var category = await _dbContext
                .Category
                .FirstOrDefaultAsync(c => c.Id == id);
            if(category == null) return false;

            category.IsActive = false;
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;


           // _dbContext.Category.Remove(category);


        }
    }
}

