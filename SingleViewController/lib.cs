using CoreGraphics;
using ObjCRuntime;
using System;
using UIKit;

namespace SingleViewController
{
    class lib
    {
        public nfloat ButtonWidth { get; set; }

        internal delegate void Anaction(object sender, EventArgs args);

        private readonly UIImage woodTexture;
        private readonly UIImage corkTexture;
        private readonly UIImage calcImage;
        public nfloat FrameWidth { get; set; }
        public nfloat FrameHeight { get; set; }

        // return true if running on simulator
        public static bool OnSimulator()
        {
            return Runtime.Arch == Arch.SIMULATOR;
        }

        public lib()
        {            
            corkTexture = UIImage.FromBundle("images/corkbutton.png");
            corkTexture = corkTexture.StretchableImage(100, 0); 
            woodTexture = UIImage.FromBundle("images/wood.png");
            woodTexture = woodTexture.StretchableImage(100, 0);
            calcImage = UIImage.FromBundle("calc.png");         
        }

        internal UIButton GetTextureButton(string _title, nfloat _x, nfloat _y, Anaction d,bool iscork=false)
        {
            var button = new UIButton(new CGRect(_x, _y, ButtonWidth, ButtonWidth));
            button.SetBackgroundImage(iscork?corkTexture:woodTexture, UIControlState.Normal);
            button.BackgroundColor = UIColor.Clear;
            button.SetTitle(_title, UIControlState.Normal);
            button.Font = UIFont.SystemFontOfSize(40);
            button.TouchUpInside += (o, e) => d(o, e);
            return button;
        }

        internal UIButton GetCalcButton(int buttonNum, Anaction d, Boolean enabled = true)
        {
            UIButton button;
            UIColor titleColor;
            var _x = 40 + (buttonNum % 3) * 85;
            var _y = 140 + (buttonNum / 3) * 60;
            if (buttonNum < 12)
            {
                button = new UIButton(new CGRect(_x, _y, 80, 55));
                enabled = true;
            }
            else
            {
                button = new UIButton(new CGRect( 210, _y, 80, 55));
                enabled = false;
            }
            button.SetBackgroundImage(calcImage, UIControlState.Normal);
            button.BackgroundColor = UIColor.Blue;
            button.SetTitle(TranslateButtonIndexToString(buttonNum), UIControlState.Normal);
            button.Font = UIFont.SystemFontOfSize(24);
            titleColor = buttonNum == 12 ? UIColor.Red : UIColor.White;
            button.SetTitleColor(titleColor, UIControlState.Normal);
            button.SetTitleColor(UIColor.DarkGray,UIControlState.Highlighted);
            button.TouchUpInside += (o, e) => d(o, e);
            button.Enabled = enabled;
            button.Tag = buttonNum;
            return button;
        }

        public static string TranslateButtonIndexToString(nint buttonnum)
        {
            var values = new [] { "7", "8", "9", "4", "5", "6", "1", "2", "3", "0", ".", "DEL", "SEND" };
            if (buttonnum < 0 || buttonnum > 12)
            {
                return "";
            }
            return values[buttonnum];
        }

        public static bool HasNumericValue(string str)
        {
            nfloat value;
            if (nfloat.TryParse(str, out value))
            {
                return value > 0.0;
            }
            return false;
        }

    }
}