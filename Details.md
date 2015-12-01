# Awesomizer

A Xamarin.iOS library full of awesomeness (animations and useful helpers)

Awesomizer provides a set of **useful** extensions to `UIView`, such as  
a set of animations, and various size, position and image effects.

It is designed to be **as easy to use as possible to integrate and use** to 
speed up everyday development tasks. All the methods are well documented 
and described. 

## Animations

There are several animations that can be applied:

### Rotation
A view can be rotated to the left or the right:

    var angle = 360.0f * (float)Math.PI / 180.0f;
    var direction = UIViewExtensions.UIViewAnimationRotationDirection.Right;
    view.RotateToAngle (angle, 0.8, direction, 1, false);

### Flip
A view van be flipped up, down, left and right:

    var direction = UIViewExtensions.UIViewAnimationFlipDirection.Up;
    view.FlipWithDuration (0.8, direction, 1, false);

### Shake
A view can be shaken horizontally and vertically:

    view.ShakeHorizontally ();
    view.ShakeVertically ();

### Pulse 
A view can be set to pulse:

    view.PulseToSize (1.2f, 0.8, true);

### Pop
A view can have a pop animation:

    view.Pop (0.8, 1, 1.5f);
 
### Motion Effects 
On iOS 7+, a view can have motion effects:

    view.ApplyMotionEffects ();

## Size & Position
Along with animations, there are several convenient methods to assist 
in moving views: 

    // position
    view.SetOriginX (10.0f);
    view.SetOriginY (10.0f);
	
	// size
    view.SetWidth (100.0f);
    view.SetHeight (100.0f);

We can also calculate the size of a string:

    var size = label.StringSize ();

## Image
Images can also be manipulated.

### Make Rounded
An image view can receive rounded corners:

    imageView.MakeRounded (5);

### Tint
Provided an image, a tint can be applied:

    var tinted = image.Tint (UIColor.Green, CGBlendMode.Normal);

## Color
Colors can be lightened or darkened:

    var light = UIColor.Green.Lighten (3);
    var dark = UIColor.Green.Darken (3);
