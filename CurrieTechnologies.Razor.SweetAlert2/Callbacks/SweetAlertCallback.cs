namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;

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
                await this.asyncCallback();
            }
            else
            {
                this.syncCallback();
            }

            await this.eventCallback.InvokeAsync(null);
        }
    }
}
