# Nemojit
네모짓은 GNU GPL version 2 or later로 이용하실 수 있습니다.

그러나, 네모짓에 사용된 다른 프로그램 및 라이브러리 - ffmpeg, libx264, DirectShow.NET 등은 별도의 라이선스를 따릅니다. License 디렉터리를 참고해주세요.

# How to Build
네모짓은 C# winform으로 개발되었으며, .NET Framework 4.7을 사용합니다. VisualStudio 2019 Community를 사용했습니다.

설치 프로그램 .ahk는 AutoHotkey 1.1.32.02을 기준으로 개발되었습니다.

1. AutoHotkey를 설치합니다.
2. VisualStudio를 이용해서 Neomjit.sln을 열어 빌드합니다. (이후, \Nemojit\bin\release\ 경로에 Nemojit.exe가 생성됩니다.)
3. \Nemojit\bin\release\ 경로에 있는 uninsall.ahk를 컴파일합니다. (uninstall.exe가 생성됩니다.)
4. \Nemojit\bin\release\ 경로에 있는 Setup.ahk를 컴파일합니다. (Setup.exe가 생성됩니다.)
5. 배포 시에는 Setup.exe와 같은 경로에 있는 License, SetupImage 폴더를 함께 묶어서 제공합니다. 나머지 파일들은 Setup.ahk를 컴파일 한 뒤 Setup.exe안에 포함되어 컴파일됩니다.