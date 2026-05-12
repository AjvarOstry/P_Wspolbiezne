#!/bin/bash
# This script builds the core logic projects, excluding UI layers.
set -e

echo "Building core logic projects..."

dotnet build \
  ConcurrentProgramming/ReactiveInteractiveUserInterface/BusinessLogic/BusinessLogic.csproj \
  ConcurrentProgramming/ReactiveInteractiveUserInterface/Data/Data.csproj \
  ConcurrentProgramming/ReactiveInteractiveUserInterface/PresentationModel/PresentationModel.csproj \
  ConcurrentProgramming/ReactiveInteractiveUserInterface/PresentationViewModel/PresentationViewModel.csproj

echo "Core logic projects built successfully."
