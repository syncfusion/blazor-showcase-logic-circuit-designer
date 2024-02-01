using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Diagram;
using Syncfusion.Blazor;

namespace LogicCircuit
{
    public partial class DiagramMain 
    {
        public DiagramMain()
        {
            Parent = new Shared.MainLayout();
            MenuBar = new DiagramMenuBar(); // Initialize MenuBar with a non-null value
            DiagramContent = new DiagramMainContent();
            DiagramPropertyPanel = new DiagramPropertyContainer();
            Toolbar = new DiagramToolBar();
            LeftSideBar = new DiagramLeftSideBar();
        }

        [Parameter]       
        public RenderFragment? ChildContent { get; set; }
        protected override async Task OnInitializedAsync()
        {           
            InitializeDiagraMainContent(this.DiagramContent);           
             await base.OnInitializedAsync();
        }

        public void InitializeDiagraMainContent(DiagramMainContent diagramMainContent) 
        {            
            DiagramContent = diagramMainContent ?? new DiagramMainContent(this);
        }
    }
}
