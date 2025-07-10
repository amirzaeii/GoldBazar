using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

public class Nav
{
    public string Href { get; set; } = "/";
    public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;
    public RenderFragment Icon { get; set; } = builder => { };
    public string Label { get; set; } = "";
}