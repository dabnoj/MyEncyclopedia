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
        DrawerLayout drawer;
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
            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            RecyclerNav = FindViewById<RecyclerView>(Resource.Id.recyclerNav);
            RecyclerNav.SetAdapter(new NavBarAdapter(SetNavList()));
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
            drawer.CloseDrawers();
        }

        //Event Selected on nav menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawer.OpenDrawer(Android.Support.V4.View.GravityCompat.End);
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
             
            };navList.Add(Search);

            Category Foo = new Category
            {
                Color = "#607D8B",
                Icon = Resource.Drawable.header_architecture,
                Name = "Search",//TODO: change for multilang support

            }; navList.Add(Foo);


            Category Foo2 = new Category
            {
                Color = "#607D8B",
                Icon = Resource.Drawable.header_architecture,
                Name = "Search",//TODO: change for multilang support

            }; navList.Add(Foo2);

            Category Foo3 = new Category
            {
                Color = "#607D8B",
                Icon = Resource.Drawable.header_architecture,
                Name = "Search",//TODO: change for multilang support

            }; navList.Add(Foo3);

            return navList;
        }

    }
}

