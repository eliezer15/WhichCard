using System;
using WhichCard.Entities;

namespace WhichCard.Repositories
{
    public interface ICategoryRepository
    {
        void Insert(Category category);

        void Delete(string id);

        void Get(string id);

        void GetAll();
    }
}
