# v4.0.0

## 🔴 Breaking Changes

Just a couple of releases after `sweetalert2@10` was released, they renamed the lifecycle hooks and deprecated the old names. (https://github.com/sweetalert2/sweetalert2/pull/2057)
As a rule, this wrapper library does not include deprecated functionality.
As a result, the old lifecycle hook names have been completely replaced with the new ones, resulting in a new major version.

### Hook name changes

Hooks were previously named as follows:

| Before Hook    | After hook     |
| -------------- | -------------- |
| `OnBeforeOpen` | `OnOpen`       |
|                | `OnRender`     |
| `OnClose`      | `OnAfterClose` |
|                | `OnDestroy`    |

All have been renamed according to the convention used by frameworks like UIKit or React:

| Before Hook | After hook  |
| ----------- | ----------- |
| `WillOpen`  | `DidOpen`   |
|             | `DidRender` |
| `WillClose` | `DidClose`  |
|             | `DidDstroy` |

## 🎉 New Features

### Additional `SweetAlertOptions` properties

- `InputLabel`
- `CustomClass.ValidationMessage`

## Dependencies

- bump `sweetalert2` to `10.5.1`
- bump `Microsoft.AspNetCore.Components` to `3.1.9`
- bump `Microsoft.AspNetCore.Components.Web` to `3.1.9`

### Webpack 5

Webpack 5 is now used behind the scenes to create the JavaScript component of the wrapper, which uses more modern JavaScript syntax for slightly smaller file sizes.

**NB:** *The IE11 Compat version wich contains some polyfills still uses the more traditional JS syntax.*
