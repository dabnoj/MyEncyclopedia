using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using MyEncyclopedia.Fragments.Objects;
using System.Collections.Generic;
using Android.Support.V4.Content;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace MyEncyclopedia.Fragments.Adapters
{
    class NavBarAdapter : RecyclerView.Adapter
    {
        public event EventHandler<NavBarAdapterClickEventArgs> ItemClick;
        public event EventHandler<NavBarAdapterClickEventArgs> ItemLongClick;
        View itemView = null;

        List<Category> items;

        public NavBarAdapter(List<Category> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            itemView = LayoutInflater.From(parent.Context).
                  Inflate(Resource.Layout.NavBarItem, parent, false);

            var vh = new NavBarAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as NavBarAdapterViewHolder;
            holder.TextCategory.Text = item.Name;
            holder.ImageCategory.SetImageDrawable(ContextCompat.GetDrawable(itemView.Context, item.Icon));
                holder.Layout.Background = new ColorDrawable { Color = Color.ParseColor(item.Color) };
           

            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => items.Count;

        void OnClick(NavBarAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(NavBarAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class NavBarAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public ImageView ImageCategory { get; set; }
        public TextView TextCategory { get; set; }
        public SquareFrameLayout Layout { get; set; }

        public NavBarAdapterViewHolder(View itemView, Action<NavBarAdapterClickEventArgs> clickListener,
                            Action<NavBarAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            ImageCategory = itemView.FindViewById<ImageView>(Resource.Id.imageNavBarItem);
            TextCategory = itemView.FindViewById<TextView>(Resource.Id.textViewNavBarItem);
            Layout = itemView.FindViewById<SquareFrameLayout>(Resource.Id.layoutNavBarItem);
            itemView.Click += (sender, e) => clickListener(new NavBarAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            ImageCategory.Click += (sender, e) => clickListener(new NavBarAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new NavBarAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class NavBarAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}