// classe qui defini une tache
public class Tache
{
    private String titre;
    private String description;
    private int importance;

    public Tache(String titre, String description, int importance)
    {
        this.titre = titre;
        this.description = description;
        this.importance = importance;
    }

    // affiche le titre
    public void affTitre()
    {
        Console.WriteLine(titre);
    }

    // affiche la description
    public void affDescription()
    {
        Console.WriteLine(description);
    }

    // affiche le niveau d'importance
    public void affImportance()
    {
        Console.WriteLine(importance);
    }

    // retourne le titre
    public String getTitre()
    {
        return titre;
    }

    // retourne la description
    public String getDescription()
    {
        return description;
    }
    
    // retourne l'importance
    public int getImportance()
    {
        return importance;
    }

    public void setTitre(String titre)
    {
        this.titre = titre;
    }

    public void setDescription(String description)
    {
        this.description = description;
    }

    public void setImportance(int importance)
    {
        this.importance = importance;
    }
}

// programme principal
public class ex13
{
    // fonction pour ajouter des taches
    public static Tache ajouterTache()
    {
        Console.WriteLine("quel est le nom de la tache que tu veux ajouter ?\n");
        String titre = Console.ReadLine();
        
        Console.WriteLine("ajoute une description a cette tache :\n");
        String description = Console.ReadLine();
        
        Console.WriteLine("quel est le niveau d'importance de cette tache ?\n");
        int importance = int.Parse(Console.ReadLine());
        
        Tache tache = new Tache(titre, description, importance);
        
        return tache;
    }

    // fonction qui affiche toutes les taches
    public static void afficherTache(List<Tache> taskList, int index)
    {
        Console.Write("titre de la tache : ");
        taskList[index].affTitre();
        Console.Write("description de la tache : ");
        taskList[index].affDescription();
        Console.Write("importance de la tache : ");
        taskList[index].affImportance();
    }

    // fonction qui tri les taches par importance
    public static void triImportance(List<Tache> taskList)
    {
        bool change;
        int n = taskList.Count;
        Tache tacheTmp;

        do
        {
            change = false;
            for (int i = 0; i < n - 1; i++)
            {
                if (taskList[i].getImportance() > taskList[i + 1].getImportance())
                {
                    tacheTmp = taskList[i];
                    taskList[i] = taskList[i + 1];
                    taskList[i + 1] = tacheTmp;
                    change = true;
                }
            }
            n--;
        }
        while (change);
    }
    
    // fonction qui trie les taches par ordre alphabetique (choix de tri par defaut)
    public static void triAlpha(List<Tache> taskList)
    {
        bool change = true;
        int n = taskList.Count;
        Tache tacheTmp;
        while (change)
        {
            change = false;
            for (int i = 0; i < n - 1; i++)
            {
                if (taskList[i].getTitre().CompareTo(taskList[i + 1].getTitre()) > 0)
                {
                    tacheTmp = taskList[i];
                    taskList[i] = taskList[i + 1];
                    taskList[i + 1] = tacheTmp;
                    change = true;
                }
            }
            n--;
        }
    }
    
    // fonction principale que je vais lancer dans le main dans Program.cs
    public static void Main(String[] args)
    {
        List<Tache> taskList = new List<Tache>();
        // mettre variable triDefaut pour trier direct a l'ajout des taches
        int choix = 1;
        int numTache = 0;
        while (choix != 7)
        {
            Console.WriteLine("que veux tu faire ?");
            Console.WriteLine("1. ajouter une tache");
            Console.WriteLine("2. supprimer une tache");
            Console.WriteLine("3. afficher la liste des taches");
            Console.WriteLine("4. afficher une tache entierement");
            Console.WriteLine("5. modifier une tache");
            Console.WriteLine("6. trier la liste");
            // ajouter sauvegarde des taches (regarde dans les parametres quel est le choix d'enregistrement) sauvegarde aussi les parametres
            // ajouter charger une sauvegarde (detecte si binaire ou json)
            // ajouter une option parametre qui vas gerer comment ca enregistre, quel est le tri par default ...
            Console.WriteLine("9. quitter");
            choix = int.Parse(Console.ReadLine());
            switch (choix)
            {
                // ajouter une tache
                case 1:
                    Tache tacheAajouter = ajouterTache();
                    taskList.Add(tacheAajouter);
                    // mettre fonction du triDefaut
                    break;
                
                // supprimer une tache
                case 2:
                    Console.WriteLine("quel est le titre de la tache que tu veux supprimer ?\n");
                    String tacheAsupprimer = Console.ReadLine();
                    // modifier pour que ca demande le numero de la tache a supprimer
                    for (int i = taskList.Count - 1; i >= 0; i--)
                    {
                        if (taskList[i].getTitre().Equals(tacheAsupprimer))
                        {
                            taskList.RemoveAt(i);
                            break;
                        }
                    }
                    break;
                
                // voir toutes les taches
                case 3:
                    numTache = 0;
                    foreach (var taches in taskList)
                    {
                        Console.WriteLine(numTache + ". " + taches.getTitre());
                        numTache++;
                    }
                    break;
                
                // voir le detail d'une tache
                case 4:
                    Console.WriteLine("indique le numerot de la tache dont tu veux voir le detail");
                    int index = int.Parse(Console.ReadLine());
                    if (index > taskList.Count - 1 || index < 0)
                    {
                        Console.WriteLine("La tache n'existe pas");
                        break;
                    }
                    afficherTache(taskList, index);
                    break;
                
                // modifier une tache
                case 5:
                    Console.WriteLine("quel est le numero de la tache que tu veux modifier ?");
                    numTache = int.Parse(Console.ReadLine());
                    Console.WriteLine("veux tu modifier le titre de la tache ? (y/n)");
                    if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                    {
                        Console.WriteLine("quel titre veux tu lui donner ?");
                        String newTitre = Console.ReadLine();
                        taskList[numTache].setTitre(newTitre);
                    }
                    if (Console.ReadLine() == "y" || Console.ReadLine() == "Y") ;
                    Console.WriteLine("veux tu modifier la description de la tache ? (y/n)");
                    if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                    {
                        Console.WriteLine("entre la description de la tache :");
                        String newDescription = Console.ReadLine();
                        taskList[numTache].setDescription(newDescription);
                    }
                    if (Console.ReadLine() == "n" || Console.ReadLine() == "N") ;
                    Console.WriteLine("veux tu modifier l'importance de la tache ? (y/n)");
                    if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                    {
                        Console.WriteLine("quelle note d'importance veux tu lui attribuer ?");
                        int newImportance = int.Parse(Console.ReadLine());
                        taskList[numTache].setImportance(newImportance);
                    }
                    if (Console.ReadLine() == "y" || Console.ReadLine() == "Y") ;
                    break;
                
                // trier la liste des taches 
                case 6:
                    Console.Clear();
                    Console.WriteLine("comment veux tu trier tes taches");
                    Console.WriteLine("1. par importance");
                    Console.WriteLine("2. par ordre alphabetique (par defaut)");
                    Console.WriteLine("3. retour");
                    int choixTri = 0;
                    while (choixTri != 3)
                    {
                        choixTri = int.Parse(Console.ReadLine());
    
                        switch (choixTri)
                        {
                            case 1:
                                triImportance(taskList);
                                break;
                            case 2:
                                triAlpha(taskList);
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("choix inconnu");
                                break;
                        }
                    }
                    break;
            }
        }
    }
}