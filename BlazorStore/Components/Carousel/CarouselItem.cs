using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStore.Components.Carousel
{

    public class CarouselItem
    {
        public string Image { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public CarouselItem(string src, string text, string link)
        {
            Image = src;
            Text = text;
            Link = link;
        }
    }
}
