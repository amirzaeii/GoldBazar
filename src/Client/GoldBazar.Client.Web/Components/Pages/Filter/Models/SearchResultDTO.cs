using GoldBazar.Shared.DTOs;

public enum FilterType
{
    Slider,
    Checkbox
}

public interface IFilterOptions
{
}

public class SliderFilterOptions : IFilterOptions
{
    public int Min { get; set; }
    public int Max { get; set; }
}

public class CheckboxOption
{
    public string Id { get; set; }
    public string Title { get; set; }
}

public class CheckboxFilterOptions : IFilterOptions
{
    public List<CheckboxOption> Items { get; set; }
}

public class Filter
{
    public FilterType Type { get; set; }

    public string Title { get; set; }

    public string? Icon { get; set; }

    public IFilterOptions? Options { get; set; }
};

public class SearchResultDTO
{
    public Dictionary<string, Filter> Filters { get; set; }
    public List<ItemDTO> Products { get; set; }
};