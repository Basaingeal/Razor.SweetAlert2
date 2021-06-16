using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    public class SweetAlertMixin : IAsyncSweetAlertService
    {
        private readonly SweetAlertOptions _storedOptions;
        private readonly SweetAlertService _swal;

        internal SweetAlertMixin(SweetAlertOptions settings, SweetAlertService service)
        {
            _storedOptions = settings;
            _swal = service;
        }

        /// <summary>
        ///     Function to display a simple SweetAlert2 modal.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public Task<SweetAlertResult> FireAsync(string title = null, string message = null, SweetAlertIcon icon = null)
        {
            var newSettings = Mix(_storedOptions);
            newSettings.Title = title;
            newSettings.Html = message ?? newSettings.Html;
            newSettings.Icon = icon ?? newSettings.Icon;
            return _swal.FireAsync(newSettings);
        }

        /// <summary>
        ///     Function to display a SweetAlert2 modal, with an object of options, all being optional.
        /// </summary>
        /// <param name="settings"></param>
        public Task<SweetAlertResult> FireAsync(SweetAlertOptions settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            return _swal.FireAsync(Mix(settings));
        }

        /// <summary>
        ///     Reuse configuration by creating a Swal instance.
        /// </summary>
        /// <param name="settings">The default options to set for this instance.</param>
        /// <returns></returns>
        public SweetAlertMixin Mixin(SweetAlertOptions settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            return new SweetAlertMixin(Mix(settings), _swal);
        }

        /// <summary>
        ///     Determines if a modal is shown.
        /// </summary>
        public Task<bool> IsVisibleAsync()
        {
            return _swal.IsVisibleAsync();
        }

        /// <summary>
        ///     Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        /// <param name="result">
        ///     The promise originally returned by <code>Swal.FireAsync()</code> will be resolved with this value.
        ///     <para>If no object is given, the promise is resolved with an empty ({}) <code>SweetAlertResult</code> object.</para>
        /// </param>
        public Task CloseAsync(SweetAlertResult result)
        {
            return _swal.CloseAsync(result);
        }

        /// <summary>
        ///     Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        public Task CloseAsync()
        {
            return _swal.CloseAsync();
        }

        /// <summary>
        ///     Updates popup options.
        /// </summary>
        /// <param name="newSettings"></param>
        public Task UpdateAsync(SweetAlertOptions newSettings)
        {
            if (newSettings == null) throw new ArgumentNullException(nameof(newSettings));

            return _swal.UpdateAsync(Mix(newSettings));
        }

        /// <summary>
        ///     Enables "Confirm" and "Cancel" buttons.
        /// </summary>
        public Task EnableButtonsAsync()
        {
            return _swal.EnableButtonsAsync();
        }

        /// <summary>
        ///     Disables "Confirm" and "Cancel" buttons.
        /// </summary>
        public Task DisableButtonsAsync()
        {
            return _swal.DisableButtonsAsync();
        }

        /// <summary>
        ///     Disables buttons and show loader. This is useful with HTML requests.
        /// </summary>
        public Task ShowLoadingAsync()
        {
            return _swal.ShowLoadingAsync();
        }

        /// <summary>
        ///     Enables buttons and hide loader.
        /// </summary>
        public Task HideLoadingAsync()
        {
            return _swal.HideLoadingAsync();
        }

        /// <summary>
        ///     Determines if modal is in the loading state.
        /// </summary>
        public Task<bool> IsLoadingAsync()
        {
            return _swal.IsLoadingAsync();
        }

        /// <summary>
        ///     Clicks the "Confirm" button programmatically.
        /// </summary>
        public Task ClickConfirmAsync()
        {
            return _swal.ClickCancelAsync();
        }

        /// <summary>
        ///     Clicks the "Deny" button programmatically.
        /// </summary>
        public Task ClickDenyAsync()
        {
            return _swal.ClickDenyAsync();
        }

        /// <summary>
        ///     Clicks the "Cancel" button programmatically.
        /// </summary>
        public Task ClickCancelAsync()
        {
            return _swal.ClickCancelAsync();
        }

        /// <summary>
        ///     Shows a validation message.
        /// </summary>
        /// <param name="validationMessage">The validation message.</param>
        public Task ShowValidationMessageAsync(string validationMessage)
        {
            return _swal.ShowValidationMessageAsync(validationMessage);
        }

        /// <summary>
        ///     Hides validation message.
        /// </summary>
        public Task ResetValidationMessageAsync()
        {
            return _swal.ResetValidationMessageAsync();
        }

        /// <summary>
        ///     Disables the modal input. A disabled input element is unusable and un-clickable.
        /// </summary>
        public Task DisableInputAsync()
        {
            return _swal.DisableInputAsync();
        }

        /// <summary>
        ///     Enables the modal input.
        /// </summary>
        public Task EnableInputAsync()
        {
            return _swal.EnableInputAsync();
        }

        /// <summary>
        ///     If `timer` parameter is set, returns number of milliseconds of timer remained.
        ///     <para>Otherwise, returns null.</para>
        /// </summary>
        public Task<double?> GetTimerLeftAsync()
        {
            return _swal.GetTimerLeftAsync();
        }

        /// <summary>
        ///     Stop timer. Returns number of milliseconds of timer remained.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public Task<double?> StopTimerAsync()
        {
            return _swal.StopTimerAsync();
        }

        /// <summary>
        ///     Resume timer. Returns number of milliseconds of timer remained.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public Task<double?> ResumeTimerAsync()
        {
            return _swal.ResumeTimerAsync();
        }

        /// <summary>
        ///     Toggle timer. Returns number of milliseconds of timer remained.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public Task<double?> ToggleTimerAsync()
        {
            return _swal.ToggleTimerAsync();
        }

        /// <summary>
        ///     Check if timer is running. Returns true if timer is running, and false is timer is paused / stopped.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public Task<bool?> IsTimerRunningAsync()
        {
            return _swal.IsTimerRunningAsync();
        }

        /// <summary>
        ///     Increase timer. Returns number of milliseconds of an updated timer.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        /// <param name="n">The number of milliseconds to add to the currect timer</param>
        public Task<double?> IncreaseTimerAsync(double n)
        {
            return _swal.IncreaseTimerAsync(n);
        }

        /// <summary>
        ///     Determines if a given parameter name is valid.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        public Task<bool> IsValidParameterAsync(string paramName)
        {
            return _swal.IsValidParameterAsync(paramName);
        }

        /// <summary>
        ///     Determines if a given parameter name is valid for Swal.update() method.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        public Task<bool> IsUpdatableParameterAsync(string paramName)
        {
            return _swal.IsUpdatableParameterAsync(paramName);
        }

        /// <summary>
        ///     Normalizes the arguments you can give to Swal.fire() in an object of type SweetAlertOptions.
        /// </summary>
        /// <param name="parameters">The array of arguments to normalize.</param>
        /// <exception cref="ArgumentException">Thrown if parameters is not 1, 2, or 3 elements long.</exception>
        public SweetAlertOptions ArgsToParams(IEnumerable<string> parameters)
        {
            return _swal.ArgsToParams(parameters);
        }


        private SweetAlertOptions Mix(SweetAlertOptions newSettings)
        {
            return SweetAlertOptionsMixingService.Mix(_storedOptions, newSettings);
        }
    }
}