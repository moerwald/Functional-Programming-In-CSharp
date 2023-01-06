using System.Collections.Immutable;
using System.Diagnostics;
using LaYumba;
using LaYumba.Functional;

using Unit = System.ValueTuple;

public static class Programm
{
    private static Unit TheUnit = new Unit();

    public static void Main(string[] _)
    {
        Func<Either<Reason, Unit>> WakeUpEarly = () => TheUnit;
        Func<Either<Reason, Ingredients>> ShopForIngredients = () => new Ingredients( ImmutableList.Create<string>("I1", "I2"));
        Func<Ingredients, Either<Reason, Food>> CookRecipe = i => new Food("Hot dog");

        Action<Food> EnjoyToGether = f => Console.WriteLine($"Yummy {f}");
        Action<Reason> ComplainAbout = r => Console.WriteLine($"Not ok: {r}");
        Action OrderPizza = () => Console.WriteLine("We're ordering pizza");

        WakeUpEarly()
            .Bind(_ => ShopForIngredients())
            .Bind(CookRecipe)
            .Match(reason =>
            {
                ComplainAbout(reason);
                OrderPizza();
            }, EnjoyToGether);
    }
}

public record Food(string Name);
public record Ingredients(ImmutableList<string> Ingreds);
public record Reason (string Description);