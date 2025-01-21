:: change these paths if you want to debug and not have to copy files every single time by hand

set SDRSharpPath="C:\Users\Yuujin\Desktop\sdrsharp-x64-next\Plugins\Remote Control\ "
set "SourcePath=%1"

TASKKILL /F /FI "IMAGENAME eq SDRSharp*" &				:: ...
robocopy %SourcePath% %SDRSharpPath% *.* /s /e			:: ...
exit 0