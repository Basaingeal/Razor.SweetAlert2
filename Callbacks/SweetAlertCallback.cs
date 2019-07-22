namespace CurrieTechnologies.Razor.SweetAlert2
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A bound event handler delagate.
    /// </summary>
    public class SweetAlertCallback
    {
        private readonly Action syncCallback;
        private readonly Func<Task> asyncCallback;
        private readonly EventCallback eventCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="SweetAlertCallback"/> class.
        /// Creates a <see cref="SweetAlertCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public SweetAlertCallback(Action callback, ComponentBase receiver = null)
        {
            this.syncCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SweetAlertCallback"/> class.
        /// Creates a <see cref="SweetAlertCallback"/> for the provided <paramref name="receiver"/> and <paramref name="callback"/>.
        /// </summary>
        /// <param name="callback">The event callback.</param>
        /// <param name="receiver">The event receiver. Pass in `this` from the calling component.</param>
        public SweetAlertCallback(Func<Task> callback, ComponentBase receiver = null)
        {
            this.asyncCallback = callback;
            if (receiver != null)
            {
                this.eventCallback = EventCallback.Factory.Create(receiver, () => { });
            }
        }

        /// <summary>
        /// Invokes the delegate associated with this binding and dispatches an event notification to the appropriate component.
        /// </summary>
        public async Task InvokeAsync()
        {
            if (this.asyncCallback != null)
            {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                await this.asyncCallback();
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            }
            else
            {
                this.syncCallback();
            }

#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await this.eventCallback.InvokeAsync(null);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        }
    }
}
