### 🛑 Breaking Changes
* 🔴 Targets .NET Standard 2.0 ([#1](https://github.com/Basaingeal/Razor.SweetAlert2/issues/1))
* 🔴 Script tag to add SweetAlert2 functionality is now `<script src="_content/CurrieTechnologies.Razor.SweetAlert2/sweetAlert2.min.js"></script>` ([aspnet/AspNetCore-Tooling@`98b168c`](https://github.com/aspnet/AspNetCore-Tooling/commit/98b168c8947d8c7f7b3f2beb3a0561fb89cadfb5))
* 🔴 Add dependency on `Microsoft.AspNetCore.Components`

### Bug Fixes
* The configured theme no longer disappears after certain page refreshes.

### Behind the Scenes
* Using babel for transiplation to match SweetAlert2 library targets (i.e. modern browsers and IE 11.)
* Use yarn for package management, mirroring the SweetAlert2 library
* Project restructured