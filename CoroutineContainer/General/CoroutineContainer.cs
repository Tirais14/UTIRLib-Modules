using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTIRLib.Diagnostics;

namespace UTIRLib.CororoutineContainer
{
    public class CoroutineContainer : MonoBehaviour, ICoroutineContainer
    {
        private readonly Dictionary<int, Coroutine> coroutines = new();

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </summary>
        public void StartNewCoroutine(IEnumerator routine, int callObjectHash)
        {
            if (routine == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(routine)));
                return;
            }

            if (coroutines.ContainsKey(callObjectHash)) {
                Debug.LogError($"Coroutine {routine} with hash {callObjectHash} already started.");
                return;
            }

            Coroutine coroutine = StartCoroutine(routine);
            coroutines.Add(callObjectHash, coroutine);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </summary>
        public void StartNewCoroutine<T>(IEnumerator routine, T callObject)
        {
            if (routine == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(routine)));
                return;
            }
            if (callObject == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(callObject)));
                return;
            }

            int callObjectHash = callObject.GetHashCode();
            if (coroutines.ContainsKey(callObjectHash)) {
                Debug.LogError($"Coroutine {routine} with hash {callObjectHash} already started.");
                return;
            }

            Coroutine coroutine = StartCoroutine(routine);
            coroutines.Add(callObjectHash, coroutine);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </summary>
        public void StartNewCoroutine(string methodName, object callObject, int callObjectHash)
        {
            if (callObject == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(callObject)));
                return;
            }

            if (coroutines.ContainsKey(callObjectHash)) {
                Debug.LogError($"Coroutine {methodName} with hash {callObjectHash} already started.");
                return;
            }

            Coroutine coroutine = StartCoroutine(methodName, callObject);
            coroutines.Add(callObjectHash, coroutine);
        }

        public void StopCoroutineBy(int callObjectHash)
        {
            if (coroutines.TryGetValue(callObjectHash, out Coroutine coroutine)) {
                StopCoroutine(coroutine);
            }
            else {
                Debug.LogError($"{nameof(Coroutine)} with hash {callObjectHash} doesn't exist");
                return;
            }
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </summary>
        public void StopCoroutineBy<T>(T callObject)
        {
            if (callObject == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(callObject)));
                return;
            }

            if (coroutines.TryGetValue(callObject.GetHashCode(), out Coroutine coroutine)) {
                StopCoroutine(coroutine);
            }
            else {
                Debug.LogError($"{nameof(Coroutine)} with hash {callObject.GetHashCode()} doesn't exist");
                return;
            }
        }
    }
}