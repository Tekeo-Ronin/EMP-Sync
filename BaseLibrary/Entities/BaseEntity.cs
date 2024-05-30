using System.Text.Json.Serialization;

namespace BaseLibrar.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    //One to many
    [JsonIgnore]
    public List<Employee>? Employees { get; set; }
}