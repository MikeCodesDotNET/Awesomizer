using System;
using UIKit;
using Foundation;

namespace Awesomizer.Controls
{
    public class AlertView : UIView
    {

        public float ContainerWidth { get; set;}
        public float ButtonHeight { get; set;}
        public float CornerRadius { get; set;}
        public float ShadowOpacity {get; set;}
        public UIFont TitleFont { get; set;}
        public UIFont MessageFont { get; set;}

        UILabel titleLabel;
        UILabel messageLabel;
        UIView containerView;
        UIView shadowView;


        public AlertView(string title, string message)
        {
            ShadowOpacity = 0.15f;
            ContainerWidth = 300;
            ButtonHeight = 50;
            CornerRadius = 6;

            titleLabel = new UILabel();
            titleLabel.Text = title;
            titleLabel.Font = TitleFont;

            messageLabel = new UILabel();
            messageLabel.Text = message;
            messageLabel.Font = MessageFont;

            Frame = UIScreen.MainScreen.Bounds;
            BackgroundColor = UIColor.FromWhiteAlpha(0, 0.2f);

            containerView = new UIView();
            containerView.Layer.CornerRadius = CornerRadius;
            containerView.Layer.MasksToBounds = true;

            shadowView.Layer.ShadowColor = UIColor.Black.CGColor;
            shadowView.Layer.ShadowOffset = CoreGraphics.CGSize.Empty;
            shadowView.Layer.ShadowOpacity = ShadowOpacity;
        }

        public void Show()
        {
            foreach (var subview in Subviews)
            {
                subview.RemoveFromSuperview();
            }


        }
    }
}

