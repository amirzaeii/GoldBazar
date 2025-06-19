using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBazar.Shared.DTOs;
public record PromotionDTO
{
    public PromotionDTO() { }

    public PromotionDTO(int id, string title, string? imageUrl)
    {
        Id = id;
        Title = title;
        ImageUrl = imageUrl;
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}