using System.Collections;

namespace UTIRLib.CororoutineContainer
{
    public interface ICoroutineContainer
    {
        public void StartNewCoroutine(IEnumerator routine, int callObjectHash);
        public void StartNewCoroutine<T>(IEnumerator routine, T callObject);
        public void StartNewCoroutine(string methodName, object callObject, int callObjectHash);

        public void StopCoroutineBy(int callObjectHash);
        public void StopCoroutineBy<T>(T callObject);
    }
}