# v5.3.0

## Notable changes

### Protestware

The author of the `sweetalert2` library added protestware concerning the war between Russia and Ukraine. (https://github.com/sweetalert2/sweetalert2/pull/2462) ([SNYK: Undesired Behavior in sweetalert2](https://security.snyk.io/vuln/SNYK-JS-SWEETALERT2-2774674))

This affects Russian speaking users visiting Russian websites. This was introduced in sweetalert2 `11.4.9` and is the reason this wrapper library has been fixed at `11.4.8` for so long. In sweetalert2 `11.5.0`, the author changed the protestware to disable the whole website. (https://github.com/sweetalert2/sweetalert2/commit/b101973c6a001fad0c1a88921c4d5e89345e9012)

```js
// Dear russian users visiting russian sites. Let's play a game.
if (typeof window !== 'undefined' && /^ru\b/.test(navigator.language) && location.host.match(/\.(ru|su|xn--p1ai)$/)) {
  document.body.style.pointerEvents = 'none'
}
```

#### Bypass

This library bypasses that protest by re-enabling `pointerEvents` if they have been disabled. Because the anti-war pop-up was removed in `11.5.0`, there should no longer be any undesired functionality when using this library.

### .NET 7

Added support for .NET 7. The dependent libraries are still in Release Candidate mode. I will be bumping up the requirement to the full release versions when they are available.
  
## Dependencies

- bump `sweetalert2` to `11.5.0`
- bump `@sweetalert2/themes` to `5.0.12`
- add support for `Microsoft.AspNetCore.Components` and `Microsoft.AspNetCore.Components.Web` `v7 preview`

