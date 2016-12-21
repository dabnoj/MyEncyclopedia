
using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using MyEncyclopedia.Fragments.Adapters;

namespace MyEncyclopedia.Fragments
{
    public class FragmentQueryResult : Fragment
    {
        View v;
       public RecyclerView recyclerView { get; private set; }

        private static FragmentQueryResult instance;
        public FragmentQueryResult()
        {

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static FragmentQueryResult Get()
        {
            if (instance == null)
            {
                instance = new FragmentQueryResult();
            }
            return instance;
        }
       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            v = inflater.Inflate(Resource.Layout.FragmentResults, container, false);
            recyclerView = v.FindViewById<RecyclerView>(Resource.Id.listQuery);
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            if (recyclerView != null)
                recyclerView.SetLayoutManager(new GridLayoutManager(Activity, 3));
            recyclerView.SetAdapter(new QueryResultAdapter(((MainActivity)Activity).EntityModel.Entities, Activity));

            return v;
        }
    }
}