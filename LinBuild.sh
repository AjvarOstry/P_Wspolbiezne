#!/bin/bash

# Ścieżka do projektu (zapiszmy w zmiennej, żeby nie pisać kilometrów)
PROJEKT="ConcurrentProgramming/ReactiveInteractiveUserInterface/AvaloniaUserInterface/AvaloniaUserInterface.csproj"

echo "--- CZYSZCZENIE ---"
# dotnet clean usuwa pliki binarne z folderów bin i obj
dotnet clean $PROJEKT

echo "--- BUDOWANIE ---"
# Budujemy projekt
dotnet build $PROJEKT

echo "--- URUCHAMIANIE ---"
# Odpalamy
dotnet run --project $PROJEKT
