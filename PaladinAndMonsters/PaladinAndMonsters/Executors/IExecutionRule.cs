using PaladinAndMonsters.Characters;

namespace PaladinAndMonsters.Executors
{
    public interface IExecutionRule<T, U> where T :Character where U : Character
    {
        void ExecuteFor(T character1, U character2);
    }
}