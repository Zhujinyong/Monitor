
namespace Centa.Monitor.Infrastructure.General.Interfaces
{
    public interface IEnumBase<TE, T> where TE : IEnumBase<TE, T>
    {
        T Value { get; }
        string Name { get; }
    }
}