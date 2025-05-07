using System;

#nullable enable
namespace UTIRLib
{
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public abstract class DisposableBase : IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
        protected bool isDisposed;

        public void Dispose() => Dispose(!isDisposed);

        protected abstract void Dispose(bool disposing);
    }
}
