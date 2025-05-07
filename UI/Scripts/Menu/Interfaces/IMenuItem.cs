#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    public interface IMenuItem
    {
        bool IsEnabled { get; }

        void Show();

        void Hide();

        bool IsSupportedObject(object? obj);
    }
}
