import "regenerator-runtime/runtime";

import flatten from "lodash/flatten";
import Swal, { SweetAlertOptions, SweetAlertResult, SweetAlertType } from "sweetalert2";
import ISimpleSweetAlertOptions from "./SimpleSweetAlertOptions";
import ISweetAlertQueueResult from "./SweetAlertQueueResult";
import ISweetAlertResult from "./SweetAlertResult";

declare const DotNet: any;
const domWindow = window as any;
const namespace = "CurrieTechnologies.Razor.SweetAlert2";

function getEnumNumber(enumString: string): number {
  if (enumString === "cancel") {
    return 0;
  }
  if (enumString === "backdrop") {
    return 1;
  }
  if (enumString === "close") {
    return 2;
  }
  if (enumString === "esc") {
    return 3;
  }
  if (enumString === "timer") {
    return 4;
  }
  return 0;
}

function getStringVerison(input: any): string {
  if (input instanceof Object) {
    return JSON.stringify(input);
  }
  return input.toString();
}

function dispatchFireResult(requestId: string, result: SweetAlertResult): Promise<void> {
  const myResult = (result as any) as ISweetAlertResult;
  myResult.value = myResult.value ? getStringVerison(myResult.value) : undefined;
  myResult.dismiss = myResult.dismiss ? getEnumNumber(myResult.dismiss.toString()) : undefined;
  return DotNet.invokeMethodAsync(namespace, "ReceiveFireResult", requestId, myResult);
}

function dispatchQueueResult(requestId: string, result: SweetAlertResult): Promise<void> {
  const queueResult = result as ISweetAlertQueueResult;
  queueResult.value = result.value
    ? flatten(result.value).map((v: any): string | undefined =>
        v ? getStringVerison(v) : undefined
      )
    : undefined;
  queueResult.dismiss = queueResult.dismiss
    ? getEnumNumber(queueResult.dismiss.toString())
    : undefined;
  return DotNet.invokeMethodAsync(namespace, "ReceiveQueueResult", requestId, queueResult);
}

function dispatchPreConfirm(requestId: string, inputValue: any): Promise<any> {
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreConfirmInput",
    requestId,
    getStringVerison(inputValue)
  );
}

function dispatchQueuePreConfirm(requestId: string, inputValue: any): Promise<any> {
  const valArray: any[] = Array.isArray(inputValue) ? inputValue : [inputValue];

  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreConfirmQueueInput",
    requestId,
    valArray.map((v): string => getStringVerison(v))
  );
}

function dispatchInputValidator(requestId: string, inputValue: any): Promise<string> {
  return DotNet.invokeMethodAsync(namespace, "ReceiveInputValidatorInput", requestId, inputValue);
}

function dispatchOnOpen(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnOpenInput", requestId);
}

function dispatchOnClose(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnCloseInput", requestId);
}

function dispatchOnBeforeOpen(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnBeforeOpenInput", requestId);
}

function dispatchOnAfterClose(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnAfterCloseInput", requestId);
}

function dispatchOnComplete(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnCompleteInput", requestId);
}

function cleanSettings(settings: ISimpleSweetAlertOptions): ISimpleSweetAlertOptions {
  const settingsToReturn: any = { ...settings } as any;
  for (const propName in settingsToReturn) {
    if (settingsToReturn[propName] === null || settingsToReturn[propName] === undefined) {
      delete settingsToReturn[propName];
    }
  }

  return settingsToReturn as ISimpleSweetAlertOptions;
}

function getSwalSettingsFromPoco(
  settings: ISimpleSweetAlertOptions,
  requestId: string,
  isQueue: boolean
): SweetAlertOptions {
  const swalSettings = (cleanSettings(settings) as any) as SweetAlertOptions;

  if (settings.preConfirm) {
    swalSettings.preConfirm = isQueue
      ? (inputValue): Promise<any> => dispatchQueuePreConfirm(requestId, inputValue)
      : (inputValue): Promise<any> => dispatchPreConfirm(requestId, inputValue);
  } else {
    delete swalSettings.preConfirm;
  }

  if (settings.inputValidator) {
    swalSettings.inputValidator = (inputValue): Promise<string> =>
      dispatchInputValidator(requestId, inputValue);
  } else {
    delete swalSettings.inputValidator;
  }

  if (settings.onBeforeOpen) {
    swalSettings.onBeforeOpen = (): void => dispatchOnBeforeOpen(requestId);
  } else {
    delete swalSettings.onBeforeOpen;
  }

  if (settings.onAfterClose) {
    swalSettings.onAfterClose = (): void => dispatchOnAfterClose(requestId);
  } else {
    delete swalSettings.onAfterClose;
  }

  if (settings.onOpen) {
    swalSettings.onOpen = (): void => dispatchOnOpen(requestId);
  } else {
    delete swalSettings.onOpen;
  }

  if (settings.onClose) {
    swalSettings.onClose = (): void => dispatchOnClose(requestId);
  } else {
    delete swalSettings.onClose;
  }

  if (settings.grow === "false") {
    swalSettings.grow = false;
  } else if (settings.grow == null) {
    delete swalSettings.grow;
  } else {
    swalSettings.grow = settings.grow;
  }

  return swalSettings;
}

domWindow.CurrieTechnologies = domWindow.CurrieTechnologies || {};
domWindow.CurrieTechnologies.Razor = domWindow.CurrieTechnologies.Razor || {};
domWindow.CurrieTechnologies.Razor.SweetAlert2 =
  domWindow.CurrieTechnologies.Razor.SweetAlert2 || {};

domWindow.CurrieTechnologies.Razor.SweetAlert2.Fire = async (
  requestId: string,
  title: string,
  message: string,
  type: SweetAlertType
): Promise<void> => {
  let params: [string] | [string, string] | [string, string, string] = [title];
  params = message
    ? ([...params, message] as [string, string])
    : ([...params, ""] as [string, string]);
  params = type ? ([...params, type.toString()] as [string, string, string]) : params;
  const result = await Swal.fire(Swal.argsToParams(params));
  await dispatchFireResult(requestId, result);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.FireSettings = async (
  requestId: string,
  settingsPoco: ISimpleSweetAlertOptions
): Promise<void> => {
  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId, false);

  const result = await Swal.fire(swalSettings);
  await dispatchFireResult(requestId, result);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.Queue = async (
  requestId: string,
  optionIds: string[],
  steps: ISimpleSweetAlertOptions[]
): Promise<void> => {
  const arrSwalSettings: SweetAlertOptions[] = optionIds.map(
    (optionId, i): SweetAlertOptions => getSwalSettingsFromPoco(steps[i], optionId, true)
  );

  const result = await Swal.queue(arrSwalSettings);
  await dispatchQueueResult(requestId, result);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.IsVisible = (): boolean => {
  return !!Swal.isVisible();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.Update = async (
  requestId: string,
  settingsPoco: ISimpleSweetAlertOptions
): Promise<void> => {
  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId, false);
  Swal.update(swalSettings);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.Close = (requestId: string): void => {
  Swal.close((): void => dispatchOnComplete(requestId));
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.EnableButtons = (): void => {
  Swal.enableButtons();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.DisableButtons = (): void => {
  Swal.disableButtons();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ShowLoading = (): void => {
  Swal.showLoading();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.HideLoading = (): void => {
  Swal.hideLoading();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.HideLoading = (): boolean => {
  return Swal.isLoading();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ClickConfirm = (): void => {
  Swal.clickConfirm();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ClickCancel = (): void => {
  Swal.clickCancel();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ShowValidationMessage = (
  validationMessage: string
): void => {
  Swal.showValidationMessage(validationMessage);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ResetValidationMessage = (): void => {
  Swal.resetValidationMessage();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.DisableInput = (): void => {
  Swal.disableInput();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.EnableInput = (): void => {
  Swal.enableInput();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.GetTimerLeft = (): number | undefined => {
  return Swal.getTimerLeft();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.StopTimer = (): number | undefined => {
  return Swal.stopTimer();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ResumeTimer = (): number | undefined => {
  return Swal.resumeTimer();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ToggleTimer = (): number | undefined => {
  return Swal.toggleTimer();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.IsTimmerRunning = (): boolean | undefined => {
  return Swal.isTimerRunning();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.IncreaseTimer = (n: number): number | undefined => {
  return Swal.increaseTimer(n);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.GetQueueStep = (): string | null => {
  return Swal.getQueueStep();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.InsertQueueStep = (
  requestId: string,
  step: ISimpleSweetAlertOptions,
  index?: number
): number => {
  const stepSettings = getSwalSettingsFromPoco(step, requestId, true);
  return Swal.insertQueueStep(stepSettings, index);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.DeleteQueueStep = (index: number): void => {
  Swal.deleteQueueStep(index);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.ShowProgressSteps = (): void => {
  Swal.showProgressSteps();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.HideProgressSteps = (): void => {
  Swal.hideProgressSteps();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.IsValidParamter = (paramName: string): boolean => {
  return Swal.isValidParameter(paramName);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.IsUpdatableParamter = (
  paramName: string
): boolean => {
  return Swal.isUpdatableParameter(paramName);
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.SetTheme = (theme: number): void => {
  let fileName = "";
  switch (theme) {
    case 1: {
      fileName = "darkTheme.min.css";
      break;
    }
    case 2: {
      fileName = "minimalTheme.min.css";
      break;
    }
    case 3: {
      fileName = "borderlessTheme.min.css";
      break;
    }
    default: {
      return;
    }
  }

  const head = document.getElementsByTagName("head")[0];
  const styleTag = document.createElement("link");
  styleTag.rel = "stylesheet";
  styleTag.href = `_content/currietechnologiesrazorsweetalert2/${fileName}`;
  head.appendChild(styleTag);
};
