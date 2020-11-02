# v4.2.0

## 🎉 New Features

### New `PreDeny` Callback propoerty

There is a new property on `SweetAlertOptions` called `PreDeny`. This is fired when someone hits the deny button. It can be a sync or async function that takes a string and returns a string (`Func<string, Task<string>>` or `Func<string, string>`).

The input string of the callback is the value that would be returned by `SweetAlertResult.Value`.

If you return `null`, the original value is passed along to `SweetAlertResult.Value`.
If you return `"false"`, the popup does not close.
If you return anything else, that returned value goes in to `SweetAlertResult.Value`.

**NB**: Be sure that `ReturnInputValueOnDeny` is set to true in order for the value to be passed into the `PreDeny` callback function.

#### Example

```csharp
var result = await Swal.FireAsync(new SweetAlertOptions
    {
        Input = SweetAlertInputType.Select,
        InputOptions = new Dictionary<string, string>
        {
            { "DC", "Don't Close" },
            { "Normal Result", "Result is Normal" },
            { "OV", "Result is overriden" }
        },
        ReturnInputValueOnDeny = true,
        PreDeny = new PreDenyCallback((string input) =>
        {
            return input switch
            {
                "DC" => "false",
                "OV" => "PreConfirm override!!!!",
                _ => null
            };
        }, this),
        ShowDenyButton = true
    });

// If "Don't Close" is selected, modal doesn't close.
// If "Result is Normal" is selected, result.Value = "Normal Result"
// If "Result is overriden" is selected, result.Value = "PreConfirm override!!!!"
```

### Similar functionality on `PreConfirm` callback property

The `PreConfirm` callback has been in place form day one.
If you returned `null`, the original value would be returned to `SweetAlertResult.Value`, otherwise the returned value of the callback would be passed to `SweetAlertResult.Value`.

Now, if you return `"false"`, the popup modal will stay open.

## Dependencies

- bump `sweetalert2` to `10.9.0`
- bump `@sweetalert2/themes` to `4.0.1`
