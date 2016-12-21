using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF.Query;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using VDS.RDF;
using VDS.RDF.Storage;
using MyEncyclopedia.Fragments.Objects;

namespace MyEncyclopedia.Fragments.Objects
{

    public class Ontology
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }

    public class Query
    {
        private static string TAG = "QueryClass";
        private readonly SparqlConnector _endpoint;
        public List<Ontology> Entities;
        private KeyValuePair<string, string> _searchKey;

        public Query(string contains, string onto)
        {
            _endpoint = new SparqlConnector(new Uri("http://dbpedia.org/sparql"));
            Entities = new List<Ontology>();
            _searchKey = new KeyValuePair<string, string>(onto, contains);
        }
        private string EntityQuery()
        {
            return
                    "PREFIX ontology: <http://dbpedia.org/ontology/>\n" +
                            "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\n" +
                            "PREFIX foaf: <http://xmlns.com/foaf/0.1/>\n" +
                            "SELECT DISTINCT ?thumb ?entity ?desc ?label ?pic \n" +
                            " WHERE\n" +
                            " {\n" +
                            "?entity  a ontology:" + _searchKey.Key + ";\n" +
                            "ontology:abstract ?desc;\n" +
                            "rdfs:label ?label.\n" +
                            "OPTIONAL{?entity foaf:depiction ?pic;" +
                            "ontology:thumbnail ?thumb}. " +
                            "FILTER(langmatches(lang(?desc),\"en\")" +
                            " && langmatches(lang(?label),\"en\"))\n" +
                            "?desc <bif:contains> \"'" + _searchKey.Value + "'\"\n" +
                            "} ORDER BY ?label LIMIT 10000 OFFSET 0";
        }

        public async Task<List<Ontology>> RunQueryAsync()
        {
            await Task.Run(() =>
            {
                Entities.AddRange(((SparqlResultSet)_endpoint.Query(EntityQuery())).Select(r => new Ontology
                {
                    Name = ((ILiteralNode)r.Value("label")).Value ?? String.Empty,
                    Description = ((ILiteralNode)r.Value("desc")).Value ?? String.Empty,
                    Url = r.Value("thumb") == null ? "https://www.transparenttextures.com/patterns/debut-light.png" : r.Value("thumb").ToString()
                }));
            });
            return Entities;
        }
    }
}


public class EntityViewModel
{
    private static string TAG = "EntityViewModel";
    public string Contains { get; set; }
    public string Ontology { get; set; }
    public ObservableCollection<Ontology> Entities { get; set; } = new ObservableCollection<Ontology>();

    public async Task SetEntitiesAsync()
    {
        Entities.Clear();
        var q = new Query(Contains, Ontology);
        var ent = await q.RunQueryAsync();


        foreach (var e in ent)
        {
            Entities.Add(e);
        }
    }
}