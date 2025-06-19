using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBazar.Shared.DTOs;
public class PromotionSliderDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public int Priority { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public PromotionSliderDTO() { }

    public PromotionSliderDTO(int id, string title, string desc, string img, string link, int priority, bool isActive, DateTime created, DateTime? updated)
    {
        Id = id;
        Title = title;
        Description = desc;
        ImageUrl = img;
        Link = link;
        Priority = priority;
        IsActive = isActive;
        CreatedAt = created;
        UpdatedAt = updated;
    }
}
