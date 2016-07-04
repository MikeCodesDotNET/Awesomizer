using System;
using UIKit;

using Awesomizer;
using MikeCodesDotNET.iOS;

namespace AwesomizerSample
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				DoClean ();
				DoApply ();
			}));
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			DoApply ();
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			DoClean ();
		}

		public Action<UIView> Apply { get; set; }

		public Action<UIView> Clean { get; set; }

		private void DoApply ()
		{
			imageView.StopAnimation ();
			if (Apply != null) {
				Apply (imageView);
			}
		}

		private void DoClean ()
		{
			if (Clean != null) {
				Clean (imageView);
			}
			imageView.StopAnimation ();
		}
	}
}
