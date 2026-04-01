#nullable enable

namespace MyMauiApp.ViewModel.Entity;

public class ApiRestResponse
{
    public List<AnimalsData>? data { get; set; }
}

public class AnimalsData
{
    public string length { get; set; }
    public string origin { get; set; }
    public string image_link { get; set; }
    public int family_friendly { get; set; }
    public int shedding { get; set; }
    public int general_health { get; set; }
    public int playfulness { get; set; }
    public int children_friendly { get; set; }
    public int grooming { get; set; }
    public int intelligence { get; set; }
    public int other_pets_friendly { get; set; }
    public double min_weight { get; set; }
    public double max_weight { get; set; }
    public double min_life_expectancy { get; set; }
    public double max_life_expectancy { get; set; }
    public string name { get; set; }
    public int? meowing { get; set; }
    public int? stranger_friendly { get; set; }
}
