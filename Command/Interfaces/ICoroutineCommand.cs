using System.Collections;

namespace UTIRLib.CommandSystem
{
#nullable enable
    public interface ICoroutineCommand : ICommand
    {
        bool IsCompleted { get; }

        bool IsReadyToExecute();
    }
}
