using CoreGraphics;
using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace SingleViewController
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window {get;set;}
        public SingleViewController controller;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Window = new UIWindow((CGRect)UIScreen.MainScreen.Bounds);
            app.IdleTimerDisabled = true; // stop screen locking

            controller = new SingleViewController();
            Window.RootViewController = controller;

            Window.MakeKeyAndVisible();
            return true;
        }

    }
}