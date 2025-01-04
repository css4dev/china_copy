set INTERVAL=1800
:loop
start /min ""  "E:\app\bin\Debug\net6.0\ChinaMall.exe"
timeout %INTERVAL%
goto:loop