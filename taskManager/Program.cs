using System;
using System.IO;
using System.Text.Json;

// class qui defini les parametres
public class Parametre
{
    private String formatEnregistrement;
    private String triDefaut;

    public Parametre(String formatEnregistrement, String triDefaut)
    {
        this.formatEnregistrement = formatEnregistrement;
        this.triDefaut = triDefaut;
    }

    // recuperer le format d'enregistrement
    public String getFormatEnregistrement()
    {
        return this.formatEnregistrement;
    }

    // recuperer le mode de tri
    public String getTriDefaut()
    {
        return this.triDefaut;
    }

    // redefinir le format d'enregistrement des taches
    public void setFormatEnregistrement(String formatEnregistrement)
    {
        this.formatEnregistrement = formatEnregistrement;
    }

    // redefinir le mode de tri par defaut
    public void setTriDefaut(String triDefaut)
    {
        this.triDefaut = triDefaut;
    }
}


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

    // modifie le titre
    public void setTitre(String titre)
    {
        this.titre = titre;
    }

    // modifie la description
    public void setDescription(String description)
    {
        this.description = description;
    }

    // modifie l'importance
    public void setImportance(int importance)
    {
        this.importance = importance;
    }
}


// programme principal
public class Program
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
        bool change = true;
        int n = taskList.Count;
        Tache tacheTmp;
        while (change)
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

    public static Parametre recupParam()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parametre.bin");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Le fichier de paramètres n'existe pas.");
            return null;
        }

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            string tri = reader.ReadString();
            string enregistrement = reader.ReadString();
            return new Parametre(tri, enregistrement);
        }
    }
    
    public static void SauvegarderParametres(Parametre parametre)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parametre.bin");
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            writer.Write(parametre.getFormatEnregistrement());
            writer.Write(parametre.getTriDefaut());
        }
    }
    
    // fonction principale que je vais lancer dans le main dans Program.cs
    public static void Main(String[] args)
    {
        Parametre parametre = recupParam();
        List<Tache> taskList = new List<Tache>();
        int choix = 1;
        int numTache = 0;
        while (choix != 10)
        {
            Console.WriteLine("que veux tu faire ?");
            Console.WriteLine("1. ajouter une tache");
            Console.WriteLine("2. supprimer une tache");
            Console.WriteLine("3. afficher la liste des taches");
            Console.WriteLine("4. afficher une tache entierement");
            Console.WriteLine("5. modifier une tache");
            Console.WriteLine("6. trier la liste");
            Console.WriteLine("7. sauvegarder une tache");
            Console.WriteLine("9. parametres");
            // les taches seront supprimées quand on quitte le programme et sont chargées quand le programme se lance
            Console.WriteLine("10. quitter");
            choix = int.Parse(Console.ReadLine());
            switch (choix)
            {
                // option pour ajouter une tache
                case 1:
                    Tache tacheAajouter = ajouterTache();
                    taskList.Add(tacheAajouter);
                    if (parametre.getTriDefaut().Equals("importance"))
                        triImportance(taskList);
                    else
                        triAlpha(taskList);
                    break;
                
                // option pour supprimer une tache
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
                
                // option pour voir toutes les taches
                case 3:
                    numTache = 0;
                    foreach (var taches in taskList)
                    {
                        Console.WriteLine(numTache + ". " + taches.getTitre());
                        numTache++;
                    }
                    break;
                
                // option pour voir le detail d'une tache
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
                
                // option pour modifier une tache
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
                    if (Console.ReadLine() == "n" || Console.ReadLine() == "N") ;
                    
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
                    break;
                
                // option pour trier la liste des taches 
                case 6:
                    Console.Clear();
                    int choixTri = 0;
                    while (choixTri != 3)
                    {
                        Console.WriteLine("comment veux tu trier tes taches");
                        Console.WriteLine("1. par importance");
                        Console.WriteLine("2. par ordre alphabetique");
                        Console.WriteLine("3. retour");
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
                
                    // changer les parametres
                    case 9:
                        Console.Clear();
                        int choixParam = 0;
                        int choixTriP = 0;
                        int choixEnregistrement = 0;
                        while (choixParam != 3)
                        {
                            Console.WriteLine("que veux tu modifier ?");
                            Console.WriteLine("1. format d'enregistrement : " + parametre.getFormatEnregistrement());
                            Console.WriteLine("2. methode de tri : " + parametre.getTriDefaut());
                            Console.WriteLine("3. quitter");
                            choixParam = int.Parse(Console.ReadLine());
                            if (choixParam == 1)
                            {
                                Console.WriteLine("quel format d'enregistrement veux tu utiliser ?");
                                Console.WriteLine("1. bin");
                                Console.WriteLine("2. Json");
                                choixEnregistrement = int.Parse(Console.ReadLine());
                                if (choixEnregistrement == 1)
                                    parametre.setFormatEnregistrement("bin");
                                else if (choixEnregistrement == 2)
                                    parametre.setFormatEnregistrement("Json");
                            }
                            else if (choixParam == 2)
                            {
                                Console.WriteLine("quelle methode de tri veux tu utiliser ?");
                                Console.WriteLine("1. tri par importance de tache");
                                Console.WriteLine("2. tri des taches par ordre alphabetique");
                                choixTriP = int.Parse(Console.ReadLine());
                                if (choixTriP == 1)
                                    parametre.setTriDefaut("importance");
                                else if (choixTriP == 2)
                                    parametre.setTriDefaut("alphabetique");
                            }
                            else if (choixParam == 3)
                            {
                                Console.WriteLine("format d'enregistrement : " + parametre.getFormatEnregistrement());
                                Console.WriteLine("methode de tri : " + parametre.getTriDefaut());
                                SauvegarderParametres(parametre);
                            }
                        }
                        break;
            }
        }
    }
}