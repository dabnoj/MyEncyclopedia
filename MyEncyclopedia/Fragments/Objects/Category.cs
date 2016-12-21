using Android.App;
using System.Collections.Generic;

namespace MyEncyclopedia.Fragments.Objects
{
    public class Category
    {
        public string Key { get; set; }

        public int Icon { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public Fragment fragment { get; set; }

        public List<Category> subCategories { get; set; }
    }

}