using System;
using UnityEngine;
using Object = UnityEngine.Object;
using UTIRLib.Diagnostics;

namespace UTIRLib.CustomTicker
{
    public abstract class Ticker : MonoBehaviour, IInitializable,
        ITicker, IFixedTicker, ILateTicker, ITicker<float>, ILateTicker<float>
    {
        protected const int MAX_TICKS_PER_FRAME_COUNT = 500;
        protected const int DEFAULT_TICKABLES_ARRAY_CAPACITY = 16;
        protected const int TICKABLES_ARRAY_RESIZER_MULTIPLIER_DECREASE_THRESHOLD = 200;
        public const float MIN_TIME_SPEED  = 0.1f;
        public const float MAX_TIME_SPEED = 100f;

        protected int ticksPerFrameCount;
        [SerializeField] protected float timeSpeed = 1f;
        [SerializeField] protected float passedTimeValue;

        //protected List<ITickable> tickables;
        protected ITickable[] tickables;
        protected int tickablesCount;
        protected IFixedTickable[] fixedTickables;
        protected int fixedTickablesCount;
        protected ILateTickable[] lateTickables;
        protected int lateTickablesCount;

        protected ITickable<float>[] tickablesFloat;
        protected int tickablesFloatCount;
        protected ILateTickable<float>[] lateTickablesFloat;
        protected int lateTickablesFloatCount;

        public bool IsInitialized { get; protected set; }

        public virtual float TimeSpeed {
            get => timeSpeed;
            set => SetTimeSpeed(value);
        }
        public int TotalCount =>
            tickablesCount + fixedTickablesCount + lateTickablesCount + tickablesFloatCount + lateTickablesFloatCount;
        int ITicker.Count => tickablesCount;
        int IFixedTicker.Count => fixedTickablesCount;
        int ILateTicker.Count => lateTickablesCount;
        int ITicker<float>.Count => tickablesFloatCount;
        int ILateTicker<float>.Count => lateTickablesFloatCount;

        public virtual void Initialize()
        {
            if (transform.parent == null) {
                DontDestroyOnLoad(this);
            }

            tickables = new ITickable[DEFAULT_TICKABLES_ARRAY_CAPACITY];
            fixedTickables = new IFixedTickable[DEFAULT_TICKABLES_ARRAY_CAPACITY];
            lateTickables = new ILateTickable[DEFAULT_TICKABLES_ARRAY_CAPACITY];

            tickablesFloat = new ITickable<float>[DEFAULT_TICKABLES_ARRAY_CAPACITY];
            lateTickablesFloat = new ILateTickable<float>[DEFAULT_TICKABLES_ARRAY_CAPACITY];

            IsInitialized = true;
        }

        /// <remarks>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public virtual void Register<T>(T obj)
        {
            if (obj == null || obj is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return;
            }

            if (obj is ITickable tickable) {
                Register(tickable);
            }
            if (obj is IFixedTickable fixedTickable) {
                Register(fixedTickable);
            }
            if (obj is ILateTickable lateTickable) {
                Register(lateTickable);
            }
            if (obj is ITickable<float> tickableFloat) {
                Register(tickableFloat);
            }
            if (obj is ILateTickable<float> lateTickableFloat) {
                Register(lateTickableFloat);
            }
        }
        /// <remarks>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="TickableAlreadyRegisteredMessage"/>
        /// </remarks>
        public void Register(ITickable tickable)
        {
            if (tickable == null || tickable is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(tickable)));
                return;
            }
            if (ArrayContains(tickables, tickable)) {
                Debug.LogError(new TickableAlreadyRegisteredMessage(tickable));
            }

            if (tickables == null || tickablesCount == tickables.Length) {
                ArrayResize(ref tickables);
            }
            ArrayAddItem(ref tickables, tickable, ref tickablesCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="TickableAlreadyRegisteredMessage"/>
        /// </summary>
        public void Register(IFixedTickable fixedTickable)
        {
            if (fixedTickable == null || fixedTickable is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(fixedTickable)));
                return;
            }
            if (ArrayContains(fixedTickables, fixedTickable)) {
                Debug.LogError(new TickableAlreadyRegisteredMessage(fixedTickable));
            }

            if (fixedTickables == null || fixedTickablesCount == fixedTickables.Length) {
                ArrayResize(ref fixedTickables);
            }
            ArrayAddItem(ref fixedTickables, fixedTickable, ref fixedTickablesCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="TickableAlreadyRegisteredMessage"/>
        /// </summary>
        public void Register(ILateTickable lateTickable)
        {
            if (lateTickable == null || lateTickable is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(lateTickable)));
                return;
            }
            if (ArrayContains(lateTickables, lateTickable)) {
                Debug.LogError(new TickableAlreadyRegisteredMessage(lateTickable));
            }

            if (lateTickables == null || lateTickablesCount == lateTickables.Length) {
                ArrayResize(ref lateTickables);
            }
            ArrayAddItem(ref lateTickables, lateTickable, ref lateTickablesCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="TickableAlreadyRegisteredMessage"/>
        /// </summary>
        public void Register(ITickable<float> tickableFloat)
        {
            if (tickableFloat == null || tickableFloat is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(tickableFloat)));
                return;
            }
            if (ArrayContains(tickablesFloat, tickableFloat)) {
                Debug.LogError(new TickableAlreadyRegisteredMessage(tickableFloat));
            }

            if (tickablesFloat == null || tickablesFloatCount == tickablesFloat.Length) {
                ArrayResize(ref tickablesFloat);
            }
            ArrayAddItem(ref tickablesFloat, tickableFloat, ref tickablesFloatCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="TickableAlreadyRegisteredMessage"/>
        /// </summary>
        public void Register(ILateTickable<float> lateTickableFloat)
        {
            if (lateTickableFloat == null || lateTickableFloat is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(lateTickableFloat)));
                return;
            }
            if (ArrayContains(lateTickablesFloat, lateTickableFloat)) {
                Debug.LogError(new TickableAlreadyRegisteredMessage(lateTickableFloat));
            }

            if (lateTickablesCount == tickables.Length) {
                ArrayResize(ref lateTickablesFloat);
            }
            ArrayAddItem(ref lateTickablesFloat, lateTickableFloat, ref lateTickablesCount);
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </summary>
        public virtual void Unregister<T>(T obj)
        {
            if (obj == null || obj is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return;
            }

            if (obj is ITickable tickable) {
                Unregister(tickable);
            }
            if (obj is IFixedTickable fixedTickable) {
                Unregister(fixedTickable);
            }
            if (obj is ILateTickable lateTickable) {
                Unregister(lateTickable);
            }
            if (obj is ITickable<float> tickableFloat) {
                Unregister(tickableFloat);
            }
            if (obj is ILateTickable<float> lateTickableFloat) {
                Unregister(lateTickableFloat);
            }
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="EmptyCollectionMessage"/>
        /// </summary>
        public void Unregister(ITickable tickable)
        {
            if (tickable == null || tickable is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(tickable)));
                return;
            }
            if (!ArrayContains(tickables, tickable)) {
                Debug.LogWarning($"Wasn't found {tickable}({tickable.GetType().Name})");
                return;
            }
            if (tickablesCount == 0) {
                Debug.LogError(new NullOrEmptyCollectionMessage(tickables, nameof(tickables)));
                return;
            }

            ArrayRemoveItem(tickables, tickable, ref tickablesCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="EmptyCollectionMessage"/>
        /// </summary>
        public void Unregister(IFixedTickable fixedTickable)
        {
            if (fixedTickable == null || fixedTickable is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(fixedTickable)));
                return;
            }
            if (!ArrayContains(fixedTickables, fixedTickable)) {
                Debug.LogWarning($"Wasn't found {fixedTickable}({fixedTickable.GetType().Name})");
                return;
            }
            if (fixedTickablesCount == 0) {
                Debug.LogError(new NullOrEmptyCollectionMessage(fixedTickables, nameof(fixedTickables)));
                return;
            }

            ArrayRemoveItem(fixedTickables, fixedTickable, ref fixedTickablesCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="EmptyCollectionMessage"/>
        /// </summary>
        public void Unregister(ILateTickable lateTickable)
        {
            if (lateTickable == null || lateTickable is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(lateTickable)));
                return;
            }
            if (!ArrayContains(lateTickables, lateTickable)) {
                Debug.LogWarning($"Wasn't found {lateTickable}({lateTickable.GetType().Name})");
                return;
            }
            if (lateTickablesCount == 0) {
                Debug.LogError(new NullOrEmptyCollectionMessage(lateTickables, nameof(lateTickables)));
                return;
            }

            ArrayRemoveItem(lateTickables, lateTickable, ref lateTickablesCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="EmptyCollectionMessage"/>
        /// </summary>
        public void Unregister(ITickable<float> tickableFloat)
        {
            if (tickableFloat == null || tickableFloat is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(tickableFloat)));
                return;
            }
            if (!ArrayContains(tickablesFloat, tickableFloat)) {
                Debug.LogWarning($"Wasn't found {tickableFloat}({tickableFloat.GetType().Name})");
                return;
            }
            if (tickablesFloatCount == 0) {
                Debug.LogError(new NullOrEmptyCollectionMessage(tickablesFloat, nameof(tickablesFloat)));
                return;
            }

            ArrayRemoveItem(tickablesFloat, tickableFloat, ref tickablesFloatCount);
        }
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="EmptyCollectionMessage"/>
        /// </summary>
        public void Unregister(ILateTickable<float> lateTickableFloat)
        {
            if (lateTickableFloat == null || lateTickableFloat is Object unityObj && unityObj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(lateTickableFloat)));
                return;
            }
            if (!ArrayContains(lateTickablesFloat, lateTickableFloat)) {
                Debug.LogWarning($"Wasn't found {lateTickableFloat}({lateTickableFloat.GetType().Name})");
                return;
            }
            if (lateTickablesFloatCount == 0) {
                Debug.LogError(new NullOrEmptyCollectionMessage(lateTickablesFloat, nameof(lateTickablesFloat)));
                return;
            }

            ArrayRemoveItem(lateTickablesFloat, lateTickableFloat, ref lateTickablesFloatCount);
        }

        /// <returns>false if input object is null (include UnityNull)</returns>
        public bool Contains<T>(T obj)
        {
            if (obj is ITickable tickable) {
                return Contains(tickable);
            }
            else if (obj is IFixedTickable fixedTickable) {
                return Contains(fixedTickable);
            }
            else if (obj is ILateTickable lateTickable) {
                return Contains(lateTickable);
            }
            else if (obj is ITickable<float> tickableFloat) {
                return Contains(tickableFloat);
            }
            else if (obj is ILateTickable<float> lateTickableFloat) {
                return Contains(lateTickableFloat);
            }

            return false;
        }
        /// <returns>false if input object is null (include UnityNull)</returns>
        public bool Contains(ITickable tickable)
        {
            if (tickable == null || tickable is Object unityObj && unityObj == null) {
                return false;
            }

            return ArrayContains(tickables, tickable);
        }
        /// <returns>false if input object is null (include UnityNull)</returns>
        public bool Contains(IFixedTickable fixedTickable)
        {
            if (fixedTickable == null || fixedTickable is Object unityObj && unityObj == null) {
                return false;
            }

            return ArrayContains(fixedTickables, fixedTickable);
        }
        /// <returns>false if input object is null (include UnityNull)</returns>
        public bool Contains(ILateTickable lateTickable)
        {
            if (lateTickable == null || lateTickable is Object unityObj && unityObj == null) {
                return false;
            }

            return ArrayContains(lateTickables, lateTickable);
        }
        /// <returns>false if input object is null (include UnityNull)</returns>
        public bool Contains(ITickable<float> tickableFloat)
        {
            if (tickableFloat == null || tickableFloat is Object unityObj && unityObj == null) {
                return false;
            }

            return ArrayContains(tickablesFloat, tickableFloat);
        }
        /// <returns>false if input object is null (include UnityNull)</returns>
        public bool Contains(ILateTickable<float> lateTickableFloat)
        {
            if (lateTickableFloat == null || lateTickableFloat is Object unityObj && unityObj == null) {
                return false;
            }

            return ArrayContains(lateTickablesFloat, lateTickableFloat);
        }

        /// <summary>Deletes all tickables from all lists. For selective removal use ticker interfaces</summary>
        public void UnregisterAll()
        {
            ((ITicker)this).UnregisterAll();
            ((IFixedTicker)this).UnregisterAll();
            ((ILateTicker)this).UnregisterAll();
            ((ITicker<float>)this).UnregisterAll();
            ((ILateTicker<float>)this).UnregisterAll();
        }
        /// <summary>Only deletes interface tickables from list</summary>
        void ITicker.UnregisterAll()
        {
            if (tickables == null || tickablesCount == 0) {
                return;
            }

            tickablesCount = 0;
            Array.Fill(tickables, null);
        }
        /// <summary>Only deletes interface tickables from list</summary>
        void IFixedTicker.UnregisterAll()
        {
            if (fixedTickables == null || fixedTickablesCount == 0) {
                return;
            }

            fixedTickablesCount = 0;
            Array.Fill(fixedTickables, null);
        }
        /// <summary>Only deletes interface tickables from list</summary>
        void ILateTicker.UnregisterAll()
        {
            if (lateTickables == null || lateTickablesCount == 0) {
                return;
            }

            lateTickablesCount = 0;
            Array.Fill(lateTickables, null);
        }
        /// <summary>Only deletes interface tickables from list</summary>
        void ITicker<float>.UnregisterAll()
        {
            if (tickablesFloat == null || tickablesFloatCount == 0) {
                return;
            }

            tickablesFloatCount = 0;
            Array.Fill(tickablesFloat, null);
        }
        /// <summary>Only deletes interface tickables from list</summary>
        void ILateTicker<float>.UnregisterAll()
        {
            if (lateTickablesFloat == null || lateTickablesFloatCount == 0) {
                return;
            }

            lateTickablesFloatCount = 0;
            Array.Fill(lateTickablesFloat, null);
        }

        public virtual void ResetTimeSpeed()
        {
            timeSpeed = 1;
            passedTimeValue = 0;
        }

        public virtual void SetTimeSpeed(float value)
        {
            if (value > MAX_TIME_SPEED) {
                timeSpeed = MAX_TIME_SPEED;
            }
            else if (value < MIN_TIME_SPEED) {
                timeSpeed = MIN_TIME_SPEED;
            }
            else {
                timeSpeed = value;
            }
        }

        protected virtual void IncreaseTickCounter() => ticksPerFrameCount++;

        protected virtual void ResetTickCounter() => ticksPerFrameCount = 0;

        protected virtual void IncreasePassedTime() => passedTimeValue += Time.deltaTime * timeSpeed;

        protected virtual void ResetPassedTime() => passedTimeValue = 0;

        protected virtual bool IsEndlessTick() =>
            ticksPerFrameCount >= MAX_TICKS_PER_FRAME_COUNT;

        protected virtual bool TickAllowed()
        {
            if (passedTimeValue >= Time.deltaTime) {
                return true;
            }

            return false;
        }

        protected void BeginTick()
        {
            ResetTickCounter();
            IncreasePassedTime();
        }
        protected void EndTick()
        {
            ResetPassedTime();
            IncreaseTickCounter();
        }

        /// <summary>
        /// Implement this method to realize Update functionality and put to same unity message
        /// </summary>
        protected virtual void Tick() { }

        /// <summary>
        /// Implement this method to realize FixedUpdate functionality and put to same unity message
        /// </summary>
        protected virtual void FixedTick() { }

        /// <summary>
        /// Implement this method to realize LateUpdate functionality and put to same unity message
        /// </summary>
        protected virtual void LateTick() { }

        protected static void ArrayAddItem<T>(ref T[] array, T value, ref int arrayItemsCount)
        {
            if (array == null || array.Length == 0 || arrayItemsCount == array.Length) {
                ArrayResize(ref array);
            }

            array[arrayItemsCount] = value;
            arrayItemsCount++;
        }

        protected static void ArrayRemoveItem<T>(T[] array, T value, ref int arrayItemsCount)
        {
            int foundValueIndex = Array.IndexOf(array, value);
            if (foundValueIndex >= 0) {
                if (arrayItemsCount == 1) {
                    array[foundValueIndex] = default;
                }
                else {
                    array[foundValueIndex] = array[^1];
                    array[^1] = default;
                }
                arrayItemsCount--;
                return;
            }

            Debug.LogError($"Wasn't found array item {value}({value.GetType().Name})");
        }

        protected static bool ArrayContains<T>(T[] array, T value)
        {
            if (array == null || array.Length == 0 || value == null) {
                return false;
            }

            return Array.IndexOf(array, value) >= 0;
        }

        protected static void ArrayResize<T>(ref T[] array)
        {
            if (array == null || array.Length == 0) {
                array = new T[DEFAULT_TICKABLES_ARRAY_CAPACITY];
                return;
            }

            if (array.Length >= DEFAULT_TICKABLES_ARRAY_CAPACITY) {
                Array.Resize(ref array, (int)(array.Length * 1.5f));
            }
            else {
                Array.Resize(ref array, array.Length * 2);
            }
        }
    }
}