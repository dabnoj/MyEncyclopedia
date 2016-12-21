using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using MyEncyclopedia.Fragments.Objects;
using MyEncyclopedia.Fragments.Adapters;
using Android.Graphics;
using MyEncyclopedia.helpers;

namespace MyEncyclopedia.Fragments
{
    public class FragmentHome : Fragment
    {
        private static FragmentHome instance;
        public MainActivity Main { get; set; }
        public FragmentHome()
        {

        }

        public static FragmentHome Get()
        {
            if (instance == null)
            {
                instance = new FragmentHome();
            }
            return instance;
        }
        View v;
        RecyclerView recyclerView;
        public RecyclerView RecyclerViewSub { get; set; }

        public Query MyQuery { get; set; }

        public Android.Support.V7.Widget.SearchView SearchView { get; private set; }
        private List<Category> categories = new List<Category>();
        public Point P { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            v = inflater.Inflate(Resource.Layout.FragmentHome, container, false);
            Main = (MainActivity)Activity;
            categories = new CategoriesHelper().SetCategories();
            Main.EntityModel.Ontology = "Animal";
            SearchView = v.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchViewQuery);



            recyclerView = v.FindViewById<RecyclerView>(Resource.Id.recyclerHome);
            recyclerView.SetAdapter(new FragmentHomeAdapter(categories, this));
            recyclerView.SetLayoutManager(new StaggeredGridLayoutManager(2, 1));


            RecyclerViewSub = v.FindViewById<RecyclerView>(Resource.Id.recyclerHomeSub);
            RecyclerViewSub.SetAdapter(new SubCategoryAdapter(categories, this, 0));
            RecyclerViewSub.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Horizontal, false));

            SearchView.SetBackgroundColor(Color.ParseColor(categories[0].Color));
            this.SearchView.QueryHint = categories[0].subCategories[0].Name;
            Main.EntityModel.Ontology = categories[0].Key;
            Main.EntityModel.Contains = categories[0].subCategories[0].Key;

            SearchView.QueryTextSubmit += async delegate
            {
                Main.EntityModel.Contains = SearchView.Query;
                Main.EntityModel.Entities.Clear();
                this.SearchView.ClearFocus();
                await Main.EntityModel.SetEntitiesAsync();

                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.fragmentContainer, FragmentQueryResult.Get());
                transaction.Commit();

            };

            P = Screen.getDisplaySize(Activity.WindowManager.DefaultDisplay);

            // Use this to return your custom view for this Fragment
            return v;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

    }



    class CategoriesHelper
    {


        public List<Category> SetCategories()
        {
            var categories = new List<Category>();

            Category Architecture = new Category
            {
                Color = "#607D8B",
                Icon = Resource.Drawable.header_architecture,
                Key = "Architecture",
                Name = "Architecture",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Building",Name = "Buildings", Icon = Resource.Drawable.architecture_building},
                    new Category{Key = "Bridge",Name = "Bridges",Icon = Resource.Drawable.architecture_bridge},
                    new Category{Key = "Monument",Name="Monuments",Icon = Resource.Drawable.architecture_monument},
                    new Category{Key = "Airport",Name="Airports",Icon = Resource.Drawable.architecture_airport},
                    new Category{Key = "AmusementParkAttraction",Name="Attraction Parks",Icon = Resource.Drawable.architecture_amusement},
                    new Category{Key= "Infrastructure",Name="Infrastructures",Icon = Resource.Drawable.architecture_infrastructure}
                }
            };
            categories.Add(Architecture);

            Category Art = new Category
            {
                Color = "#9C27B0",
                Icon = Resource.Drawable.header_art,
                Key = "Art",
                Name = "Art",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Work",Name = "Work", Icon = Resource.Drawable.art_work},
                    new Category{Key = "WrittenWork",Name = "Literary Work",Icon = Resource.Drawable.art_literarywork},
                    new Category{Key = "Writer",Name="Writer",Icon = Resource.Drawable.art_writer},
                    new Category{Key = "Philosopher",Name="Philosopher",Icon = Resource.Drawable.art_philosoph},
                    new Category{Key = "Award",Name="Award",Icon = Resource.Drawable.art_award},
                    new Category{Key= "TopicalConcept",Name="Topical Concept",Icon = Resource.Drawable.art_topical}
                }
            }; categories.Add(Art);

            Category Cinema = new Category
            {
                Color = "#f44336",
                Icon = Resource.Drawable.header_cinema,
                Key = "Cinema",
                Name = "Cinema",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Film",Name = "Films",Icon = Resource.Drawable.cinema_film},
                    new Category{Key = "Anime",Name = "Animes",Icon = Resource.Drawable.cinema_cartoon},
                    new Category{Key = "RadioProgram",Name="Radio",Icon = Resource.Drawable.cinema_radio},
                    new Category{Key = "Comedian",Name="Comedians",Icon = Resource.Drawable.cinema_comedian},
                    new Category{Key = "Actor",Name="Actors",Icon = Resource.Drawable.cinema_actor},
                    new Category{Key= "Journalist",Name="Journalists",Icon = Resource.Drawable.cinema_journalist},
                    new Category{Key= "Presenter",Name="Presenter",Icon = Resource.Drawable.cinema_presenter}
                }



            }; categories.Add(Cinema);



            Category Geography = new Category
            {
                Color = "#2196F3",
                Icon = Resource.Drawable.header_geography,
                Key = "Geography",
                Name = "Geography",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Country",Name = "Country", Icon = Resource.Drawable.geo_country},
                    new Category{Key = "Place",Name = "Place",Icon = Resource.Drawable.geo_place},
                    new Category{Key = "PopulatedPlace",Name="Populated Place",Icon = Resource.Drawable.geo_populated_place},
                    new Category{Key = "NaturalPlace",Name="Natural Place",Icon = Resource.Drawable.geo_natureplace},
                    new Category{Key = "EthnicGroup",Name="Ethnic Group",Icon = Resource.Drawable.geo_ethnics},
                    new Category{Key= "Language",Name="Language",Icon = Resource.Drawable.geo_language}
                }
            }; categories.Add(Geography);



            Category Leisure = new Category
            {
                Color = "#9E9E9E",
                Icon = Resource.Drawable.header_leisure,
                Key = "Leisure",
                Name = "Sport and Leisure",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Game",Name = "Game", Icon = Resource.Drawable.leisure_game},
                    new Category{Key = "VideoGame",Name = "Video Game",Icon = Resource.Drawable.leisure_videogame},
                    new Category{Key = "SportsLeague",Name="Sports League",Icon = Resource.Drawable.leisure_sportleague},
                    new Category{Key = "SportsTeam",Name="Sports Team",Icon = Resource.Drawable.leisure_sportteam},
                    new Category{Key = "Sport",Name="Sport",Icon = Resource.Drawable.leisure_sport},
                    new Category{Key= "Food",Name="Food",Icon = Resource.Drawable.leisure_food},
                    new Category{Key= "Event",Name="Event",Icon = Resource.Drawable.leisure_event}

                }
            }; categories.Add(Leisure);


            Category Nature = new Category
            {
                Color = "#00C853",
                Icon = Resource.Drawable.header_nature,
                Key = "Nature",
                Name = "Nature",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Animal",Name = "Animals", Icon = Resource.Drawable.nature_animal},
                    new Category{Key = "Fungus",Name = "Mushrooms",Icon = Resource.Drawable.nature_mushroom},
                    new Category{Key = "Plant",Name = "Plants",Icon = Resource.Drawable.nature_plant}
                }
            }; categories.Add(Nature);

            Category Music = new Category
            {
                Color = "#FF9800",
                Icon = Resource.Drawable.header_music,
                Key = "Music",
                Name = "Music and Musicians",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "MusicalWork",Name = "Musical Work", Icon = Resource.Drawable.music_musicalwork},
                    new Category{Key = "MusicalArtist",Name = "Musical Artist",Icon = Resource.Drawable.music_musicartist}

                }
            }; categories.Add(Music);


            Category People = new Category
            {
                Color = "#FFC107",
                Icon = Resource.Drawable.header_people,
                Key = "People",
                Name = "People",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Person",Name = "Person", Icon = Resource.Drawable.people_person},
                    new Category{Key = "Athlete",Name = "Athlete",Icon = Resource.Drawable.people_athlete},
                    new Category{Key = "Politician",Name = "Politician",Icon = Resource.Drawable.people_politician}

                }
            }; categories.Add(People);

            Category Science = new Category
            {
                Color = "#009688",
                Icon = Resource.Drawable.header_sciences,
                Key = "Sciences",
                Name = "Sciences",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "BioMolecule",Name = "BioMolecule", Icon = Resource.Drawable.science_biomolecule},
                    new Category{Key = "ChemicalSubstance",Name = "Chemical Substance",Icon = Resource.Drawable.science_chemical},
                    new Category{Key = "Disease",Name="Disease",Icon = Resource.Drawable.science_disease},
                    new Category{Key = "Drug",Name="Drugs",Icon = Resource.Drawable.science_hemp},
                    new Category{Key = "AnatomicalStructure",Name="Anatomy",Icon = Resource.Drawable.science_anatomy},
                    new Category{Key= "Scientist",Name="Scientists",Icon = Resource.Drawable.science_scientist},
                    new Category{Key= "Astronaut",Name="Astronauts",Icon = Resource.Drawable.science_astronaut},
                    new Category{Key= "EducationalInstitution",Name="Educational Institution",Icon = Resource.Drawable.science_institution},
                    new Category{Key= "Medicine",Name="Medicine",Icon = Resource.Drawable.science_medicine}

                }
            }; categories.Add(Science);

            Category Technology = new Category
            {
                Color = "#673AB7",
                Icon = Resource.Drawable.header_technology,
                Key = "Technology",
                Name = "Technologies",//TODO: change for multilang support
                subCategories = new List<Category>
                {
                    new Category{Key = "Automobile",Name = "Automobile", Icon = Resource.Drawable.techno_automobile},
                    new Category{Key = "Motorcycle",Name = "Motorcycle",Icon = Resource.Drawable.techno_motorcycle},
                    new Category{Key = "Train",Name="Train",Icon = Resource.Drawable.techno_train},
                    new Category{Key = "Aircraft",Name="Aircraft",Icon = Resource.Drawable.techno_aircraft},
                    new Category{Key = "Ship",Name="Ship",Icon = Resource.Drawable.techno_ship},
                    new Category{Key= "MeanOfTransportation",Name="Mean Of Transportation",Icon = Resource.Drawable.techno_meantransportation},
                    new Category{Key= "Device",Name="Device",Icon = Resource.Drawable.techno_device}

                }
            }; categories.Add(Technology);


            return categories;
        }
    }
}