using CoreGraphics;
using System;
using UIKit;

namespace SingleViewController
{
    sealed class MainView : UIView
    {
        private nfloat col1,col2;
        private nfloat bottomy,topy;
        private lib _lib = new lib();
        private string calcText;
        public UIButton SendButton;
        public UILabel calcLabel;

        public MainView(CGRect frame)
            : base(frame)
        {
            BackgroundColor = UIColor.Gray;
            _lib.ButtonWidth = frame.Width/2.0f - 5f;
            _lib.FrameHeight = frame.Height;
            _lib.FrameWidth = frame.Width;
            col1 = 5;
            col2 = col1 + 5 + _lib.ButtonWidth;
            bottomy = frame.Height - _lib.ButtonWidth-5;
            topy = 35;
        }

        // Removes all controls (views) from the View
        private void TidyViews()
        {
            foreach (var view in Subviews)
            {
                view.RemoveFromSuperview();
            }
        }

        public void Start(object sender, EventArgs e)
        {
            TidyViews();
            AddSubview(_lib.GetTextureButton("Cork", col1, bottomy, Cork));
            AddSubview(_lib.GetTextureButton("Calc", col2, bottomy, Calc));
        }

        public void Cork(object sender, EventArgs e)
        {
            TidyViews();
            AddSubview(_lib.GetTextureButton("Wood", col2, topy, Start, true));
            AddSubview(_lib.GetTextureButton("Calc", col2, topy+_lib.ButtonWidth+10, Calc,true));
        }

        // Builds up calculator screen
        public void Calc(object sender, EventArgs e)
        {
            TidyViews();
            calcLabel = new UILabel()
            {
                Text = @"",
                Lines = 1,
                Frame = new CGRect( 65f, 100f, _lib.FrameWidth - 65f, 30f),
                TextColor = UIColor.Yellow ,
                Font = UIFont.FromName("Helvetica", 24),
                BackgroundColor = UIColor.Clear
            };
            calcText = "";
            AddSubview(calcLabel);
            for (var buttonNum = 0; buttonNum < 13; buttonNum++)
            {
                var button = _lib.GetCalcButton(buttonNum, ClickButton);
                if (buttonNum == 12)
                    SendButton = button; 
                AddSubview(button);
            }
        }

        public void ShowValue()
        {                
            var appdelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;          
            var alert = UIAlertController.Create("Result", "Value: " + calcText, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
            appdelegate.controller.PresentViewController(alert, animated: true, completionHandler: null);
        }

        // This has all the logic for the keypad buttons
        private void ClickButton(object sender, EventArgs e)
        {
            var buttonNum = (sender as UIButton).Tag;
            var str = lib.TranslateButtonIndexToString(buttonNum);
            if (str == "SEND")
            {
                ShowValue();
                Start(sender,e);
                return;
            }
            if (str == "." && calcText.Contains(".")) // number can only have 1 decimal point
            {
                return;
            }

            if (str == "DEL") // delete last character
            {
                if (calcText.Length > 0)
                {
                    calcText = calcText.Substring(0, calcText.Length - 1);
                }
            }
            else if (calcText.Length < 6) // limit number of chars including dp to 6 
            {
                calcText += str;
            }

            SendButton.Enabled = (calcText.Length > 0 && lib.HasNumericValue(calcText));

            calcLabel.Text = calcText;
            calcLabel.SizeToFit();
        }

    }
}