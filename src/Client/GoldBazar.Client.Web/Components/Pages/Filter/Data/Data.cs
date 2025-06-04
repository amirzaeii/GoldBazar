

using GoldBazar.Shared.DTOs;

public static class DummySearchData
{
    public static SearchResultDTO GetSearchResult()
    {
        return new SearchResultDTO
        {
            Sorts = new List<Sort>
            {
                new Sort
                {
                    Title = "هەرزان بۆ گران",
                    Value = "ascending"
                },
                new Sort
                {
                    Title = "گران بۆ هەرزان",
                    Value = "descending"
                },
            },
            DefaultSort = "ascending",
            Filters = new Dictionary<string, Filter>
            {
                {
                    "price", new Filter
                    {
                        Type = FilterType.Slider,
                        Title = "نرخ",
                        Icon = "slider-icon",
                        Options = new SliderFilterOptions
                        {
                            Min = 0,
                            Max = 1000
                        }
                    }
                },
                {
                    "productType", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "جۆری بەرهەم",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "1", Title = "جۆر ١" },
                                new CheckboxOption { Id = "2", Title = "جۆر ٢" }
                            }
                        }
                    }
                },
                {
                    "duration", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "مەودای کێش",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "short", Title = "کەم" },
                                new CheckboxOption { Id = "long", Title = "زۆر" }
                            }
                        }
                    }
                },
                {
                    "material", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "ماددە",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "metal", Title = "ئاسن" },
                                new CheckboxOption { Id = "plastic", Title = "پلاستیک" }
                            }
                        }
                    }
                },
                {
                    "occasion", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "بۆنە",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "wedding", Title = "هاوسەرگیری" },
                                new CheckboxOption { Id = "daily", Title = "ڕۆژانە" }
                            }
                        }
                    }
                },
                {
                    "braceletType", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "جۆری عەڵقە",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "leather", Title = "پۆست" },
                                new CheckboxOption { Id = "chain", Title = "زنجیر" }
                            }
                        }
                    }
                },
                {
                    "buckleType", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "جۆری ملوانکە",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "hook", Title = "هوک" },
                                new CheckboxOption { Id = "clasp", Title = "کلیپ" }
                            }
                        }
                    }
                },
                {
                    "bezelType", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "جۆری بازن",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "fixed", Title = "بەجێماوە" },
                                new CheckboxOption { Id = "rotating", Title = "بەدوورخستن" }
                            }
                        }
                    }
                },
                {
                    "backType", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "جۆری پشتێن",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "transparent", Title = "ڕووناک" },
                                new CheckboxOption { Id = "solid", Title = "تەخت" }
                            }
                        }
                    }
                },
                {
                    "setType", new Filter
                    {
                        Type = FilterType.Checkbox,
                        Title = "جۆری تاقم",
                        Icon = "checkbox-icon",
                        Options = new CheckboxFilterOptions
                        {
                            Items = new List<CheckboxOption>
                            {
                                new CheckboxOption { Id = "single", Title = "تاق" },
                                new CheckboxOption { Id = "set", Title = "تاقم" }
                            }
                        }
                    }
                }
            },
            Products = new List<ItemDTO>
            {
                new ItemDTO
                {
                Discount = 10,
                MainPhoto = "/images/product/0.png",
                IsFavorite = true,
                Caption = "گوارەی عەیارە 18",
                Price = 3232323,
                OldPrice = 145454,
                },
                new ItemDTO
                {
                Discount = 10,
                MainPhoto = "/images/product/2.png",
                IsFavorite = true,
                Caption = "گوارەی عەیارە 18",
                },
                new ItemDTO
                {
                Discount = 15,
                MainPhoto = "/images/product/1.png",
                IsFavorite = true,
                Caption = "گوارەی عەیارە 24",
                Price = 7894561,
                OldPrice = 8120000,
                },
                new ItemDTO
                {
                Discount = 15,
                MainPhoto = "/images/product/1.png",
                IsFavorite = true,
                Caption = "گوارەی عەیارە 24",
                Price = 7894561,
                OldPrice = 8120000,
                }
            }
        };
    }
}
