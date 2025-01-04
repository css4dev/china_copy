Set WshShell = CreateObject("WScript.Shell") 
WshShell.Run chr(34) & "E:\app\bin\Debug\net6.0\batch.bat" & Chr(34), 0
Set WshShell = Nothing