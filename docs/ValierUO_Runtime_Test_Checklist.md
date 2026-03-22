# ValierUO Runtime Test Checklist

Use this after a successful build/package.

## Launch tests

Run from packaged release folder:

```powershell
cd X:\UO\Client\Dev\ClassicUO_Valier\release\ValierClassicUOClient
.\ValierClassicUOClient.bat
```

Also test from extracted zip:

```powershell
cd X:\UO\Client\Dev\ClassicUO_Valier\release
Expand-Archive -Path .\ValierClassicUOClient-win-x64.zip -DestinationPath .\test-client -Force
cd .\test-client\ValierClassicUOClient
.\ValierClassicUOClient.bat
```

## What to verify

### Startup / package sanity
- launches from release folder
- launches from extracted zip
- no missing-file errors
- no immediate crash popup
- settings still load correctly

### Window behavior
- resize larger
- resize smaller
- maximize
- restore down
- drag between monitors if available
- close and reopen
- confirm size behavior is stable

### Branding
- icon is Valier
- title bar says `ValierUO - <version>`
- castle background shows
- logo shows in expected place
- note any remaining `ClassicUO` text anywhere

### Login screen
- panel/frame renders instead of grey placeholder gump
- account field focus works
- password field focus works
- typing works in both
- Tab works
- Enter works
- Login button works
- Credits button works
- Quit button works
- panel stays positioned correctly while resizing
- note whether panel is still too small

### Server selection
- shard/server selection opens correctly
- panel/frame renders correctly
- mouse click on shard works
- Enter works
- resize while on server selection
- panel stays positioned correctly while resizing
- note whether panel is still too small or clipped

### Persistence
- Save Account toggle works as expected
- reopen client and verify expected persistence
- shard/UO path settings still behave correctly

## Report back with
- title result
- background result
- logo result
- login panel placement
- shard selection placement
- which buttons work / do not work
- any clipping, stretching, alpha halo, or broken alignment
