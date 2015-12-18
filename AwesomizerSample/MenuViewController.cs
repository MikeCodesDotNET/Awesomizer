using System;
using Foundation;
using UIKit;

using Awesomizer;

namespace AwesomizerSample
{
	partial class MenuViewController : UITableViewController
	{
		public MenuViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			var controller = segue.DestinationViewController as ViewController;
			if (controller != null) {
				controller.Clean = null;
				controller.Apply = null;

				if (segue.Identifier == "ShakeAnimations") {
					controller.Title = "Shake Animations";

					var horizontal = true;
					controller.Apply = view => {
						if (horizontal) {
							view.ShakeHorizontally (24);
						} else {
							view.ShakeVertically ();
						}
						horizontal = !horizontal;
					};
				} else if (segue.Identifier == "PopAnimation") {
					controller.Title = "Pop Animation";

					var repeat = false;
					controller.Apply = view => {
						view.Pop (0.8, repeat ? int.MaxValue : 1, 1.5f);
						repeat = !repeat;
					};
				} else if (segue.Identifier == "PulseAnimation") {
					controller.Title = "Pulse Animation";

					var repeat = false;
					controller.Apply = view => {
						view.PulseToSize (1.2f, 0.8, repeat);
						repeat = !repeat;
					};
				} else if (segue.Identifier == "FlipAnimations") {
					controller.Title = "Flip Animations";

					var direction = 0;
					controller.Apply = view => {
						view.FlipWithDuration (0.8, (UIViewExtensions.UIViewAnimationFlipDirection)direction, 1, false);
						direction = ++direction % 4;
					};
				} else if (segue.Identifier == "RotateAnimations") {
					controller.Title = "Rotate Animations";

					var direction = 0;
					controller.Apply = view => {
						var angle = 360.0f * (float)Math.PI / 180.0f;
						view.RotateToAngle (angle, 0.8, (UIViewExtensions.UIViewAnimationRotationDirection)direction, 1, false);
						direction = ++direction % 2;
					};
				} else if (segue.Identifier == "MotionEffects") {
					controller.Title = "Motion Effects";

					var enabled = true;
					controller.Apply = view => {
						if (enabled) {
							view.ApplyMotionEffects (75.0f, 75.0f);
						}
						enabled = !enabled;
					};
				}
			}

			base.PrepareForSegue (segue, sender);
		}
	}
}
