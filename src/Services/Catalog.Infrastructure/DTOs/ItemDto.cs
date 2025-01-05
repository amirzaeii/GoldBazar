namespace Catalog.Infrastructure;

public record ItemDto(int Id, 
string Caption, 
string Description,
decimal CostPerGram,
decimal Weight,
int Size,
int TypeId,
string TypeName,
int MetalId,
string MetalName,
decimal KT,
int ShopId,
string ShopName,
string City,
int MaterialId,
string MaterailName,
int OccasionId,
string OccasionName,
int StyleId,
string StyleName,
decimal Discount,
bool Status
)
{
    public decimal Price { get; init; } = ((2625.0m/31.103m)* KT/1000 + CostPerGram - Discount) * Weight;
    public decimal OldPrice { get; init; } = ((2625.0m/31.103m)* KT/1000 + CostPerGram) * Weight;
}
