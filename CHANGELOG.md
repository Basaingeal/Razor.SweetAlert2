# v5.3.0

## Notable changes

### Protestware

The author of the `sweetalert2` library added protestware concerning the war between Russia and Ukraine. (https://github.com/sweetalert2/sweetalert2/pull/2462) ([SNYK: Undesired Behavior in sweetalert2](https://security.snyk.io/vuln/SNYK-JS-SWEETALERT2-2774674))

This affects Russian speaking users visiting Russian websites. This was introduced in sweetalert `11.4.9` and is the reason this wrapper library has been fixed at `11.4.8` for so long. However, I don't want users to begin missing out on new features from the sweetalert2 library. Since the Snyk risk assesment is low, I'm going to be resuming updating the sweetalert2 dependency. If this becomes a problem for you or your users, feel free to open an issue, and I will do what I can to neutralize the code causing the pop-up.

### .NET 7

Adding support for .NET 7. The dependent libraries are still in Release Candidate mode. I will be bumping up the requirement to the full release versions when they are available.
  
## Dependencies

- bump `sweetalert2` to `11.5.0`
- bump `@sweetalert2/themes` to `5.0.12`
- add support for `Microsoft.AspNetCore.Components` and `Microsoft.AspNetCore.Components.Web` `v7 preview`

