using System.Threading.Tasks;
namespace WhichCard.Validators
{
    public interface IEntityValidator<T>
    {
        Task ValidateAndThrowAsync(T entity);
    }
}
