<p align="center">
<span style="font-size:x-large">Server-side Blazor</span>
<br>
+
<br>
  <a href="https://sweetalert2.github.io/">
    <img src="https://raw.github.com/sweetalert2/sweetalert2/master/assets/swal2-logo.png" alt="SweetAlert2">
  </a>
</p>

<p align="center">
  A beautiful, responsive, customizable, accessible (WAI-ARIA) replacement for JavaScript's popup boxes.
</p>

<p align="center">
  <a href="https://sweetalert2.github.io/">
    <img src="https://raw.github.com/sweetalert2/sweetalert2/master/assets/sweetalert2.gif" width="562"><br>
    See SweetAlert2 in action ↗
  </a>
</p>

---
[![Build Status](https://dev.azure.com/michaeljcurrie136/CurrieTechnologies.Blazor/_apis/build/status/Basaingeal.Razor.SweetAlert2?branchName=master)](https://dev.azure.com/michaeljcurrie136/CurrieTechnologies.Blazor/_build/latest?definitionId=16&branchName=master)
![Nuget](https://img.shields.io/nuget/v/CurrieTechnologies.Razor.SweetAlert2.svg?style=popout)

## This package is for Server-side Blazor only. For Client-side Blazor use [CurrieTechnologies.Blazor.SweetAlert2](https://github.com/Basaingeal/Blazor.SweetAlert2)

### 🙌 Includes themes from the [Official SweetAlert2 Themes project](https://github.com/sweetalert2/sweetalert2-themes) 🙌

Installation
------------

```sh
Install-Package CurrieTechnologies.Razor.SweetAlert2
```

Or grab from [Nuget](https://www.nuget.org/packages/CurrieTechnologies.Razor.SweetAlert2/)


Usage
-----
Register the service in your Startup file.
```cs
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
...
	services.AddSweetAlert2();
...
}
```

**OR**

If you want to use one of the Official SweetAlert2 themes
```cs
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
...
	services.AddSweetAlert2(options => {
		options.Theme = SweetAlertTheme.Dark;
	});
...
}
```

Add this script tag in  your root html file (Likely _Host.cshtml), right under the `<script src="_framework/blazor.server.js"></script>` tag.
```html
<script src="_content/currietechnologiesrazorsweetalert2/sweetalert2.min.js"></script>
```

Inject the SweetAlertService into any Blazor component
```cs
// Sample.razor
@inject SweetAlertService Swal;
<button class="btn btn-primary"
		@onclick="@(async () => await Swal.FireAsync("Any fool can use a computer"))">
	Try me!
</button>
```


Examples
--------

The most basic message:

```cs
await Swal.FireAsync("Hello world!");
```

A message signaling an error:

```cs
await Swal.FireAsync("Oops...", "Something went wrong!", "error");
```

Handling the result of SweetAlert2 modal:

```cs
// async/await
SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
	{
		Title = "Are you sure?",
		Text = "You will not be able to recover this imaginary file!",
		Type = SweetAlertType.Warning,
		ShowCancelButton = true,
		ConfirmButtonText = "Yes, delete it!",
		CancelButtonText = "No, keep it"
	});

if (!string.IsNullOrEmpty(result.Value))
{
	await Swal.FireAsync(
		"Deleted",
		"Your imaginary file has been deleted.",
		SweetAlertType.Success
		);
}
else if (result.Dismiss == DismissReason.Cancel)
{
	await Swal.FireAsync(
		"Cancelled",
		"Your imaginary file is safe :)",
		SweetAlertType.Error
		);
}

// Promise/Task based
Swal.FireAsync(new SweetAlertOptions
	{
		Title = "Are you sure?",
		Text = "You will not be able to recover this imaginary file!",
		Type = SweetAlertType.Warning,
		ShowCancelButton = true,
		ConfirmButtonText = "Yes, delete it!",
		CancelButtonText = "No, keep it"
	}).ContinueWith(swalTask => 
	{
		SweetAlertResult result = swalTask.Result;
		if (!string.IsNullOrEmpty(result.Value))
		{
			Swal.FireAsync(
				"Deleted",
				"Your imaginary file has been deleted.",
				SweetAlertType.Success
				);
		}
		else if (result.Dismiss == DismissReason.Cancel)
		{
			Swal.FireAsync(
				"Cancelled",
				"Your imaginary file is safe :)",
				SweetAlertType.Error
				);
		}
	});


```

## [More examples can be found on the SweetAlert2 project site](https://sweetalert2.github.io/)


Notable differences from the JavaScript library
---------------------
- No methods that return an HTMLElement are included (e. g. `Swal.getContainer()`)
- The value of a `SweetAlertResult` (`result.Value`) can only be a string (or a collection of strings if returned from a queue request). Numbers and booleans must be converted. Object must be parsed to/from JSON in your code.
- `OnOpenAsync()`, `OnCloseAsync()`, `OnBeforeOpenAsync()`, and `OnAfterCloseAsync()` can all take asynchronous callbacks. 🎉 (none will return an HTMLElement though.)
- Callbacks must be passed inside of objects specifically designed for the given callback property. e.g. the `InputValidator` property takes an `InputValidatorCallback` created like so:
```cs
new SweetAlertOptions {
	...
	InputValidator = new InputValidatorCallback((string input) => input.Length == 0 ? "Please provide a value" : null, this),
	...
}
```
`this` is passed in so that the Blazor `EventCallback` used behind the scenes can trigger a re-render if the state of the calling component was changed in the callback. If the callback does not require the calling component to re-render, passing in `this` is optional.
These callbacks are necessary because there is currently no way to create an `EventCallback` in Blazor that isn't a component parameter without using the `EventCallbackFactory` which is clunky. It also allows the callback to return a value that can be used by the SweetAlert2 library. (e.g. A validation message to show if input validation fails.) Native Blazor `EventCallback`s only return generic `Task`s.

Browser compatibility
---------------------

 IE11* | Edge | Chrome | Firefox | Safari | Opera | UC Browser
-------|------|--------|---------|--------|-------|------------
 :heavy_check_mark:  | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark:  |

\* ES6 Promise polyfill should be included, see usage example.

Related projects
-------------------------

- [SweetAlert2](https://sweetalert2.github.io/) - Original SweetAlert2 project
