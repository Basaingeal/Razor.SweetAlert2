## Enhancements
* Bump `sweetalert2` to `8.17.1`
  * Feature: add onRender lifecycle hook (https://github.com/sweetalert2/sweetalert2/issues/1729) (https://github.com/sweetalert2/sweetalert2/commit/bdcc35c)
  * Bug Fix: **types:** more precise SweetAlertArrayOptions type (https://github.com/sweetalert2/sweetalert2/commit/e0225e7)
* Added `OnRender` callback in `SweetAlertOptions`. Callback is triggered after `FireAsync()` and `UpdateAsync()`. See (https://github.com/sweetalert2/sweetalert2/issues/1729) and (https://github.com/sweetalert2/sweetalert2/issues/1728) for more information.