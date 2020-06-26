import { ColorScheme } from "./ColorScheme";
import { SweetAlertTheme } from "./SweetAlertTheme";
import Swal, {
  SweetAlertOptions,
  SweetAlertResult,
  SweetAlertIcon,
  SweetAlertArrayOptions,
  SweetAlertUpdatableParameters,
} from "sweetalert2";
import SimpleSweetAlertOptions from "./SimpleSweetAlertOptions";
import SweetAlertQueueResult from "./SweetAlertQueueResult";
import EnumSweetAlertResult from "./EnumSweetAlertResult";
import { ColorSchemeDictionary } from "./ColorSchemeDictionary";

declare const DotNet: any;
const domWindow = window as any;
const namespace = "CurrieTechnologies.Razor.SweetAlert2";

function getEnumNumber(enumString: Swal.DismissReason): number | undefined {
  switch (enumString) {
    case Swal.DismissReason.cancel:
      return 0;
    case Swal.DismissReason.backdrop:
      return 1;
    case Swal.DismissReason.close:
      return 2;
    case Swal.DismissReason.esc:
      return 3;
    case Swal.DismissReason.timer:
      return 4;
  }

  return undefined;
}

function getEnumString(enumNumber: number): Swal.DismissReason | undefined {
  switch (enumNumber) {
    case 0:
      return Swal.DismissReason.cancel;
    case 1:
      return Swal.DismissReason.backdrop;
    case 2:
      return Swal.DismissReason.close;
    case 3:
      return Swal.DismissReason.esc;
    case 4:
      return Swal.DismissReason.timer;
  }

  return undefined;
}

function getStringVersion(input: any): string {
  if (input instanceof Object) {
    return JSON.stringify(input);
  }
  return String(input);
}

function dispatchFireResult(requestId: string, result: SweetAlertResult): Promise<void> {
  const myResult = (result as SweetAlertResult | EnumSweetAlertResult) as EnumSweetAlertResult;
  myResult.value = myResult.value !== undefined ? getStringVersion(myResult.value) : undefined;
  myResult.dismiss = myResult.dismiss !== undefined ? getEnumNumber(myResult.dismiss) : undefined;
  return DotNet.invokeMethodAsync(namespace, "ReceiveFireResult", requestId, myResult);
}

const flatten = (arr: any[]): any[] =>
  arr.reduce((flat, next): any[] => flat.concat(Array.isArray(next) ? flatten(next) : next), []);
//arr.flat(Infinity)

function dispatchQueueResult(requestId: string, result: SweetAlertResult): Promise<void> {
  const queueResult = result as SweetAlertQueueResult;
  queueResult.value =
    queueResult.value !== undefined
      ? flatten(queueResult.value).map((v: any): string | undefined =>
          v !== undefined ? getStringVersion(v) : undefined
        )
      : undefined;
  queueResult.dismiss =
    queueResult.dismiss !== undefined ? getEnumNumber(queueResult.dismiss) : undefined;
  return DotNet.invokeMethodAsync(namespace, "ReceiveQueueResult", requestId, queueResult);
}

function dispatchPreConfirm(requestId: string, inputValue: any): Promise<any> {
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreConfirmInput",
    requestId,
    getStringVersion(inputValue)
  );
}

function dispatchQueuePreConfirm(requestId: string, inputValue: any): Promise<any> {
  const valArray: any[] = Array.isArray(inputValue) ? inputValue : [inputValue];

  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreConfirmQueueInput",
    requestId,
    valArray.map((v): string => getStringVersion(v))
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

function dispatchOnRender(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnRenderInput", requestId);
}

function dispatchOnBeforeOpen(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnBeforeOpenInput", requestId);
}

function dispatchOnAfterClose(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnAfterCloseInput", requestId);
}

function dispatchOnDestroy(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveOnDestroyInput", requestId);
}

function numberStringToNumber(numberString: string): string | number {
  return Number.isNaN(Number(numberString)) ? numberString : Number(numberString);
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
  const swalSettings = (cleanSettings(settings) as
    | SimpleSweetAlertOptions
    | SweetAlertOptions) as SweetAlertOptions;

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

  if (settings.onDestroy) {
    swalSettings.onDestroy = (): void => dispatchOnDestroy(requestId);
  } else {
    delete swalSettings.onDestroy;
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

  if (settings.onRender) {
    swalSettings.onRender = (): void => dispatchOnRender(requestId);
  } else {
    delete swalSettings.onRender;
  }

  if (settings.grow === "false") {
    swalSettings.grow = false;
  } else if (settings.grow == null) {
    delete swalSettings.grow;
  } else {
    swalSettings.grow = settings.grow;
  }

  if (settings.width) {
    swalSettings.width = numberStringToNumber(settings.width);
  }

  return swalSettings;
}

function getFileNameByTheme(theme: SweetAlertTheme): string {
  switch (theme) {
    case SweetAlertTheme.Dark: {
      return "darkTheme.min.css";
    }
    case SweetAlertTheme.Minimal: {
      return "minimalTheme.min.css";
    }
    case SweetAlertTheme.Borderless: {
      return "borderlessTheme.min.css";
    }
    case SweetAlertTheme.Bootstrap4: {
      return "bootstrap4Theme.min.css";
    }
    case SweetAlertTheme.MaterialUI: {
      return "materialUITheme.min.css";
    }
    case SweetAlertTheme.WordpressAdmin: {
      return "wordpressAdminTheme.css";
    }
    case SweetAlertTheme.Default:
    default: {
      return "defaultTheme.min.css";
    }
  }
}

function getColorSchemeName(colorScheme: ColorScheme): string {
  switch (colorScheme) {
    case ColorScheme.Light:
      return "light";
    case ColorScheme.Dark:
      return "dark";
    case ColorScheme.NoPreference:
    default:
      return "no-preference";
  }
}

function setTheme(theme: SweetAlertTheme, colorSchemeThemes: ColorSchemeDictionary): void {
  const colorSchemeMap: Map<ColorScheme, SweetAlertTheme> = new Map();
  colorSchemeThemes.forEach((pair) => {
    colorSchemeMap.set(pair[0], pair[1]);
  });

  const tagId = "currietechnologies-razor-sweetalert2-theme-link";

  const existingThemeTag = document.getElementById(tagId);
  let themeShouldBeSet = true;
  if (existingThemeTag !== null) {
    const currentTheme = Number(existingThemeTag.dataset.themeNumber);
    if (currentTheme === theme) {
      themeShouldBeSet = false;
    } else {
      existingThemeTag.remove();
      themeShouldBeSet = true;
    }
  }

  if (theme !== SweetAlertTheme.Default && themeShouldBeSet) {
    const head = document.getElementsByTagName("head")[0];
    const styleTag = document.createElement("link");
    styleTag.rel = "stylesheet";
    styleTag.id = tagId;
    styleTag.href = `_content/CurrieTechnologies.Razor.SweetAlert2/${getFileNameByTheme(theme)}`;
    styleTag.setAttribute("data-theme-number", String(theme));
    head.appendChild(styleTag);
  }

  colorSchemeMap.forEach((theme, colorScheme) => {
    const schemeTagId = `currietechnologies-razor-sweetalert2-scheme-link-${colorScheme}`;
    const existingSchemeTag = document.getElementById(schemeTagId);
    if (existingSchemeTag !== null) {
      const currentThemeForScheme = Number(existingSchemeTag.dataset.themeNumber);
      if (currentThemeForScheme === theme) {
        return;
      }
      existingSchemeTag.remove();
    }

    const schemeTag = document.createElement("link");
    schemeTag.rel = "stylesheet";
    schemeTag.id = schemeTagId;
    schemeTag.href = `_content/CurrieTechnologies.Razor.SweetAlert2/${getFileNameByTheme(theme)}`;
    schemeTag.setAttribute("data-theme-number", String(theme));
    schemeTag.setAttribute("data-scheme-number", String(colorScheme));
    schemeTag.setAttribute("media", `(prefers-color-scheme: ${getColorSchemeName(colorScheme)})`);

    const head = document.getElementsByTagName("head")[0];
    head.appendChild(schemeTag);
  });
}

domWindow.CurrieTechnologies = domWindow.CurrieTechnologies ?? {};
domWindow.CurrieTechnologies.Razor = domWindow.CurrieTechnologies.Razor ?? {};
domWindow.CurrieTechnologies.Razor.SweetAlert2 =
  domWindow.CurrieTechnologies.Razor.SweetAlert2 ?? {};

const razorSwal = domWindow.CurrieTechnologies.Razor.SweetAlert2;

razorSwal.Fire = (
  requestId: string,
  title: string | null,
  message: string | null,
  icon: SweetAlertIcon | null,
  theme: number,
  colorSchemeThemes: ColorSchemeDictionary
): void => {
  setTheme(theme, colorSchemeThemes);

  const params: SweetAlertArrayOptions = [
    title ?? undefined,
    message ?? undefined,
    icon ?? undefined,
  ];
  Swal.fire(params[0], params[1], params[2]).then((result): void => {
    dispatchFireResult(requestId, result);
  });
};

razorSwal.FireSettings = (
  requestId: string,
  settingsPoco: SimpleSweetAlertOptions,
  theme: number,
  colorSchemeThemes: ColorSchemeDictionary
): void => {
  setTheme(theme, colorSchemeThemes);

  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId, false);

  Swal.fire(swalSettings).then((result): void => {
    dispatchFireResult(requestId, result);
  });
};

razorSwal.Queue = (
  requestId: string,
  optionIds: string[],
  steps: SimpleSweetAlertOptions[],
  theme: number,
  colorSchemeThemes: ColorSchemeDictionary
): void => {
  setTheme(theme, colorSchemeThemes);

  const arrSwalSettings: SweetAlertOptions[] = optionIds.map(
    (optionId, i): SweetAlertOptions => getSwalSettingsFromPoco(steps[i], optionId, true)
  );

  Swal.queue(arrSwalSettings).then((result: any): void => {
    dispatchQueueResult(requestId, result);
  });
};

razorSwal.IsVisible = (): boolean => {
  return !!Swal.isVisible();
};

razorSwal.Update = (requestId: string, settingsPoco: SimpleSweetAlertOptions): void => {
  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId, false);
  Swal.update(swalSettings);
};

razorSwal.CloseResult = (result: SweetAlertResult): void => {
  const dismissString = getEnumString((result.dismiss as any) as number);
  Swal.close({ ...result, dismiss: dismissString });
};

razorSwal.Close = (): void => {
  Swal.close();
};

razorSwal.EnableButtons = (): void => {
  Swal.enableButtons();
};

razorSwal.DisableButtons = (): void => {
  Swal.disableButtons();
};

razorSwal.ShowLoading = (): void => {
  Swal.showLoading();
};

razorSwal.HideLoading = (): void => {
  Swal.hideLoading();
};

razorSwal.IsLoading = (): boolean => {
  return Swal.isLoading();
};

razorSwal.ClickConfirm = (): void => {
  Swal.clickConfirm();
};

razorSwal.ClickCancel = (): void => {
  Swal.clickCancel();
};

razorSwal.ShowValidationMessage = (validationMessage: string): void => {
  Swal.showValidationMessage(validationMessage);
};

razorSwal.ResetValidationMessage = (): void => {
  Swal.resetValidationMessage();
};

razorSwal.DisableInput = (): void => {
  Swal.disableInput();
};

razorSwal.EnableInput = (): void => {
  Swal.enableInput();
};

razorSwal.GetTimerLeft = (): number | undefined => {
  return Swal.getTimerLeft();
};

razorSwal.StopTimer = (): number | undefined => {
  return Swal.stopTimer();
};

razorSwal.ResumeTimer = (): number | undefined => {
  return Swal.resumeTimer();
};

razorSwal.ToggleTimer = (): number | undefined => {
  return Swal.toggleTimer();
};

razorSwal.IsTimerRunning = (): boolean | undefined => {
  return Swal.isTimerRunning();
};

razorSwal.IncreaseTimer = (n: number): number | undefined => {
  return Swal.increaseTimer(n);
};

razorSwal.GetQueueStep = (): string | null => {
  return Swal.getQueueStep();
};

razorSwal.InsertQueueStep = (
  requestId: string,
  step: SimpleSweetAlertOptions,
  index?: number
): number => {
  const stepSettings = getSwalSettingsFromPoco(step, requestId, true);
  return Swal.insertQueueStep(stepSettings, index);
};

razorSwal.DeleteQueueStep = (index: number): void => {
  Swal.deleteQueueStep(index);
};

razorSwal.IsValidParameter = (paramName: keyof SweetAlertOptions): boolean => {
  return Swal.isValidParameter(paramName);
};

razorSwal.IsUpdatableParameter = (paramName: SweetAlertUpdatableParameters): boolean => {
  return Swal.isUpdatableParameter(paramName);
};
