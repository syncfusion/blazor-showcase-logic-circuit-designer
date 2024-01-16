using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using Syncfusion.Blazor.Diagram;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace LogicCircuit
{
    public partial class DiagramToolBar
    {
        [Inject]
        protected IJSRuntime? jsRuntime { get; set; }
        
        #region events
       
        /// <summary>
        ///This method sets the zoom level to the diagram 
        /// </summary>
        private void DrawZoomChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            double currentZoom = Parent.DiagramContent.CurrentZoom;
            switch (args.Item.Text)
            {
                case "Zoom In":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { Type = "ZoomIn", ZoomFactor = 0.2 });
                    break;
                case "Zoom Out":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { Type = "ZoomOut", ZoomFactor = 0.2 });
                    break;
                case "Zoom to Fit":
                    FitOptions fitoption = new FitOptions()
                    {
                        Mode = FitMode.Both,
                        Region = DiagramRegion.Content,
                    };
                    diagram.FitToPage(fitoption);                  
                    break;
                case "Zoom to 50%":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { ZoomFactor = ((0.5 / currentZoom) - 1) });
                    break;
                case "Zoom to 100%":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { ZoomFactor = ((1 / currentZoom) - 1) });
                    break;
                case "Zoom to 200%":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { ZoomFactor = ((2 / currentZoom) - 1) });
                    break;
            }
            ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
        }
        /// <summary>
        ///This method will be called when an item in the Toolbar is clicked.
        /// </summary>
        private async Task ToolbarEditorClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            var commandType = args.Item.TooltipText.ToLower(new CultureInfo("en-US"));
            switch (commandType)
            {
                case "undo":
                    diagram.Undo();
                    await EnableToolbarItems(new object() { }, "historychange");
                    break;
                case "redo":
                    diagram.Redo();
                    await EnableToolbarItems(new object() { }, "historychange");
                    break;
                case "pan tool":
                    Parent.DiagramContent.UpdateTool();
                    diagram.ClearSelection();
                    Parent.DiagramPropertyPanel.PanelVisibility();
                    break;
                case "pointer":
                    Parent.DiagramContent.DiagramDrawingObject = null;
                    Parent.DiagramContent.UpdatePointerTool();
                    break;
                case "distribute objects vertically":
                    diagram.SetDistribute(DistributeOptions.RightToLeft);
                    break;
                case "distribute objects horizontally":
                    diagram.SetDistribute(DistributeOptions.BottomToTop);
                    break;              
            }
            if (commandType == "pan tool" || commandType == "pointer" || commandType == "text tool")
            {
#pragma warning disable CA1307 // Specify StringComparison
                if (args.Item.CssClass.IndexOf("tb-item-selected") == -1)
#pragma warning restore CA1307 // Specify StringComparison
                {
                    if (commandType == "pan tool")
                        PanItemCssClass += " tb-item-selected";
                    if (commandType == "pointer")
                        PointerItemCssClass += " tb-item-selected";
                    if (commandType == "text tool")
                        TextItemCssClass += " tb-item-selected";
                    removeSelectedToolbarItem(commandType);
                }
            }
            Parent.DiagramPropertyPanel.PanelVisibility();
            Parent.DiagramContent.StateChanged();
        }
        
        

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// This method removes a selected toolbar item from the user interface 
        /// </summary>
        /// <param name="tool">The name or identifier of the toolbar item to be removed</param>
        /// <returns>Task</returns>
        public void removeSelectedToolbarItem(string tool)
        {
#pragma warning disable CA1307 // Specify StringComparison

            if (DrawConnectorItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                DrawConnectorItemCssClass = DrawConnectorItemCssClass.Replace(" tb-item-selected", "");
            }            
            if (tool != "pan tool" && PanItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                PanItemCssClass = PanItemCssClass.Replace(" tb-item-selected", "");
            }
            if (tool != "pointer" && PointerItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                PointerItemCssClass = PointerItemCssClass.Replace(" tb-item-selected", "");
            }
                       
            removeSelectedToolbarItems(tool);
            StateHasChanged();
#pragma warning restore CA1307 // Specify StringComparison

        }
        #endregion
        /// <summary>
        /// This method removes a selected toolbar item from the connector dropdown
        /// </summary>
        /// <param name="tool">The name or identifier of the toolbar item to be removed</param>       
        private void removeSelectedToolbarItems(string tool)
        {
            string shape = "tb-item-selected";
            if (ConnectorItem.Contains(shape))
            {
                int first = ConnectorItem.IndexOf(shape);
                ConnectorItem = ConnectorItem.Remove(first);
            }           
            if (tool == "connector")
            {
                ConnectorItem += " tb-item-selected";
            }
        }
        
        /// <summary>
        /// This method used to update the current zoom value of the Diagram.
        /// </summary>     
        public void DiagramZoomValueChange()
        {
            ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
            StateHasChanged();
        }

        #region public methods
        /// <summary>
        /// This method allows you to enable specific toolbar items associated with a particular event.
        /// </summary>
        /// <param name="obj">Gives the details of the current object(node/connector)</param>
        /// <param name="eventname">The name of the event</param>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task EnableToolbarItems<T>(T obj, string eventname)
        {

            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            ObservableCollection<NodeBase> collection = new ObservableCollection<NodeBase>();
            if (eventname == "selectionchange")
            {               

                if (obj != null && obj is ObservableCollection<IDiagramObject> diagramObjects && diagramObjects != null)
                {
                    foreach (NodeBase node in diagramObjects)
                    {
                        Node? val = node as Node;
                        collection.Add(node);
                    }
                }
                UtilityMethods_enableToolbarItems(collection);
            }

            if (eventname == "historychange")
            {
                RemoveUndo();
                RemoveRedo();
                if (diagram.HistoryManager.CanUndo)
                {
                    this.Parent.DiagramContent.IsUndo = diagram.HistoryManager.CanUndo;
                    this.Parent.DiagramContent.IsRedo = diagram.HistoryManager.CanRedo;
                    toolbarClassName += " db-undo";
                }
                if (diagram.HistoryManager.CanRedo)
                {
                    this.Parent.DiagramContent.IsRedo = diagram.HistoryManager.CanRedo;
                    this.Parent.DiagramContent.IsUndo = diagram.HistoryManager.CanUndo;
                    toolbarClassName += " db-redo";
                }
                StateHasChanged();
            }
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        /// <summary>
        /// Removes the "Undo" class from the toolbar class name.
        /// </summary>
        public void RemoveUndo()
        {
            string undo = "undo";
            if (toolbarClassName.Contains(undo))
            {
                int first = toolbarClassName.IndexOf(undo);
                toolbarClassName = toolbarClassName.Remove(first - 4, 8);
            }
        }
        /// <summary>
        /// Removes the "Redo" class from the toolbar class name.
        /// </summary>
        public void RemoveRedo()
        {
            string redo = "redo";
            if (toolbarClassName.Contains(redo))
            {
                int first = toolbarClassName.IndexOf(redo);
                toolbarClassName = toolbarClassName.Remove(first - 4, 8);
            }
        }
        /// <summary>
        /// This method is used to enable the toolbar items
        /// </summary>
        public void UtilityMethods_enableToolbarItems(ObservableCollection<NodeBase> SelectedObjects)
        {
            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            removeClassElement();
            if (this.Parent.DiagramContent.IsUndo)
            {
                toolbarClassName += " db-undo";
            }
            if (this.Parent.DiagramContent.IsRedo)
            {
                toolbarClassName += " db-redo";
            }
            if (SelectedObjects.Count == 1)
            {
                toolbarClassName = toolbarClassName + " db-select";
              
                if (SelectedObjects.Count > 0 && SelectedObjects[0] is NodeGroup selectedGroup)
                {
                    if (selectedGroup.Children?.Length > 2)
                    {
                        toolbarClassName += " db-select db-double db-multiple db-node db-group";
                    }
                    else
                    {
                        toolbarClassName += " db-select db-double db-node db-group";
                    }
                }

                else
                {
                    if (SelectedObjects[0] is Connector)
                    {
                        toolbarClassName = toolbarClassName + " db-select";
                    }
                    else
                    {
                        toolbarClassName = toolbarClassName + " db-select db-node";
                    }
                }
            }
            else if (SelectedObjects.Count == 2)
            {
                if (!((SelectedObjects[0] is Node) && (SelectedObjects[1] is Node)))
                {
                    fill = "tb-item-start tb-item-fill";
                }
                GroupIcon = "e-icons sf-icon-Group tb-icons";
                GroupToolTip = "Group";
                toolbarClassName = toolbarClassName + " db-select db-double";
            }
            else if (SelectedObjects.Count > 2)
            {
                for (int i = 0; i <= SelectedObjects.Count - 1; i++)
                {
                    if (SelectedObjects[i] is Connector)
                    {
                        fill = "tb-item-start tb-item-fill";
                    }
                }
                GroupIcon = "e-icons sf-icon-Group tb-icons";
                GroupToolTip = "Group";
                toolbarClassName = toolbarClassName + " db-select db-double db-multiple";
            }           
            if (SelectedObjects.Count > 1)
                StateHasChanged();
        }
        /// <summary>
        /// This method is used to hide the Property Panel
        /// </summary>
        public async Task HidePropertyContainer()
        {
            await this.HideElements("hide-properties");         
            Parent.MenuBar.StateChanged();          
        }
        public void removeClassElement()
        {
            string space = " ";
            if (toolbarClassName.Contains(space))
            {
                int first = toolbarClassName.IndexOf(space);
                if (first != 0)
                {
                    toolbarClassName = toolbarClassName.Remove(20);
                }
            }
            fill = "tb-item-start tb-item-fill";
            stroke = "tb-item-end tb-item-stroke";
        }
         /// <summary>
        /// This method hides the provided item.
        /// </summary>
        /// <param name="eventname">The item(toolabr, palette,propertypanel) needs to be hide from the view </param>
        public async Task HideElements(string eventname)
        {
            await jsRuntime!.InvokeAsync<object>("UtilityMethods_hideElements", eventname).ConfigureAwait(true);
        }
        #endregion
    }
}
