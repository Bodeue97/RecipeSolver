
using RecipeSolverAPI.Data.DataModels;


namespace RecipeSolverAPI.Data;


public class DbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<DataContext>()!;

            context.Database.EnsureCreated();
            Console.WriteLine(context.FoodProducts.Count());


            if (context.FoodProducts.Count() == 0) 
            {

                var products = new List<FoodProduct>
                {
                new FoodProduct
                {
                Name = "Mleko 3.5%",
                Unit = "g",
                Type = "Nabiał",
                EnergyValue = 64m,
                Protein = 3.3m,
                TotalFat = 3.5m,
                SaturatedFat = 2.1m,
                MonounsaturatedFat = 1.11m,
                PolyunsaturatedFat = 0.1m,
                Cholesterol = 14m,
                DigestibleCarbohydrates = 4.8m,
                Glucose = 0m,
                Fructose = 0m,
                Sucrose = 0.2m,
                Lactose = 4.6m,
                Fiber = 0m,
                Sodium = 44m,
                Potassium = 138m,
                Calcium = 118m,
                Phosphorus = 85m,
                Magnesium = 12m,
                Iron = 0.1m,
                VitaminA = 40m,
                BetaCarotene = 22m,
                VitaminD = 0.03m,
                VitaminE = 0.11m,
                Thiamine = 0.036m,
                Riboflavin = 0.17m,
                Niacin = 0.1m,
                VitaminC = 1m
            },
                new FoodProduct
            {
                Name = "Mleko kozie",
                Unit = "g",
                Type = "Nabiał",
                EnergyValue = 86m,
                Protein = 3.2m,
                TotalFat = 4.1m,
                SaturatedFat = 2.34m,
                MonounsaturatedFat = 1.05m,
                PolyunsaturatedFat = 0.14m,
                Cholesterol = 11m,
                DigestibleCarbohydrates = 4.5m,
                Glucose = 0m,
                Fructose = 0m,
                Sucrose = 0m,
                Lactose = 4.4m,
                Fiber = 0m,
                Sodium = 40m,
                Potassium = 161m,
                Calcium = 130m,
                Phosphorus = 127m,
                Magnesium = 14,
                Iron = 0.1m,
                VitaminA = 74m,
                BetaCarotene = 35m,
                VitaminD = 0.11m,
                VitaminE = 0.1m,
                Thiamine = 0.03m,
                Riboflavin = 0.133m,
                Niacin = 0.25m,
                VitaminC = 1m
            },
                new FoodProduct
             {
                Name = "Jaja kurze",
                Unit = "g",
                Type = "Mięso/Białko zwierzęce",
                EnergyValue = 140m,
                Protein = 12.5m,
                TotalFat = 9.7m,
                SaturatedFat = 3m ,
                MonounsaturatedFat = 4.34m ,
                PolyunsaturatedFat = 0.81m,
                Cholesterol = 360m,
                DigestibleCarbohydrates = 0.6m,
                Glucose = 0m,
                Fructose = 0m,
                Sucrose = 0m,
                Lactose = 0m,
                Fiber = 0m,
                Sodium = 141m,
                Potassium = 133m,
                Calcium = 47m,
                Phosphorus = 204m,
                Magnesium = 12m,
                Iron = 2.2m,
                VitaminA = 272m,
                BetaCarotene = 13m,
                VitaminD = 1.7m,
                VitaminE = 0.73m,
                Thiamine = 0.064m,
                Riboflavin = 0.542m,
                Niacin = 0.06m,
                VitaminC = 0m
             },
                new FoodProduct
             {
                Name = "Pierś kurczaka",
                Unit = "g",
                Type = "Mięso/Białko zwierzęce",
                EnergyValue = 98m,
                Protein = 21.5m,
                TotalFat = 1.3m,
                SaturatedFat = 0.29m,
                MonounsaturatedFat = 0.3m,
                PolyunsaturatedFat = 0.3m,
                Cholesterol = 58m,
                DigestibleCarbohydrates = 0m,
                Glucose = 0m,
                Fructose = 0m,
                Sucrose = 0m,
                Lactose = 0m,
                Fiber = 0m,
                Sodium = 55m,
                Potassium = 385m,
                Calcium = 5m,
                Phosphorus = 240m,
                Magnesium = 33m,
                Iron = 0.4m,
                VitaminA = 6m,
                BetaCarotene = 0m,
                VitaminD = 0m,
                VitaminE = 0.3m,
                Thiamine = 0.09m,
                Riboflavin = 0.153m,
                Niacin = 12.44m,
                VitaminC = 0m

             },
                new FoodProduct
             {
                Name = "Cebula",
                Unit = "g",
                Type = "Warzywa",
                EnergyValue = 33m,
                Protein = 1.4m,
                TotalFat = 0.4m,
                SaturatedFat = 0.11m,
                MonounsaturatedFat = 0.04m,
                PolyunsaturatedFat = 0.17m,
                Cholesterol = 0m,
                DigestibleCarbohydrates = 5.2m,
                Glucose = 1.7m,
                Fructose = 1.5m,
                Sucrose = 1.9m,
                Lactose = 0m,
                Fiber = 1.7m,
                Sodium = 6m,
                Potassium = 121m,
                Calcium = 25m,
                Phosphorus = 14m,
                Magnesium = 8m,
                Iron = 0.5m,
                VitaminA = 2m,
                BetaCarotene = 12m,
                VitaminD = 0m,
                VitaminE = 0.12m,
                Thiamine = 0.03m,
                Riboflavin = 0.03m,
                Niacin = 0.2m,
                VitaminC = 6m

             },
                new FoodProduct
             {
                Name = "Pomidor",
                Unit = "g",
                Type = "Owoce",
                EnergyValue = 19m,
                Protein = 0.9m,
                TotalFat = 0.2m,
                SaturatedFat = 0.05m,
                MonounsaturatedFat = 0.02m,
                PolyunsaturatedFat = 0.12m,
                Cholesterol = 0,
                DigestibleCarbohydrates = 2.9m,
                Glucose = 1.2m,
                Fructose = 1.5m,
                Sucrose = 0.1m,
                Lactose = 0m,
                Fiber = 1.2m,
                Sodium = 8m,
                Potassium = 282m,
                Calcium = 9m,
                Phosphorus = 21m,
                Magnesium = 8m,
                Iron = 0.5m,
                VitaminA = 107m,
                BetaCarotene = 640m,
                VitaminD = 0m,
                VitaminE = 1.22m,
                Thiamine = 0.064m,
                Riboflavin = 0.042m,
                Niacin = 1m,
                VitaminC = 23m

                },

            };
                context.FoodProducts.AddRange(products);
                context.SaveChanges();
                Console.WriteLine("Data seeded successfully!");




            }
            else
            {
                Console.WriteLine(context.FoodProducts.Count());

            }



        }
    }

   
}