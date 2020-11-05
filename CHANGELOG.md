# v4.3.0

## 🎉 New Features

### Expose JavaScript Swal object to DOM window

Previously, the `Swal` JavaScript object was used by JSInterop but not available through `window.Swal`. That meant that you couldn't write your own JavaScript snippets that reference `Swal` without duplicating the library through a CDN tag. 

Now, if you add the `<script src="_content/CurrieTechnologies.Razor.SweetAlert2/sweetAlert2.min.js"></script>` tag to your page, that page has access to `Swal` and you can do custom functionality like assigning event listeners, and in this issue: (https://github.com/Basaingeal/Razor.SweetAlert2/issues/1012)

#### Example

Previously, this was not possible due to `Swal` not being available on the DOM. It will now work.

```html
<script src="_content/CurrieTechnologies.Razor.SweetAlert2/sweetAlert2.min.js"></script>
<script>
    function bindSwalMouseOver() {
        var toast = Swal.getContainer();

        toast.removeEventListener('mouseover', Swal.stopTimer);
        toast.removeEventListener('mouseleave', Swal.resumeTimer);

        toast.addEventListener('mouseenter', Swal.stopTimer);
        toast.addEventListener('mouseleave', Swal.resumeTimer);
    }
</script>
```
