// See https://aka.ms/new-console-template for more information
using System.Reflection;

Console.WriteLine("Hello, World!");

Hero hero = new Hero();
hero.Loot(new Loup());
hero.Loot(new Orc());
hero.Loot(new Dragon());


class Hero
{
    public void Loot(Monstre monstre)
    {
        Type type = monstre.GetType();
        Console.WriteLine($"Je loot un '{type.Name}'");
        
        IEnumerable<LootAttribute> lootAttributes = type.GetCustomAttributes<LootAttribute>();
        foreach(LootAttribute lootAttribute in lootAttributes)
        {
            Console.WriteLine($"Je ramasse {lootAttribute.Quantity} {lootAttribute.DisplayName}");
        }


        Console.WriteLine();
    }
}

[AttributeUsage(AttributeTargets.Class)]
public abstract class LootAttribute : Attribute
{
    private Random _random;
    private int _maxValue;

    public LootAttribute(int maxValue, string displayName)
    {
        _random = new Random(Random.Shared.Next());
        _maxValue = maxValue;
        DisplayName = displayName;
    }

    public int Quantity
    {
        get { return _random.Next(_maxValue); }
    }

    public string DisplayName { get; init; }

}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class OrAttribute : LootAttribute
{
    public OrAttribute(int maximum = 6) : base(maximum, "pièce(s) d'or")
    {
        
    }
}

public class CuirAttribute : LootAttribute
{
    public CuirAttribute(int maximum = 4) : base(maximum, "cuir(s)")
    {

    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class GemmeAttribute : LootAttribute
{
    public GemmeAttribute(int maximum) : base(maximum, "émeraude(s)")
    {

    }
}

[Gemme(25)]
public abstract class Monstre
{

}

[Cuir]
public class Loup : Monstre
{

}

[Or]
public class Orc : Monstre
{

}

[Or(250)]
[Cuir(25)]
[Gemme(35)]
public class Dragon : Monstre
{

}