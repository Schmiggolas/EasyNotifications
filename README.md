# Easy Notifications

A simple and flexible notification system for Unity projects.

## Overview

Easy Notifications is a lightweight package that allows you to quickly implement customizable notifications in your Unity applications.

## Features

- 🚀 Simple API for displaying notifications
- 🎨 Customizable appearance (colors, icons, animations)
- ⏱️ Automatic timeout with configurable duration
- 📱 Responsive design for different screen sizes
- 🧩 Easy integration with existing UI systems. Works nicely with Unity Layouting System
- 📦 Minimal dependencies (TextMeshPro only)

## Installation

### Using Unity Package Manager

1. Open the Package Manager window in Unity (Window > Package Manager)
2. Click the "+" button and select "Add package from git URL..."
3. Enter: `https://github.com/schmiggolas/easy-notifications.git`
4. Click "Add"

### Manual Installation

1. Download the latest release
2. Extract the contents into your Unity project's Packages folder

## Quick Start

Add and setup a NotificationController in your scene. If you're having trouble with the setup, check out the included demo scene.
Then in your code call the function as follows.

Check out 

```csharp
using Schmiggolas.EasyNotifications;

public void SpawnNotificationNoButtons()
{
    var settings = new NotificationSettings(0, "Test Notification No Buttons");
    NotificationController.EnqueueNotification(settings);
}

public void SpawnNotificationNoButtonsWithTimeout()
{
    var settings = new NotificationSettings(10, "Test Notification No Buttons");
    NotificationController.EnqueueNotification(settings);
}
public void SpawnNotificationWithButtons()
{
    var settings = new NotificationSettings(0, "Test Notification With Buttons",null, new ButtonSettings[]{
        new("Button 1", () => Debug.Log("Button 1 clicked"), false),
        new("Button 2", () => Debug.Log("Button 2 clicked"), false),
        new("Close", () => Debug.Log("Button 3 clicked"), true) // closes the notification after clicking
    });

    NotificationController.EnqueueNotification(settings);
}
```

## License

This package is licensed under the MIT License - see the LICENSE file for details.

## Support

If you encounter any issues or have questions, please file an issue on the [issue tracker](https://github.com/Schmiggolas/EasyNotifications/issues).

---

Made with