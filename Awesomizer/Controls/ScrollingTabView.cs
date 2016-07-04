﻿using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace MikeCodesDotNET.iOS.Controls
{
    public class ScrollingTabView : UIView, IUIScrollViewDelegate
    {
        public UIFont TitleFont { get; set; }
        public float TabBarHeight {get; set;}
        public UIColor TabbarBackgroundColor { get; set; }
        public UIColor SelectedTabUnderlineColor { get; set; }
        public UIColor SelectedTabTextColor { get; set; }
        public UIColor TabTextColor { get; set; }
        public UIColor ContentBackgroundColor { get; set; }
        public int CurrentIndex { get; private set; }

        public ScrollingTabView(List<UIViewController> viewControllers, UIFont titleFont, float tabBarHeight = 42)
        {
            //Create views
            tabbarBackground = new UIView();
            selectedTabUnderlineView = new UIStackView();
            contentScrollView = new UIScrollView();

            ViewControllers = viewControllers;
            DefaultColors();

            CurrentIndex = 0;
            TabItemSelected += HandleTabItemSelected;

            TabBarHeight = tabBarHeight;
            TitleFont = titleFont;

            contentScrollView.ShowsHorizontalScrollIndicator = false;
            contentScrollView.ShowsVerticalScrollIndicator = false;
        }



        public List<UIViewController> ViewControllers { get; private set; }

        public override void LayoutSubviews()
        {
            //ContentView 
            contentScrollView = new UIScrollView();
            contentScrollView.Frame = new CGRect(0, TabBarHeight, Bounds.Width, Bounds.Height - TabBarHeight - 98);
            contentScrollView.BackgroundColor = ContentBackgroundColor;
            contentScrollView.PagingEnabled = true;

            //tabbar 
            tabbarBackground.BackgroundColor = TabbarBackgroundColor;
            tabbarBackground.Frame = new CGRect(0, 0, Bounds.Width, TabBarHeight);

            nfloat tabbuttonWidth = 0;
            foreach (var viewController in ViewControllers)
            {
                var index = ViewControllers.IndexOf(viewController);

                var button = new UIButton(UIButtonType.RoundedRect);
                button.Tag = index;
                button.SetTitle(viewController.Title, UIControlState.Normal);
                button.Font = TitleFont;

                if (index == 0)
                    button.SetTitleColor(SelectedTabTextColor, UIControlState.Normal);
                else
                    button.SetTitleColor(TabTextColor, UIControlState.Normal);

                tabbuttonWidth = Bounds.Width / ViewControllers.Count + 1;
                button.Frame = new CGRect(tabbuttonWidth * index, 2, tabbuttonWidth, TabBarHeight - 4);
                button.TouchUpInside += delegate
                {
                    CurrentIndex = index;
                    TabItemSelected(index);
                    ResignFirstResponder();
                };

                tabbarBackground.AddSubview(button);
                viewController.View.Frame = new CGRect(Bounds.Width * index, 0, Bounds.Width, contentScrollView.Frame.Height);

                contentScrollView.AddSubview(viewController.View);
            }

            contentScrollView.ContentSize = new CGSize(Bounds.Width * ViewControllers.Count, 400);
            contentScrollView.Scrolled += ContentScrolled;

            //Underline 
            selectedTabUnderlineView = new UIView();
            selectedTabUnderlineView.BackgroundColor = SelectedTabUnderlineColor;
            selectedTabUnderlineView.Frame = new CGRect(0, TabBarHeight - 4, tabbuttonWidth, 4);

            //Add views
            AddSubview(contentScrollView);
            AddSubview(tabbarBackground);
            AddSubview(selectedTabUnderlineView);
        }

        void ContentScrolled(object sender, EventArgs e)
        {
            var x = contentScrollView.ContentOffset.X;
            selectedTabUnderlineView.Frame = new CGRect(x / ViewControllers.Count, selectedTabUnderlineView.Frame.Y, selectedTabUnderlineView.Frame.Width, selectedTabUnderlineView.Frame.Height);

            if (x < 369)
            {
                UpdateTabBar(0);
            }
            if (x > 370 && x < 739)
            {
                UpdateTabBar(1);
            }
            if (x > 740 && x < 1125)
            {
                UpdateTabBar(2);
            }
        }

        void HandleTabItemSelected(int index)
        {
            MoveToIndex(index);
            UpdateTabBar(index);
        }

        void MoveToIndex(int index)
        {
            if (index >= 0 && index < ViewControllers.Count)
            {
                CurrentIndex = index;
                var rect = new CGRect(Frame.Width * index, 0, Frame.Size.Width, Frame.Size.Height);
                contentScrollView.ScrollRectToVisible(rect, true);
            }
        }

        void UpdateTabBar(int index)
        {
            if (index == CurrentIndex)
                return;

            var newSelectedButton = tabbarBackground.Subviews[index] as UIButton;
            newSelectedButton.SetTitleColor(SelectedTabTextColor, UIControlState.Normal);
            if(SelectionChanged != null)
                SelectionChanged(index, newSelectedButton.Title(UIControlState.Normal));

            var oldSelectedButton = tabbarBackground.Subviews[CurrentIndex] as UIButton;
            oldSelectedButton.SetTitleColor(TabTextColor, UIControlState.Normal);
            CurrentIndex = index;
        }

        void DefaultColors()
        {
            //These are unique for Beer Drinkin
            TabbarBackgroundColor = UIColor.White;
            SelectedTabUnderlineColor = "514F52".ToUIColor();
            SelectedTabTextColor = "514F52".ToUIColor();
            TabTextColor = "D5D5D5".ToUIColor();
            ContentBackgroundColor = "F5F5F5".ToUIColor();
        }

        public delegate void TabItemSelectedHandler(int index);
        public event TabItemSelectedHandler TabItemSelected;

        public delegate void SelectionChangedHandler(int index, string title);
        public event SelectionChangedHandler SelectionChanged;

        UIView tabbarBackground;
        UIView selectedTabUnderlineView;
        UIScrollView contentScrollView;

    }
}

