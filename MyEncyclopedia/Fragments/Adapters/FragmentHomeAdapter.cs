using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using MyEncyclopedia.Fragments.Objects;
using System.Collections.Generic;
using Android.Support.V4.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.App;

namespace MyEncyclopedia.Fragments.Adapters
{


    public class FragmentHomeAdapter : RecyclerView.Adapter
    {
        public event EventHandler<FragmentHomeAdapterClickEventArgs> ItemClick;
        public event EventHandler<FragmentHomeAdapterClickEventArgs> ItemLongClick;
        public FragmentHome Fragment { get; set; }
        public List<Category> Categories { get; set; }
        public string Col;
        View itemView = null;
        bool isSub;

        public FragmentHomeAdapter(List<Category> data, FragmentHome fragment, bool isSub = false, string Color = "#abcdef")
        {
            Categories = data;
            this.Fragment = fragment;
            this.isSub = isSub;
            this.Col = Color;
            
        }
        
        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.CategoryItem, parent, false);

            var vh = new FragmentHomeAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Categories[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as FragmentHomeAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.TextCategory.Text = item.Name;
            holder.ImageCategory.SetImageDrawable(ContextCompat.GetDrawable(itemView.Context, item.Icon));
            if (!isSub)
                holder.Layout.Background = new ColorDrawable { Color = Color.ParseColor(item.Color) };
            else
            {
                holder.Layout.Background = new ColorDrawable { Color = Color.ParseColor(this.Col) };

            }
        }

        public override int ItemCount => Categories.Count;

        void OnClick(FragmentHomeAdapterClickEventArgs args)
        {

            Toast.MakeText(itemView.Context, Categories[args.Position].Name, ToastLength.Short).Show();
            Fragment.SearchView.Background = new ColorDrawable { Color = Color.ParseColor(Categories[args.Position].Color) };

            ((AppCompatActivity)Fragment.Activity).SupportActionBar.SetBackgroundDrawable(new ColorDrawable { Color = Color.ParseColor(Categories[args.Position].Color) });

            Fragment.RecyclerViewSub.SetAdapter(new SubCategoryAdapter(Categories, Fragment, args.Position));
            Fragment.SearchView.QueryHint = Categories[args.Position].subCategories[0].Key;
            Fragment.Main.EntityModel.Ontology = Categories[args.Position].subCategories[0].Key;
            Fragment.SearchView.ClearFocus();
        }
        void OnLongClick(FragmentHomeAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);


    }
    public class FragmentHomeAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }

        public ImageView ImageCategory { get; set; }
        public TextView TextCategory { get; set; }
        public FrameLayout Layout { get; set; }
        public FragmentHomeAdapterViewHolder(View itemView, Action<FragmentHomeAdapterClickEventArgs> clickListener,
                            Action<FragmentHomeAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            ImageCategory = itemView.FindViewById<ImageView>(Resource.Id.imageCategory);
            TextCategory = itemView.FindViewById<TextView>(Resource.Id.textCategory);
            Layout = itemView.FindViewById<FrameLayout>(Resource.Id.layoutCategory);
            itemView.Click += (sender, e) => clickListener(new FragmentHomeAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            ImageCategory.Click += (sender, e) => clickListener(new FragmentHomeAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new FragmentHomeAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
    public class FragmentHomeAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }    
    }



    public class SubCategoryAdapter : RecyclerView.Adapter
    {
        public event EventHandler<FragmentHomeAdapterClickEventArgs> ItemClick;
        public event EventHandler<FragmentHomeAdapterClickEventArgs> ItemLongClick;

        List<Category> categories = new List<Category>();
        public string CatColor;
        View itemView;
        public FragmentHome Fragment { get; set; }

        public override int ItemCount => categories.Count;

        public SubCategoryAdapter(List<Category> horizontalList, FragmentHome context, int index = 0)
        {
            this.categories = horizontalList[index].subCategories;
            this.CatColor = horizontalList[index].Color;
            this.Fragment = context;
        }


        public class MyViewHolder : RecyclerView.ViewHolder
        {

            public ImageView ImageView { get; set; }
            public TextView TextView { get; set; }
            public LinearLayout Layout { get; set; }
            public MyViewHolder(View view, Action<FragmentHomeAdapterClickEventArgs> clickListener,
                            Action<FragmentHomeAdapterClickEventArgs> longClickListener) : base(view)
            {
                ImageView = view.FindViewById<ImageView>(Resource.Id.imageview);
                TextView = view.FindViewById<TextView>(Resource.Id.txtview);
                Layout = view.FindViewById<LinearLayout>(Resource.Id.layoutSubCategory);
                ImageView.Click += (sender, e) => clickListener(new FragmentHomeAdapterClickEventArgs { View = view, Position = AdapterPosition });
                TextView.Click += (sender, e) => clickListener(new FragmentHomeAdapterClickEventArgs { View = view, Position = AdapterPosition });

            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SubCategoryItem, parent, false);
            return new MyViewHolder(itemView, OnClick, OnLongClick);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var hold = holder as MyViewHolder;

            hold.ImageView.SetImageDrawable(ContextCompat.GetDrawable(itemView.Context, categories[position].Icon));
            hold.TextView.Text = categories[position].Name;
            hold.Layout.SetBackgroundColor(Color.ParseColor(CatColor));
            hold.Layout.LayoutParameters.Width = Fragment.P.X / 3;
        }

        void OnClick(FragmentHomeAdapterClickEventArgs args)
        {

            Fragment.SearchView.QueryHint = categories[args.Position].Name;
            Fragment.SearchView.Iconified = false;
            Fragment.Main.EntityModel.Ontology = categories[args.Position].Key;

        }
        void OnLongClick(FragmentHomeAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
    }
}
