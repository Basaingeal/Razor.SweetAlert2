namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// A bound event handler delagate.
    /// </summary>
    public class PreConfirmCallback
    {
        private readonly Func<string, Task<string>> asyncCallback;
        private readonly Func<string, string> syncCallback;
        private readonly Func<IEnumerable<string>, Task<IEnumerable<string>>> asyncQueueCallback;
        private readonly Func<IEnumerable<string>, IEnumerable<string>> syncQueueCallback;
        private readonly EventCallback eventCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Queue request.</exception>
        public PreConfirmCallback(Func<string, Task<string>> callback, ComponentBase receiver = null)
        {
            this.asyncCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Queue request.</exception>
        public PreConfirmCallback(Func<Task<string>> callback, ComponentBase receiver = null)
        {
            this.asyncCallback = (string _) => { return callback(); };
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Queue request.</exception>
        public PreConfirmCallback(Func<string, string> callback, ComponentBase receiver = null)
        {
            this.syncCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Fire requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Queue request.</exception>
        public PreConfirmCallback(Func<string> callback, ComponentBase receiver = null)
        {
            this.syncCallback = (string _) => { return callback(); };
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Queue requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Fire request.</exception>
        public PreConfirmCallback(Func<IEnumerable<string>, Task<IEnumerable<string>>> callback, ComponentBase receiver = null)
        {
            this.asyncQueueCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Queue requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Fire request.</exception>
        public PreConfirmCallback(Func<Task<IEnumerable<string>>> callback, ComponentBase receiver = null)
        {
            this.asyncQueueCallback = (IEnumerable<string> _) => { return callback(); };
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Queue requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Fire request.</exception>
        public PreConfirmCallback(Func<IEnumerable<string>, IEnumerable<string>> callback, ComponentBase receiver = null)
        {
            this.syncQueueCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConfirmCallback"/> class.
        /// Creates a <see cref="PreConfirmCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// <para>Use in Queue requests.</para>
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        /// <exception cref="ArgumentException">Thrown if used in Fire request.</exception>
        public PreConfirmCallback(Func<IEnumerable<string>> callback, ComponentBase receiver = null)
        {
            this.syncQueueCallback = (IEnumerable<string> _) => { return callback(); };
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Invokes the delegate associated with this binding and dispatches an event notification to the appropriate component.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <exception cref="ArgumentException">Thrown if used in Queue request.</exception>
        public async Task<string> InvokeAsync(string arg)
        {
            string ret;
            if (this.asyncCallback != null)
            {
                ret = await this.asyncCallback(arg);
            }
            else if (this.syncCallback != null)
            {
                ret = this.syncCallback(arg);
            }
            else
            {
                throw new ArgumentException("use string (not IEnumerable<string>) for Fire requests");
            }

            await this.eventCallback.InvokeAsync(arg);

            return ret;
        }

        /// <summary>
        /// Invokes the delegate associated with this binding and dispatches an event notification to the appropriate component.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <exception cref="ArgumentException">Thrown if used in Fire request.</exception>
        public async Task<IEnumerable<string>> InvokeAsync(IEnumerable<string> arg)
        {
            IEnumerable<string> ret;
            if (this.asyncQueueCallback != null)
            {
                ret = await this.asyncQueueCallback(arg);
            }
            else if (this.syncQueueCallback != null)
            {
                ret = this.syncQueueCallback(arg);
            }
            else
            {
                throw new ArgumentException("use IEnumerable<string> for Queue requests");
            }

            await this.eventCallback.InvokeAsync(arg);

            return ret;
        }
    }
}
