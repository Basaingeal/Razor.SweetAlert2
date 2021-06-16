using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    /// <summary>
    ///     A bound event handler delegate.
    /// </summary>
    public class SweetAlertCallback
    {
        private readonly Func<Task> _asyncCallback;
        private readonly EventCallback _eventCallback;
        private readonly Action _syncCallback;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SweetAlertCallback" /> class.
        ///     Creates a <see cref="SweetAlertCallback" /> for the provided <paramref name="receiver" /> and
        ///     <paramref name="callback" />.
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public SweetAlertCallback(Action callback, ComponentBase receiver = null)
        {
            _syncCallback = callback;
            if (receiver != null) _eventCallback = EventCallback.Factory.Create(receiver, () => { });
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SweetAlertCallback" /> class.
        ///     Creates a <see cref="SweetAlertCallback" /> for the provided <paramref name="receiver" /> and
        ///     <paramref name="callback" />.
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public SweetAlertCallback(Func<Task> callback, ComponentBase receiver = null)
        {
            _asyncCallback = callback;
            if (receiver != null) _eventCallback = EventCallback.Factory.Create(receiver, () => { });
        }

        /// <summary>
        ///     Invokes the delegate associated with this binding and dispatches an event notification to the appropriate
        ///     component.
        /// </summary>
        public async Task InvokeAsync()
        {
            if (_asyncCallback != null)
                await _asyncCallback().ConfigureAwait(true);
            else
                _syncCallback();

            await _eventCallback.InvokeAsync(null).ConfigureAwait(true);
        }
    }
}