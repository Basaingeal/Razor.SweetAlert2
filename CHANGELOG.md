# v2.0.0

`sweetalert2` released `v9` which introduced some breaking changes and new deprecations. (Remember, this library does not include deprecated items.)

## Breaking Changes

### 🔴 Breaking change #1 - rename `Type` to `Icon`

Old

```cs
Swal.FireAsync(new SweetAlertOptions{
  Type = SweetAlertType.Success
});
```

New

```cs
Swal.FireAsync(new SweetAlertOptions{
  Icon = SweetAlertIcon.Success
});
```

_Implicit string conversion still works the same way._

```cs
Swal.FireAsync(new SweetAlertOptions{
  Icon = "success"
});
```

### 🔴 Breaking change #2 - Deprecated API methods and params were removed

* `SweetAlertOptions.Animated` removed. (use `SweetAlertOptions.ShowClass` and `SweetAlertOptions.HideClass` now.)
* `SweetAlertService.ShowProgressStepsAsync()` and `SweetAlertService.HideProgressStepsAsync()` removed.

## New Features

### 🎉 `ShowClass` and `HideClass`

Now, it's possible to change showing/hiding animations of popups:

```cs
Swal.FireAsync(new SweetAlertOptions{
  ShowClass = new SweetAlertShowClass {
    Popup = "...",
    Backdrop: "...",
    Icon: "..."
  },
  HideClass = new SweetAlertHideClass {
    Popup = "...",
    Backdrop: "...",
    Icon: "..."
  }
});
```

### 🎉 `IconHtml`

Use any HTML inside icons (e.g. Font Awesome)

```cs
Swal.FireAsync(new SweetAlertOptions {
  Icon = "success",
  IconHtml = "<i class=\"far fa-thumbs-up\"></i>"
});
```
