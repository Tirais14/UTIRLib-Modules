using UnityEngine;

namespace UTIRLib.CommandSystem
{
#nullable enable
    public interface ICommandReciever
    {
        void AddCommand(ICommand command);
    }

    public interface ICommandReciever<in T>
        where T : ICommand
    {
        void AddCommand(T command);
    }
}
