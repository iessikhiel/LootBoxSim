using System;
using System.Drawing;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using LootBoxSimulator.Models.DAO;
using Microsoft.EntityFrameworkCore;

RemoteDatabaseContext db = new RemoteDatabaseContext();
StringBuilder stringBuilder = new StringBuilder();

RemoteDatabaseContext databaseContext = new RemoteDatabaseContext();

int i = 0;

    decimal[] rates = new decimal[6];
    foreach (var rateDao in db.Rates)
        { 
            rates[i] = rateDao.Rate;    
            i++;
        }

    Array.Sort(rates);
    Array.Reverse(rates);

    stringBuilder.Append("________PRINT________");
    stringBuilder.AppendLine();

    stringBuilder.Append("1. Print category");
    stringBuilder.AppendLine();
    stringBuilder.Append("2. Print rate");
    stringBuilder.AppendLine();
    stringBuilder.Append("3. Print items");
    stringBuilder.AppendLine();
    stringBuilder.Append("4. Chests information");

    stringBuilder.AppendLine();
    stringBuilder.Append("________CATEGORY_______");
    stringBuilder.AppendLine();

    stringBuilder.Append("5. Edit category");
    stringBuilder.AppendLine();
    stringBuilder.Append("6. Add category");
    stringBuilder.AppendLine();
    stringBuilder.Append("7. Remove category");

    stringBuilder.AppendLine();
    stringBuilder.Append("______________________");
    stringBuilder.AppendLine();

    stringBuilder.Append("8. Check range");
    stringBuilder.AppendLine();
    stringBuilder.Append("9. Open(Test)");
    stringBuilder.AppendLine();

    stringBuilder.Append("______________________");
    stringBuilder.AppendLine();

    stringBuilder.Append("10. Open chests");
    stringBuilder.AppendLine();

    stringBuilder.Append("11. Add item");
    stringBuilder.AppendLine();


Console.WriteLine(stringBuilder.ToString());

if (!int.TryParse(Console.ReadLine(), out int menuPosition))
{
    error("Please enter a valid number");
    return;
}
    
    Console.Clear();
    Console.WriteLine("==============================");

switch (menuPosition)
{
    case 1:
        foreach (var categoryDao in db.Categories)
        {
            Console.WriteLine($"Id: {categoryDao.Id} Name: {categoryDao.Name}");
        }
        break;

    case 2:
    foreach (var rateDao in db.Rates)
        {
            Console.WriteLine($"Id: {rateDao.Id} Name: {rateDao.Name} Rate: {rateDao.Rate}");
        }
        break;

    case 3:
    foreach (var itemDao in db.Items)
        {
            Console.WriteLine($"Id: {itemDao.Id} Name: {itemDao.Name} Url: {itemDao.Url}");
        }
        break;

    case 4:
    foreach (var ChestDao in db.Chest)
        {
            Console.WriteLine($"Name: {ChestDao.Name}, Capacity: {ChestDao.Capacity}");
        }
        break;

    case 5: ChangeCategory(); break;

    case 6: AddCategory(); break;

    case 7: RemoveCategory(); break;

    case 8: NumberInRange(rates); break;

    case 9: ItemRandom(); break;

    case 10: OpenChest(); break;

    case 11: AddItem(); break;

    default:
        error("Unkown Menu Position");
        break;
}

void ChangeCategory()

{
    Console.WriteLine("Input Id: ");
    if(!int.TryParse(Console.ReadLine(), out int CategoryID))
    {
        Console.WriteLine("Invalid Id Z#@!#x$4#@");
        return;
    }

    Console.WriteLine("New category name: ");
    String? CategoryName = Console.ReadLine();

    if(String.IsNullOrEmpty(CategoryName)) 
    {
        error("Name mustn't be empty!");
        return;
    }
    var categoryDao = db.Categories.Find(CategoryID);

    if (categoryDao == null)
    {
        error("Category is not found.");
        return;
    }

    categoryDao.Name = CategoryName;
    db.SaveChanges();
        success();
}

void AddCategory()
{
    Console.WriteLine("Name for new category: ");
    String? CategoryName = Console.ReadLine();

    if(String.IsNullOrEmpty(CategoryName)) 
    {
        error("Name cannot be empty!");
    }
    CategoryDao categoryDao = new CategoryDao();
    categoryDao.Name = CategoryName;
    db.Categories.Add(categoryDao);
    db.SaveChanges();
        success();
}

void AddItem()
{
    Console.WriteLine("Name for new item: ");
    String? ItemName = Console.ReadLine();

    if(String.IsNullOrEmpty(ItemName)) 
    {
        error("Name cannot be empty!");
    }
    ItemDao itemDao = new ItemDao();
    itemDao.Name = ItemName;
    db.Items.Add(itemDao);

    db.SaveChanges();
        success();
}

void RemoveCategory()
{
    Console.WriteLine("Input Id to remove: ");
    if(!int.TryParse(Console.ReadLine(), out int CategoryID))
    {
        error("Invalid Id Z#@!#x$4#@");
        return;
    }
    var categoryDao = db.Categories.Find(CategoryID);
    db.Categories.Remove(categoryDao);
    db.SaveChanges();
        success();
}

void NumberInRange(decimal[] rates)
{
    decimal a = rates[0];
    decimal b = rates[1];

    Console.WriteLine("Input your percent: ");
    decimal c = Convert.ToDecimal(Console.ReadLine());

    if (c < 0 || c > 100)
    {
        error("Invalid value");
        return;
    }

while (c !> a && c !< b)
    {
        int i = 0;
        int j = 1;

        if (c < rates[0])
            Console.WriteLine("Your percent is over " + rates[0]);

        a = rates[i++];
        b = rates[j++];

        Console.WriteLine("Your percent is about " + rates[i] ," and " + rates[j]);
    }
}

void ItemRandom ()
{
   RemoteDatabaseContext databaseContext = new RemoteDatabaseContext();

    var sum = databaseContext.Rates.Select(dsadsa => dsadsa.Rate).Sum();

    var result = RandomizeRate();
    var item = RandomizeItem(result);
    Console.WriteLine($"{item?.Name} {item?.Id} \n{item?.Description}");

ItemDao? RandomizeItem(RateDao? rateDao){
    if(rateDao == null) return null;

    var items = databaseContext.Items.Where(item => item.Rate.Id == rateDao.Id).ToList();

    if(items.Count == 0) return null;
    Random random = new Random();
    int randomIndex = random.Next(0, items.Count);
    return items[randomIndex];
}


RateDao? RandomizeRate()
    {
        var rates = databaseContext.Rates.OrderByDescending(item => item.Rate).ToList();

        if (rates.Count == 0) return null;    

        decimal start = 0;
        decimal end = rates[0].Rate;

        Random random = new Random();

        decimal number = Convert.ToDecimal(random.NextDouble());
        for (var i = 1; i < rates.Count; i++)
        {
            if (number >= start && number <= end)
            {
                Console.WriteLine($"{number} {start} {end}");
                return rates[i];
            }
            
            start = end;
            end += rates[i].Rate;
        }
        return null;
    }
}

void OpenChest()
{
    Console.WriteLine("Please enter the chest Id:");
        if (!int.TryParse(Console.ReadLine(), out int ChestId))
        {
            Console.WriteLine("Error: Invalid Lootbox ID format.");
            return;
        }

    var Chest = db.Chest
           .Include(it => it.Items).ThenInclude(item => item.Category)
            .Include(it => it.Items).ThenInclude(item => item.Rate)
               .FirstOrDefault(chest => chest.Id == ChestId);


    if (Chest == null)
        {
            Console.WriteLine($"Error: Lootbox with ID {ChestId} not found.");
            return;
        }

        if (Chest.Items == null || Chest.Items.Count == 0)
        {
            Console.WriteLine($"Error: Lootbox with ID {ChestId} has no items.");
            return;
        }

        Random rnd = new Random();
        int index = rnd.Next(Chest.Items.Count);
        var item = Chest.Items[index];


        Console.ForegroundColor = GetConsoleColor(item.Rate.Color);
        Console.WriteLine($"{item.Id}, {item.Name}, {item.Category.Name}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"{item.Description}");
        Console.ResetColor();
        Console.WriteLine($"{item.Rate.Name}, Rate: {item.Rate.Rate}");
}
    static ConsoleColor GetConsoleColor(string colorName)
    {
        return colorName.ToLower() switch   
        {
            "orange" => ConsoleColor.DarkYellow,
            "gray" => ConsoleColor.Gray,
            "yellow" => ConsoleColor.Yellow,
            "purple" => ConsoleColor.Magenta,_ => ConsoleColor.White,
        };
    }

void success()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Change successful");
    Console.ResetColor(); 
}

void error(string word)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine(word);
    Console.ResetColor();
}