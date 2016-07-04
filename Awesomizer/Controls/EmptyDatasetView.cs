using System;
using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace Awesomizer.Controls
{
    public class EmptyDatasetView : UIView
    {
        public UIView ContentView { get; set; }
        public UILabel TitleLabel { get; set; }
        public UILabel DetailLabel { get; set; }
        public UIImageView ImageView { get; set; }
        public UIView CustomView { get; set; }
        public UITapGestureRecognizer TapGesture { get; set; }
        public float VerticleOffset { get; set; }
        public float VerticleSpace { get; set; }
        public bool FadeInOnDisplay { get; set; }

        public EmptyDatasetView(string title, string detail, UIImage image, UIView customView = null)
        {
            TitleLabel = new UILabel();
            DetailLabel = new UILabel();
            ImageView = new UIImageView();
            CustomView = new UIView();

            TitleLabel.Text = title;
            DetailLabel.Text = detail;
            ImageView.Image = image;
            CustomView = customView;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();


        }
    }

}


