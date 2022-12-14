# v5.4.0

## Notable changes

### Remove explicit .NET Core 3.1 support

This library no longer targets .NET Core 3.1 as it is no longer a supported version of .NET. The library still targets .NET Standard 2.0, so apps that are still on .NET Core 3.1 should still be able to use it.

### Public `IAsyncSweetAlertService` interface

The `IAsyncSweetAlertService` interface has been made public by request. (https://github.com/Basaingeal/Razor.SweetAlert2/issues/2183)

### Protestware

The author of the `sweetalert2` library added protestware concerning the war between Russia and Ukraine. (https://github.com/sweetalert2/sweetalert2/pull/2462) ([SNYK: Undesired Behavior in sweetalert2](https://security.snyk.io/vuln/SNYK-JS-SWEETALERT2-2774674))

This protestware was recently updated to auto-play the Ukrainian national anthem in addition to disabling all user events. This library will now detect when the `<audio>` element that plays the anthem has been added to the document, and removes it.
  
## Dependencies

- bump `sweetalert2` to `11.6.15`
- bump `@sweetalert2/themes` to `5.0.15`

