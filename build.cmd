@echo Off
set config=%1
if "%config%" == "" (
   set config=debug
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild AzureCLI.sln /p:Configuration="%config%";ExcludeXmlAssemblyFiles=false /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false