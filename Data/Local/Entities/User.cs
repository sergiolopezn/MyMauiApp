using SQLite;

namespace MyMauiApp.Data.Local.Entities;

[Table("user")]
public class User
{
    // Primary key with auto-increment
    [PrimaryKey, AutoIncrement, Column("_id")]
    public int Id { get; set; }

    [Column("name"), MaxLength(100)]
    public string Name { get; set; }
    [Column("email"), MaxLength(250)]
    public string Email { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
