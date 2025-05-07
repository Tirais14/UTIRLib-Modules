namespace UTIRLib.UI
{
    public interface IClosable
    {
        bool Opened { get; }

        void Open();

        void Close();
    }
}
