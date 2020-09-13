using System.Threading.Tasks;

namespace Location.Domain
{
    public interface ICommandService<in TCommand, TResult>
    {
        public Task<TResult> Execute(TCommand command);
    }
}