using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using MyEncyclopedia.Fragments.Objects;
using System.Collections.ObjectModel;
using Square.Picasso;

namespace MyEncyclopedia.Fragments.Adapters
{



    class QueryResultAdapter : RecyclerView.Adapter
        {
            private static string TAG = "Query.adapter";

            public event EventHandler<QueryAdapterClickEventArgs> ItemClick;
            public event EventHandler<QueryAdapterClickEventArgs> ItemLongClick;

            private ObservableCollection<Ontology> Items;
            private Context _context;

            public QueryResultAdapter(ObservableCollection<Ontology> data, Context c)
            {
                Items = data;
                _context = c;
            }

            // Create new views (invoked by the layout manager)
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {

                //Setup your layout here
                View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.QueryResultItem, parent, false);

                var vh = new QueryResultViewHolder(itemView, OnClick, OnLongClick);
                return vh;
            }



            // Replace the contents of a view (invoked by the lay ut manager)
            public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
            {
                var item = Items[position];

                // Replace the contents of the view with that element
                var holder = viewHolder as QueryResultViewHolder;
                holder.TextView.Text = item.Name;
                Picasso.With(holder.ImageView.Context).Load(item.Url).Into(holder.ImageView);

            }


            public override void OnViewRecycled(Java.Lang.Object viewHolder)
            {
                var holder = viewHolder as QueryResultViewHolder;
                base.OnViewRecycled(holder);
                Picasso.With(holder.ImageView.Context)
                      .CancelRequest(holder.ImageView);
            }

            public override int ItemCount => Items.Count;

            void OnClick(QueryAdapterClickEventArgs args)
            {
                Toast.MakeText(_context, Items[args.Position].Name, ToastLength.Short).Show();

            }
            void OnLongClick(QueryAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        }

        public class QueryResultViewHolder : RecyclerView.ViewHolder
        {
            //public TextView TextView { get; set; }

            public TextView TextView;
            public ImageView ImageView;
            public QueryResultViewHolder(View itemView, Action<QueryAdapterClickEventArgs> clickListener,
                                Action<QueryAdapterClickEventArgs> longClickListener) : base(itemView)
            {
                TextView = itemView.FindViewById<TextView>(Resource.Id.textViewQueryResult);
                ImageView = itemView.FindViewById<ImageView>(Resource.Id.imageQueryResult);
                itemView.Click += (sender, e) => clickListener(new QueryAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
                itemView.LongClick += (sender, e) => longClickListener(new QueryAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            }
        }

        public class QueryAdapterClickEventArgs : EventArgs
        {
            public View View { get; set; }
            public int Position { get; set; }
        }
    
}
