#!/bin/bash

# Function to check if adb is installed
check_adb() {
    if ! command -v adb &> /dev/null; then
        echo "❌ ADB is not installed. Please install Android Platform Tools."
        echo "Mac Users: Run 'brew install android-platform-tools'"
        exit 1
    fi
}

# Function to prompt user for input with default values
get_user_input() {
    read -p "Enter Device IP (default: 192.168.2.27): " DEVICE_IP
    DEVICE_IP=${DEVICE_IP:-192.168.2.27}  # Use default if empty

    read -p "Enter Pairing Port (default: 46799): " PAIRING_PORT
    PAIRING_PORT=${PAIRING_PORT:-46799}  # Use default if empty

    read -p "Enter Pairing Code: " PAIRING_CODE  # No default, must be entered

    read -p "Enter Debugging Port (default: same as Pairing Port - $PAIRING_PORT): " DEBUG_PORT
    DEBUG_PORT=${DEBUG_PORT:-$PAIRING_PORT}  # Default to Pairing Port if empty
}

# Function to pair and connect device
pair_device() {
    echo "🔄 Pairing device..."
    adb pair $DEVICE_IP:$PAIRING_PORT $PAIRING_CODE

    if [ $? -ne 0 ]; then
        echo "❌ Pairing failed! Please check the IP, port, and pairing code."
        exit 1
    fi

    echo "✅ Successfully paired to $DEVICE_IP:$PAIRING_PORT"
}

connect_device() {
    echo "🔄 Connecting to device for debugging..."
    adb connect $DEVICE_IP:$DEBUG_PORT

    if [ $? -ne 0 ]; then
        echo "❌ Connection failed! Check if ADB debugging is enabled."
        exit 1
    fi

    echo "✅ Successfully connected to $DEVICE_IP:$DEBUG_PORT"
}

# Function to verify connection
verify_connection() {
    echo "🔍 Checking connected devices..."
    adb devices
}

# Execute functions
check_adb
get_user_input
pair_device
connect_device
verify_connection

echo "🚀 Device is ready for .NET MAUI debugging!"
