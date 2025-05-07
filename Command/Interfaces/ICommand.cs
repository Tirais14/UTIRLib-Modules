namespace UTIRLib.CommandSystem
{
#nullable enable
    public interface ICommand
    {
        void Execute();

        void Undo();
    }
}
