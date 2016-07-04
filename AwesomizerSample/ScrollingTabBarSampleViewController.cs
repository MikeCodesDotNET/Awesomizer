using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace AwesomizerSample
{
    public partial class ScrollingTabBarSampleViewController : UIViewController
    {

        UIViewController vc1;
        UIViewController vc2;

        public ScrollingTabBarSampleViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            vc1 = Storyboard.InstantiateViewController("BlueViewController");
            vc1.Title = "Blue";
            vc2 = Storyboard.InstantiateViewController("PinkViewController");
            vc2.Title = "Pink";
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var font = UIFont.FromName("Avenir-Medium", 18f);
            var list = new List<UIViewController> { vc1, vc2 };

            var tabbar = new MikeCodesDotNET.iOS.Controls.ScrollingTabView(list, font);
            tabbar.Frame = placeholderView.Bounds;
            placeholderView.AddSubview(tabbar);
        }
    }
}