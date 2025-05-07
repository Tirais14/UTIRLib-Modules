using System;
using System.Collections;
using UnityEngine;

namespace UTIRLib.CommandSystem
{
#nullable enable
    public sealed class CoroutineCommand<T> : ICoroutineCommand
    {
        private readonly Func<T, bool> predicate;
        private readonly Action action;
        private readonly T predicateArg;

        public bool IsCompleted { get; private set; } 

        public CoroutineCommand(Func<T, bool> predicate, T predicateArg, Action action)
        {
            this.predicate = predicate; 
            this.action = action;
            this.predicateArg = predicateArg;
        }

        public void Execute()
        {
            if (IsCompleted) {
                return;
            }

            action();
            IsCompleted = true;
        }
        public bool IsReadyToExecute() => predicate(predicateArg);
        public void Undo() => IsCompleted = true;
    }
}
