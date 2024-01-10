using System.IO.Compression;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        PlantId = 1,
        Species = "Feverfew",
        LightNeeds = 3,
        AskingPrice = 11.49M,
        City = "Naperville",
        ZIP = 62564,
        Sold = true,
        AvailableUntil = new DateTime (2024, 7, 4),

    },
    new Plant()
    {
        PlantId = 2,
        Species = "Ginkgo",
        LightNeeds = 4,
        AskingPrice = 9.99M,
        City = "Aurora",
        ZIP = 75464,
        Sold = false,
        AvailableUntil = new DateTime (2023, 8, 4),

    },
    new Plant()
    {
        PlantId = 3,
        Species = "Shameplant",
        LightNeeds = 5,
        AskingPrice = 12.50M,
        City = "Lisle",
        ZIP = 23524,
        Sold = true,
        AvailableUntil = new DateTime (2025, 12, 4),

    },
    new Plant()
    {
        PlantId = 4,
        Species = "Common Wheat",
        LightNeeds = 6,
        AskingPrice = 4.14M,
        City = "Lombard",
        ZIP = 13564,
        Sold = false,
        AvailableUntil = new DateTime (2026, 9, 4),

    },
    new Plant()
    {
        PlantId = 5,
        Species  = "Norway Spruce",
        LightNeeds = 7,
        AskingPrice = 10.00M,
        City = "Chicago",
        ZIP = 60564,
        Sold = false,
        AvailableUntil = new DateTime (2025, 1, 1),
    }
};

List<Plant> adoptedPlants = new List<Plant>();

Plant plantOfTheDay;

string greeting = @"Welcome to ExtraVert
Where users can browse, list, and buy secondhand plants!" + Environment.NewLine;

string keystroke = "Press any key to continue...";

Console.WriteLine(greeting);

ViewShopOptions();

void ViewAllPlants()
{
    Console.WriteLine("\n\nPlants:\n\n");

    for (int i = 0; i < plants.Count; i++)
    {
        string plantInfo = PlantDetails(plants[i]);

        Console.WriteLine($"{i + 1}. {plantInfo}\n");
    }

    Console.WriteLine();
    Console.WriteLine(keystroke);
    Console.ReadKey();
    Console.Clear();
}

void ViewShopOptions()
{
    string choice = null;
    while (choice != "8")
    {
        Console.WriteLine
        (Environment.NewLine + "Choose an option:\n\n" +
                  "0. Display All Plants\n" +
                  "1. Plant of the Day\n" +
                  "2. Post a Plant to be Adopted\n" +
                  "3. Adopt a Plant\n" +
                  "4. View Adopted Plants\n" +
                  "5. Delist a Plant\n" +
                  "6. Search by Light Needs\n" +
                  "7. App Statistics\n" +
                  "8. Exit\n");

        choice = Console.ReadLine();
        if (choice == "0")
        {
            ViewAllPlants();
        }
        else if (choice == "1")
        {
            PlantOfTheDay();
        }
        else if (choice == "2")
        {
            PostAPlant();
        }
        else if (choice == "3")
        {
            AdoptAPlant();
        }
        else if (choice == "4")
        {
            ViewMyAdoptedPlants();
        }
        else if (choice == "5")
        {
            DelistAPlant();
        }
        else if (choice == "6")
        {
            SearchByLightNeeds();
        }
        else if (choice == "7")
        {
            AppStatistics();
        }
        else if (choice == "8")
        {
            Console.WriteLine("\n\nThanks for Stopping by, Come back Soon!");
            break;
        }
    }

}

void PostAPlant()
{
    Console.WriteLine("\n\nEnter plant details:");

    try
    {
        string plantSpecies;

        do
        {
            Console.Write("Species: ");
            plantSpecies = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(plantSpecies))
            {
                Console.WriteLine("Please enter a valid plant species.");
            }

        } while (string.IsNullOrWhiteSpace(plantSpecies));

        Console.Write("Enter the plant's Light Needs (out of 10): ");
        int lightNeeds;
        while (!int.TryParse(Console.ReadLine(), out lightNeeds) || lightNeeds < 0 || lightNeeds > 10)
        {
            Console.WriteLine("Please enter a valid integer between 0 and 10.");
            Console.Write("Enter Light Needs (out of 10): ");
        }

        decimal askingPrice;
        string input;

        do
        {
            Console.Write("Enter the Asking Price: ");
            input = Console.ReadLine();

            if (!decimal.TryParse(input, out askingPrice))
            {
                Console.WriteLine("Please enter a valid decimal value for the price.");
            }
        } while (!decimal.TryParse(input, out askingPrice));

        string plantCity;

        do
        {
            Console.Write("City: ");
            plantCity = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(plantCity))
            {
                Console.WriteLine("Please enter a valid city name.");
            }

        } while (string.IsNullOrWhiteSpace(plantCity));

        int plantZIP;

        do
        {
            Console.Write("ZIP Code: ");
            if (!int.TryParse(Console.ReadLine(), out plantZIP) || plantZIP.ToString().Length != 5)
            {
                Console.WriteLine("Please enter a valid 5-digit ZIP code.");
            }
        } while (plantZIP.ToString().Length != 5);

        Console.Write("Enter the expiration year: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Enter the expiration month: ");
        int month = int.Parse(Console.ReadLine());

        Console.Write("Enter the expiration day: ");
        int day = int.Parse(Console.ReadLine());

        try
        {
            DateTime expirationDate = new DateTime(year, month, day);

            // Instance
            Plant newPlant = new Plant
            {
                PlantId = plants.Count + 1,
                Species = plantSpecies,
                LightNeeds = lightNeeds,
                AskingPrice = askingPrice,
                City = plantCity,
                ZIP = plantZIP,
                Sold = false,
                AvailableUntil = expirationDate,
            };

            // List Add
            plants.Add(newPlant);

            Console.WriteLine("Plant added successfully!");
        }
        catch (ArgumentOutOfRangeException exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
            //prints specific message :)
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input. Please enter a valid numeric value.");
    }
    catch (Exception exception)
    {
        Console.WriteLine($"An unexpected error occurred: {exception.Message}");
    }
}

void AdoptAPlant()
{
    Console.WriteLine("\n\nAvailable Plants:\n");

    DateTime currentDate = DateTime.Now;

    for (int i = 0; i < plants.Count; i++)
    {
        if (!plants[i].Sold && plants[i].AvailableUntil > currentDate)
        {
            Console.WriteLine($"{i + 1}. {plants[i].Species}, {plants[i].City}, {plants[i].AskingPrice:C}");
        }
    }

    Console.WriteLine($"{plants.Count + 1}. Go back to the main menu");
    Console.Write("Enter the number of the plant to adopt (or 0 to cancel): ");

    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= plants.Count)
    {
        if (!plants[choice - 1].Sold && plants[choice - 1].AvailableUntil > currentDate)
        {
            Plant adoptedPlant = plants[choice - 1];
            adoptedPlant.Sold = true;
            adoptedPlants.Add(adoptedPlant);
            Console.WriteLine("Plant adopted successfully!");
        }
        else if (plants[choice - 1].Sold)
        {
            Console.WriteLine("Error: This plant has already been sold. Please choose another plant.");
        }
        else
        {
            Console.WriteLine("Error: This plant has expired. Please choose another plant.");
        }
    }
    else if (choice == plants.Count + 1)
    {

    }
    else
    {
        Console.WriteLine("0 indicated or invalid choice. Adoption canceled.");
    }

    Console.WriteLine(keystroke);
    Console.ReadKey();
    Console.Clear();
}

void ViewMyAdoptedPlants()
{
    Console.WriteLine("\n\nMy Adopted Plants:\n");

    foreach (var adoptedPlant in adoptedPlants)
    {
        Console.WriteLine($"- {adoptedPlant.Species}, {adoptedPlant.City}, {adoptedPlant.AskingPrice:C}");
    }

    Console.WriteLine(keystroke);
    Console.ReadKey();
    Console.Clear();
}

void DelistAPlant()
{
    Console.WriteLine("\n\nEnter the ID of the plant you want to delist:");

    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} ");
    }

    int plantIdToDelist;
    while (!int.TryParse(Console.ReadLine(), out plantIdToDelist) || plantIdToDelist < 1 || plantIdToDelist > plants.Count)
    {
        Console.WriteLine("Please enter a valid plant ID.");
        Console.WriteLine("Enter the ID of the plant you want to delist:");

        for (int i = 0; i < plants.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {plants[i].Species} ");
        }
    }

    Plant plantToDelist = plants[plantIdToDelist - 1];

    Console.WriteLine($"Are you sure you want to delist the plant: {plantToDelist.Species}? (yes/no)");

    string confirmation = Console.ReadLine().Trim().ToLower();

    if (confirmation == "yes")
    {
        plants.Remove(plantToDelist);
        Console.WriteLine("Plant delisted successfully!");
    }
    else
    {
        Console.WriteLine("Plant delist canceled.");
    }
}

void PlantOfTheDay()
{
    Random random = new Random();

    List<Plant> unsoldPlants = plants.Where(plant => !plant.Sold).ToList();

    if (unsoldPlants.Count > 0)
    {
        int randomPlantIndex = random.Next(0, unsoldPlants.Count);
        plantOfTheDay = unsoldPlants[randomPlantIndex];

        Console.WriteLine("\n\nPlant of the Day:\n");
        Console.WriteLine($"{plantOfTheDay.Species} is the Plant of the Day!");
        Console.WriteLine($"Light Needs: {plantOfTheDay.LightNeeds}");
        Console.WriteLine($"Asking Price: {plantOfTheDay.AskingPrice:C}");
        Console.WriteLine($"City: {plantOfTheDay.City}");
        Console.WriteLine($"ZIP Code: {plantOfTheDay.ZIP}");
    }
    else
    {
        Console.WriteLine("\n\nPlant of the Day: No unsold plants available.");
    }
}

void SearchByLightNeeds()
{
    Console.Write("Enter the maximum Light Needs Rating (1-10): ");
    if (int.TryParse(Console.ReadLine(), out int maxLightNeeds) && maxLightNeeds >= 1 && maxLightNeeds <= 10)
    {
        Console.WriteLine($"\nPlants with Light Needs at or below {maxLightNeeds}:\n");

        foreach (var plant in plants)
        {
            if (plant.LightNeeds <= maxLightNeeds)
            {
                Console.WriteLine($"{plant.Species}, Light Needs: {plant.LightNeeds}, Asking Price: {plant.AskingPrice:C}, City: {plant.City}, ZIP Code: {plant.ZIP}");
            }
        }
    }
    else
    {
        Console.WriteLine("Input invalid. Please enter a valid integer between 1 and 10.");
    }

    Console.WriteLine(keystroke);
    Console.ReadKey();
    Console.Clear();
}

void AppStatistics()
{
    //first or default returns the first value in the sorted list or the default property which in this case would be Null
    var lowestPricePlant = plants.OrderBy(p => p.AskingPrice).FirstOrDefault();
    Console.WriteLine($"Lowest price plant: {(lowestPricePlant != null ? $"{lowestPricePlant.Species} - {lowestPricePlant.AskingPrice:C}" : "No plants available")}");

    
    int availablePlantsCount = plants.Count(p => !p.Sold && p.AvailableUntil > DateTime.Now);
    Console.WriteLine($"Number of available plants: {availablePlantsCount}");

    // utilized null conditional operator in conjunction with null-coalescing operator
    var highestLightPlant = plants.OrderByDescending(p => p.LightNeeds).FirstOrDefault();
    Console.WriteLine($"Plant with highest light needs: {highestLightPlant?.Species ?? "No plants available"}");

    // doubles are utilized in average calculations
    double averageLightNeeds = plants.Average(p => p.LightNeeds);
    Console.WriteLine($"Average light needs: {averageLightNeeds:F}");

    // doubles are utilized in average calculations
    double percentageAdopted = (double)adoptedPlants.Count / plants.Count * 100;
    Console.WriteLine($"Percentage of plants adopted: {percentageAdopted:F}%");

    Console.WriteLine(keystroke);
    Console.ReadKey();
    Console.Clear();
}

string PlantDetails(Plant plant)
{
    string plantString = $"Species: {plant.Species}\n" +
                         $"Light Needs: {plant.LightNeeds}/10\n" +
                         $"Asking Price: {plant.AskingPrice:C}\n" +
                         $"City: {plant.City}\n" +
                         $"ZIP Code: {plant.ZIP}\n" +
                         $"Sold: {(plant.Sold ? "Yes" : "No")}\n" +
                         $"Available Until: {plant.AvailableUntil}\n";

    return plantString;
}