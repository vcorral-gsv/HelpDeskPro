@echo off
setlocal enabledelayedexpansion

:: === Validar argumentos ===
if "%~1"=="" (
    echo Uso: %~nx0 [ruta_relativa1] [ruta_relativa2] [...]
    echo Ejemplo: %~nx0 src Entities "Carpeta Con Espacios"
    exit /b 1
)

:: === Configurar rutas base y salida ===
set "BASE_DIR=%~dp0"
set "OUTPUT=%BASE_DIR%code.txt"
if exist "%OUTPUT%" del "%OUTPUT%"

:: === Extensiones permitidas ===
set "ALLOWED_EXT=.js .ts .py .cs"

:: === Procesar cada argumento (cada ruta) ===
:nextArg
if "%~1"=="" goto endArgs

call :processDir "%~1"
shift
goto nextArg

goto :eof

:: ==========================================================
:: Subrutina: processDir
::   %1 -> ruta relativa a la carpeta del script
:: ==========================================================
:processDir
set "REL_DIR=%~1"
set "TARGET_DIR=%BASE_DIR%%REL_DIR%"

:: Normalizar barra final
if not "%TARGET_DIR:~-1%"=="\" set "TARGET_DIR=%TARGET_DIR%\"

:: Comprobar existencia
if not exist "%TARGET_DIR%" (
    echo [ADVERTENCIA] Carpeta no encontrada: "%REL_DIR%"
    echo.
    goto :eof
)

echo Procesando archivos en: "%TARGET_DIR%"
echo.

for /r "%TARGET_DIR%" %%F in (*) do (
    set "FILE=%%~F"
    set "EXT=%%~xF"
    set "INCLUDE_FILE=no"

    for %%E in (%ALLOWED_EXT%) do (
        if /I "%%E"=="!EXT!" set "INCLUDE_FILE=yes"
    )

    if "!INCLUDE_FILE!"=="yes" (
        set "REL_PATH=!FILE:%BASE_DIR%=!"
        >>"%OUTPUT%" echo [!REL_PATH!]
        type "%%~F" >> "%OUTPUT%"
        >>"%OUTPUT%" echo.
        >>"%OUTPUT%" echo ----------------------------------
        >>"%OUTPUT%" echo.
    )
)

goto :eof

:endArgs
echo.
echo Archivo generado en: "%OUTPUT%"
pause
endlocal
