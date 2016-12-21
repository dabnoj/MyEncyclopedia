using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V7.App;
using MyEncyclopedia.Fragments;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using MyEncyclopedia.Fragments.Objects;
using MyEncyclopedia.Fragments.Adapters;

namespace MyEncyclopedia
{


    [Activity(Label = "MyEncyclopedia", MainLauncher = true, Icon = "@drawable/icon" , ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity
    {

        Android.Support.V7.Widget.Toolbar toolbar;
       public DrawerLayout Drawer { get; set; }
        NavigationView navigationView;
       public  RecyclerView RecyclerNav { get; set; }
        public List<Category> navList;
        public EntityViewModel EntityModel { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);
            EntityModel = new EntityViewModel();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            Drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            RecyclerNav = FindViewById<RecyclerView>(Resource.Id.recyclerNav);
            RecyclerNav.SetAdapter(new NavBarAdapter(SetNavList(),this));
            RecyclerNav.SetLayoutManager(new LinearLayoutManager(this,LinearLayoutManager.Vertical,false));

            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;


            SetSupportActionBar(toolbar);
            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowTitleEnabled(false);
                SupportActionBar.SetHomeButtonEnabled(true);
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_action_menu);
            }
            // Set our view from the "main" layout resource

            if (FindViewById<FrameLayout>(Resource.Id.fragmentContainer).ChildCount <= 0)
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.fragmentContainer, FragmentHome.Get());
                transaction.Commit();
            }
        }

        


        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            //SwitchFragment(e.MenuItem.ItemId);

            /*
            if (e.MenuItem.ItemId != currentFragmentId) {
                SwitchFragment (e.MenuItem.ItemId);
            }
            */
            Drawer.CloseDrawers();
        }

        //Event Selected on nav menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Drawer.OpenDrawer(Android.Support.V4.View.GravityCompat.End);
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }
        public override void OnBackPressed()
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.fragmentContainer, FragmentHome.Get());
            transaction.Commit();
        }

        public List<Category> SetNavList()
        {
            navList = new List<Category>();

            Category Search = new Category
            {
                Color = "#607D8B",
                Icon = Resource.Drawable.ic_navbar_search,
                Name = "Search",//TODO: change for multilang support
                fragment = FragmentHome.Get()
             
            };navList.Add(Search);

            Category MapSearch = new Category
            {
                Color = "#9C27B0",
                Icon = Resource.Drawable.ic_map,
                Name = "MapSearch",//TODO: change for multilang support

            }; navList.Add(MapSearch);


            Category ImageSearch = new Category
            {
                Color = "#2196F3",
                Icon = Resource.Drawable.ic_images,
                Name = "Images",//TODO: change for multilang support

            }; navList.Add(ImageSearch);

            Category Videos = new Category
            {
                Color = "#9E9E9E",
                Icon = Resource.Drawable.ic_youtube,
                Name = "Videos",//TODO: change for multilang support

            }; navList.Add(Videos);

            Category Infos = new Category
            {
                Color = "#FF9800",
                Icon = Resource.Drawable.ic_info,
                Name = "Infos",//TODO: change for multilang support

            }; navList.Add(Infos);

            return navList;
        }

    }
}

