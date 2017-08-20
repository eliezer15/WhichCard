using System;
using WhichCard.Entities;

namespace WhichCard.Repositories
{
    public interface ICategoryRepository
    {
        void InsertAsync(Category category);

        void Delete(string id);

        Category Get(string id);

        Category GetAll(string ids);
    }
}
