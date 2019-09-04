namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SweetAlertMixin : IAsyncSweetAlertService
    {
        private readonly SweetAlertOptions storedOptions;
        private readonly SweetAlertService swal;

        internal SweetAlertMixin(SweetAlertOptions settings, SweetAlertService service)
        {
            this.storedOptions = settings;
            this.swal = service;
        }

        /// <summary>
        /// Function to display a simple SweetAlert2 modal.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ValueTask<SweetAlertResult> FireAsync(string title = null, string message = null, SweetAlertType type = null)
        {
            SweetAlertOptions newSettings = this.Mix(this.storedOptions);
            newSettings.Title = title;
            newSettings.Html = message ?? newSettings.Html;
            newSettings.Type = type ?? newSettings.Type;
            return this.swal.FireAsync(newSettings);
        }

        /// <summary>
        /// Function to display a SweetAlert2 modal, with an object of options, all being optional.
        /// </summary>
        /// <param name="settings"></param>
        public ValueTask<SweetAlertResult> FireAsync(SweetAlertOptions settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.swal.FireAsync(this.Mix(settings));
        }

        /// <summary>
        /// Reuse configuration by creating a Swal instance.
        /// </summary>
        /// <param name="settings">The default options to set for this instance.</param>
        /// <returns></returns>
        public SweetAlertMixin Mixin(SweetAlertOptions settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return new SweetAlertMixin(this.Mix(settings), this.swal);
        }

        /// <summary>
        /// Determines if a modal is shown.
        /// </summary>
        public ValueTask<bool> IsVisibleAsync()
        {
            return this.swal.IsVisibleAsync();
        }

        /// <summary>
        /// Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        /// <param name="result">The promise originally returned by <code>Swal.FireAsync()</code> will be resolved with this value.
        /// <para>If no object is given, the promise is resolved with an empty ({}) <code>SweetAlertResult</code> object.</para>
        /// </param>
        public ValueTask CloseAsync(SweetAlertResult result)
        {
            return this.swal.CloseAsync(result);
        }

        /// <summary>
        /// Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        /// <param name="result">The promise originally returned by <code>Swal.FireAsync()</code> will be resolved with this value.
        /// <para>If no object is given, the promise is resolved with an empty ({}) <code>SweetAlertResult</code> object.</para>
        /// </param>
        public ValueTask CloseAsync(SweetAlertQueueResult result)
        {
            return this.swal.CloseAsync(result);
        }

        /// <summary>
        /// Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        public ValueTask CloseAsync()
        {
            return this.swal.CloseAsync();
        }

        /// <summary>
        /// Updates popup options.
        /// </summary>
        /// <param name="newSettings"></param>
        public ValueTask UpdateAsync(SweetAlertOptions newSettings)
        {
            if (newSettings == null)
            {
                throw new ArgumentNullException(nameof(newSettings));
            }

            return this.swal.UpdateAsync(this.Mix(newSettings));
        }

        /// <summary>
        /// Enables "Confirm" and "Cancel" buttons.
        /// </summary>
        public ValueTask EnableButtonsAsync()
        {
            return this.swal.EnableButtonsAsync();
        }

        /// <summary>
        /// Disables "Confirm" and "Cancel" buttons.
        /// </summary>
        public ValueTask DisableButtonsAsync()
        {
            return this.swal.DisableButtonsAsync();
        }

        /// <summary>
        /// Disables buttons and show loader. This is useful with HTML requests.
        /// </summary>
        public ValueTask ShowLoadingAsync()
        {
            return this.swal.ShowLoadingAsync();
        }

        /// <summary>
        /// Enables buttons and hide loader.
        /// </summary>
        public ValueTask HideLoadingAsync()
        {
            return this.swal.HideLoadingAsync();
        }

        /// <summary>
        /// Determines if modal is in the loading state.
        /// </summary>
        public ValueTask<bool> IsLoadingAsync()
        {
            return this.swal.IsLoadingAsync();
        }

        /// <summary>
        /// Clicks the "Confirm"-button programmatically.
        /// </summary>
        public ValueTask ClickConfirmAsync()
        {
            return this.swal.ClickCancelAsync();
        }

        /// <summary>
        /// Clicks the "Cancel"-button programmatically.
        /// </summary>
        public ValueTask ClickCancelAsync()
        {
            return this.swal.ClickCancelAsync();
        }

        /// <summary>
        /// Shows a validation message.
        /// </summary>
        /// <param name="validationMessage">The validation message.</param>
        public ValueTask ShowValidationMessageAsync(string validationMessage)
        {
            return this.swal.ShowValidationMessageAsync(validationMessage);
        }

        /// <summary>
        /// Hides validation message.
        /// </summary>
        public ValueTask ResetValidationMessageAsync()
        {
            return this.swal.ResetValidationMessageAsync();
        }

        /// <summary>
        /// Disables the modal input. A disabled input element is unusable and un-clickable.
        /// </summary>
        public ValueTask DisableInputAsync()
        {
            return this.swal.DisableInputAsync();
        }

        /// <summary>
        /// Enables the modal input.
        /// </summary>
        public ValueTask EnableInputAsync()
        {
            return this.swal.EnableInputAsync();
        }

        /// <summary>
        /// If `timer` parameter is set, returns number of milliseconds of timer remained.
        /// <para>Otherwise, returns null.</para>
        /// </summary>
        public ValueTask<double?> GetTimerLeftAsync()
        {
            return this.swal.GetTimerLeftAsync();
        }

        /// <summary>
        /// Stop timer. Returns number of milliseconds of timer remained.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public ValueTask<double?> StopTimerAsync()
        {
            return this.swal.StopTimerAsync();
        }

        /// <summary>
        /// Resume timer. Returns number of milliseconds of timer remained.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public ValueTask<double?> ResumeTimerAsync()
        {
            return this.swal.ResumeTimerAsync();
        }

        /// <summary>
        /// Toggle timer. Returns number of milliseconds of timer remained.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public ValueTask<double?> ToggleTimerAsync()
        {
            return this.swal.ToggleTimerAsync();
        }

        /// <summary>
        /// Check if timer is running. Returns true if timer is running, and false is timer is paused / stopped.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public ValueTask<bool?> IsTimmerRunningAsync()
        {
            return this.swal.IsTimmerRunningAsync();
        }

        /// <summary>
        /// Increase timer. Returns number of milliseconds of an updated timer.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        /// <param name="n">The number of milliseconds to add to the currect timer</param>
        public ValueTask<double?> IncreaseTimerAsync(double n)
        {
            return this.swal.IncreaseTimerAsync(n);
        }

        /// <summary>
        /// Provide an array of SweetAlert2 parameters to show multiple modals, one modal after another.
        /// </summary>
        /// <param name="steps">The steps' configuration.</param>
        public ValueTask<SweetAlertQueueResult> QueueAsync(IEnumerable<SweetAlertOptions> steps)
        {
            return this.swal.QueueAsync(steps.Select(s => this.Mix(s)));
        }

        /// <summary>
        /// Gets the index of current modal in queue. When there's no active queue, null will be returned.
        /// </summary>
        public ValueTask<string> GetQueueStepAsync()
        {
            return this.swal.GetQueueStepAsync();
        }

        /// <summary>
        /// Inserts a modal in the queue.
        /// </summary>
        /// <param name="step">The step configuration (same object as in the Swal.fire() call).</param>
        /// <param name="index">The index to insert the step at. By default a modal will be added to the end of a queue.</param>
        public ValueTask<double> InsertQueueStepAsync(SweetAlertOptions step, double? index = null)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }

            return this.swal.InsertQueueStepAsync(this.Mix(step), index);
        }

        /// <summary>
        /// Deletes the modal at the specified index in the queue.
        /// </summary>
        /// <param name="index">The modal index in the queue.</param>
        public ValueTask DeleteQueueStepAsync(double index)
        {
            return this.swal.DeleteQueueStepAsync(index);
        }

        /// <summary>
        /// Shows progress steps.
        /// </summary>
        public ValueTask ShowProgressStepsAsync()
        {
            return this.swal.ShowProgressStepsAsync();
        }

        /// <summary>
        /// Shows progress steps.
        /// </summary>
        public ValueTask HideProgressStepsAsync()
        {
            return this.swal.HideProgressStepsAsync();
        }

        /// <summary>
        /// Determines if a given parameter name is valid.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        public ValueTask<bool> IsValidParamterAsync(string paramName)
        {
            return this.swal.IsValidParamterAsync(paramName);
        }

        /// <summary>
        /// Determines if a given parameter name is valid for Swal.update() method.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        public ValueTask<bool> IsUpdatableParamterAsync(string paramName)
        {
            return this.swal.IsUpdatableParamterAsync(paramName);
        }

        /// <summary>
        /// Normalizes the arguments you can give to Swal.fire() in an object of type SweetAlertOptions.
        /// </summary>
        /// <param name="paramaters">The array of arguments to normalize.</param>
        /// <exception cref="ArgumentException">Thrown if parameters is not 1, 2, or 3 elements long.</exception>
        public SweetAlertOptions ArgsToParams(IEnumerable<string> paramaters)
        {
            return this.swal.ArgsToParams(paramaters);
        }

        private SweetAlertOptions Mix(SweetAlertOptions newSettings)
        {
            return new SweetAlertOptions
            {
                Title = newSettings.Title ?? this.storedOptions.Title,
                TitleText = newSettings.TitleText ?? this.storedOptions.TitleText,
                Text = newSettings.Text ?? this.storedOptions.Text,
                Html = newSettings.Html ?? this.storedOptions.Html,
                Footer = newSettings.Footer ?? this.storedOptions.Footer,
                Type = newSettings.Type ?? this.storedOptions.Type,
                Backdrop = newSettings.Backdrop ?? this.storedOptions.Backdrop,
                Toast = newSettings.Toast ?? this.storedOptions.Toast,
                Target = newSettings.Target ?? this.storedOptions.Target,
                Input = newSettings.Input ?? this.storedOptions.Input,
                Width = newSettings.Width ?? this.storedOptions.Width,
                Padding = newSettings.Padding ?? this.storedOptions.Padding,
                Background = newSettings.Background ?? this.storedOptions.Background,
                Position = newSettings.Position ?? this.storedOptions.Position,
                Grow = newSettings.Grow ?? this.storedOptions.Grow,
                CustomClass = newSettings.CustomClass ?? this.storedOptions.CustomClass,
                Timer = newSettings.Timer ?? this.storedOptions.Timer,
                Animation = newSettings.Animation ?? this.storedOptions.Animation,
                HeightAuto = newSettings.HeightAuto ?? this.storedOptions.HeightAuto,
                AllowOutsideClick = newSettings.AllowOutsideClick ?? this.storedOptions.AllowOutsideClick,
                AllowEscapeKey = newSettings.AllowEscapeKey ?? this.storedOptions.AllowEscapeKey,
                AllowEnterKey = newSettings.AllowEnterKey ?? this.storedOptions.AllowEnterKey,
                StopKeydownPropagation = newSettings.StopKeydownPropagation ?? this.storedOptions.StopKeydownPropagation,
                KeydownListenerCapture = newSettings.KeydownListenerCapture ?? this.storedOptions.KeydownListenerCapture,
                ShowConfirmButton = newSettings.ShowConfirmButton ?? this.storedOptions.ShowConfirmButton,
                ShowCancelButton = newSettings.ShowCancelButton ?? this.storedOptions.ShowCancelButton,
                ConfirmButtonText = newSettings.ConfirmButtonText ?? this.storedOptions.ConfirmButtonText,
                CancelButtonText = newSettings.CancelButtonText ?? this.storedOptions.CancelButtonText,
                ConfirmButtonColor = newSettings.ConfirmButtonColor ?? this.storedOptions.ConfirmButtonColor,
                CancelButtonColor = newSettings.CancelButtonColor ?? this.storedOptions.CancelButtonColor,
                ConfirmButtonAriaLabel = newSettings.ConfirmButtonAriaLabel ?? this.storedOptions.ConfirmButtonAriaLabel,
                CancelButtonAriaLabel = newSettings.CancelButtonAriaLabel ?? this.storedOptions.CancelButtonAriaLabel,
                ButtonsStyling = newSettings.ButtonsStyling ?? this.storedOptions.ButtonsStyling,
                ReverseButtons = newSettings.ReverseButtons ?? this.storedOptions.ReverseButtons,
                FocusConfirm = newSettings.FocusConfirm ?? this.storedOptions.FocusConfirm,
                FocusCancel = newSettings.FocusCancel ?? this.storedOptions.FocusCancel,
                ShowCloseButton = newSettings.ShowCloseButton ?? this.storedOptions.ShowCloseButton,
                CloseButtonHtml = newSettings.CloseButtonHtml ?? this.storedOptions.CloseButtonHtml,
                CloseButtonAriaLabel = newSettings.CloseButtonAriaLabel ?? this.storedOptions.CloseButtonAriaLabel,
                ShowLoaderOnConfirm = newSettings.ShowLoaderOnConfirm ?? this.storedOptions.ShowLoaderOnConfirm,
                PreConfirm = newSettings.PreConfirm ?? this.storedOptions.PreConfirm,
                ImageUrl = newSettings.ImageUrl ?? this.storedOptions.ImageUrl,
                ImageWidth = newSettings.ImageWidth ?? this.storedOptions.ImageWidth,
                ImageHeight = newSettings.ImageHeight ?? this.storedOptions.ImageHeight,
                ImageAlt = newSettings.ImageAlt ?? this.storedOptions.ImageAlt,
                InputPlaceholder = newSettings.InputPlaceholder ?? this.storedOptions.InputPlaceholder,
                InputValue = newSettings.InputValue ?? this.storedOptions.InputValue,
                InputOptions = newSettings.InputOptions ?? this.storedOptions.InputOptions,
                InputAutoTrim = newSettings.InputAutoTrim ?? this.storedOptions.InputAutoTrim,
                InputAttributes = newSettings.InputAttributes ?? this.storedOptions.InputAttributes,
                InputValidator = newSettings.InputValidator ?? this.storedOptions.InputValidator,
                ValidationMessage = newSettings.ValidationMessage ?? this.storedOptions.ValidationMessage,
                ProgressSteps = newSettings.ProgressSteps ?? this.storedOptions.ProgressSteps,
                CurrentProgressStep = newSettings.CurrentProgressStep ?? this.storedOptions.CurrentProgressStep,
                ProgressStepsDistance = newSettings.ProgressStepsDistance ?? this.storedOptions.ProgressStepsDistance,
                OnBeforeOpen = newSettings.OnBeforeOpen ?? this.storedOptions.OnBeforeOpen,
                OnAfterClose = newSettings.OnAfterClose ?? this.storedOptions.OnAfterClose,
                OnOpen = newSettings.OnOpen ?? this.storedOptions.OnOpen,
                OnClose = newSettings.OnClose ?? this.storedOptions.OnClose,
                OnRender = newSettings.OnRender ?? this.storedOptions.OnRender,
                ScrollbarPadding = newSettings.ScrollbarPadding ?? this.storedOptions.ScrollbarPadding,
            };
        }
    }
}
