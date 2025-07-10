using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBazar.Shared.Components.Extension.Helpers;
public static class PriceHelper
{
    /// <summary>
    /// Format a decimal price with 2 decimal points.
    /// </summary>
    public static string ToPriceString(this decimal price)
    {
        return price.ToString("N2");
    }
}
