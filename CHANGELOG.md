## Enhancements
* Bump `sweetalert2` to `8.16.3`
  * Bug Fix: **types**: Swal.close() now takes the value to resolve with, not a callback (https://github.com/sweetalert2/sweetalert2/commit/8def219)

## Bug Fixes / Breaking Changes
* Releated to the SweetAlert2 bug fix: The OnClose functionality was based off of the types declaration in the sweetalert2 library. As a result `OnCloseAsync()` used to take in a callback to be enacted on close. Turns out, that's not the actual functionality of the library. It actually takes a SweetAlertResult to resolve the initial request with. So `OnCloseAsync()` now (optionally) take a `SweetAlertResult` or a `SweetAlertQueueResult` instead of a `SweetAlertCallback`