using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public class MediaTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ImageTemplate { get; set; }
        public DataTemplate VideoTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is SfRotatorItem rotatorItem)
            {
                return rotatorItem.Type == "Image" ? ImageTemplate : VideoTemplate;
            }
            return null;
        }
    }

}
