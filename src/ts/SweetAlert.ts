import Swal, {
  SweetAlertArrayOptions,
  SweetAlertIcon,
  SweetAlertOptions,
  SweetAlertResult,
  SweetAlertUpdatableParameters,
} from "sweetalert2";
import { ColorScheme } from "./ColorScheme";
import { ColorSchemeDictionary } from "./ColorSchemeDictionary";
import EnumSweetAlertResult from "./EnumSweetAlertResult";
import SimpleSweetAlertOptions from "./SimpleSweetAlertOptions";
import { SweetAlertTheme } from "./SweetAlertTheme";

interface SwalWindow extends Window {
  CurrieTechnologies: any;
  Swal: typeof Swal;
}

declare let window: SwalWindow;
const namespace = "CurrieTechnologies.Razor.SweetAlert2";

window.Swal = Swal;

disableProtestware();

function getEnumNumber(enumString: Swal.DismissReason) {
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

function getEnumString(enumNumber: number | undefined) {
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

function getStringVersion(input: unknown) {
  if (input instanceof Object) {
    return JSON.stringify(input);
  }
  return String(input);
}

function dispatchFireResult(requestId: string, result: SweetAlertResult) {
  const myResult = result as EnumSweetAlertResult;
  myResult.value =
    myResult.value !== undefined ? getStringVersion(myResult.value) : undefined;
  myResult.dismiss =
    myResult.dismiss !== undefined
      ? getEnumNumber(myResult.dismiss)
      : undefined;
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceiveFireResult",
    requestId,
    myResult
  );
}

function dispatchPreConfirm(requestId: string, inputValue: unknown) {
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreConfirmInput",
    requestId,
    getStringVersion(inputValue)
  );
}

function dispatchPreDeny(requestId: string, inputValue: unknown) {
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceivePreDenyInput",
    requestId,
    getStringVersion(inputValue)
  );
}

function dispatchInputValidator(requestId: string, inputValue: unknown) {
  return DotNet.invokeMethodAsync(
    namespace,
    "ReceiveInputValidatorInput",
    requestId,
    inputValue
  );
}

function dispatchDidOpen(requestId: string) {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidOpenInput", requestId);
}

function dispatchWillClose(requestId: string) {
  DotNet.invokeMethodAsync(namespace, "ReceiveWillCloseInput", requestId);
}

function dispatchDidRender(requestId: string) {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidRenderInput", requestId);
}

function dispatchWillOpen(requestId: string) {
  DotNet.invokeMethodAsync(namespace, "ReceiveWillOpenInput", requestId);
}

function dispatchDidClose(requestId: string) {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidCloseInput", requestId);
}

function dispatchDidDestroy(requestId: string) {
  DotNet.invokeMethodAsync(namespace, "ReceiveDidDestroyInput", requestId);
}

function numberStringToNumber(numberString: string) {
  return Number.isNaN(Number(numberString))
    ? numberString
    : Number(numberString);
}

function cleanSettings(settings: SimpleSweetAlertOptions) {
  const settingsToReturn = settings as any;
  for (const propName in settingsToReturn) {
    if (
      settingsToReturn[propName] === null ||
      settingsToReturn[propName] === undefined
    ) {
      delete settingsToReturn[propName];
    }
  }

  return settingsToReturn as SimpleSweetAlertOptions;
}

function getSwalSettingsFromPoco(
  settings: SimpleSweetAlertOptions,
  requestId: string
) {
  const swalSettings = cleanSettings(settings) as
    | SimpleSweetAlertOptions
    | SweetAlertOptions as SweetAlertOptions;

  function processPreConfirmDenyResult(value: unknown) {
    if (value === null) {
      return undefined;
    }
    if (value === "false") {
      return false;
    }
    return value;
  }

  if (settings.preConfirm) {
    swalSettings.preConfirm = (inputValue) =>
      dispatchPreConfirm(requestId, inputValue).then(
        processPreConfirmDenyResult
      );
  } else {
    delete swalSettings.preConfirm;
  }

  if (settings.preDeny) {
    swalSettings.preDeny = (inputValue) =>
      dispatchPreDeny(requestId, inputValue).then(processPreConfirmDenyResult);
  } else {
    delete swalSettings.preDeny;
  }

  if (settings.inputValidator) {
    swalSettings.inputValidator = (inputValue) =>
      dispatchInputValidator(requestId, inputValue) as Promise<string>;
  } else {
    delete swalSettings.inputValidator;
  }

  if (settings.willOpen) {
    swalSettings.willOpen = () => dispatchWillOpen(requestId);
  } else {
    delete swalSettings.willOpen;
  }

  if (settings.didClose) {
    swalSettings.didClose = () => dispatchDidClose(requestId);
  } else {
    delete swalSettings.didClose;
  }

  if (settings.didDestroy) {
    swalSettings.didDestroy = () => dispatchDidDestroy(requestId);
  } else {
    delete swalSettings.didDestroy;
  }

  if (settings.didOpen) {
    swalSettings.didOpen = () => dispatchDidOpen(requestId);
  } else {
    delete swalSettings.didOpen;
  }

  if (settings.willClose) {
    swalSettings.willClose = () => dispatchWillClose(requestId);
  } else {
    delete swalSettings.willClose;
  }

  if (settings.didRender) {
    swalSettings.didRender = () => dispatchDidRender(requestId);
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

function getFileNameByTheme(theme: SweetAlertTheme) {
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

function getColorSchemeName(colorScheme: ColorScheme) {
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

function setTheme(
  theme: SweetAlertTheme,
  colorSchemeThemes: ColorSchemeDictionary
) {
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
    styleTag.href = `_content/CurrieTechnologies.Razor.SweetAlert2/${getFileNameByTheme(
      theme
    )}`;
    styleTag.setAttribute("data-theme-number", String(theme));
    head.appendChild(styleTag);
  }

  colorSchemeMap.forEach((theme, colorScheme) => {
    const schemeTagId = `currietechnologies-razor-sweetalert2-scheme-link-${colorScheme}`;
    const existingSchemeTag = document.getElementById(schemeTagId);
    if (existingSchemeTag !== null) {
      const currentThemeForScheme = Number(
        existingSchemeTag.dataset.themeNumber
      );
      if (currentThemeForScheme === theme) {
        return;
      }
      existingSchemeTag.remove();
    }

    const schemeTag = document.createElement("link");
    schemeTag.rel = "stylesheet";
    schemeTag.id = schemeTagId;
    schemeTag.href = `_content/CurrieTechnologies.Razor.SweetAlert2/${getFileNameByTheme(
      theme
    )}`;
    schemeTag.setAttribute("data-theme-number", String(theme));
    schemeTag.setAttribute("data-scheme-number", String(colorScheme));
    schemeTag.setAttribute(
      "media",
      `(prefers-color-scheme: ${getColorSchemeName(colorScheme)})`
    );

    const head = document.getElementsByTagName("head")[0];
    head.appendChild(schemeTag);
  });
}

window.CurrieTechnologies = window.CurrieTechnologies ?? {};
window.CurrieTechnologies.Razor = window.CurrieTechnologies.Razor ?? {};
window.CurrieTechnologies.Razor.SweetAlert2 =
  window.CurrieTechnologies.Razor.SweetAlert2 ?? {};

const razorSwal = window.CurrieTechnologies.Razor.SweetAlert2;

razorSwal.SendThemesToJS = (
  theme: SweetAlertTheme,
  colorSchemeThemes: ColorSchemeDictionary
) => {
  setTheme(theme, colorSchemeThemes);
};

razorSwal.Fire = (
  requestId: string,
  title: string | null,
  message: string | null,
  icon: SweetAlertIcon | null
) => {
  const params: SweetAlertArrayOptions = [
    title ?? undefined,
    message ?? undefined,
    icon ?? undefined,
  ];
  Swal.fire(params[0], params[1], params[2]).then((result) => {
    dispatchFireResult(requestId, result);
  });
};

razorSwal.FireSettings = (
  requestId: string,
  settingsPoco: SimpleSweetAlertOptions
) => {
  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId);

  Swal.fire(swalSettings).then((result) => {
    dispatchFireResult(requestId, result);
  });
};

razorSwal.IsVisible = () => {
  return !!Swal.isVisible();
};

razorSwal.Update = (
  requestId: string,
  settingsPoco: SimpleSweetAlertOptions
) => {
  const swalSettings = getSwalSettingsFromPoco(settingsPoco, requestId);
  Swal.update(swalSettings);
};

razorSwal.CloseResult = (result: SweetAlertResult) => {
  const dismissString = getEnumString(result.dismiss);
  Swal.close({ ...result, dismiss: dismissString });
};

razorSwal.Close = () => {
  Swal.close();
};

razorSwal.EnableButtons = () => {
  Swal.enableButtons();
};

razorSwal.DisableButtons = () => {
  Swal.disableButtons();
};

razorSwal.ShowLoading = () => {
  Swal.showLoading();
};

razorSwal.HideLoading = () => {
  Swal.hideLoading();
};

razorSwal.IsLoading = () => {
  return Swal.isLoading();
};

razorSwal.ClickConfirm = () => {
  Swal.clickConfirm();
};

razorSwal.ClickDeny = () => {
  Swal.clickDeny();
};

razorSwal.ClickCancel = () => {
  Swal.clickCancel();
};

razorSwal.ShowValidationMessage = (validationMessage: string) => {
  Swal.showValidationMessage(validationMessage);
};

razorSwal.ResetValidationMessage = () => {
  Swal.resetValidationMessage();
};

razorSwal.DisableInput = () => {
  Swal.disableInput();
};

razorSwal.EnableInput = () => {
  Swal.enableInput();
};

razorSwal.GetTimerLeft = () => {
  return Swal.getTimerLeft();
};

razorSwal.StopTimer = () => {
  return Swal.stopTimer();
};

razorSwal.ResumeTimer = () => {
  return Swal.resumeTimer();
};

razorSwal.ToggleTimer = () => {
  return Swal.toggleTimer();
};

razorSwal.IsTimerRunning = () => {
  return Swal.isTimerRunning();
};

razorSwal.IncreaseTimer = (n: number) => {
  return Swal.increaseTimer(n);
};

razorSwal.IsValidParameter = (paramName: keyof SweetAlertOptions) => {
  return Swal.isValidParameter(paramName);
};

razorSwal.IsUpdatableParameter = (paramName: SweetAlertUpdatableParameters) => {
  return Swal.isUpdatableParameter(paramName);
};

function disableProtestware() {
  const protestwareObserver = new MutationObserver((mutations) => {
    mutations.forEach((childListMutation) => {
      childListMutation.addedNodes.forEach((addedNode) => {
        if (addedNode.nodeName.toLowerCase() === "audio") {
          const audioNode = addedNode as HTMLAudioElement;
          if (
            audioNode.src ===
            "https://flag-gimn.ru/wp-content/uploads/2021/09/Ukraina.mp3"
          ) {
            protestwareObserver.disconnect();
            audioNode.remove();
            if (document.body.style.pointerEvents === "none") {
              document.body.style.pointerEvents = "auto";
            }
            console.debug("SweetAlert2 protestware disabled.");
            return;
          }
        }
      });
    });
  });

  protestwareObserver.observe(document.body, { childList: true });
}
