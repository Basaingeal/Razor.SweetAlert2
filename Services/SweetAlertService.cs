using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    public class SweetAlertService : IAsyncSweetAlertService
    {
        private static readonly IDictionary<Guid, TaskCompletionSource<SweetAlertResult>> PendingFireRequests =
            new Dictionary<Guid, TaskCompletionSource<SweetAlertResult>>();

        private static readonly IDictionary<Guid, PreConfirmCallback> PreConfirmCallbacks =
            new Dictionary<Guid, PreConfirmCallback>();

        private static readonly IDictionary<Guid, PreDenyCallback> PreDenyCallbacks =
            new Dictionary<Guid, PreDenyCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> DidOpenCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> WillCloseCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> DidRenderCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> WillOpenCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> DidCloseCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> DidDestroyCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, InputValidatorCallback> InputValidatorCallbacks =
            new Dictionary<Guid, InputValidatorCallback>();

        private readonly int[][] _colorSchemeThemes = Array.Empty<int[]>();

        private readonly CultureInfo _culture = CultureInfo.GetCultureInfo("en-US");

        private readonly SweetAlertOptions _defaultOptions;

        private readonly IJSRuntime _jSRuntime;

        private readonly SweetAlertTheme _theme = SweetAlertTheme.Default;

        public SweetAlertService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public SweetAlertService(IJSRuntime jSRuntime, SweetAlertServiceOptions options)
        {
            _jSRuntime = jSRuntime;
            if (options == null) return;
            _theme = options.Theme;
            _colorSchemeThemes = options.ColorSchemeThemes
                .Select(kvp => new[] {(int) kvp.Key, (int) kvp.Value})
                .ToArray();

            if (options.DefaultOptions != null) _defaultOptions = options.DefaultOptions;

            _ = SendThemesToJs();
        }

        /// <summary>
        ///     Function to display a simple SweetAlert2 modal.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public async Task<SweetAlertResult> FireAsync(string title = null, string message = null,
            SweetAlertIcon icon = null)
        {
            var tcs = new TaskCompletionSource<SweetAlertResult>();
            var requestId = Guid.NewGuid();
            PendingFireRequests.Add(requestId, tcs);
            await _jSRuntime.InvokeAsync<object>(
                    "CurrieTechnologies.Razor.SweetAlert2.Fire",
                    requestId,
                    title,
                    message,
                    icon?.ToString())
                .ConfigureAwait(false);
            return await tcs.Task.ConfigureAwait(false);
        }

        /// <summary>
        ///     Function to display a SweetAlert2 modal, with an object of options, all being optional.
        /// </summary>
        /// <example>
        ///     <code>
        /// Swal.FireAsync(new SweetAlertOptions {
        ///     Title = "Auto close alert!",
        ///     Text = "I will close in 2 seconds.",
        ///     Timer = 2000
        /// });
        /// </code>
        /// </example>
        /// <param name="settings"></param>
        public async Task<SweetAlertResult> FireAsync(SweetAlertOptions settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            if (_defaultOptions != null) settings = SweetAlertOptionsMixingService.Mix(_defaultOptions, settings);

            var tcs = new TaskCompletionSource<SweetAlertResult>();
            var requestId = Guid.NewGuid();
            PendingFireRequests.Add(requestId, tcs);

            AddCallbackToDictionaries(settings, requestId);

            await _jSRuntime.InvokeAsync<SweetAlertResult>(
                "CurrieTechnologies.Razor.SweetAlert2.FireSettings",
                requestId,
                settings.ToPOCO()).ConfigureAwait(false);
            return await tcs.Task.ConfigureAwait(false);
        }

        /// <summary>
        ///     Reuse configuration by creating a Swal instance.
        /// </summary>
        /// <param name="settings">The default options to set for this instance.</param>
        /// <returns></returns>
        public SweetAlertMixin Mixin(SweetAlertOptions settings)
        {
            if (_defaultOptions != null) settings = SweetAlertOptionsMixingService.Mix(_defaultOptions, settings);
            return new SweetAlertMixin(settings, this);
        }

        /// <summary>
        ///     Determines if a modal is shown.
        /// </summary>
        public async Task<bool> IsVisibleAsync()
        {
            return await _jSRuntime.InvokeAsync<bool>("CurrieTechnologies.Razor.SweetAlert2.IsVisible")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        /// <param name="result">
        ///     The promise originally returned by <code>Swal.FireAsync()</code> will be resolved with this value.
        ///     <para>If no object is given, the promise is resolved with an empty ({}) <code>SweetAlertResult</code> object.</para>
        /// </param>
        public async Task CloseAsync(SweetAlertResult result)
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.CloseResult", result)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        public async Task CloseAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.Close").ConfigureAwait(false);
        }

        /// <summary>
        ///     Updates popup options.
        /// </summary>
        /// <param name="newSettings"></param>
        public async Task UpdateAsync(SweetAlertOptions newSettings)
        {
            if (newSettings == null) throw new ArgumentNullException(nameof(newSettings));
            if (_defaultOptions != null) newSettings = SweetAlertOptionsMixingService.Mix(_defaultOptions, newSettings);

            var requestId = Guid.NewGuid();
            AddCallbackToDictionaries(newSettings, requestId);
            await _jSRuntime.InvokeAsync<SweetAlertResult>(
                "CurrieTechnologies.Razor.SweetAlert2.Update",
                requestId,
                newSettings.ToPOCO()).ConfigureAwait(false);
        }

        /// <summary>
        ///     Enables "Confirm" and "Cancel" buttons.
        /// </summary>
        public async Task EnableButtonsAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.EnableButtons")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Disables "Confirm" and "Cancel" buttons.
        /// </summary>
        public async Task DisableButtonsAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.DisableButtons")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Disables buttons and show loader. This is useful with HTML requests.
        /// </summary>
        public async Task ShowLoadingAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ShowLoading")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Enables buttons and hide loader.
        /// </summary>
        public async Task HideLoadingAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.HideLoading")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Determines if modal is in the loading state.
        /// </summary>
        public Task<bool> IsLoadingAsync()
        {
            return _jSRuntime.InvokeAsync<bool>("CurrieTechnologies.Razor.SweetAlert2.IsLoading").AsTask();
        }

        /// <summary>
        ///     Clicks the "Confirm" button programmatically.
        /// </summary>
        public async Task ClickConfirmAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ClickConfirm")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Clicks the "Deny" button programmatically.
        /// </summary>
        public async Task ClickDenyAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ClickDeny")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Clicks the "Cancel" button programmatically.
        /// </summary>
        public async Task ClickCancelAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ClickCancel")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Shows a validation message.
        /// </summary>
        /// <param name="validationMessage">The validation message.</param>
        public async Task ShowValidationMessageAsync(string validationMessage)
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ShowValidationMessage",
                    validationMessage)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Hides validation message.
        /// </summary>
        public async Task ResetValidationMessageAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ResetValidationMessage")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Disables the modal input. A disabled input element is unusable and un-clickable.
        /// </summary>
        public async Task DisableInputAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.DisableInput")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Enables the modal input.
        /// </summary>
        public async Task EnableInputAsync()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.EnableInput")
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     If `timer` parameter is set, returns number of milliseconds of timer remained.
        ///     <para>Otherwise, returns null.</para>
        /// </summary>
        public async Task<double?> GetTimerLeftAsync()
        {
            var response = await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.GetTimerLeft")
                .ConfigureAwait(false);
            return response == null ? null : (double?) Convert.ToDouble(response.ToString(), _culture.NumberFormat);
        }

        /// <summary>
        ///     Stop timer. Returns number of milliseconds of timer remained.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<double?> StopTimerAsync()
        {
            var response = await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.StopTimer")
                .ConfigureAwait(false);
            return response == null ? null : (double?) Convert.ToDouble(response.ToString(), _culture.NumberFormat);
        }

        /// <summary>
        ///     Resume timer. Returns number of milliseconds of timer remained.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<double?> ResumeTimerAsync()
        {
            var response = await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ResumeTimer")
                .ConfigureAwait(false);
            return response == null ? null : (double?) Convert.ToDouble(response.ToString(), _culture.NumberFormat);
        }

        /// <summary>
        ///     Toggle timer. Returns number of milliseconds of timer remained.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<double?> ToggleTimerAsync()
        {
            var response = await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.ToggleTimer")
                .ConfigureAwait(false);
            return response == null ? null : (double?) Convert.ToDouble(response.ToString(), new NumberFormatInfo());
        }

        /// <summary>
        ///     Check if timer is running. Returns true if timer is running, and false is timer is paused / stopped.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<bool?> IsTimerRunningAsync()
        {
            var response = await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.IsTimerRunning")
                .ConfigureAwait(false);
            return response == null ? null : (bool?) Convert.ToBoolean(response.ToString(), _culture.NumberFormat);
        }

        /// <summary>
        ///     Increase timer. Returns number of milliseconds of an updated timer.
        ///     <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        /// <param name="n">The number of milliseconds to add to the current timer</param>
        public async Task<double?> IncreaseTimerAsync(double n)
        {
            var response = await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.IncreaseTimer", n)
                .ConfigureAwait(false);
            return response == null ? null : (double?) Convert.ToDouble(response.ToString(), _culture.NumberFormat);
        }

        /// <summary>
        ///     Determines if a given parameter name is valid.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        /// <returns></returns>
        public Task<bool> IsValidParameterAsync(string paramName)
        {
            return _jSRuntime.InvokeAsync<bool>("CurrieTechnologies.Razor.SweetAlert2.IsValidParameter", paramName)
                .AsTask();
        }

        /// <summary>
        ///     Determines if a given parameter name is valid for Swal.update() method.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        /// <returns></returns>
        public Task<bool> IsUpdatableParameterAsync(string paramName)
        {
            return _jSRuntime.InvokeAsync<bool>("CurrieTechnologies.Razor.SweetAlert2.IsUpdatableParameter", paramName)
                .AsTask();
        }

        /// <summary>
        ///     Normalizes the arguments you can give to Swal.fire() in an object of type SweetAlertOptions.
        /// </summary>
        /// <param name="parameters">The array of arguments to normalize.</param>
        /// <exception cref="ArgumentException">Thrown if parameters is not 1, 2, or 3 elements long.</exception>
        public SweetAlertOptions ArgsToParams(IEnumerable<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var parametersList = parameters.ToList();
            var paramLength = parametersList.Count;
            if (paramLength > 3 || paramLength < 1)
                throw new ArgumentException("Parameters can only be 1, 2, or 3 elements long.");
            var paramEnum = parametersList.GetEnumerator();
            paramEnum.MoveNext();
            var optionsToReturn = new SweetAlertOptions
            {
                Title = paramEnum.Current
            };

            if (paramEnum.MoveNext()) optionsToReturn.Html = paramEnum.Current;
            if (paramEnum.MoveNext()) optionsToReturn.Icon = paramEnum.Current;

            paramEnum.Dispose();

            return optionsToReturn;
        }

        private async Task SendThemesToJs()
        {
            await _jSRuntime.InvokeAsync<object>("CurrieTechnologies.Razor.SweetAlert2.SendThemesToJS", (int) _theme,
                    _colorSchemeThemes)
                .ConfigureAwait(false);
        }

        [JSInvokable]
        public static Task ReceiveFireResult(string requestId, SweetAlertResult result)
        {
            var requestGuid = Guid.Parse(requestId);
            PendingFireRequests.TryGetValue(requestGuid, out var pendingTask);
            PendingFireRequests.Remove(requestGuid);
            pendingTask?.SetResult(result);
            return Task.CompletedTask;
        }

        private static void AddCallbackToDictionaries(SweetAlertOptions settings, Guid requestId)
        {
            if (settings.PreConfirm != null) PreConfirmCallbacks.Add(requestId, settings.PreConfirm);

            if (settings.PreDeny != null) PreDenyCallbacks.Add(requestId, settings.PreDeny);

            if (settings.InputValidator != null) InputValidatorCallbacks.Add(requestId, settings.InputValidator);

            if (settings.DidOpen != null) DidOpenCallbacks.Add(requestId, settings.DidOpen);

            if (settings.WillClose != null) WillCloseCallbacks.Add(requestId, settings.WillClose);

            if (settings.DidRender != null) DidRenderCallbacks.Add(requestId, settings.DidRender);

            if (settings.WillOpen != null) WillOpenCallbacks.Add(requestId, settings.WillOpen);

            if (settings.DidClose != null) DidCloseCallbacks.Add(requestId, settings.DidClose);

            if (settings.DidDestroy != null) DidDestroyCallbacks.Add(requestId, settings.DidDestroy);
        }

        [JSInvokable]
        public static Task<string> ReceivePreConfirmInput(string requestId, string inputValue)
        {
            var requestIdGuid = Guid.Parse(requestId);
            PreConfirmCallbacks.TryGetValue(requestIdGuid, out var callback);
            return callback?.InvokeAsync(inputValue);
        }

        [JSInvokable]
        public static Task<string> ReceivePreDenyInput(string requestId, string inputValue)
        {
            var requestIdGuid = Guid.Parse(requestId);
            PreDenyCallbacks.TryGetValue(requestIdGuid, out var callback);
            return callback?.InvokeAsync(inputValue);
        }

        [JSInvokable]
        public static Task<string> ReceiveInputValidatorInput(string requestId, string inputValue)
        {
            var requestIdGuid = Guid.Parse(requestId);
            InputValidatorCallbacks.TryGetValue(requestIdGuid, out var callback);
            return callback?.InvokeAsync(inputValue);
        }

        [JSInvokable]
        public static async Task ReceiveDidOpenInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            DidOpenCallbacks.TryGetValue(requestIdGuid, out var callback);
            DidOpenCallbacks.Remove(requestIdGuid);
            if (callback != null) await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveWillCloseInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            WillCloseCallbacks.TryGetValue(requestIdGuid, out var callback);
            WillCloseCallbacks.Remove(requestIdGuid);
            if (callback != null) await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveDidRenderInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            DidRenderCallbacks.TryGetValue(requestIdGuid, out var callback);
            if (callback != null) await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveWillOpenInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            WillOpenCallbacks.TryGetValue(requestIdGuid, out var callback);
            WillOpenCallbacks.Remove(requestIdGuid);
            if (callback != null) await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveDidCloseInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            DidCloseCallbacks.TryGetValue(requestIdGuid, out var callback);
            DidCloseCallbacks.Remove(requestIdGuid);
            if (callback != null) await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveDidDestroyInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            DidDestroyCallbacks.TryGetValue(requestIdGuid, out var callback);
            DidDestroyCallbacks.Remove(requestIdGuid);
            if (callback != null) await callback.InvokeAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    ///     An enum of possible reasons that can explain an alert dismissal.
    /// </summary>
    public enum DismissReason
    {
        Cancel,
        Backdrop,
        Close,
        Esc,
        Timer
    }
}