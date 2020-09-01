#SingleInstance force

if not A_IsAdmin 
{ 
	try{
		Run *RunAs "%A_ScriptFullPath%"
	}
    ExitApp
}

if (SubStr(A_OSVersion, 1, 2) != 10 || A_Is64bitOS = false)
{
	BitName := A_Is64bitOS ? "64비트" : "32비트"
	MsgBox, % "네모짓은 Windows 10 (64비트)에서만 지원합니다.`n`n요구 OS 버전: 10.0+, 64비트`n사용자 컴퓨터: " A_OSVersion ", " BitName
	ExitApp
}

version := "v1.0.3"
global state = "normal"

Gui, Color, FFFFFF
Gui, Font, S15,맑은 고딕
Gui, Add, Text, x10 y100 w590 Center vText1, 안녕하세요!
Gui, Font, S11,맑은 고딕
Gui, Add, Text, x100 y150 w400 Center vText2, 네모짓을 선택해주셔서 감사합니다.`n`n현재 버전은 %version%입니다.
Gui, Add, Pic, x260 y370 w80 h23 vBtnPic gBtnPic, SetupImage\다음.png
Gui, Show, w600 h425, 네모짓 설치하기
OnMessage(0x200, "OnMouseMove")
page := 0
return

BtnPic:
if (page = 0)
{
	FileRead, license, License\네모짓\license.txt
	Gui, Add, Text, x20 y20 w560 vText3, 다음을 누르면 라이선스에 동의하는 것으로 간주됩니다.
	Gui, Add, Edit, x20 y50 w560 h295 vEdit1 ReadOnly, %license%
	GuiControl,Hide,Text1
	GuiControl,Hide,Text2
	page++
}
else if (page = 1)
{
	GuiControl, Hide, Edit1
	Gui, Font, s9, 굴림
	Gui, Add, Edit, x20 y50 w300 h20 vPath ReadOnly, C:\
	Gui, Add, Button, x320 y49 w80 h22 vPathBtn +0x8000 gPathBtn , 경로지정
	Gui, Add, CheckBox, x20 y80 w300 h22 vDesktopShortcut Checked, 바탕화면에 바로가기 설정
	GuiControl,,Text3, 저장 경로 및 바탕화면 바로가기 설정
	page++
}
else if (page = 2)
{
	Gui, +AlwaysOnTop
	run, https://nemojit.github.io/thanks,,Max
	GuiControl,Disabled,BtnPic
	Gui, Submit, NoHide
	installPath := SubStr(Path, 1, StrLen(Path) - 1)
	isShortcut := DesktopShortcut
	GuiControl,,Text3,네모짓을 선택해주셔서 감사합니다!
	Gui, Add, progress, x20 y50 w560 h20 c0F4B81 vProgressBar, 0
	GuiControl, Hide, Path
	GuiControl, Hide, PathBtn
	GuiControl, Hide, DesktopShortcut
	FileCreateDir, %installPath%\Nemojit
	GuiControl,,ProgressBar,1
	FileInstall, Nemojit.exe, %installPath%\Nemojit\Nemojit.exe, 1
	GuiControl,,ProgressBar,3
	FileInstall, Nemojit.exe.config, %installPath%\Nemojit\Nemojit.exe.config, 1
	GuiControl,,ProgressBar,4
	FileInstall, virtual-audio-capturer-x64.dll, %installPath%\Nemojit\virtual-audio-capturer-x64.dll, 1
	FileInstall, screen-capture-recorder-x64.dll, %installPath%\Nemojit\screen-capture-recorder-x64.dll, 1
	GuiControl,,ProgressBar,6
	FileInstall, ffmpeg-Nemojit.exe, %installPath%\Nemojit\ffmpeg-Nemojit.exe, 1
	GuiControl,,ProgressBar,75
	FileCreateDir, %installPath%\Nemojit\License
	FileCreateDir, %installPath%\Nemojit\License\FFmpeg
	FileCreateDir, %installPath%\Nemojit\License\DirectShow.NET
	FileCreateDir, %installPath%\Nemojit\License\libx264
	FileCreateDir, %installPath%\Nemojit\License\네모짓
	FileInstall, License\FFmpeg\LICENSE.txt, %installPath%\Nemojit\License\FFmpeg\license.txt, 1
	FileInstall, License\DirectShow.NET\LICENSE.txt, %installPath%\Nemojit\License\DirectShow.NET\LICENSE.txt, 1
	FileInstall, License\libx264\license.txt, %installPath%\Nemojit\License\libx264\license.txt, 1
	FileInstall, License\네모짓\License.txt, %installPath%\Nemojit\License\네모짓\License.txt, 1
	GuiControl,,ProgressBar,80
	FileInstall, DirectShowLib-2005.dll, %installPath%\Nemojit\DirectShowLib-2005.dll, 1
	GuiControl,,ProgressBar,82
	FileInstall, Microsoft.WindowsAPICodePack.dll, %installPath%\Nemojit\Microsoft.WindowsAPICodePack.dll, 1
	GuiControl,,ProgressBar,83
	FileInstall, Microsoft.WindowsAPICodePack.Shell.dll, %installPath%\Nemojit\Microsoft.WindowsAPICodePack.Shell.dll, 1
	GuiControl,,ProgressBar,85
	FileInstall, Microsoft.WindowsAPICodePack.xml, %installPath%\Nemojit\Microsoft.WindowsAPICodePack.xml, 1
	GuiControl,,ProgressBar,88
	FileInstall, Microsoft.WindowsAPICodePack.Shell.xml, %installPath%\Nemojit\Microsoft.WindowsAPICodePack.Shell.xml, 1
	GuiControl,,ProgressBar,89
	IniWrite,% Round(A_ScreenWidth / 2 - 314), %installPath%\Nemojit\Options.ini, AreaSave, AreaX
	IniWrite,% Round(A_ScreenHeight / 2 - 225), %installPath%\Nemojit\Options.ini, AreaSave, AreaY
	IniWrite, 629, %installPath%\Nemojit\Options.ini, AreaSave, AreaW
	IniWrite, 450, %installPath%\Nemojit\Options.ini, AreaSave, AreaH
	GuiControl,,ProgressBar,90
	IniWrite, 44, %installPath%\Nemojit\Options.ini, HotkeySave, Key1Main
	IniWrite, 0, %installPath%\Nemojit\Options.ini, HotkeySave, Key1Sub
	IniWrite, 44, %installPath%\Nemojit\Options.ini, HotkeySave, Key2Main
	IniWrite, 1, %installPath%\Nemojit\Options.ini, HotkeySave, Key2Sub
	IniWrite, 44, %installPath%\Nemojit\Options.ini, HotkeySave, Key3Main
	IniWrite, 4, %installPath%\Nemojit\Options.ini, HotkeySave, Key3Sub
	GuiControl,,ProgressBar,91
	IniWrite, 0, %installPath%\Nemojit\Options.ini, General, CloseTray
	IniWrite, 1, %installPath%\Nemojit\Options.ini, General, RememberPos
	IniWrite, #0F4B81, %installPath%\Nemojit\Options.ini, General, Theme
	GuiControl,,ProgressBar,92
	IniWrite, 0, %installPath%\Nemojit\Options.ini, Rec, CloseControler
	IniWrite, 0, %installPath%\Nemojit\Options.ini, Rec, CloseArea
	IniWrite, 1, %installPath%\Nemojit\Options.ini, Rec, Noti
	IniWrite, 1, %installPath%\Nemojit\Options.ini, Rec, Sound
	IniWrite, 23, %installPath%\Nemojit\Options.ini, Rec, CRF
	GuiControl,,ProgressBar,93
	IniWrite, Number, %installPath%\Nemojit\Options.ini, Save, SaveFormat
	IniWrite, %A_Desktop%, %installPath%\Nemojit\Options.ini, Save, SavePath
	GuiControl,,ProgressBar,94
	Gui, -AlwaysOnTop
	Run, *RunAs %Comspec% /c RegSvr32 "%installPath%\Nemojit\virtual-audio-capturer-x64.dll",, Hide
	Run, *RunAs %Comspec% /c RegSvr32 "%installPath%\Nemojit\screen-capture-recorder-x64.dll",, Hide
	GuiControl,,ProgressBar,97
	if (isShortcut = 1)
	{
		FileCreateShortcut,%installPath%\Nemojit\Nemojit.exe,%A_Desktop%\네모짓.lnk,,,,%installPath%\Nemojit\Nemojit.exe
	}
	FileCreateShortcut,%installPath%\Nemojit\Nemojit.exe,C:\ProgramData\Microsoft\Windows\Start Menu\Programs\네모짓.lnk,,,,%installPath%\Nemojit\Nemojit.exe
	GuiControl,,ProgressBar,99
	FileInstall, uninstall.exe, %installPath%\Nemojit\uninstall.exe, 1
	sleep, 1000
	RegWrite,REG_SZ,HKLM,SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, DisplayIcon, %installPath%\Nemojit\Nemojit.exe
	RegWrite,REG_SZ,HKLM,SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, DisplayName, 네모짓
	RegWrite,REG_SZ,HKLM,SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, DisplayVersion, %version%
	RegWrite,REG_SZ,HKLM,SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, Publisher, FLOW
	RegWrite,REG_SZ,HKLM,SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, UninstallString, %installPath%\Nemojit\uninstall.exe
	RegWrite,REG_SZ,HKLM,SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, URLInfoAbout, https://nemojit.github.io
	RegWrite,REG_SZ,HKLM,SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, patha, %installPath%\Nemojit\
	GuiControl,,ProgressBar,100
	GuiControl,Enabled,BtnPic
	GuiControl,,Text3, 설치가 완료되었습니다.
	page++
}
else
	ExitApp

return

PathBtn:
FileSelectFolder, PathDir
if (pathDir = "")
	return
GuiControl,,Path,%PathDir%
Gui, Submit, NoHide
if (SubStr(Path, 0, 1) != "\")
	GuiControl,,Path,%Path%\
return

GuiClose:
ExitApp

OnMouseMove(wParam, lParam)
{
	MouseGetPos,,,,ControlName
	if (ControlName = "Static3")
	{
		if (state = "normal")
		{
			state := "hover"
			GuiControl,,BtnPic,SetupImage\다음_hover.png
		}
	}
	else
	{
		if (state = "hover")
		{
			state := "normal"
			GuiControl,,BtnPic,SetupImage\다음.png
		}
	}

}