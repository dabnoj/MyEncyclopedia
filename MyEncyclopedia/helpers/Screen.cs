using Android.Views;
using Android.Graphics;
using Java.Lang;

namespace MyEncyclopedia.helpers
{
    public class Screen
    {

        public static Point getDisplaySize(Display display)
        {
            Point point = new Point();
            try
            {
                display.GetSize(point);

            }
            catch (NoSuchMethodError ignore)
            { // Older device
                point.X = display.Width;
                point.Y = display.Height;
            }
            return point;
        }
    }


}