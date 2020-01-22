# v2.5.0

- bump `sweetalert2` to `9.7.0`
- New options property: `OnDestroy`

From the original PR in the `sweetalert2` repository (https://github.com/sweetalert2/sweetalert2/pull/1872#issue-365026220):

> The difference between `onAfterClose` and `onDestroy` is that `onAfterClose` is called for user interactions only (clicks). `onDestroy` is called always, both for user interactions and popup being closed by another popup.

In my testing, `Swal.CloseAsync()` will trigger both `OnAfterClose` and `OnDestroy`.
