namespace LootBoxSimulator.Models.DAO;

public class ChestDao
{
    public int Id { get; set; }
    public string Name { get; set; }   
    public string Description { get; set; }
    public int Capacity { get; set; }
    public virtual List<ItemDao>? Items { get; set; }
}