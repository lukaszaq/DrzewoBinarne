using System.Collections;

Person Dyrektor = new Person("Jakub", "Dyrektor", 18);
Person Kierownik1 = new Person("Tomasz", "Kierownik1", 18);
Person Kierownik2 = new Person("Janusz", "Kierownik2", 18);
Person Podwładny11 = new Person("Adam", "Podwładny11", 18);
Person Podwładny12 = new Person("Kamil", "Podwładny12", 18);
Person Podwładny13 = new Person("Józef", "Podwładny13", 18);
Person Podwładny21 = new Person("Łukasz", "Podwładny21", 18);
Person Podwładny22 = new Person("Adrian", "Podwładny22", 18);
Person Podwładny23 = new Person("Marcin", "Podwładny23", 18);

Firma firma = new Firma(Dyrektor);

firma.DodajPodwładnego(Kierownik1);
firma.DodajPodwładnego(Podwładny11);
firma.DodajPodwładnego(Podwładny12);
firma.DodajPodwładnego(Podwładny13);
firma.WybierzDyrektora();
firma.DodajPodwładnego(Kierownik2);
firma.DodajPodwładnego(Podwładny21);
firma.DodajPodwładnego(Podwładny22);
firma.DodajPodwładnego(Podwładny23);

firma.WybierzDyrektora();
firma.WypiszPodwladnych();

firma.WybierzPracownika(Kierownik1);
firma.WypiszPodwladnych();

public class Firma
{
    TreeNode<Person> tree;
    public TreeNode<Person> wybranyPracownik;

    public Firma(Person Dyrektor)
    {
        tree = new TreeNode<Person>(Dyrektor);
        wybranyPracownik = tree;
    }

    public void DodajPodwładnego(Person podwładny)
    {
        wybranyPracownik = wybranyPracownik.AddChild(podwładny);
    }

    public void WyswietlHierarchie()
    {
        TreeNode<Person> tmp = wybranyPracownik;
        while (!tmp.IsRoot())
        {
            Console.WriteLine(tmp.Data.ToString());
            tmp = tmp.Parent;
        }
    }

    public void WybierzDyrektora()
    {
        TreeNode<Person> tmp = wybranyPracownik;
        while (!tmp.IsRoot())
        {
            tmp = tmp.Parent;
        }
        wybranyPracownik = tmp;
    }

    public void WybierzPracownika(Person pracownik)
    {
        if(tree.Data.Equals(pracownik))
        {
            wybranyPracownik = tree;
        }
        else if(!tree.IsLeaf())
        {
            tree.Childrens.ForEach(podwladny =>
            {
                WybierzPracownika(podwladny);
            });
        }
        else
        {
            Console.Write($"{pracownik} Nie istnieje w firmie");
        }
    }

    public void WypiszPodwladnych()
    {
        Console.WriteLine($"\t Podwładni pracownika: {wybranyPracownik.Data} : \n");
        wybranyPracownik.Childrens.ForEach(podwladny =>
        {
            Console.WriteLine(podwladny.Data.ToString());
            this.WypiszPodwladnych(podwladny);
        });
    }

    private void WybierzPracownika(TreeNode<Person> pracownik)
    {
        if (pracownik.Data.Equals(pracownik.Data))
        {
            wybranyPracownik = pracownik;
        }
        else if(!pracownik.IsLeaf())
        {
            pracownik.Childrens.ForEach(podwladny =>
            {
                WybierzPracownika(podwladny);
            });
        }
    }

    private void WypiszPodwladnych(TreeNode<Person> kierownik)
    {
        kierownik.Childrens.ForEach(podwladny =>
        {
            if(!podwladny.IsLeaf())
            {
                Console.WriteLine($"\t {podwladny.Data}");
                this.WypiszPodwladnych(podwladny);
            }
        });
    }
}
public class Person
{
    public int Id { get; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    static int _id = 101;
    public Person(string Firstname, string Lastname, int Age)
    {
        this.Id = _id++;
        this.Firstname = Firstname; this.Lastname = Lastname; this.Age = Age;
    }
    public override string ToString() => $"{Firstname} {Lastname} age: {Age} id: {Id}";
    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else if(obj.GetHashCode == this.GetHashCode)
        {
            return true;
        }
        else
        {
            Person p = (Person)obj;
            return this.Id == p.Id;
        }
    }

    public override int GetHashCode()
    {
        return this.Id;
    }
}

public class TreeNode<T>
{
    public T Data { get; set; }
    public TreeNode<T> Parent { get; set; }
    public List<TreeNode<T>> Childrens { get; set; }
    public List<TreeNode<T>> Brothers { get; set; }

    public TreeNode(T data)
    {
        this.Data = data;
        this.Childrens = new List<TreeNode<T>>();
        this.Brothers = new List<TreeNode<T>>();
    }

    public TreeNode<T> AddChild(T child)
    {
        TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
        this.Childrens.Add(childNode);
        this.Childrens.ForEach(child => {
            childNode.AddBrother(childNode);
        });
        return childNode;
    }

    private void AddBrother(TreeNode<T> brotherNode)
    {
        this.Brothers.Add(brotherNode);
    }

    public bool IsRoot()
    {
        return this.Parent == null;
    }    
    public bool IsLeaf()
    {
        return this.Childrens == null;
    }
}

