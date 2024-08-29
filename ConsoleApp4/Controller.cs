namespace ConsoleApp4;
using System.Xml.Linq;
using System.Xml.Serialization;
using ConsoleApp4;
using System.Text.Json;

public class Controller
{
    string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    private List<Person>? getJsonData()
    {
        //récupération du Json
        
        string jsonFilePath = $"{projectDirectory}/dataJson.json";
        string jsonData = File.ReadAllText(jsonFilePath);

        List<Person>? data = JsonSerializer.Deserialize<List<Person>>(jsonData);
        return data;
    }

    private List<Person> getXmlData()
    {
        //récupération du xml
        string xmlFilePath = $"{projectDirectory}/dataXml.xml";

        XmlSerializer serializer = new XmlSerializer(
            typeof(List<Person>), 
            new XmlRootAttribute("Groupe")
            );

        List<Person> data;
        using (FileStream f = new FileStream(xmlFilePath, FileMode.Open))
        {
            data = (List<Person>)serializer.Deserialize(f)!;
        }

        return data;
    }

    public string jsonToXml()
    {
        //récupération du Json
        var data = getJsonData();
        
        //Conversion Json - XML
        XElement xmlData = new XElement("Groupe", //Creation du groupe principal en Xml
            from item in data //ajout de toutes les données dans le groupe principal
            select new XElement("Person", //On ajoute l'objet
                new XElement("Nom", item.Nom),
                new XElement("Age", item.Age),
                new XElement("Residance", item.Residance)
            )
        );
        
        //sauvegarde du xml crée
        try
        {
            File.WriteAllText($"{projectDirectory}/jsonToXmlOutput.xml", xmlData.ToString());
            return $"fichier sauvegarder dans : {projectDirectory}/jsonToXmlOutput.xml";
        }
        catch (Exception e)
        {
            return e.ToString();
        }
        
    }

    public List<Person> rechercheMixAge(int? age = 0)
    {
        //récupération des bases de doénes
        List<Person>? jsonData = getJsonData();
        List<Person>? xmlData = getXmlData();
        
        //combiner les deux
        List<Person>? data = jsonData.Concat(xmlData).ToList();
        
        //recherche
        List<Person> result = data.Where(x => x.Age >= age).ToList();

        return result;
    }
    
    public List<Person> rechercheMixNom(string nom)
    {
        //récupération des bases de doénes
        List<Person>? jsonData = getJsonData();
        List<Person>? xmlData = getXmlData();
        
        //combiner les deux
        List<Person>? data = jsonData.Concat(xmlData).ToList();
        
        //recherche
        List<Person> result = data.Where(x => x.Nom == nom).ToList();

        return result;
    }
    
    public List<Person> rechercheMixResidance(string residance)
    {
        //récupération des bases de doénes
        List<Person>? jsonData = getJsonData();
        List<Person>? xmlData = getXmlData();
        
        //combiner les deux
        List<Person>? data = jsonData.Concat(xmlData).ToList();
        
        //recherche
        List<Person> result = data.Where(x => x.Residance == residance).ToList();

        return result;
    }
    
    public List<Person> rechercheMix()
    {
        //récupération des bases de doénes
        List<Person>? jsonData = getJsonData();
        List<Person>? xmlData = getXmlData();
        
        //combiner les deux
        List<Person>? data = jsonData.Concat(xmlData).ToList();
        
        //recherche
        List<Person> result = data.ToList();

        return result;
    }

    public List<Person> tri(List<Person> data)
    {
        List<Person> result = data.OrderBy(x => x.Nom).ToList();
        return result;
    }
}