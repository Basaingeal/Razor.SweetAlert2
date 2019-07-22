namespace CurrieTechnologies.Razor.SweetAlert2
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A bound event handler delagate.
    /// </summary>
    public class InputValidatorCallback
    {
        private readonly Func<string, Task<string>> asyncCallback;
        private readonly Func<string, string> syncCallback;
        private readonly EventCallback eventCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidatorCallback"/> class.
        /// Creates an <see cref="InputValidatorCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public InputValidatorCallback(Func<string, Task<string>> callback, ComponentBase receiver = null)
        {
            this.asyncCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidatorCallback"/> class.
        /// Creates an <see cref="InputValidatorCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public InputValidatorCallback(Func<string, string> callback, ComponentBase receiver = null)
        {
            this.syncCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Invokes the delegate associated with this binding and dispatches an event notification to the appropriate component.
        /// </summary>
        /// <param name="arg">The argument.</param>
        public async Task<string> InvokeAsync(string arg)
        {
            string ret;
            if (this.asyncCallback != null)
            {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                ret = await this.asyncCallback(arg);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            }
            else
            {
                ret = this.syncCallback(arg);
            }

#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await this.eventCallback.InvokeAsync(arg);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task


            return ret;
        }
    }
}
