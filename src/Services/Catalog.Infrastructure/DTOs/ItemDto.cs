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
decimal Purity,
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
    public decimal Price { get; init; } = (((2745m/31.103m)* Purity) + CostPerGram - Discount) * Weight;
    public decimal OldPrice { get; init; } = (((2745m/31.103m)* Purity) + CostPerGram) * Weight;
}
