using System;
using System.Collections.Generic;
using UnityEngine;

namespace UTIRLib.CustomTicker
{
    using Diagnostics;
    using UTIRLib.Diagnostics.Exceptions;
    using Object = UnityEngine.Object;

    public class TickerRegistry : MonoBehaviour, IInitializable<bool>
    {
        private Dictionary<Type, Ticker> registeredTickers;
        
        public static TickerRegistry Instance { get; private set; }
        public bool IsInitialized { get; private set; }

        public void Initialize(bool alsoRegisterAllTickableComponents)
        {
            Instance = this;
            RegisterTickers();
            if (alsoRegisterAllTickableComponents) {
                RegisterAllGameObjectComponents<MonoBehaviour>();
            }

            IsInitialized = true;
        }

        public static void SRegister<T>(T obj, Type tickerType) => Instance.Register(obj, tickerType);
        public static void SRegister<T, TTicker>(T obj)
            where TTicker : class => Instance.Register<T, TTicker>(obj);

        public static bool STryRegister<T>(T obj, Type tickerType) => Instance.TryRegister(obj, tickerType);
        public static bool STryRegister<T, TTicker>(T obj)
            where TTicker : class => Instance.TryRegister<T, TTicker>(obj);

        public static void SRegisterAllGameObjectComponents<T>()
            where T : Object => Instance.RegisterAllGameObjectComponents<T>();

        /// <remarks>
        /// <br/>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="TickableNotRegisteredMessage"/>
        /// </remarks>
        public void Register<T>(T obj, Type tickerType)
        {
            if (tickerType == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(tickerType)));
                return;
            }
            if (!TickableUtility.IsTickable(obj.GetType())) {
                new TickableNotRegisteredMessage(typeof(T), "Is not tickable.");
                return;
            }
            if (!registeredTickers.TryGetValue(tickerType, out Ticker ticker)) {
                new TickableNotRegisteredMessage(typeof(T), $"{tickerType.Name} ticker doesn't registered.");
                return;
            }

            ticker.Register(obj);
        }
        /// <remarks>
        /// <br/>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="TickableNotRegisteredMessage"/>
        /// </remarks>
        public void Register<T, TTicker>(T obj)
            where TTicker : class
        {
            if (!TickableUtility.IsTickable(obj.GetType())) {
                new TickableNotRegisteredMessage(typeof(T), "Is not tickable.");
                return;
            }
            if (!registeredTickers.TryGetValue(typeof(TTicker), out Ticker ticker)) {
                new TickableNotRegisteredMessage(typeof(T), $"{typeof(TTicker).Name} ticker doesn't registered.");
                return;
            }

            ticker.Register(obj);
        }

        public bool TryRegister<T>(T obj, Type tickerType)
        {
            if (tickerType == null || !TickableUtility.IsTickable(obj.GetType()) ||
                !registeredTickers.TryGetValue(tickerType, out Ticker ticker)) {
                return false;
            }

            ticker.Register(obj);
            return true;
        }
        public bool TryRegister<T, TTicker>(T obj)
        {
            if (!TickableUtility.IsTickable(obj.GetType()) || 
                !registeredTickers.TryGetValue(typeof(TTicker), out Ticker ticker)) {
                return false;
            }

            ticker.Register(obj);
            return true;
        }

        public void RegisterAllGameObjectComponents<T>()
            where T : Object
        {
            UnregisterAllTickables();
            T[] allGameObjectComponents = GetAllGameObjectComponents<T>();
            int allGameObjectComponentsCount = allGameObjectComponents.Length;
            for (int i = 0; i < allGameObjectComponentsCount; i++) {
                if (TickableUtility.TryGetCustomTickableAttribute(allGameObjectComponents[i].GetType(),
                    out CustomTickableAttribute tickableAttribute)) {
                    Register(allGameObjectComponents[i], tickableAttribute.TickerType);
                }
            }
        }

        private void UnregisterAllTickables()
        {
            foreach (var ticker in registeredTickers.Values) {
                ticker.UnregisterAll();
            }
        }

        private void RegisterTickers()
        {
            Ticker[] tickers = GetAllTickers();
            registeredTickers = new Dictionary<Type, Ticker>(tickers.Length);
            foreach (Ticker ticker in tickers) {
                registeredTickers.Add(ticker.GetType(), ticker);
            }
        }

        private static T[] GetAllGameObjectComponents<T>()
            where T : Object => FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        /// <exception cref="ObjectNotFoundException"></exception>
        private static Ticker[] GetAllTickers()
        {
            Ticker[] tickers = FindObjectsByType<Ticker>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (tickers == null || tickers.Length == 0) {
                throw new ObjectNotFoundException(nameof(tickers), typeof(Ticker));
            }

            return tickers;
        }
    }
}