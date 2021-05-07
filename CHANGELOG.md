# v4.5.0

## Bug Fixes

### Handle custom serialization settings better

Previously, if a user were to change their default JSON serialization settings, it would break the JSInterop.
Now interop properties won't be affected by user serialization settings.

## Changes

### Deprecate Queue methods

Generally, as a rule, this library doesn't include deprecated methods from the `sweetalert2` library.
The library just deprecated all of the queue functions in anticiaption of `v11` which will remove them.

If I were to remove them, that would require a new major verison release now, and then another when they realease `v11`.
So for now those methods are also deprecated in this library.

- `QueueAsync`
- `GetQueueStepAsync`
- `InsertQueueStepAsync`
- `DeleteQueueStepAsync`

## Dependencies

- bump `sweetalert2` to `10.16.7`
- bump `@sweetalert2/themes` to `4.0.5`


