import Swal, { SweetAlertOptions, SweetAlertResult, SweetAlertType } from "sweetalert2";
import SimpleSweetAlertOptions from "./SimpleSweetAlertOptions";
import SweetAlertQueueResult from "./SweetAlertQueueResult";
import EnumSweetAlertResult from "./EnumSweetAlertResult";

declare const DotNet: any;
const domWindow = window as any;
const namespace = "CurrieTechnologies.Razor.SweetAlert2";

function getEnumNumber(enumString: string): number | undefined {
  switch (enumString) {
    case "cancel":
      return 0;
    case "backdrop":
      return 1;
    case "close":
      return 2;
    case "esc":
      return 3;
    case "timer":
      return 4;
  }

  return undefined;
}

function getStringVerison(input: any): string {
  if (input instanceof Object) {
    return JSON.stringify(input);
  }
  return input.toString();
}

function dispatchFireResult(requestId: string, result: SweetAlertResult): Promise<void> {
  const myResult = (result as (SweetAlertResult | EnumSweetAlertResult)) as EnumSweetAlertResult;
  myResult.value = myResult.value !== undefined ? getStringVerison(myResult.value) : undefined;
  myResult.dismiss =
    myResult.dismiss !== undefined ? getEnumNumber(myResult.dismiss.toString()) : undefined;
  return DotNet.invokeMethodAsync(namespace, "ReceiveFireResult", requestId, myResult);
}

const flatten = (arr: any[]): any[] =>
  arr.reduce((flat, next): any[] => flat.concat(Array.isArray(next) ? flatten(next) : next), []);

function dispatchQueueResult(requestId: string, result: SweetAlertResult): Promise<void> {
  const queueResult = result as SweetAlertQueueResult;
  queueResult.value =
    queueResult.value !== undefined
      ? flatten(queueResult.value).map((v: any): string | undefined =>
          v !== undefined ? getStringVerison(v) : undefined
        )
      : undefined;
  queueResult.dismiss =
    queueResult.dismiss !== undefined ? getEnumNumber(queueResult.dismiss.toString()) : undefined;
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

function cleanSettings(settings: SimpleSweetAlertOptions): SimpleSweetAlertOptions {
  const settingsToReturn: any = settings as any;
  for (const propName in settingsToReturn) {
    if (settingsToReturn[propName] === null || settingsToReturn[propName] === undefined) {
      delete settingsToReturn[propName];
    }
  }

  return settingsToReturn as SimpleSweetAlertOptions;
}

function getSwalSettingsFromPoco(
  settings: SimpleSweetAlertOptions,
  requestId: string,
  isQueue: boolean
): SweetAlertOptions {
  const swalSettings = (cleanSettings(settings) as (
    | SimpleSweetAlertOptions
    | SweetAlertOptions)) as SweetAlertOptions;

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

function setTheme(theme: number): void {
  let fileName: string;
  const tagId = "currietechnologies-razor-sweetalert2-theme-link";

  const existingThemeTag = document.getElementById(tagId);
  if (existingThemeTag !== null) {
    const currentTheme = Number(existingThemeTag.dataset.themeNumber);
    if (currentTheme === theme) {
      return;
    } else {
      existingThemeTag.remove();
    }
  }

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
    case 4: {
      fileName = "bootstrap4Theme.min.css";
      break;
    }
    default: {
      return;
    }
  }

  const head = document.getElementsByTagName("head")[0];
  const styleTag = document.createElement("link");
  styleTag.rel = "stylesheet";
  styleTag.id = tagId;
  styleTag.href = `_content/CurrieTechnologies.Razor.SweetAlert2/${fileName}`;
  styleTag.setAttribute("data-theme-number", theme.toString());
  head.appendChild(styleTag);
}

domWindow.CurrieTechnologies = domWindow.CurrieTechnologies || {};
domWindow.CurrieTechnologies.Razor = domWindow.CurrieTechnologies.Razor || {};
domWindow.CurrieTechnologies.Razor.SweetAlert2 =
  domWindow.CurrieTechnologies.Razor.SweetAlert2 || {};

domWindow.CurrieTechnologies.Razor.SweetAlert2.Fire = (
  requestId: string,
  title: string,
  message: string,
  type: SweetAlertType,
  theme: number
): void => {
  setTheme(theme);

  let params: [string] | [string, string] | [string, string, string] = [title];
  params = params.concat(message || "") as [string, string];
  params = type ? (params.concat(type.toString()) as [string, string, string]) : params;
  Swal.fire(Swal.argsToParams(params)).then((result): void => {
    dispatchFireResult(requestId, result);
  });
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.FireSettings = (
  requestId: string,
  settingsPoco: SimpleSweetAlertOptions,
  theme: number
): void => {
  setTheme(theme);

  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId, false);

  Swal.fire(swalSettings).then((result): void => {
    dispatchFireResult(requestId, result);
  });
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.Queue = (
  requestId: string,
  optionIds: string[],
  steps: SimpleSweetAlertOptions[],
  theme: number
): void => {
  setTheme(theme);

  const arrSwalSettings: SweetAlertOptions[] = optionIds.map(
    (optionId, i): SweetAlertOptions => getSwalSettingsFromPoco(steps[i], optionId, true)
  );

  Swal.queue(arrSwalSettings).then((result): void => {
    dispatchQueueResult(requestId, result);
  });
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.IsVisible = (): boolean => {
  return !!Swal.isVisible();
};

domWindow.CurrieTechnologies.Razor.SweetAlert2.Update = (
  requestId: string,
  settingsPoco: SimpleSweetAlertOptions
): void => {
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
  step: SimpleSweetAlertOptions,
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
