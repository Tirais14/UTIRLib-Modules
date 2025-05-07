#nullable enable
namespace UTIRLib.Database
{
    public interface IDatabaseFileInfo
    {
        string Path { get; }
        string Name { get; }
        bool IsLoadOnInitialize { get; }
    }
}
