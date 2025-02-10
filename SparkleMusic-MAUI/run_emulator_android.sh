#!/bin/bash

# Emulator name
AVD_NAME="Medium_Phone_API_35"

# Port number
PORT=8782

# Check if emulator exists
if ! emulator -list-avds | grep -q "$AVD_NAME"; then
    echo "Error: AVD '$AVD_NAME' not found. Run 'emulator -list-avds' to check available emulators."
    exit 1
fi

# Start the emulator with debugging enabled
emulator -avd "$AVD_NAME" -debug all -port $PORT