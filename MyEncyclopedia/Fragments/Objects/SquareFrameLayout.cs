using System;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.Util;

namespace MyEncyclopedia.Fragments.Objects
{
    [Register("MyEncyclopedia.Objects.SquareFrameLayout")]
   public class SquareFrameLayout : FrameLayout
    {
        public SquareFrameLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public SquareFrameLayout(Context context) : base(context)
        {
        }

        public SquareFrameLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public SquareFrameLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public SquareFrameLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, widthMeasureSpec);
        }
    }
}