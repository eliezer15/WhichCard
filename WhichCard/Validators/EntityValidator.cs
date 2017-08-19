using System.Threading.Tasks;
using FluentValidation;
namespace WhichCard.Validators
{
    public class EntityValidator<T> : AbstractValidator<T>, IEntityValidator<T>
    {
        public Task ValidateAndThrowAsync(T entity) => this.ValidateAndThrowAsync(entity, null);
    }
}
