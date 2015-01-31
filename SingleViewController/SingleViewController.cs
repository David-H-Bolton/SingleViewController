using System;
using UIKit;

namespace SingleViewController
{
    public sealed class SingleViewController : UIViewController
    {

        public void StartUp()
        {           
            View = new MainView(UIScreen.MainScreen.Bounds);
            var mainView = View as MainView;
            mainView.Start(null, new EventArgs());
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            StartUp();
        }

    }
}