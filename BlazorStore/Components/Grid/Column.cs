using Microsoft.AspNetCore.Components;

namespace BlazorStore.Components.Grid
{
    public class Column<ItemType> : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string SortField { get; set; }

        [Parameter]
        public RenderFragment<ItemType> ChildContent { get; set; }

        [CascadingParameter]
        public Grid<ItemType> Parent { get; set; }

        protected override void OnInitialized()
        {
            Parent.AddColumn(this);
        }
    }
}
