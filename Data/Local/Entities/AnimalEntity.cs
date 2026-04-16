using SQLite;

namespace MyMauiApp.Data.Local.Entities;

[Table("animal")]
public class AnimalEntity
{
    [PrimaryKey, AutoIncrement, Column("id")]
    public int Id { get; set; }
    
    [Column("image_link")]
    public string imageLink { get; set; }
    
    [Column("origin"), MaxLength(100)]
    public string origin { get; set; }
    
    [Column("name"), MaxLength(100)]
    public string name { get; set; }

}
