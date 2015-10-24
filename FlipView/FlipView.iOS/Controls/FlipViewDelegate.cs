using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace FlipView.iOS.Controls {
    public class FlipViewDelegate : UIScrollViewDelegate {


        public override void Scrolled(UIScrollView scrollView) {
            base.Scrolled(scrollView);

            var flip = (FlipView)scrollView;
            var pageWidth = flip.Frame.Size.Width;
            var page = (int)Math.Floor((flip.ContentOffset.X - pageWidth / 2) / pageWidth) + 1;
            flip.PageControl.CurrentPage = page;
        }

    }
}
