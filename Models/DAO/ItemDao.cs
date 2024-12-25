namespace LootBoxSimulator.Models.DAO;

public class ItemDao
{
    public int Id { get; set; }
    public string Name { get; set; }   
    public string Description { get; set; }
    public string Url { get; set; }
    public virtual CategoryDao? Category { get; set; }
    public virtual RateDao? Rate { get; set; }
    public virtual List<ChestDao> Chest { get; set; }
    
}