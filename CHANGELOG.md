# v5.0.0

## Breaking Changes

### 🔴 Breaking change # 1 - IE11 and Legacy Edge support is DISCONTINUED

If you need to support these old browsers in your project, please use the previous major release [v4.5.0](https://github.com/Basaingeal/Razor.SweetAlert2/releases/tag/v4.5.0)

### 🔴 Breaking change # 2 - `.QueueAsync()`, `.GetQueueStepAsync()`, `.InsertQueueStepAsync()`, `.DeleteQueueStepAsync()` methods are REMOVED

`async/await` can perfectly replace all use-cases of `.QueueAsync()`.


## 💅 Styling Changes

`sweetalert2@11` introduced a variety of styling changes.

- Update button color
- Switch to CSS Grid Layout
- Refreshed look for toasts
- Loaded in toasts moved to the left side (instead of the icon)

Examples and explainations for these changes can be found on the sweetalert2 v11.0.0 [release notes](https://github.com/sweetalert2/sweetalert2/releases/tag/v11.0.0).

## 🎉 New features

### Default Settings

You can now specify global defaults during Startup for your SweetAlertOptions that apply to all your SweetAlert2 dialogs.

```csharp
services.AddSweetAlert2(options => {
 options.DefaultOptions = new SweetAlertOptions {
   HeightAuto = false
 };
});
```

View the [README.md](https://github.com/Basaingeal/Razor.SweetAlert2/blob/develop/README.md#default-settings) for more information

## Dependencies

- bump `sweetalert2` to `11.0.17`
- bump `@sweetalert2/themes` to `5.0.0`

### Multi-targetting enabled

Blazor dependencies are now dependant on which version of .NET is being used in the application.

#### .NETCoreApp 3.1

- `Microsoft.AspNetCore.Components` (>= `3.1.0` && < `5.0.0`)
- `Microsoft.AspNetCore.Components.Web` (>= `3.1.0` && < `5.0.0`)

#### .NETStandard 2.0
- `Microsoft.AspNetCore.Components` (>= `3.1.0` && < `5.0.0`)
- `Microsoft.AspNetCore.Components.Web` (>= `3.1.0` && < `5.0.0`)

#### net5.0
- `Microsoft.AspNetCore.Components` (>= `5.0.0` && < `6.0.0`)
- `Microsoft.AspNetCore.Components.Web` (>= `5.0.0` && < `6.0.0`)


