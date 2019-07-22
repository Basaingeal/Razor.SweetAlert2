using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    internal interface IAsyncSweetAlertService
    {
        Task<SweetAlertResult> FireAsync(string title, string message, SweetAlertType type);

        Task<SweetAlertResult> FireAsync(SweetAlertOptions settings);

        SweetAlertMixin Mixin(SweetAlertOptions settings);

        Task<bool> IsVisibleAsync();

        Task CloseAsync(SweetAlertCallback onComplete);

        Task CloseAsync();

        Task UpdateAsync(SweetAlertOptions newSettings);

        Task EnableButtonsAsync();

        Task DisableButtonsAsync();

        Task ShowLoadingAsync();

        Task HideLoadingAsync();

        Task<bool> IsLoadingAsync();

        Task ClickConfirmAsync();

        Task ClickCancelAsync();

        Task ShowValidationMessageAsync(string validationMessage);

        Task ResetValidationMessageAsync();

        Task DisableInputAsync();

        Task EnableInputAsync();

        Task<double?> GetTimerLeftAsync();

        Task<double?> StopTimerAsync();

        Task<double?> ResumeTimerAsync();

        Task<double?> ToggleTimerAsync();

        Task<bool?> IsTimmerRunningAsync();

        Task<double?> IncreaseTimerAsync(double n);

        Task<SweetAlertQueueResult> QueueAsync(IEnumerable<SweetAlertOptions> steps);

        Task<string> GetQueueStepAsync();

        Task<double> InsertQueueStepAsync(SweetAlertOptions step, double? index);


        Task DeleteQueueStepAsync(double index);


        Task ShowProgressStepsAsync();

        Task HideProgressStepsAsync();

        Task<bool> IsValidParamterAsync(string paramName);

        Task<bool> IsUpdatableParamterAsync(string paramName);

        SweetAlertOptions ArgsToParams(IEnumerable<string> paramaters);
    }
}
