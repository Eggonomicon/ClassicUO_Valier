# ValierUO Build Test Checklist

## Build validation
- Run the build script.
- Confirm:
  - bootstrap published
  - client published
  - release folder assembled
  - zip created
  - patch manifest created

## Smoke test
Launch from packaged folder:
```powershell
cd X:\UO\Client\Dev\ClassicUO_Valier\release\ValierClassicUOClient
.\ValierClassicUOClient.bat
```

## Runtime checks
1. Startup reaches login without crash.
2. Title bar text is ValierUO.
3. Castle background renders.
4. Grey classic panel is gone.
5. New branded login frame renders.
6. Login panel sits right-center.
7. Server selection panel sits right-center.
8. Quit / Credits / Login image buttons render.
9. Account/password fields accept input.
10. Enter / click login works.
11. Resize larger.
12. Resize smaller.
13. Maximize.
14. Restore.
15. Close and reopen.
16. Extract packaged zip to a clean folder and retest there too.

## Note outcomes
Record:
- what still says ClassicUO
- what still looks too small
- what breaks when resized
- any button/input issues
