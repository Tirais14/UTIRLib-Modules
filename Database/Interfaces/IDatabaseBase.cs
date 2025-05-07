using UnityEngine;

namespace UTIRLib.Database
{
    public interface IDatabaseBase
    {
        int Count { get; }
        bool IsLoaded { get; }
    }
}
