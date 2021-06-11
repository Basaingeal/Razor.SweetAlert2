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
import EnumSweetAlertResult from "./EnumSweetAlertResult";
import { ColorSchemeDictionary } from "./ColorSchemeDictionary";

declare const DotNet: any;
const domWindow = window as any;
const namespace = "CurrieTechnologies.Razor.SweetAlert2";

domWindow.Swal = Swal;

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
  const myResult = result as SweetAlertResult | EnumSweetAlertResult as EnumSweetAlertResult;
  myResult.value = myResult.value !== undefined ? getStringVersion(myResult.value) : undefined;
  myResult.dismiss = myResult.dismiss !== undefined ? getEnumNumber(myResult.dismiss) : undefined;
  return DotNet.invokeMethodAsync(namespace, "ReceiveFireResult", requestId, myResult);
}

function dispatchPreConfirm(requestId: string, inputValue: any): Promise<any> {
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreConfirmInput",
    requestId,
    getStringVersion(inputValue)
  );
}

function dispatchPreDeny(requestId: string, inputValue: any): Promise<any> {
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreDenyInput",
    requestId,
    getStringVersion(inputValue)
  );
}

function dispatchInputValidator(requestId: string, inputValue: any): Promise<string> {
  return DotNet.invokeMethodAsync(namespace, "ReceiveInputValidatorInput", requestId, inputValue);
}

function dispatchDidOpen(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidOpenInput", requestId);
}

function dispatchWillClose(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveWillCloseInput", requestId);
}

function dispatchDidRender(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidRenderInput", requestId);
}

function dispatchWillOpen(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveWillOpenInput", requestId);
}

function dispatchDidClose(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidCloseInput", requestId);
}

function dispatchDidDestroy(requestId: string): void {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidDestroyInput", requestId);
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
  requestId: string
): SweetAlertOptions {
  const swalSettings = cleanSettings(settings) as
    | SimpleSweetAlertOptions
    | SweetAlertOptions as SweetAlertOptions;

  function processPreConfirmDenyResult(value: any) {
    if (value === null) {
      return undefined;
    }
    if (value === "false") {
      return false;
    }
    return value;
  }

  if (settings.preConfirm) {
    swalSettings.preConfirm = (inputValue): Promise<any> =>
      dispatchPreConfirm(requestId, inputValue).then(processPreConfirmDenyResult);
  } else {
    delete swalSettings.preConfirm;
  }

  if (settings.preDeny) {
    swalSettings.preDeny = (inputValue): Promise<any> =>
      dispatchPreDeny(requestId, inputValue).then(processPreConfirmDenyResult);
  } else {
    delete swalSettings.preDeny;
  }

  if (settings.inputValidator) {
    swalSettings.inputValidator = (inputValue): Promise<string> =>
      dispatchInputValidator(requestId, inputValue);
  } else {
    delete swalSettings.inputValidator;
  }

  if (settings.willOpen) {
    swalSettings.willOpen = (): void => dispatchWillOpen(requestId);
  } else {
    delete swalSettings.willOpen;
  }

  if (settings.didClose) {
    swalSettings.didClose = (): void => dispatchDidClose(requestId);
  } else {
    delete swalSettings.didClose;
  }

  if (settings.didDestroy) {
    swalSettings.didDestroy = (): void => dispatchDidDestroy(requestId);
  } else {
    delete swalSettings.didDestroy;
  }

  if (settings.didOpen) {
    swalSettings.didOpen = (): void => dispatchDidOpen(requestId);
  } else {
    delete swalSettings.didOpen;
  }

  if (settings.willClose) {
    swalSettings.willClose = (): void => dispatchWillClose(requestId);
  } else {
    delete swalSettings.willClose;
  }

  if (settings.didRender) {
    swalSettings.didRender = (): void => dispatchDidRender(requestId);
  } else {
    delete swalSettings.didRender;
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
      return "wordpressAdminTheme.min.css";
    }
    case SweetAlertTheme.Bulma: {
      return "bulmaTheme.min.css";
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

razorSwal.SendThemesToJS = (
  theme: SweetAlertTheme,
  colorSchemeThemes: ColorSchemeDictionary
): void => {
  setTheme(theme, colorSchemeThemes);
};

razorSwal.Fire = (
  requestId: string,
  title: string | null,
  message: string | null,
  icon: SweetAlertIcon | null
): void => {
  const params: SweetAlertArrayOptions = [
    title ?? undefined,
    message ?? undefined,
    icon ?? undefined,
  ];
  Swal.fire(params[0], params[1], params[2]).then((result): void => {
    dispatchFireResult(requestId, result);
  });
};

razorSwal.FireSettings = (requestId: string, settingsPoco: SimpleSweetAlertOptions): void => {
  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId);

  Swal.fire(swalSettings).then((result): void => {
    dispatchFireResult(requestId, result);
  });
};

razorSwal.IsVisible = (): boolean => {
  return !!Swal.isVisible();
};

razorSwal.Update = (requestId: string, settingsPoco: SimpleSweetAlertOptions): void => {
  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId);
  Swal.update(swalSettings);
};

razorSwal.CloseResult = (result: SweetAlertResult): void => {
  const dismissString = getEnumString(result.dismiss as any as number);
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

razorSwal.ClickDeny = (): void => {
  Swal.clickDeny();
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

razorSwal.IsValidParameter = (paramName: keyof SweetAlertOptions): boolean => {
  return Swal.isValidParameter(paramName);
};

razorSwal.IsUpdatableParameter = (paramName: SweetAlertUpdatableParameters): boolean => {
  return Swal.isUpdatableParameter(paramName);
};
