using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    internal interface IAsyncSweetAlertService
    {
        ValueTask<SweetAlertResult> FireAsync(string title, string message, SweetAlertType type);

        ValueTask<SweetAlertResult> FireAsync(SweetAlertOptions settings);

        SweetAlertMixin Mixin(SweetAlertOptions settings);

        ValueTask<bool> IsVisibleAsync();

        ValueTask CloseAsync(SweetAlertResult result);

        ValueTask CloseAsync(SweetAlertQueueResult result);

        ValueTask CloseAsync();

        ValueTask UpdateAsync(SweetAlertOptions newSettings);

        ValueTask EnableButtonsAsync();

        ValueTask DisableButtonsAsync();

        ValueTask ShowLoadingAsync();

        ValueTask HideLoadingAsync();

        ValueTask<bool> IsLoadingAsync();

        ValueTask ClickConfirmAsync();

        ValueTask ClickCancelAsync();

        ValueTask ShowValidationMessageAsync(string validationMessage);

        ValueTask ResetValidationMessageAsync();

        ValueTask DisableInputAsync();

        ValueTask EnableInputAsync();

        ValueTask<double?> GetTimerLeftAsync();

        ValueTask<double?> StopTimerAsync();

        ValueTask<double?> ResumeTimerAsync();

        ValueTask<double?> ToggleTimerAsync();

        ValueTask<bool?> IsTimmerRunningAsync();

        ValueTask<double?> IncreaseTimerAsync(double n);

        ValueTask<SweetAlertQueueResult> QueueAsync(IEnumerable<SweetAlertOptions> steps);

        ValueTask<string> GetQueueStepAsync();

        ValueTask<double> InsertQueueStepAsync(SweetAlertOptions step, double? index);


        ValueTask DeleteQueueStepAsync(double index);


        ValueTask ShowProgressStepsAsync();

        ValueTask HideProgressStepsAsync();

        ValueTask<bool> IsValidParamterAsync(string paramName);

        ValueTask<bool> IsUpdatableParamterAsync(string paramName);

        SweetAlertOptions ArgsToParams(IEnumerable<string> paramaters);
    }
}
