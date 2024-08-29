using ConsoleApp4;

void afficherResultat(List<Person> resultat)
{
    foreach (var item in resultat)
    {
        Console.WriteLine($"{item.Nom}, {item.Age}, {item.Residance} \n");
    }
}

int? userInputHandeler(int valeurMax = 0)
{
    int choix;
    bool choixValide = false;
    Console.WriteLine("choix :");

    while (!choixValide)
    {
        string input = Console.ReadLine();
    
        if (int.TryParse(input, out choix))
        {
            if ((choix <= valeurMax && choix != 0) || valeurMax == 0)
            {
                choixValide = true;
                return choix;
            }
            else
            {
                Console.WriteLine("Choix Incorécte \n");
            }
        }
        else
        {
            Console.WriteLine("\n Saisir des nombre entier \n");
        }
        Console.WriteLine("choix :");
    }

    return null;
}

void exportAction()
{
    Controller controller = new Controller();
    string response = controller.jsonToXml();
    Console.WriteLine(response);
    menuPrincipal();
}

void rechercheAction()
{
    Console.WriteLine("Que rechercher : \n" +
                      "1: Tout \n" +
                      "2: Par Nom (exemple : Jean) \n" +
                      "3: Par Age (exemple 18 ans ou plus) \n" +
                      "4: Par Ville de Residance \n");
    int? choix = userInputHandeler(4);
    Controller controller = new Controller();
    List<Person> result = null;
    switch (choix)
    {
        case 1 :
            result = controller.rechercheMix();
            break;
        case 2 :
            Console.WriteLine("Quel nom ? : "); 
            string nom = Console.ReadLine();
            Console.WriteLine("\n");
            result = controller.rechercheMixNom(nom);
            break;
        case 3:
            Console.WriteLine("Quel Age ? : ");
            int? age = userInputHandeler();
            Console.WriteLine("\n");
            result = controller.rechercheMixAge(age);
            break;
        case 4:
            Console.WriteLine("Quel ville de résidance ? : "); 
            string residance = Console.ReadLine();
            Console.WriteLine("\n");
            result = controller.rechercheMixResidance(residance);
            break;
    }
    afficherResultat(result);
    Console.WriteLine("Trier? : \n" +
                      "1: Oui \n" +
                      "2: Non \n");
    choix = userInputHandeler(2);
    switch (choix)
    {
        case 1:
            result = controller.tri(result);
            afficherResultat(result);
            break;
        case 2:
            break;
    }

    menuPrincipal();
}

void menuPrincipal()
{
    Console.WriteLine("Bienvenue dans l'outil de recherche de base de donées \n");
    Console.WriteLine("Deux base de donée sont present un Json et un Xml");
    Console.WriteLine("Saisir le numero de l'operation : \n" +
                      "1 : Exporter le json en xml \n" +
                      "2 : rechercher dans toutes la base \n");

    int? choix = userInputHandeler(2);
    switch (choix)
    {
        case 1:
            exportAction();
            break;
        case 2:
            rechercheAction();
            break;
    }
}

menuPrincipal();