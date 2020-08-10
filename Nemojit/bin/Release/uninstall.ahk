#SingleInstance force

if not A_IsAdmin 
{ 
	try{
		Run *RunAs "%A_ScriptFullPath%"
	}
    ExitApp
}

Msgbox, 4, 네모짓 제거, 네모짓을 제거하시겠습니까?
IfMsgBox, Yes
{
	run, taskkill /f /im Nemojit.exe,,hide
	sleep, 100
	
	FileDelete, C:\ProgramData\Microsoft\Windows\Start Menu\Programs\네모짓.lnk
	Sleep, 100
	
	Loop
	{
		sleep,500
		Process, Exist, Nemojit.exe
		if (errorlevel = 0)
			break
	}
	
	RegRead, patha, HKLM, SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit, patha
	
	Msgbox, 4, 네모짓 제거, 설치 경로가 맞습니까?`n경로: %patha%`n`n경로가 맞지 않으면 즉시 "아니오" 버튼을 누른 후 devsil.teamflow@gmail.com으로 연락 주세요.
	IfMsgBox, No
	{
		ExitApp
	}
	
	a := "rd /s /q "

	FileAppend,
	(
	@echo off
	echo 네모짓을 삭제 중입니다.
	timeout /T 1 /Nobreak
	:point
	%a% "%patha%"
	if exist "%patha%" (
	echo 네모짓 폴더가 남아있는지 검사 중입니다.
	timeout /T 1 /Nobreak
	goto point
	`) else `(
	echo 네모짓은 완전히 삭제되었습니다.
	`) 
	timeout /T 1
	),%patha%Nemojit_delete.bat
	RegDelete, HKLM, SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Nemojit
	
	run, %patha%Nemojit_delete.bat
	Exitapp
}