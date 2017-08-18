using System;
using WhichCard.Entities;

namespace WhichCard.Repositories
{
    public interface IUserRepository
    {
        void Insert(User user);

        void Delete(string id);

        void Get(string id);
    }
}
