namespace robot_controller_api;

public class Map
{
    public int id { get; set; }
    public int columns { get; set; }
    public int rows { get; set; }
    public string name { get; set; } 
    public string? description { get; set; }
    public DateTime createdDate { get; set; }
    public DateTime modifiedDate { get; set; }

    public Map()
    {
    }
        
    public Map(int Id, int Columns, int Rows, string Name, DateTime CreatedDate, DateTime ModifiedDate, string? Description = null)
    {
        id = Id;
        columns = Columns;
        rows = Rows;
        name = Name;
        description = Description;
        createdDate = CreatedDate;
        modifiedDate = ModifiedDate;
    }
}
