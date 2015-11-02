using System;
using CoreAnimation;
using Foundation;
using UIKit;

namespace Awesomizer
{
    public static class Animations
    {
        public enum UIViewAnimationFlipDirection
        {
            Top,
            Left,
            Right,
            Bottom
        }

        public enum UIViewAnimationRotationDirection
        {
            Right,
            Left
        }

        public static void ShakeHorizontally(this UIView view)
        {
            CAKeyFrameAnimation animation = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform.translation.x");
            animation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
            animation.Duration = 0.5;
            animation.Values = new NSObject[]
                {
                    NSNumber.FromFloat(-12),
                    NSNumber.FromFloat(12),
                    NSNumber.FromFloat(-8),
                    NSNumber.FromFloat(8),
                    NSNumber.FromFloat(-4),
                    NSNumber.FromFloat(4),
                    NSNumber.FromFloat(0)
                };

            view.Layer.AddAnimation(animation, "shake");
        }

        public static void ShakeVertically(this UIView view)
        {
            CAKeyFrameAnimation animation = CAKeyFrameAnimation.FromKeyPath("transform.translation.y");
            animation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
            animation.Duration = 0.5;
            animation.Values = new NSObject[]
                {
                    NSNumber.FromFloat(-12),
                    NSNumber.FromFloat(12),
                    NSNumber.FromFloat(-8),
                    NSNumber.FromFloat(8),
                    NSNumber.FromFloat(-4),
                    NSNumber.FromFloat(4),
                    NSNumber.FromFloat(0)
                };

            view.Layer.AddAnimation(animation, "shake");
        }

        public static void Pop(this UIView view, double duration, int repeatCount, float force)
        {
            CAKeyFrameAnimation animation = CAKeyFrameAnimation.FromKeyPath("transform.scale");
            animation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
            animation.KeyTimes = new NSNumber[]
                {
                    NSNumber.FromFloat(0),
                    NSNumber.FromFloat(0.2f),
                    NSNumber.FromFloat(0.4f),
                    NSNumber.FromFloat(0.6f),
                    NSNumber.FromFloat(0.8f),
                    NSNumber.FromFloat(1)
                };
            animation.Duration = duration;
            animation.Additive = true;
            animation.RepeatCount = repeatCount;
            animation.Values = new NSObject[]
                {
                    NSNumber.FromFloat(0),
                    NSNumber.FromFloat(0.2f * force),
                    NSNumber.FromFloat(-0.2f * force),
                    NSNumber.FromFloat(0.2f * force),
                    NSNumber.FromFloat(0)
                };

            view.Layer.AddAnimation(animation, "pop");
        }

        public static void PulseToSize(this UIView view, float scale, double duration, bool repeat)
        {
            CABasicAnimation pulseAnimation = CABasicAnimation.FromKeyPath("transform.scale");
            pulseAnimation.Duration = duration;
            pulseAnimation.To = NSNumber.FromFloat(scale);
            pulseAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
            pulseAnimation.RepeatCount = repeat == false ? 0 : float.MaxValue;

            view.Layer.AddAnimation(pulseAnimation, "pulse");
        }

        public static void FlipWithDuration(this UIView view, double duration, UIViewAnimationFlipDirection direction, int repeatCount, bool shouldAutoReverse)
        {
            var subType = string.Empty;
            switch (direction)
            {
                case UIViewAnimationFlipDirection.Top:
                    subType = "fromTop";
                    break;
                case UIViewAnimationFlipDirection.Left:
                    subType = "fromLeft";
                    break;
                case UIViewAnimationFlipDirection.Bottom:
                    subType = "fromBottom";
                    break;
                case UIViewAnimationFlipDirection.Right:
                    subType = "fromRight";
                    break;
                default:
                    break;
            }

            CATransition transition = new CATransition();
            transition.StartProgress = 0;
            transition.EndProgress = 1;
            transition.Type = "flip";
            transition.Subtype = subType;
            transition.Duration = duration;
            transition.RepeatCount = repeatCount;
            transition.AutoReverses = shouldAutoReverse;

            view.Layer.AddAnimation(transition, "spin");
        }

        public static void RotateToAngle(this UIView view, float angle, double duration, UIViewAnimationRotationDirection direction, int repeatCount, bool shouldAutoReverse)
        {
            CABasicAnimation rotateAnimation = CABasicAnimation.FromKeyPath("transform.rotation.z");

            rotateAnimation.To = direction == UIViewAnimationRotationDirection.Right ? NSNumber.FromFloat(angle) : NSNumber.FromFloat(-angle);
            rotateAnimation.Duration = duration;
            rotateAnimation.AutoReverses = shouldAutoReverse;
            rotateAnimation.RepeatCount = repeatCount;
            rotateAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);

            view.Layer.AddAnimation(rotateAnimation, "transform.rotation.z");
        }

        public static void ApplyMotionEffects(this UIView view)
        {
            int SystemVersion = Convert.ToInt16 (UIDevice.CurrentDevice.SystemVersion.Split ('.') [0]);
            if (SystemVersion >= 7)
            {
                UIInterpolatingMotionEffect horizontalEffect = new UIInterpolatingMotionEffect("center.x", UIInterpolatingMotionEffectType.TiltAlongHorizontalAxis);
                horizontalEffect.MinimumRelativeValue = NSNumber.FromFloat(-10);
                horizontalEffect.MaximumRelativeValue = NSNumber.FromFloat(10);

                UIInterpolatingMotionEffect verticalEffect = new UIInterpolatingMotionEffect("center.6", UIInterpolatingMotionEffectType.TiltAlongVerticalAxis);
                verticalEffect.MinimumRelativeValue = NSNumber.FromFloat(-10);
                verticalEffect.MaximumRelativeValue = NSNumber.FromFloat(10);
                UIMotionEffectGroup motionEffectGroup = new UIMotionEffectGroup();
                motionEffectGroup.MotionEffects = new UIMotionEffect[] {horizontalEffect, verticalEffect};

                view.AddMotionEffect(motionEffectGroup);
            }
        }

        public static void StopAnimation(this UIView view)
        {
            view.Layer.RemoveAllAnimations();
        }

        public static bool IsBeingAnimated(this UIView view)
        {
            return view.Layer.AnimationKeys.Count() > 0;
        }
    }
}

