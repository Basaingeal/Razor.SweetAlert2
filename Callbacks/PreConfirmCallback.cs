using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    /// <summary>
    ///     A bound event handler delegate.
    /// </summary>
    public class PreConfirmCallback
    {
        private readonly Func<string, Task<string>> _asyncCallback;
        private readonly EventCallback _eventCallback;
        private readonly Func<string, string> _syncCallback;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PreConfirmCallback" /> class.
        ///     Creates a <see cref="PreConfirmCallback" /> for the provided <paramref name="receiver" /> and
        ///     <paramref name="callback" />.
        ///     <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public PreConfirmCallback(Func<string, Task<string>> callback, ComponentBase receiver = null)
        {
            _asyncCallback = callback;
            if (receiver != null) _eventCallback = EventCallback.Factory.Create(receiver, () => { });
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PreConfirmCallback" /> class.
        ///     Creates a <see cref="PreConfirmCallback" /> for the provided <paramref name="receiver" /> and
        ///     <paramref name="callback" />.
        ///     <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public PreConfirmCallback(Func<Task<string>> callback, ComponentBase receiver = null)
        {
            _asyncCallback = _ => callback();
            if (receiver != null) _eventCallback = EventCallback.Factory.Create(receiver, () => { });
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PreConfirmCallback" /> class.
        ///     Creates a <see cref="PreConfirmCallback" /> for the provided <paramref name="receiver" /> and
        ///     <paramref name="callback" />.
        ///     <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public PreConfirmCallback(Func<string, string> callback, ComponentBase receiver = null)
        {
            _syncCallback = callback;
            if (receiver != null) _eventCallback = EventCallback.Factory.Create(receiver, () => { });
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PreConfirmCallback" /> class.
        ///     Creates a <see cref="PreConfirmCallback" /> for the provided <paramref name="receiver" /> and
        ///     <paramref name="callback" />.
        ///     <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public PreConfirmCallback(Func<string> callback, ComponentBase receiver = null)
        {
            _syncCallback = _ => callback();
            if (receiver != null) _eventCallback = EventCallback.Factory.Create(receiver, () => { });
        }

        /// <summary>
        ///     Invokes the delegate associated with this binding and dispatches an event notification to the appropriate
        ///     component.
        /// </summary>
        /// <param name="arg">The argument.</param>
        public async Task<string> InvokeAsync(string arg)
        {
            string ret;
            if (_asyncCallback != null)
                ret = await _asyncCallback(arg).ConfigureAwait(true);
            else
                ret = _syncCallback(arg);

            await _eventCallback.InvokeAsync(arg).ConfigureAwait(true);

            return ret;
        }
    }
}