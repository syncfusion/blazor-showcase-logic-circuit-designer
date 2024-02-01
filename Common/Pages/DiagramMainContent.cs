using Microsoft.JSInterop;
using Syncfusion.Blazor.Diagram;
using System.Text.Json;

using LogicCircuit.Shared;

namespace LogicCircuit
{
    public partial class DiagramMainContent
    {
        /// <summary>
        /// Represents options for fitting content within a diagram.
        /// </summary>
        FitOptions options = new FitOptions() { Mode = FitMode.Both ,Region=DiagramRegion.Content};

        /// <summary>
        /// Gets or sets the value when diagram is completed loaded.
        /// </summary>
        public bool IsLoadingData = true;
        /// <summary>
        /// Notify when collection changed is completed or not.
        /// </summary>
        internal bool IsCollectionChangeCompleted = true;
        /// <summary>
        /// Gets or sets the initial timer value for clock node
        /// </summary>
        public int timerValue = 3000;
        /// <summary>
        /// Gets or sets the SfDiagramComponent associated with this instance.
        /// </summary>
        public SfDiagramComponent Diagram { get; set; }
        /// <summary>
        /// Gets or sets the parent DiagramMain associated with this instance.
        /// </summary>
        public DiagramMain Parent { get; set; }
        /// <summary>
        /// Gets or sets the DotNetObjectReference associated with this instance.
        /// </summary>
        public DotNetObjectReference<DiagramMainContent>? objRef;
        public DiagramMainContent()
        {
            
        }
        public DiagramMainContent(DiagramMain parent)
        {
            Parent = parent;
            this.Diagram = new SfDiagramComponent();
        }
        /// <summary>
        /// Invoked when component parameters are set.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            Parent?.InitializeDiagraMainContent(this);

        }
        /// <summary>
        /// This method is called when the component has been initialized.
        /// </summary>
        /// <remarks>
        /// Override this method to perform initialization logic after the component has been rendered on the initial render.
        /// </remarks>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Parent?.InitializeDiagraMainContent(this);
            objRef = DotNetObjectReference.Create(this);
        }
        /// <summary>
        /// This method is called when mouse down on the push button node.
        /// </summary>
        /// <param name="id"></param>
        private async void PushButtonMouseDown(string id)
        {
            Diagram.BeginUpdate();
            List<Node> nodeGroups = Diagram.Nodes.Where(node => (node is NodeGroup && node.ID.IndexOf("Push") != -1)).ToList();
            for (int i = 0; i< nodeGroups.Count; i++)
            {
                if (nodeGroups[i] is NodeGroup grpNode)
                {
                    if (grpNode.ID.IndexOf(id) != -1)
                    {
                        UpdatePushButtonNodeStyle(grpNode);
                        
                    }
                    else
                    {
                        for (int j = 0; j<grpNode.Children.Length; j++)
                        {
                            if (grpNode.Children[j]==id)
                            {
                                UpdatePushButtonNodeStyle(grpNode);
                            }
                        }
                    }
                    
                }
                
            }
            await Diagram.EndUpdate();
        }
        /// <summary>
        /// Updates the objects in the diagram when mouse down action is performed on the push button.
        /// </summary>
        private void UpdatePushButtonNodeStyle(NodeGroup grpNode)
        {
            Node? child = Diagram.GetObject(grpNode.Children[1]) as Node;
            Node? child1 = Diagram.GetObject(grpNode.Children[2]) as Node;
            if (child1 !=null && child!=null)
            {
                child1.Style.Fill = "#05DAC5";
                child.AdditionalInfo["BinaryState"] = 1;                
                SetBinaryStateFromInput(child);
                RunSimulation();
            }
        }
        /// <summary>
        /// Updated the objects when push button mouse is released.
        /// </summary>
        private async void PushButtonMouseUp(string id)
        {
            Diagram.BeginUpdate();
            if (id == "mouseUp")
            {
                if (Diagram.SelectionSettings != null && Diagram.SelectionSettings.Nodes.Count > 0 && Diagram.SelectionSettings.Nodes[0] is NodeGroup grpNode && grpNode.ID.IndexOf("Push") != -1)
                {
                    UpdatePushUpNodeStyle(grpNode);
                }
            }
            else
            {
                List<Node> nodeGroups = Diagram.Nodes.Where(node => (node is NodeGroup && node.ID.IndexOf("Push") != -1)).ToList();
                for (int i = 0; i < nodeGroups.Count; i++)
                {
                    if (nodeGroups[i] is NodeGroup grpNode)
                    {
                        if (grpNode.ID.IndexOf(id) != -1)
                        {
                            UpdatePushUpNodeStyle(grpNode);
                        }
                        else
                        {
                            for (int j = 0; j < grpNode.Children.Length; j++)
                            {
                                if (grpNode.Children[j] == id)
                                {
                                    UpdatePushUpNodeStyle(grpNode);
                                }
                            }
                        }
                    }
                }
            }            
            await Diagram.EndUpdate();
        }

        /// <summary>
        /// Update the diagram objects when push Up action is performed.
        /// </summary>
        private void UpdatePushUpNodeStyle(NodeGroup grpNode)
        {
            Node? child = Diagram.GetObject(grpNode.Children[1]) as Node;
            Node? child1 = Diagram.GetObject(grpNode.Children[2]) as Node;

            if (child1!=null && child!=null)
            {
                child1.Style.Fill = "white";
                child.AdditionalInfo["BinaryState"] = 0;                
                SetBinaryStateFromInput(child);
                RunSimulation();
            }
        }
        /// <summary>
        /// This method invoke when the mousedown action is performed in the diagram
        /// </summary>
        /// <param name="id">id defines the target element</param>
        [JSInvokable]
        public void OnMouseDownEvent(string id)
        {
            if (IsCollectionChangeCompleted)
            {
                if (id.Contains("content"))
                    PushButtonMouseDown(id.Replace("_content", ""));
                else if (id.Contains("container"))
                    PushButtonMouseDown(id.Replace("group_container", ""));
                else if (id == "Push Button")
                    PushButtonMouseDown(id);
            }
        }
        /// <summary>
        /// This method invoke when mouseup event is triggered in the daigram
        /// </summary>
        /// <param name="id">id defines the target element</param>
        [JSInvokable]
        public void OnMouseUpEvent(string id)
        {
            if (IsCollectionChangeCompleted)
            {
                if (id.Contains("content"))
                    PushButtonMouseUp(id.Replace("_content", ""));
                else if (id.Contains("container"))
                    PushButtonMouseUp(id.Replace("group_container", ""));
                else if (id.Contains("diagram_diagramLayer_svg"))
                    PushButtonMouseUp("mouseUp");
                else if (id == "Push Button")
                    PushButtonMouseUp(id);
            }
        }

        /// <summary>
        /// Change the current state of the nodes and connectos
        /// </summary>
        [JSInvokable]
        public async void ChangeState()
        {
            if (IsCollectionChangeCompleted && Diagram.Nodes != null && Diagram.Nodes.Count > 0)
            {
                Diagram.BeginUpdate();
                List<Node> clkNodes = Diagram.Nodes.Where(node => node.ID.Contains("Clock", StringComparison.CurrentCulture) && node is NodeGroup && node.AdditionalInfo != null).ToList();
                for (int i = 0; i < clkNodes.Count; i++)
                {
                    Node clkNode = clkNodes[i];
                    if (clkNode.ID.IndexOf("Clock") != -1 && clkNode is NodeGroup grpNode && clkNode.AdditionalInfo != null)
                    {
                        if (grpNode.AdditionalInfo.Count == 0)
                        {
                            Node? firstChild = Diagram.GetObject(grpNode.Children[0]) as Node;
                            if (firstChild != null && firstChild.AdditionalInfo.Count > 0)
                            {
                                clkNode = firstChild;
                            }
                        }
                        bool binaryState = (int)clkNode.AdditionalInfo["BinaryState"] == 0;
                        clkNode.AdditionalInfo["BinaryState"] = binaryState ? 1 : 0;
                        Node? child = Diagram.GetObject(grpNode.Children[1]) as Node;
                        if (child != null)
                            child.Style.Fill = binaryState ? "#05DAC5" : "white";

                        SetBinaryStateFromInput(clkNode);
                        RunSimulation();
                    }
                }
                await Diagram.EndUpdate();
            }
        }
        /// <summary>
        /// This method will be called when diagram is created
        /// </summary>  
        private async Task OnCreated()
        {
            Diagram.FitToPage(options);
            Parent.Toolbar.ZoomItemVisibility = true;
            Parent.Toolbar.HideItemVisibility = true;
            Parent.Toolbar.StateChanged();
            Diagram.BeginUpdate();
            RunSimulation();
            await Diagram.EndUpdate();
            await JSRuntime.InvokeAsync<string>("WireMouseEvents");
            await JSRuntime.InvokeAsync<string>("InvokeClockEvent", objRef, timerValue);
           
            Service?.Update(new NotifyProperties() { HideSpinner = true });
        }
        /// <summary>
        /// This method is used to add the ports to the Node
        /// </summary>
        private PointPort AddPort(string id, double x, double y, bool constraints)
        {
            if(constraints)
            {
                return new PointPort()
                {
                    ID = id,
                    Shape = PortShapes.Circle,
                    Width = 10,
                    Height = 12,
                    Visibility = PortVisibility.Visible,
                    Offset = new DiagramPoint() { X = x, Y = y },
                    Constraints = (PortConstraints.Default | PortConstraints.Draw)&~PortConstraints.OutConnect
                };
            }
            return new PointPort()
            {
                ID = id,
                Shape = PortShapes.Circle,
                Width = 10,
                Height = 12,
                Visibility = PortVisibility.Visible,
                Offset = new DiagramPoint() { X = x, Y = y },
                Constraints = (PortConstraints.Default | PortConstraints.Draw) &~PortConstraints.InConnect
            };

        }
        /// <summary>
        /// Initializes the diagram model with default circuit diagram
        /// </summary> 
        private void InitDiagramModel()
        {
            Node switchOuter = new Node()
            {
                ID = "SwitchOuter",
                Width = 80,
                Height = 60,
                OffsetX=140,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M60 27C60 29.7614 62.2386 32 65 32C67.7614 32 70 29.7614 70 27C70 24.2386 67.7614 22 65 22C62.2386 22 60 24.2386 60 27ZM60 27H43M43 27V1H1V53H43V27Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2
                },
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("TogglePort",0.92,0.5,false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "InputControl" } },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect)
            };
            nodes.Add(switchOuter);
            Node switchInner = new Node()
            {
                ID = "SwitchInner",
                Width = 40,
                Height = 50,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M36 46V8H7V46H36Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2
                },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect)
            };
            nodes.Add(switchInner);

            Node switchOff = new Node()
            {
                ID = "SwitchOff",
                Width = 30,
                Height = 40,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M33 15L32 12H11L10 15M33 15H10M33 15L31 27M10 15L12 27M31 27H12M31 27V42H12V27"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2
                },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect)
            };
            nodes.Add(switchOff);
            Node switchOn = new Node()
            {
                ID = "SwitchOn",
                Width = 30,
                Height = 40,
                OffsetX=125,
                OffsetY=100,
                IsVisible=false,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M33 39L31.5 42H11L10 39M33 39H10M33 39L31 27M10 39L12 27M12 27H31M12 27V12H31V27"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="transparent",
                    Fill="transparent",
                    StrokeWidth=2
                },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect)
            };
            nodes.Add(switchOn);
            NodeGroup groupNode = new NodeGroup();
            groupNode.ID ="Toggle1 Switch";
            groupNode.Children = new string[] { "SwitchOuter", "SwitchInner" , "SwitchOff", "SwitchOn" };
            
            groupNode.Constraints  = NodeConstraints.Default &~ NodeConstraints.InConnect;
            groupNode.OffsetX=140;
            groupNode.OffsetY=100;
            nodes.Add(groupNode);

            Node pushButtonOuterRect = new Node()
            {
                ID = "Push BtnOuterRect",
                Width = 82,
                Height = 62,
                OffsetX=140,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M1 1V0H0V1H1ZM43 1H44V0H43V1ZM43 53V54H44V53H43ZM1 53H0V54H1V53ZM69 27C69 29.2091 67.2091 31 65 31V33C68.3137 33 71 30.3137 71 27H69ZM65 31C62.7909 31 61 29.2091 61 27H59C59 30.3137 61.6863 33 65 33V31ZM61 27C61 24.7909 62.7909 23 65 23V21C61.6863 21 59 23.6863 59 27H61ZM65 23C67.2091 23 69 24.7909 69 27H71C71 23.6863 68.3137 21 65 21V23ZM43 28H60V26H43V28ZM1 2H43V0H1V2ZM43 52H1V54H43V52ZM2 53V1H0V53H2ZM42 1V27H44V1H42ZM42 27V53H44V27H42Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=0,
                    Fill="black"
                },
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("pushBtnPort", 0.92, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "InputControl" } },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~ NodeConstraints.InConnect
            };
            nodes.Add(pushButtonOuterRect);

            Node transparentnode = new Node()
            {
                ID = "transparentNode",
                Width = 65,
                Height = 62,
                OffsetX=140,
                OffsetY=100,
                Shape=new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                },
                Style=new ShapeStyle()
                {
                    StrokeWidth=0,
                    Fill="#ffffff",
                    Opacity=0,
                    StrokeColor="#ffffff"
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            nodes.Add(transparentnode);

            Node pushBtnOuter = new Node()
            {
                ID = "Push BtnOuter",
                Width = 40,
                Height = 40,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M34 27C34 33.6274 28.6274 39 22 39C15.3726 39 10 33.6274 10 27C10 20.3726 15.3726 15 22 15C28.6274 15 34 20.3726 34 27Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2,
                    Fill="white"
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            nodes.Add(pushBtnOuter);
            Node pushBtnInner = new Node()
            {
                ID = "Push BtnInner",
                Width = 30,
                Height = 30,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M29 27C29 30.866 25.866 34 22 34C18.134 34 15 30.866 15 27C15 23.134 18.134 20 22 20C25.866 20 29 23.134 29 27Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2,
                    Fill="white"

                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            nodes.Add(pushBtnInner);
            NodeGroup PushButton = new NodeGroup();
            PushButton.ID ="Push Button";
            PushButton.Children = new string[] { "transparentNode", "Push BtnOuterRect", "Push BtnOuter", "Push BtnInner" };
            PushButton.Constraints = NodeConstraints.Default &~NodeConstraints.InConnect;
            PushButton.OffsetX = 140;
            PushButton.OffsetY = 270;
            
            nodes.Add(PushButton);

            Node ClkOuterRect = new Node()
            {
                ID = "ClockOuterRect",
                Width = 84,
                Height = 55,
                OffsetX=140,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M49 22H61M49 22V1H1V43H49V22ZM61 22C61 24.7614 63.2386 27 66 27C68.7614 27 71 24.7614 71 22C71 19.2386 68.7614 17 66 17C63.2386 17 61 19.2386 61 22Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2,
                },
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("ClkPort", 0.92, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "InputControl" } },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect)
            };
            nodes.Add(ClkOuterRect);
            Node ClkInnerPart = new Node()
            {
                ID = "ClockInnerPart",
                Width = 42,
                Height = 30,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M14.5 25.5H10.5V13.5H26.5V26.5H34.5V18.5H38.5V30.5H22.5V17.5H14.5V25.5Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2,
                    Fill="white"
                },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect)
            };
            nodes.Add(ClkInnerPart);
            NodeGroup ClkButton = new NodeGroup();
            ClkButton.ID ="Clock";
            ClkButton.Children = new string[] { "ClockOuterRect", "ClockInnerPart" };
            
            ClkButton.Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect;
            ClkButton.OffsetX=140;
            ClkButton.OffsetY =470;
            nodes.Add(ClkButton);

            Node switchOuter1 = new Node()
            {
                ID = "SwitchOuter1",
                Width = 80,
                Height = 60,
                OffsetX=140,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M60 27C60 29.7614 62.2386 32 65 32C67.7614 32 70 29.7614 70 27C70 24.2386 67.7614 22 65 22C62.2386 22 60 24.2386 60 27ZM60 27H43M43 27V1H1V53H43V27Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2
                },
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("ToggleSwitch2Port", 0.92, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "InputControl" } },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            nodes.Add(switchOuter1);
            Node switchInner1 = new Node()
            {
                ID = "SwitchInner1",
                Width = 40,
                Height = 50,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M36 46V8H7V46H36Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            nodes.Add(switchInner1);

            Node switchOff1 = new Node()
            {
                ID = "SwitchOff1",
                Width = 30,
                Height = 40,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M33 15L32 12H11L10 15M33 15H10M33 15L31 27M10 15L12 27M31 27H12M31 27V42H12V27"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    StrokeWidth=2
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            nodes.Add(switchOff1);
            Node switchOn1 = new Node()
            {
                ID = "SwitchOn1",
                Width = 30,
                Height = 40,
                OffsetX=125,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M33 39L31.5 42H11L10 39M33 39H10M33 39L31 27M10 39L12 27M12 27H31M12 27V12H31V27"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="transparent",
                    Fill="transparent",
                    StrokeWidth=2
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            nodes.Add(switchOn1);
            NodeGroup ToggleSwitch2 = new NodeGroup();
            ToggleSwitch2.ID = "Toggle2 Switch";
            ToggleSwitch2.Children = new string[] { "SwitchOuter1", "SwitchInner1", "SwitchOff1", "SwitchOn1" };
            ToggleSwitch2.Constraints = NodeConstraints.Default &~NodeConstraints.InConnect;
            
            ToggleSwitch2.OffsetX=140;
            ToggleSwitch2.OffsetY=630;
            nodes.Add(ToggleSwitch2);

            Node OrGate = new Node()
            {
                ID = "ORGate",
                OffsetX=350,
                OffsetY=350,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M70.9412 20L69.9412 20.0012L70.9412 20ZM33.5076 11L34.4744 10.7444L33.5076 11ZM33.7565 29L34.7303 29.2276L33.7565 29ZM29 1L29.0345 0.000594001C28.6575 -0.0124037 28.3053 0.187777 28.1237 0.518291C27.942 0.848804 27.9617 1.25342 28.1747 1.56469L29 1ZM29 40L28.1747 39.4353C27.9617 39.7466 27.942 40.1512 28.1237 40.4817C28.3053 40.8122 28.6575 41.0124 29.0345 40.9994L29 40ZM66.5674 29.1936L67.3548 29.8101L67.3548 29.8101L66.5674 29.1936ZM45.21 39.441L45.2445 40.4404L45.2445 40.4404L45.21 39.441ZM45.6846 1.57533L45.719 0.575923L45.6846 1.57533ZM66.5 11L67.2718 10.3641L66.5 11ZM10 11C10 13.2091 8.20914 15 6 15V17C9.31371 17 12 14.3137 12 11H10ZM6 15C3.79086 15 2 13.2091 2 11H0C0 14.3137 2.68629 17 6 17V15ZM2 11C2 8.79086 3.79086 7 6 7V5C2.68629 5 0 7.68629 0 11H2ZM6 7C8.20914 7 10 8.79086 10 11H12C12 7.68629 9.31371 5 6 5V7ZM10 29C10 31.2091 8.20914 33 6 33V35C9.31371 35 12 32.3137 12 29H10ZM6 33C3.79086 33 2 31.2091 2 29H0C0 32.3137 2.68629 35 6 35V33ZM2 29C2 26.7909 3.79086 25 6 25V23C2.68629 23 0 25.6863 0 29H2ZM6 25C8.20914 25 10 26.7909 10 29H12C12 25.6863 9.31371 23 6 23V25ZM98 20C98 22.2091 96.2091 24 94 24V26C97.3137 26 100 23.3137 100 20H98ZM94 24C91.7909 24 90 22.2091 90 20H88C88 23.3137 90.6863 26 94 26V24ZM90 20C90 17.7909 91.7909 16 94 16V14C90.6863 14 88 16.6863 88 20H90ZM94 16C96.2091 16 98 17.7909 98 20H100C100 16.6863 97.3137 14 94 14V16ZM70.9412 21H89V19H70.9412V21ZM11 12H33.5076V10H11V12ZM11 30H33.7565V28H11V30ZM28.9655 1.99941L45.6501 2.57474L45.719 0.575923L29.0345 0.000594001L28.9655 1.99941ZM45.1755 38.4416L28.9655 39.0006L29.0345 40.9994L45.2445 40.4404L45.1755 38.4416ZM65.7801 28.5771C60.9514 34.7438 53.1021 38.1683 45.1755 38.4416L45.2445 40.4404C53.6589 40.1503 62.1031 36.517 67.3548 29.8101L65.7801 28.5771ZM45.6501 2.57474C53.3279 2.83949 60.9322 5.81523 65.7282 11.6359L67.2718 10.3641C62.0267 3.99837 53.8221 0.85534 45.719 0.575923L45.6501 2.57474ZM34.4744 10.7444C33.499 7.05472 31.9517 3.54311 29.8253 0.435311L28.1747 1.56469C30.1606 4.46711 31.6183 7.76579 32.5408 11.2556L34.4744 10.7444ZM29.8253 40.5647C32.1482 37.1698 33.7801 33.2928 34.7303 29.2276L32.7827 28.7724C31.8833 32.6208 30.3442 36.2645 28.1747 39.4353L29.8253 40.5647ZM34.7303 29.2276C36.1399 23.1963 36.055 16.7243 34.4744 10.7444L32.5408 11.2556C34.0374 16.9173 34.1184 23.0575 32.7827 28.7724L34.7303 29.2276ZM65.7282 11.6359C68.669 15.205 69.9383 17.651 69.9412 20.0012L71.9412 19.9988C71.9374 16.896 70.246 13.9738 67.2718 10.3641L65.7282 11.6359ZM69.9412 20.0012C69.944 22.3473 68.6863 24.8654 65.7801 28.5771L67.3548 29.8101C70.2867 26.0656 71.9449 23.0763 71.9412 19.9988L69.9412 20.0012Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect,
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("Or_port1", 0.05, 0.25, true),
                    AddPort("Or_port2", 0.05, 0.73, true),
                    AddPort("Or_port3", 0.94, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "Gates" } }
            };
            nodes.Add(OrGate);
            Node AndGate1 = new Node()
            {
                ID = "ANDGate1",
                OffsetX=350,
                OffsetY=180,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M29 1C29 0.447715 29.4477 0 30 0H52.5C63.4538 0 72.4534 8.14084 72.976 19H88.083C88.559 16.1623 91.027 14 94 14C97.3137 14 100 16.6863 100 20C100 23.3137 97.3137 26 94 26C91.027 26 88.559 23.8377 88.083 21H72.9761C72.4556 31.8562 63.5176 41 52.5 41H30C29.4477 41 29 40.5523 29 40V30H11.917C11.441 32.8377 8.973 35 6 35C2.68629 35 0 32.3137 0 29C0 25.6863 2.68629 23 6 23C8.973 23 11.441 25.1623 11.917 28H29V12H11.917C11.441 14.8377 8.973 17 6 17C2.68629 17 0 14.3137 0 11C0 7.68629 2.68629 5 6 5C8.973 5 11.441 7.16229 11.917 10H29V1ZM31 2V39H52.5C62.6847 39 71 30.2504 71 20C71 9.81642 62.7516 2 52.5 2H31ZM6 7C3.79086 7 2 8.79086 2 11C2 13.2091 3.79086 15 6 15C8.20914 15 10 13.2091 10 11C10 8.79086 8.20914 7 6 7ZM94 16C91.7909 16 90 17.7909 90 20C90 22.2091 91.7909 24 94 24C96.2091 24 98 22.2091 98 20C98 17.7909 96.2091 16 94 16ZM6 25C3.79086 25 2 26.7909 2 29C2 31.2091 3.79086 33 6 33C8.20914 33 10 31.2091 10 29C10 26.7909 8.20914 25 6 25Z"
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect,
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "Gates" } },
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("And_port1", 0.05, 0.25, true),
                    AddPort("And_port2", 0.05, 0.73, true),
                    AddPort("And_port3", 0.94, 0.5, false),

                }
            };
            nodes.Add(AndGate1);
            Node AndGate2 = new Node()
            {
                ID = "ANDGate2",
                OffsetX=350,
                OffsetY=520,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M29 1C29 0.447715 29.4477 0 30 0H52.5C63.4538 0 72.4534 8.14084 72.976 19H88.083C88.559 16.1623 91.027 14 94 14C97.3137 14 100 16.6863 100 20C100 23.3137 97.3137 26 94 26C91.027 26 88.559 23.8377 88.083 21H72.9761C72.4556 31.8562 63.5176 41 52.5 41H30C29.4477 41 29 40.5523 29 40V30H11.917C11.441 32.8377 8.973 35 6 35C2.68629 35 0 32.3137 0 29C0 25.6863 2.68629 23 6 23C8.973 23 11.441 25.1623 11.917 28H29V12H11.917C11.441 14.8377 8.973 17 6 17C2.68629 17 0 14.3137 0 11C0 7.68629 2.68629 5 6 5C8.973 5 11.441 7.16229 11.917 10H29V1ZM31 2V39H52.5C62.6847 39 71 30.2504 71 20C71 9.81642 62.7516 2 52.5 2H31ZM6 7C3.79086 7 2 8.79086 2 11C2 13.2091 3.79086 15 6 15C8.20914 15 10 13.2091 10 11C10 8.79086 8.20914 7 6 7ZM94 16C91.7909 16 90 17.7909 90 20C90 22.2091 91.7909 24 94 24C96.2091 24 98 22.2091 98 20C98 17.7909 96.2091 16 94 16ZM6 25C3.79086 25 2 26.7909 2 29C2 31.2091 3.79086 33 6 33C8.20914 33 10 31.2091 10 29C10 26.7909 8.20914 25 6 25Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect,
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("portAnd1", 0.05, 0.25, true),
                    AddPort("portAnd2", 0.05, 0.73, true),
                    AddPort("portAnd3", 0.94, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "Gates" } }
            };
            nodes.Add(AndGate2);
            Node AndGate3 = new Node()
            {
                ID = "ANDGate3",
                OffsetX=550,
                OffsetY=440,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M29 1C29 0.447715 29.4477 0 30 0H52.5C63.4538 0 72.4534 8.14084 72.976 19H88.083C88.559 16.1623 91.027 14 94 14C97.3137 14 100 16.6863 100 20C100 23.3137 97.3137 26 94 26C91.027 26 88.559 23.8377 88.083 21H72.9761C72.4556 31.8562 63.5176 41 52.5 41H30C29.4477 41 29 40.5523 29 40V30H11.917C11.441 32.8377 8.973 35 6 35C2.68629 35 0 32.3137 0 29C0 25.6863 2.68629 23 6 23C8.973 23 11.441 25.1623 11.917 28H29V12H11.917C11.441 14.8377 8.973 17 6 17C2.68629 17 0 14.3137 0 11C0 7.68629 2.68629 5 6 5C8.973 5 11.441 7.16229 11.917 10H29V1ZM31 2V39H52.5C62.6847 39 71 30.2504 71 20C71 9.81642 62.7516 2 52.5 2H31ZM6 7C3.79086 7 2 8.79086 2 11C2 13.2091 3.79086 15 6 15C8.20914 15 10 13.2091 10 11C10 8.79086 8.20914 7 6 7ZM94 16C91.7909 16 90 17.7909 90 20C90 22.2091 91.7909 24 94 24C96.2091 24 98 22.2091 98 20C98 17.7909 96.2091 16 94 16ZM6 25C3.79086 25 2 26.7909 2 29C2 31.2091 3.79086 33 6 33C8.20914 33 10 31.2091 10 29C10 26.7909 8.20914 25 6 25Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect,
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("Andgateport1", 0.05, 0.25, true),
                    AddPort("Andgateport2", 0.05, 0.73, true),
                    AddPort("Andgateport3", 0.94, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "Gates" } }
            };
            nodes.Add(AndGate3);
            Node OrGate2 = new Node()
            {
                ID = "ORGate2",
                OffsetX=750,
                OffsetY=280,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M70.9412 20L69.9412 20.0012L70.9412 20ZM33.5076 11L34.4744 10.7444L33.5076 11ZM33.7565 29L34.7303 29.2276L33.7565 29ZM29 1L29.0345 0.000594001C28.6575 -0.0124037 28.3053 0.187777 28.1237 0.518291C27.942 0.848804 27.9617 1.25342 28.1747 1.56469L29 1ZM29 40L28.1747 39.4353C27.9617 39.7466 27.942 40.1512 28.1237 40.4817C28.3053 40.8122 28.6575 41.0124 29.0345 40.9994L29 40ZM66.5674 29.1936L67.3548 29.8101L67.3548 29.8101L66.5674 29.1936ZM45.21 39.441L45.2445 40.4404L45.2445 40.4404L45.21 39.441ZM45.6846 1.57533L45.719 0.575923L45.6846 1.57533ZM66.5 11L67.2718 10.3641L66.5 11ZM10 11C10 13.2091 8.20914 15 6 15V17C9.31371 17 12 14.3137 12 11H10ZM6 15C3.79086 15 2 13.2091 2 11H0C0 14.3137 2.68629 17 6 17V15ZM2 11C2 8.79086 3.79086 7 6 7V5C2.68629 5 0 7.68629 0 11H2ZM6 7C8.20914 7 10 8.79086 10 11H12C12 7.68629 9.31371 5 6 5V7ZM10 29C10 31.2091 8.20914 33 6 33V35C9.31371 35 12 32.3137 12 29H10ZM6 33C3.79086 33 2 31.2091 2 29H0C0 32.3137 2.68629 35 6 35V33ZM2 29C2 26.7909 3.79086 25 6 25V23C2.68629 23 0 25.6863 0 29H2ZM6 25C8.20914 25 10 26.7909 10 29H12C12 25.6863 9.31371 23 6 23V25ZM98 20C98 22.2091 96.2091 24 94 24V26C97.3137 26 100 23.3137 100 20H98ZM94 24C91.7909 24 90 22.2091 90 20H88C88 23.3137 90.6863 26 94 26V24ZM90 20C90 17.7909 91.7909 16 94 16V14C90.6863 14 88 16.6863 88 20H90ZM94 16C96.2091 16 98 17.7909 98 20H100C100 16.6863 97.3137 14 94 14V16ZM70.9412 21H89V19H70.9412V21ZM11 12H33.5076V10H11V12ZM11 30H33.7565V28H11V30ZM28.9655 1.99941L45.6501 2.57474L45.719 0.575923L29.0345 0.000594001L28.9655 1.99941ZM45.1755 38.4416L28.9655 39.0006L29.0345 40.9994L45.2445 40.4404L45.1755 38.4416ZM65.7801 28.5771C60.9514 34.7438 53.1021 38.1683 45.1755 38.4416L45.2445 40.4404C53.6589 40.1503 62.1031 36.517 67.3548 29.8101L65.7801 28.5771ZM45.6501 2.57474C53.3279 2.83949 60.9322 5.81523 65.7282 11.6359L67.2718 10.3641C62.0267 3.99837 53.8221 0.85534 45.719 0.575923L45.6501 2.57474ZM34.4744 10.7444C33.499 7.05472 31.9517 3.54311 29.8253 0.435311L28.1747 1.56469C30.1606 4.46711 31.6183 7.76579 32.5408 11.2556L34.4744 10.7444ZM29.8253 40.5647C32.1482 37.1698 33.7801 33.2928 34.7303 29.2276L32.7827 28.7724C31.8833 32.6208 30.3442 36.2645 28.1747 39.4353L29.8253 40.5647ZM34.7303 29.2276C36.1399 23.1963 36.055 16.7243 34.4744 10.7444L32.5408 11.2556C34.0374 16.9173 34.1184 23.0575 32.7827 28.7724L34.7303 29.2276ZM65.7282 11.6359C68.669 15.205 69.9383 17.651 69.9412 20.0012L71.9412 19.9988C71.9374 16.896 70.246 13.9738 67.2718 10.3641L65.7282 11.6359ZM69.9412 20.0012C69.944 22.3473 68.6863 24.8654 65.7801 28.5771L67.3548 29.8101C70.2867 26.0656 71.9449 23.0763 71.9412 19.9988L69.9412 20.0012Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect,
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("port1", 0.05, 0.25, true),
                    AddPort("port2", 0.05, 0.73, true),
                    AddPort("port3", 0.94, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "Gates" } }
            };
            nodes.Add(OrGate2);
            Node NotGate = new Node()
            {
                ID = "NOTGate",
                OffsetX=750,
                OffsetY=520,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M27 1L27.4122 0.0888921C27.1027 -0.0511131 26.7433 -0.0244422 26.4579 0.159712C26.1724 0.343866 26 0.66032 26 1H27ZM27 40H26C26 40.3432 26.176 40.6623 26.4661 40.8456C26.7563 41.0288 27.1201 41.0504 27.4299 40.9029L27 40ZM10 20C10 22.2091 8.20914 24 6 24V26C9.31371 26 12 23.3137 12 20H10ZM6 24C3.79086 24 2 22.2091 2 20H0C0 23.3137 2.68629 26 6 26V24ZM2 20C2 17.7909 3.79086 16 6 16V14C2.68629 14 0 16.6863 0 20H2ZM6 16C8.20914 16 10 17.7909 10 20H12C12 16.6863 9.31371 14 6 14V16ZM98 20C98 22.2091 96.2091 24 94 24V26C97.3137 26 100 23.3137 100 20H98ZM94 24C91.7909 24 90 22.2091 90 20H88C88 23.3137 90.6863 26 94 26V24ZM90 20C90 17.7909 91.7909 16 94 16V14C90.6863 14 88 16.6863 88 20H90ZM94 16C96.2091 16 98 17.7909 98 20H100C100 16.6863 97.3137 14 94 14V16ZM74 20C74 21.1046 73.1046 22 72 22V24C74.2091 24 76 22.2091 76 20H74ZM72 22C70.8954 22 70 21.1046 70 20H68C68 22.2091 69.7909 24 72 24V22ZM70 20C70 18.8954 70.8954 18 72 18V16C69.7909 16 68 17.7909 68 20H70ZM72 18C73.1046 18 74 18.8954 74 20H76C76 17.7909 74.2091 16 72 16V18ZM75.5 21H89V19H75.5V21ZM26 1V40H28V1H26ZM27.4299 40.9029L69.4299 20.9029L68.5701 19.0971L26.5701 39.0971L27.4299 40.9029ZM69.4122 19.0889L27.4122 0.0888921L26.5878 1.91111L68.5878 20.9111L69.4122 19.0889ZM11.5 21H26V19H11.5V21Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect,
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("Not_port1", 0.05, 0.5, true),
                    AddPort("Not_port2", 0.94, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "Gates" } }
            };
            nodes.Add(NotGate);
            Node XorGate = new Node()
            {
                ID = "XORGate",
                OffsetX=950,
                OffsetY=420,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M70.9412 20L69.9412 20.0012L70.9412 20ZM29 1L29.0345 0.000594001C28.6575 -0.0124037 28.3053 0.187777 28.1237 0.518291C27.942 0.848804 27.9617 1.25342 28.1747 1.56469L29 1ZM29 40L28.1747 39.4353C27.9617 39.7466 27.942 40.1512 28.1237 40.4817C28.3053 40.8122 28.6575 41.0124 29.0345 40.9994L29 40ZM66.5674 29.1936L67.3548 29.8101L67.3548 29.8101L66.5674 29.1936ZM45.21 39.441L45.2445 40.4404L45.2445 40.4404L45.21 39.441ZM45.6846 1.57533L45.719 0.575923L45.6846 1.57533ZM66.5 11L67.2718 10.3641L66.5 11ZM21.1747 39.4353C20.8628 39.8911 20.9795 40.5134 21.4353 40.8253C21.8911 41.1372 22.5134 41.0205 22.8253 40.5647L21.1747 39.4353ZM22.8253 0.435311C22.5134 -0.0204919 21.8911 -0.137173 21.4353 0.174696C20.9795 0.486566 20.8628 1.10889 21.1747 1.56469L22.8253 0.435311ZM10 11C10 13.2091 8.20914 15 6 15V17C9.31371 17 12 14.3137 12 11H10ZM6 15C3.79086 15 2 13.2091 2 11H0C0 14.3137 2.68629 17 6 17V15ZM2 11C2 8.79086 3.79086 7 6 7V5C2.68629 5 0 7.68629 0 11H2ZM6 7C8.20914 7 10 8.79086 10 11H12C12 7.68629 9.31371 5 6 5V7ZM10 29C10 31.2091 8.20914 33 6 33V35C9.31371 35 12 32.3137 12 29H10ZM6 33C3.79086 33 2 31.2091 2 29H0C0 32.3137 2.68629 35 6 35V33ZM2 29C2 26.7909 3.79086 25 6 25V23C2.68629 23 0 25.6863 0 29H2ZM6 25C8.20914 25 10 26.7909 10 29H12C12 25.6863 9.31371 23 6 23V25ZM98 20C98 22.2091 96.2091 24 94 24V26C97.3137 26 100 23.3137 100 20H98ZM94 24C91.7909 24 90 22.2091 90 20H88C88 23.3137 90.6863 26 94 26V24ZM90 20C90 17.7909 91.7909 16 94 16V14C90.6863 14 88 16.6863 88 20H90ZM94 16C96.2091 16 98 17.7909 98 20H100C100 16.6863 97.3137 14 94 14V16ZM70.9412 21H89V19H70.9412V21ZM11 12H33.5076V10H11V12ZM11 30H33.7565V28H11V30ZM28.9655 1.99941L45.6501 2.57474L45.719 0.575923L29.0345 0.000594001L28.9655 1.99941ZM45.1755 38.4416L28.9655 39.0006L29.0345 40.9994L45.2445 40.4404L45.1755 38.4416ZM65.7801 28.5771C60.9514 34.7438 53.1021 38.1683 45.1755 38.4416L45.2445 40.4404C53.6589 40.1503 62.1031 36.517 67.3548 29.8101L65.7801 28.5771ZM45.6501 2.57474C53.3279 2.83949 60.9322 5.81523 65.7282 11.6359L67.2718 10.3641C62.0267 3.99837 53.8221 0.85534 45.719 0.575923L45.6501 2.57474ZM22.8253 40.5647C30.6947 29.0635 30.6947 11.9365 22.8253 0.435311L21.1747 1.56469C28.5782 12.385 28.5782 28.615 21.1747 39.4353L22.8253 40.5647ZM65.7282 11.6359C68.669 15.205 69.9383 17.651 69.9412 20.0012L71.9412 19.9988C71.9374 16.896 70.246 13.9738 67.2718 10.3641L65.7282 11.6359ZM69.9412 20.0012C69.944 22.3473 68.6863 24.8654 65.7801 28.5771L67.3548 29.8101C70.2867 26.0656 71.9449 23.0763 71.9412 19.9988L69.9412 20.0012ZM29.8253 40.5647C32.1482 37.1698 33.7801 33.2928 34.7303 29.2276L32.7827 28.7724C31.8833 32.6208 30.3442 36.2645 28.1747 39.4353L29.8253 40.5647ZM34.7303 29.2276C36.1399 23.1963 36.055 16.7243 34.4744 10.7444L32.5408 11.2556C34.0374 16.9173 34.1184 23.0575 32.7827 28.7724L34.7303 29.2276ZM34.4744 10.7444C33.499 7.05472 31.9517 3.54311 29.8253 0.435311L28.1747 1.56469C30.1606 4.46711 31.6183 7.76579 32.5408 11.2556L34.4744 10.7444Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect,
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("XOrport1", 0.05, 0.25, true),
                    AddPort("XOrport2", 0.05, 0.73, true),
                    AddPort("XOrport3", 0.94, 0.5, false),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "Gates" } }
            };
            nodes.Add(XorGate);
            Node BulpFullPath = new Node()
            {
                ID = "FullPath",
                Width = 40,
                Height = 60,
                OffsetX=140,
                OffsetY=100,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M9 37.0015H10C10 36.687 9.85204 36.3908 9.60058 36.2019L9 37.0015ZM33 37.0015L32.3994 36.2019C32.148 36.3908 32 36.687 32 37.0015H33ZM11 19L11.6247 18.2191C11.2928 17.9536 10.8292 17.9269 10.469 18.1526C10.1088 18.3783 9.93066 18.8071 10.0249 19.2216L11 19ZM16 23L15.3753 23.7809C15.5866 23.9499 15.8575 24.0261 16.1259 23.992C16.3944 23.958 16.6376 23.8165 16.8 23.6L16 23ZM19 19L19.8 18.4C19.6111 18.1482 19.3148 18 19 18C18.6852 18 18.3889 18.1482 18.2 18.4L19 19ZM22 23L21.2 23.6C21.3889 23.8518 21.6852 24 22 24C22.3148 24 22.6111 23.8518 22.8 23.6L22 23ZM25 19L25.8944 18.5528C25.7394 18.2428 25.435 18.0352 25.0898 18.004C24.7446 17.9729 24.4079 18.1227 24.2 18.4L25 19ZM27 23L26.1056 23.4472C26.2406 23.7173 26.4905 23.9119 26.7855 23.9767C27.0804 24.0415 27.3889 23.9695 27.6247 23.7809L27 23ZM32 19L32.9648 19.2631C33.0787 18.8454 32.9109 18.4019 32.549 18.1642C32.1871 17.9265 31.7134 17.9487 31.3753 18.2191L32 19ZM21 62.5C17.6863 62.5 15 65.1863 15 68.5L17 68.5C17 66.2909 18.7909 64.5 21 64.5L21 62.5ZM15 68.5C15 71.8137 17.6863 74.5 21 74.5L21 72.5C18.7909 72.5 17 70.7091 17 68.5L15 68.5ZM21 74.5C24.3137 74.5 27 71.8137 27 68.5L25 68.5C25 70.7091 23.2091 72.5 21 72.5L21 74.5ZM27 68.5C27 65.1863 24.3137 62.5 21 62.5L21 64.5C23.2091 64.5 25 66.2909 25 68.5L27 68.5ZM22 63.5V55H20V63.5H22ZM2 21C2 10.5066 10.5066 2 21 2V0C9.40202 0 0 9.40202 0 21H2ZM21 2C31.4934 2 40 10.5066 40 21H42C42 9.40202 32.598 0 21 0V2ZM21 54C17.7065 54 14.9584 52.9731 13.0457 51.2436C11.1421 49.5222 10 47.0473 10 44H8C8 47.5801 9.35785 50.6052 11.7043 52.727C14.0416 54.8406 17.2935 56 21 56V54ZM32 44C32 47.0473 30.8579 49.5222 28.9543 51.2436C27.0416 52.9731 24.2935 54 21 54V56C24.7065 56 27.9584 54.8406 30.2957 52.727C32.6421 50.6052 34 47.5801 34 44H32ZM9.60058 36.2019C4.98342 32.7338 2 27.2155 2 21H0C0 27.8711 3.301 33.9715 8.39942 37.801L9.60058 36.2019ZM40 21C40 27.2155 37.0166 32.7338 32.3994 36.2019L33.6006 37.801C38.699 33.9715 42 27.8711 42 21H40ZM10 44V41H8V44H10ZM10 41V37.0015H8V41H10ZM32 37.0015V41H34V37.0015H32ZM32 41V44H34V41H32ZM9 42H33V40H9V42ZM16.9751 40.7784L11.9751 18.7784L10.0249 19.2216L15.0249 41.2216L16.9751 40.7784ZM10.3753 19.7809L15.3753 23.7809L16.6247 22.2191L11.6247 18.2191L10.3753 19.7809ZM16.8 23.6L19.8 19.6L18.2 18.4L15.2 22.4L16.8 23.6ZM18.2 19.6L21.2 23.6L22.8 22.4L19.8 18.4L18.2 19.6ZM22.8 23.6L25.8 19.6L24.2 18.4L21.2 22.4L22.8 23.6ZM24.1056 19.4472L26.1056 23.4472L27.8944 22.5528L25.8944 18.5528L24.1056 19.4472ZM27.6247 23.7809L32.6247 19.7809L31.3753 18.2191L26.3753 22.2191L27.6247 23.7809ZM31.0352 18.7369L25.0352 40.7369L26.9648 41.2631L32.9648 19.2631L31.0352 18.7369ZM33 40H26V42H33V40ZM26 40H16V42H26V40ZM16 40H9V42H16V40Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    
                    Fill = "black"
                },
                Ports = new DiagramObjectCollection<PointPort>()
                {
                    AddPort("BulpPort", 0.5, 0.95, true),
                },
                AdditionalInfo = new Dictionary<string, object> { { "BinaryState", 0 }, { "ControlType", "OutputControl" } },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect),
            };
            nodes.Add(BulpFullPath);
            Node BulpBlackPart = new Node()
            {
                ID = "BulpBlackpart",
                Width = 20,
                Height = 12,
                OffsetX=140,
                OffsetY=110,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M9 44C9 50.6274 14 55 21 55C28 55 33 50.6274 33 44V41H26H16H9V44Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    Fill="black"
                },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect),
            };
            nodes.Add(BulpBlackPart);

            Node InnerBulpPart = new Node()
            {
                ID = "InnerBulpPart",
                Width = 20,
                Height = 16,
                OffsetX=140,
                OffsetY=95,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M16 41H26L32 19L27 23L25 19L22 23L19 19L16 23L11 19L16 41Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    Fill="white"
                },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect),
            };
            nodes.Add(InnerBulpPart);
            Node OuterBulpPart = new Node()
            {
                ID = "OuterBulpPart",
                Width = 36,
                Height = 32,
                OffsetX=140,
                OffsetY=87,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M21 1C9.9543 1 1 9.9543 1 21C1 27.5433 4.14221 33.3526 9 37.0015V41H16L11 19L16 23L19 19L22 23L25 19L27 23L32 19L26 41H33V37.0015C37.8578 33.3526 41 27.5433 41 21C41 9.9543 32.0457 1 21 1Z"
                },
                Style=new ShapeStyle()
                {
                    StrokeColor="black",
                    Fill="white",
                },
                Constraints = NodeConstraints.Default & ~(NodeConstraints.Select | NodeConstraints.InConnect),
            };
            nodes.Add(OuterBulpPart);
            NodeGroup Bulp = new NodeGroup();
            Bulp.ID="LightBulb";
            Bulp.OffsetX=1050;
            Bulp.Width=40;Bulp.Height=60;
            Bulp.OffsetY=170;
            Bulp.Children = new string[] { "FullPath", "BulpBlackpart", "InnerBulpPart", "OuterBulpPart" };
           
            Bulp.Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect;
            
            Bulp.Style=new ShapeStyle()
            {
                Fill="none"
            };
            nodes.Add(Bulp);

            CreateConnector("Connector1", "ANDGate1", "ORGate2", "And_port3", "port1", 0);
            CreateConnector("Connector2", "ORGate", "ANDGate3", "Or_port3", "Andgateport1", 0);
            CreateConnector("Connector3", "ANDGate2", "ANDGate3", "portAnd3", "Andgateport2", 0);
            CreateConnector("Connector4", "ANDGate3", "ORGate2", "Andgateport3", "port2", 0);
            CreateConnector("Connector5", "ORGate2", "XORGate", "port3", "XOrport1", 0);
            CreateConnector("Connector6", "NOTGate", "XORGate", "Not_port2", "XOrport2", 0);
            CreateConnector("Connector7", "SwitchOuter", "ANDGate1", "TogglePort", "And_port1", 0);
            CreateConnector("Connector8", "Push BtnOuterRect", "ANDGate1", "pushBtnPort", "And_port2", 0);
            CreateConnector("Connector9", "Push BtnOuterRect", "ORGate", "pushBtnPort", "Or_port1", 0);
            CreateConnector("Connector10", "Push BtnOuterRect", "ANDGate2", "pushBtnPort", "portAnd1", 0);
            CreateConnector("Connector11", "ClockOuterRect", "ORGate", "ClkPort", "Or_port2", 0);
            CreateConnector("Connector12", "ClockOuterRect", "ANDGate2", "ClkPort", "portAnd2", 0);
            CreateConnector("Connector13", "SwitchOuter1", "NOTGate", "ToggleSwitch2Port", "Not_port1", 0);
            CreateConnector("Connector14", "XORGate", "FullPath", "XOrport3", "BulpPort", 0);
        }

        /// <summary>
        /// This method creates the diagram connector with given parameters
        /// </summary>
        /// <param name="id">Sets the ID of the connector</param>
        /// <param name="sourceID">Sets the Source ID of the connector.</param>
        /// <param name="targetID">Sets the Target ID of the connector.</param>
        /// <param name="sourcePortID">Sets the Source Port ID of the connector.</param>
        /// <param name="targetPortId">Sets the Target port ID of the connector.</param>
        /// <param name="binaryState">Sets the Binary state of the Connector</param>
        private void CreateConnector(string id, string sourceID, string targetID, string sourcePortID, string targetPortId, int binaryState)
        {
            Dictionary<string, object> ConnectorInfo = new Dictionary<string, object>()
            { {id, binaryState } };
            Connector Connector = new Connector()
            {
                ID = id,
                SourceID = sourceID,
                TargetID = targetID,
                Type = ConnectorSegmentType.Bezier,
                SourcePortID = sourcePortID,
                TargetPortID = targetPortId,
                Style =new ShapeStyle() { StrokeWidth=2},
                TargetDecorator = new DecoratorSettings()
                {
                    Shape = DecoratorShape.None,
                },
                AdditionalInfo = ConnectorInfo
            };
            connectors.Add(Connector);
        }
        /// <summary>
        /// Update the nodes and connectors in the diagram when the input gets changed.
        /// </summary>
        public void RunSimulation()
        {
            DiagramObjectCollection<Node> RegulateNodes = new DiagramObjectCollection<Node>();
            for (int i = 0; i < Diagram.Nodes.Count; i++)
            {
                Node node = Diagram.Nodes[i];

                if (node.AdditionalInfo != null && node.AdditionalInfo.Count > 0)
                {
                    string binaryStateValue = node.AdditionalInfo["ControlType"].ToString();

                    switch (binaryStateValue)
                    {
                        case "InputControl":
                            SetBinaryStateFromInput(node);
                            break;
                        case "Gates":
                            GatesOutput(node);
                            break;
                        case "FlipFlop":
                            FlipFlopOutput(node);
                            break;
                        case "OtherControl":
                            OtherControlOutput(node);
                            break;
                        case "OutputControl":
                            foreach (NodeGroup grpNode in Diagram.Nodes.OfType<NodeGroup>())
                            {
                                if (grpNode.Children.Contains(node.ID))
                                {
                                    OutputControlOutput(grpNode);
                                }
                            }
                            if (node.ID.IndexOf("Digit") != -1)
                            {
                                OutputControlOutput(node);
                            }
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Asynchronously sets the binary state of a node based on its input.
        /// </summary>
        /// <param name="node">The node for which the binary state is to be set.</param>
        private void SetBinaryStateFromInput(Node node)
        {
            bool canEnable = Diagram.Nodes.OfType<NodeGroup>().Any(grpNode => grpNode.Children.Contains(node.ID));
            if ((node.ID.IndexOf("Switch") != -1 || node.ID.IndexOf("Push") != -1 || node.ID.IndexOf("Constant") != -1 || node.ID.IndexOf("Clock") != -1) || canEnable)
            {
                
                int binaryStateValue = GetIntegerFromJSONData(node);
                if (node.AdditionalInfo != null && binaryStateValue == 0)
                {
                    for(int i = 0;i<node.OutEdges.Count;i++)
                    {
                        Connector? connector = Diagram.GetObject(node.OutEdges[i]) as Connector;
                        if(connector != null)
                        {
                            connector.AdditionalInfo["BinaryState"]=0;
                            connector.Style.StrokeColor="black";
                            connector.Style.StrokeWidth = 2;

                        }
                    }
                }
                else
                {
                    
                    for (int i = 0; i<node.OutEdges.Count; i++)
                    {
                        Connector? connector = Diagram.GetObject(node.OutEdges[i]) as Connector;
                        if (connector != null)
                        {
                            connector.AdditionalInfo["BinaryState"] = 1;
                            connector.Style.StrokeColor="#05DAC5";
                            connector.Style.StrokeWidth = 2;

                        }
                    }
                    
                }
            }
        }
        /// <summary>
        /// Retrieves the input connector associated with the specified port index of a node.
        /// </summary>
        /// <param name="node">The node for which the input connector is to be retrieved.</param>
        /// <param name="portIndex">The index of the port for which the input connector is to be retrieved.</param>
        private Connector? GetInputConnector(Node node, int portIndex)
        {
            if (node.Ports.Count > portIndex && node.Ports[portIndex].InEdges.Count > 0)
            {
                return Diagram.GetObject(node.Ports[portIndex].InEdges[0]) as Connector;
            }

            return null;
        }
        /// <summary>
        /// Computes the output of gates associated with the specified node.
        /// </summary>
        /// <param name="node">The node for which the gates output is to be computed.</param>
        private void GatesOutput(Node node)
        {
            if (node.ID.IndexOf("XNOR") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);
                Connector? inputConnector2 = GetInputConnector(node, 1);

                int inputState1 = 0;
                int inputState2 = 0;
                if (inputConnector1 != null && inputConnector2 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputConnector2.AdditionalInfo != null)
                    {
                        inputState2 = GetIntegerFromJSONData(inputConnector2);
                    }
                    int state = 0;
                    if ((inputState1 == 0 && inputState2 == 0) || (inputState1 == 1 && inputState2 == 1))
                    {
                        state = 1;
                    }
                    else
                        state = 0;
                    node.AdditionalInfo["BinaryState"] = state;
                    if (state == 1)
                    {
                        UpdateConnectorStyle(node, state);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, state);
                    }
                }
            }
            else if (node.ID.IndexOf("XOR") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);
                Connector? inputConnector2 = GetInputConnector(node, 1);

                int inputState1 = 0;
                int inputState2 = 0;
                if (inputConnector1 != null && inputConnector2 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputConnector2.AdditionalInfo != null)
                    {
                        inputState2 = GetIntegerFromJSONData(inputConnector2);
                    }
                    int state = 0;
                    if ((inputState1 == 0 && inputState2 == 0) || (inputState1 == 1 && inputState2 == 1))
                    {
                        state = 0;
                    }
                    else
                        state = 1;
                    node.AdditionalInfo["BinaryState"] = state;
                    if (state == 1)
                    {
                        UpdateConnectorStyle(node, state);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, state);
                    }
                }
            }
            else if (node.ID.IndexOf("NOR") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);
                Connector? inputConnector2 = GetInputConnector(node, 1);

                int inputState1 = 0;
                int inputState2 = 0;
                if (inputConnector1 != null && inputConnector2 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputConnector2.AdditionalInfo != null)
                    {
                        inputState2 = GetIntegerFromJSONData(inputConnector2);
                    }
                    int state = 0;
                    if ((inputState1 == 0 && inputState2 == 0))
                    {
                        state = 1;
                    }
                    else
                        state = 0;
                    node.AdditionalInfo["BinaryState"] = state;
                    if (state == 1)
                    {
                        UpdateConnectorStyle(node, state);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, state);
                    }
                }
            }
            else if (node.ID.IndexOf("OR") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);
                Connector? inputConnector2 = GetInputConnector(node, 1);

                int inputState1 = 0;
                int inputState2 = 0;
                if (inputConnector1 != null && inputConnector2 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputConnector2.AdditionalInfo != null)
                    {
                        inputState2 = GetIntegerFromJSONData(inputConnector2);
                    }
                    int state = 0;
                    if ((inputState1 == 0 && inputState2 == 0))
                    {
                        state = 0;
                    }
                    else
                        state = 1;
                    node.AdditionalInfo["BinaryState"] = state;
                    if (state == 1)
                    {
                        UpdateConnectorStyle(node, state);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, state);
                    }
                }
            }
            else
                LogicGatesOutput(node);
            
        }
        /// <summary>
        /// This method is used to update the properties of logic gates.
        /// </summary>
        private void LogicGatesOutput(Node node)
        {
            if (node.ID.IndexOf("NAND") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);
                Connector? inputConnector2 = GetInputConnector(node, 1);

                int inputState1 = 0;
                int inputState2 = 0;
                if (inputConnector1 != null && inputConnector2 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputConnector2.AdditionalInfo != null)
                    {
                        inputState2 = GetIntegerFromJSONData(inputConnector2);
                    }
                    int state = 0;
                    if ((inputState1 == 1 && inputState2 == 1))
                    {
                        state = 0;
                    }
                    else
                        state = 1;
                    node.AdditionalInfo["BinaryState"] = state;
                    if (state == 1)
                    {
                        UpdateConnectorStyle(node, state);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, state);
                    }
                }
            }
            else if (node.ID.IndexOf("AND") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);
                Connector? inputConnector2 = GetInputConnector(node, 1);

                int inputState1 = 0;
                int inputState2 = 0;
                if (inputConnector1 != null && inputConnector2 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputConnector2.AdditionalInfo != null)
                    {
                        inputState2 = GetIntegerFromJSONData(inputConnector2);
                    }
                    int state = 0;
                    if ((inputState1 == 1 && inputState2 == 1))
                    {
                        state = 1;
                    }
                    else
                        state = 0;
                    node.AdditionalInfo["BinaryState"] = state;
                    if (state == 1)
                    {
                        UpdateConnectorStyle(node, state);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, state);
                    }
                }
            }
            else if (node.ID.IndexOf("NOT") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);

                int inputState1 = 0;
                if (inputConnector1 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputState1 == 0)
                    {
                        node.AdditionalInfo["BinaryState"] = 1;
                        UpdateConnectorStyle(node, 1);
                    }
                    else
                    {
                        node.AdditionalInfo["BinaryState"] = 0;
                        UpdateConnectorStyle(node, 0);
                    }


                }
            }
            else if (node.ID.IndexOf("Buffer") != -1)
            {
                Connector? inputConnector1 = GetInputConnector(node, 0);

                int inputState1 = 0;
                if (inputConnector1 != null)
                {
                    if (inputConnector1.AdditionalInfo != null)
                    {
                        inputState1 = GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputState1 == 1)
                    {
                        node.AdditionalInfo["BinaryState"] = 1;
                        UpdateConnectorStyle(node, inputState1);
                    }
                    else
                    {
                        node.AdditionalInfo["BinaryState"] = 0;
                        UpdateConnectorStyle(node, inputState1);
                    }

                }
            }
        }
        /// <summary>
        /// Updates the style of connectors associated with the specified node based on the provided state value.
        /// </summary>
        /// <param name="element">The node for which the connector style is to be updated.</param>
        /// <param name="stateValue">The state value used to determine the connector style.</param>
        private void UpdateConnectorStyle(Node element, int stateValue)
        {
            if(stateValue == 1)
            {
                for (int i = 0; i<element.OutEdges.Count; i++)
                {
                    Connector? connector = Diagram.GetObject(element.OutEdges[i]) as Connector;
                    if (connector != null)
                    {
                        connector.AdditionalInfo["BinaryState"] = 1;
                        connector.Style.StrokeColor="#05DAC5";
                        connector.Style.StrokeWidth = 2;
                    }
                }
            }
            else
            {
                for (int i = 0; i<element.OutEdges.Count; i++)
                {
                    Connector? connector = Diagram.GetObject(element.OutEdges[i]) as Connector;
                    if (connector != null)
                    {
                        connector.AdditionalInfo["BinaryState"]=0;
                        connector.Style.StrokeColor="black";
                        connector.Style.StrokeWidth = 2;
                    }
                }
            }
            
        }
        /// <summary>
        /// Flips the output state of a flip-flop node.
        /// </summary>
        private void FlipFlopOutput(Node node)
        {
            if (node.ID.IndexOf("JK") != -1)
            {
                bool skip = false;
                
                Connector? inputConnector1 = node.Ports[0].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[0].InEdges[0]) as Connector : null;
                Connector? inputConnector2 = node.Ports[1].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[1].InEdges[0]) as Connector : null;
                Connector? inputConnector3 = node.Ports[2].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[2].InEdges[0]) as Connector : null;
                Connector? inputConnector4 = node.Ports[5].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[5].InEdges[0]) as Connector : null;
                Connector? inputConnector5 = node.Ports[6].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[6].InEdges[0]) as Connector : null;
                int inputState1 = 0; int inputState2 = 0; int inputState3 = 0; int inputState4 = 0; int inputState5 = 0;
                DiagramObjectCollection<Connector> inputConnectors = new DiagramObjectCollection<Connector>();
                if (inputConnector1 != null) { inputConnectors.Add(inputConnector1); }
                if(inputConnector2 != null) { inputConnectors.Add(inputConnector2);}
                if (inputConnector3 != null) { inputConnectors.Add(inputConnector3); }
                if (inputConnector4 != null) { inputConnectors.Add(inputConnector4); }
                if (inputConnector5 != null) { inputConnectors.Add(inputConnector5); }

                int outstate1 = 0;int outstate2 = 0;
                foreach (Connector connector in inputConnectors)
                {
                    if (connector != null)
                    {
                        if(connector.TargetPortID.IndexOf("JPort") != -1)
                        {
                            inputState1 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("ClkPort") != -1)
                        {
                            inputState2 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("KPort") != -1)
                        {
                            inputState3 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("PrePort") != -1)
                        {
                            inputState4 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("ClrPort") != -1)
                        {
                            inputState5 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                    }
                }
                bool checkConnection = false;
                for(int j=0; j<inputConnectors.Count;j++)
                {
                    if (inputConnectors[j] != null)
                    {
                        checkConnection = true;
                        break;
                    }
                }
                if(checkConnection)
                {
                    if((inputState1==0 && inputState2==0 && inputState3==0 && inputState4==0 && inputState5==0) || (inputState1==0 && inputState2==1 && inputState3==0 && inputState4==0 && inputState5==0)) 
                    {
                        outstate1 = 1; outstate2 = 1;
                    }
                    else if ((inputState1==1 && inputState2==1 && inputState3==1 && inputState4==1 && inputState5==1))
                    {
                        outstate1 = 1; outstate2 = 0;
                    }
                    else if ((inputState1==1 && inputState2==0 && inputState3==1 && inputState4==1 && inputState5==1))
                    {
                        outstate1 = 0; outstate2 = 1;
                    }
                    else if ((inputState1==0 && inputState3==1 && inputState4==1 && inputState5==1) || (inputState4==1 && inputState5==0))
                    {
                        outstate1 = 0; outstate2 = 1;
                    }
                    else if ((inputState1==1 && inputState3==0 && inputState4==1 && inputState5==1) || (inputState4==0 && inputState5==1))
                    {
                        outstate1 = 1; outstate2 = 0;
                    }
                    else if (inputState4==1 && inputState5==1)
                    {
                        skip = true;
                    }

                    if(!skip)
                    {
                        node.AdditionalInfo["BinaryState1"] = outstate1;
                        node.AdditionalInfo["BinaryState2"] = outstate2;

                        Connector? con1 = node.Ports[3].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[3].OutEdges[0]) as Connector : null;
                        Connector? con2 = node.Ports[4].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[4].OutEdges[0]) as Connector : null;

                        Connector? outConnector1 = null;
                        Connector? outConnector2 = null;
                        if(con1 != null && con1.SourcePortID.IndexOf("q1Port") !=-1)
                            outConnector2 = con1;
                        else if(con1 != null && con1.SourcePortID.IndexOf("qPort")!=-1)
                            outConnector1 = con1;

                        if(con2 !=null && con2.SourcePortID.IndexOf("q1Port") !=-1)
                            outConnector2 = con2;
                        else if(con2 !=null && con2.SourcePortID.IndexOf("qPort") != -1)
                            outConnector1 = con2;

                        if(outstate1 == 1)
                        {
                            if(outConnector1 != null)
                            {
                                outConnector1.AdditionalInfo["BinaryState"] = 1;
                                outConnector1.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                            }
                        }
                        else
                        {
                            if (outConnector1 != null)
                            {
                                outConnector1.AdditionalInfo["BinaryState"] = 0;
                                outConnector1.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                            }
                        }

                        if(outstate2 == 1)
                        {
                            if (outConnector2 != null)
                            {
                                outConnector2.AdditionalInfo["BinaryState"] = 1;
                                outConnector2.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                            }
                        }
                        else
                        {
                            if (outConnector2 != null)
                            {
                                outConnector2.AdditionalInfo["BinaryState"] = 0;
                                outConnector2.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                            }
                        }
                    }
                }
            }
            else if(node.ID.IndexOf("D") != -1)
            {
                Connector? inputConnector1 = node.Ports[0].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[0].InEdges[0]) as Connector : null;
                Connector? inputConnector2 = node.Ports[1].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[1].InEdges[0]) as Connector : null;
                Connector? inputConnector3 = node.Ports[4].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[4].InEdges[0]) as Connector : null;
                Connector? inputConnector4 = node.Ports[5].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[5].InEdges[0]) as Connector : null;
                int inputState1 = 0; int inputState2 = 0; int inputState3 = 0; int inputState4 = 0;
                DiagramObjectCollection<Connector> inputConnectors = new DiagramObjectCollection<Connector>();
                if (inputConnector1 != null) { inputConnectors.Add(inputConnector1); }
                if (inputConnector2 != null) { inputConnectors.Add(inputConnector2); }
                if (inputConnector3 != null) { inputConnectors.Add(inputConnector3); }
                if (inputConnector4 != null) { inputConnectors.Add(inputConnector4); }

                int outstate1 = 0; int outstate2 = 0;
                foreach (Connector connector in inputConnectors)
                {
                    if (connector != null)
                    {
                        if (connector.TargetPortID.IndexOf("DTPort") != -1)
                        {
                            inputState1 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("ClkPort") != -1)
                        {
                            inputState2 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("PrePort") != -1)
                        {
                            inputState3 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("ClrPort") != -1)
                        {
                            inputState4 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                    }
                }
                bool checkConnection = false;
                for (int j = 0; j<inputConnectors.Count; j++)
                {
                    if (inputConnectors[j] != null)
                    {
                        checkConnection = true;
                        break;
                    }
                }
                if (checkConnection)
                {
                    if ((inputState3==0 && inputState4==0) && (inputState1==0))
                    {
                        outstate1 = 1; outstate2 = 1;
                    }
                    else if ((inputState3==1 && inputState4==1) && (inputState1==1))
                    {
                        outstate1 = 1; outstate2 = 0;
                    }
                    else if ((inputState3==1 && inputState4==1) && (inputState1==0))
                    {
                        outstate1 = 0; outstate2 = 1;
                    }
                    else if ((inputState3==1 && inputState4==0))
                    {
                        outstate1 = 0; outstate2 = 1;
                    }
                    else if ((inputState3==0 && inputState4==1))
                    {
                        outstate1 = 1; outstate2 = 0;
                    }

                    node.AdditionalInfo["BinaryState1"] = outstate1;
                    node.AdditionalInfo["BinaryState2"] = outstate2;

                    
                    Connector? con1 = node.Ports[2].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[2].OutEdges[0]) as Connector : null;
                    Connector? con2 = node.Ports[3].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[3].OutEdges[0]) as Connector : null;

                    Connector? outConnector1 = null;
                    Connector? outConnector2 = null;
                    if (con1 != null && con1.SourcePortID.IndexOf("q1Port") != -1)
                        outConnector2 = con1;
                    else if (con1 != null && con1.SourcePortID.IndexOf("qPort") != -1)
                        outConnector1 = con1;

                    if (con2 !=null && con2.SourcePortID.IndexOf("q1Port") != -1)
                        outConnector2 = con2;
                    else if (con2 !=null && con2.SourcePortID.IndexOf("qPort") != -1)
                        outConnector1 = con2;

                    if (outstate1 == 1)
                    {
                        if (outConnector1 != null)
                        {
                            outConnector1.AdditionalInfo["BinaryState"] = 1;
                            outConnector1.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                        }
                    }
                    else
                    {
                        if (outConnector1 != null)
                        {
                            outConnector1.AdditionalInfo["BinaryState"] = 0;
                            outConnector1.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                        }
                    }

                    if (outstate2 == 1)
                    {
                        if (outConnector2 != null)
                        {
                            outConnector2.AdditionalInfo["BinaryState"] = 1;
                            outConnector2.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                        }
                    }
                    else
                    {
                        if (outConnector2 != null)
                        {
                            outConnector2.AdditionalInfo["BinaryState"] = 0;
                            outConnector2.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                        }
                    }
                    
                }
            }
            else if (node.ID.IndexOf("T") != -1)
            {
                bool skip = false;
                
                Connector? inputConnector1 = node.Ports[0].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[0].InEdges[0]) as Connector : null;
                Connector? inputConnector2 = node.Ports[1].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[1].InEdges[0]) as Connector : null;
                Connector? inputConnector3 = node.Ports[4].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[4].InEdges[0]) as Connector : null;
                Connector? inputConnector4 = node.Ports[5].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[5].InEdges[0]) as Connector : null;
                int inputState1 = 0; int inputState2 = 0; int inputState3 = 0; int inputState4 = 0;
                DiagramObjectCollection<Connector> inputConnectors = new DiagramObjectCollection<Connector>();
                if (inputConnector1 != null) { inputConnectors.Add(inputConnector1); }
                if (inputConnector2 != null) { inputConnectors.Add(inputConnector2); }
                if (inputConnector3 != null) { inputConnectors.Add(inputConnector3); }
                if (inputConnector4 != null) { inputConnectors.Add(inputConnector4); }

                int outstate1 = 0; int outstate2 = 0;
                foreach (Connector connector in inputConnectors)
                {
                    if (connector != null)
                    {
                        if (connector.TargetPortID.IndexOf("DTPort") != -1)
                        {
                            inputState1 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("ClkPort") != -1)
                        {
                            inputState2 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("PrePort") != -1)
                        {
                            inputState3 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("ClrPort") != -1)
                        {
                            inputState4 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                    }
                }
                bool checkConnection = false;
                for (int j = 0; j<inputConnectors.Count; j++)
                {
                    if (inputConnectors[j] != null)
                    {
                        checkConnection = true;
                        break;
                    }
                }
                if (checkConnection)
                {
                    if ((inputState1==0 && inputState2==0 && inputState3==0 && inputState4==0) || (inputState1==1 && inputState2==1 && inputState3==0 && inputState4==0) || (inputState1==0 && inputState2==1 && inputState3==0 && inputState4==0))
                    {
                        outstate1 = 1; outstate2 = 1;
                    }
                    else if ((inputState1==1 && inputState3==1 && inputState4==1))
                    {
                        if(inputState2==1)
                        {
                            
                            Connector? out1 = node.Ports[2].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[2].OutEdges[0]) as Connector : null;
                            Connector? out2 = node.Ports[3].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[3].OutEdges[0]) as Connector : null;

                            DiagramObjectCollection<Connector> outConn = new DiagramObjectCollection<Connector>();
                            if (out1 != null) { outConn.Add(out1); }
                            if (out2 != null) { outConn.Add(out2); }

                            foreach (Connector connector in outConn)
                            {
                                if(connector != null)
                                {
                                    if(connector.SourcePortID.IndexOf("qPort") !=-1)
                                    {
                                        outstate1 = GetIntegerFromJSONData(connector);
                                    }
                                    else if(connector.SourcePortID.IndexOf("q1Port") !=-1)
                                    {
                                        outstate2 = GetIntegerFromJSONData(connector);
                                    }
                                }
                            }
                        }
                        else if (inputState2==0)
                        {
                            skip=true;

                        }
                    }
                    else if ((inputState3==1 && inputState4==0))
                    {
                        outstate1 = 0; outstate2 = 1;
                    }
                    else if ((inputState3==0 && inputState4==1))
                    {
                        outstate1 = 1; outstate2 = 0;
                    }
                    else if ((inputState3==1 && inputState4==1))
                    {
                        skip=true;
                    }
                    if(!skip)
                    {
                        node.AdditionalInfo["BinaryState1"] = outstate1;
                        node.AdditionalInfo["BinaryState2"] = outstate2;

                        
                        Connector? con1 = node.Ports[2].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[2].OutEdges[0]) as Connector : null;
                        Connector? con2 = node.Ports[3].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[3].OutEdges[0]) as Connector : null;
                        Connector? outConnector1 = null;
                        Connector? outConnector2 = null;
                        if (con1 != null && con1.SourcePortID.IndexOf("q1Port") != -1)
                            outConnector2 = con1;
                        else if (con1 != null && con1.SourcePortID.IndexOf("qPort") != -1)
                            outConnector1 = con1;

                        if (con2 !=null && con2.SourcePortID.IndexOf("q1Port") != -1)
                            outConnector2 = con2;
                        else if (con2 !=null && con2.SourcePortID.IndexOf("qPort") != -1)
                            outConnector1 = con2;

                        if (outstate1 == 1)
                        {
                            if (outConnector1 != null)
                            {
                                outConnector1.AdditionalInfo["BinaryState"] = 1;
                                outConnector1.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                            }
                        }
                        else
                        {
                            if (outConnector1 != null)
                            {
                                outConnector1.AdditionalInfo["BinaryState"] = 0;
                                outConnector1.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                            }
                        }

                        if (outstate2 == 1)
                        {
                            if (outConnector2 != null)
                            {
                                outConnector2.AdditionalInfo["BinaryState"] = 1;
                                outConnector2.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                            }
                        }
                        else
                        {
                            if (outConnector2 != null)
                            {
                                outConnector2.AdditionalInfo["BinaryState"] = 0;
                                outConnector2.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                            }
                        }
                    }
                }
            }
            else if (node.ID.IndexOf("SR") != -1)
            {
                bool skip = false;

                Connector? inputConnector1 = node.Ports[0].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[0].InEdges[0]) as Connector : null;
                Connector? inputConnector2 = node.Ports[1].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[1].InEdges[0]) as Connector : null;
                Connector? inputConnector3 = node.Ports[2].InEdges.Count > 0 ? Diagram.GetObject(node.Ports[2].InEdges[0]) as Connector : null;
                int inputState1 = 0; int inputState2 = 0; int inputState3 = 0;
                DiagramObjectCollection<Connector> inputConnectors = new DiagramObjectCollection<Connector>();
                if (inputConnector1 != null) { inputConnectors.Add(inputConnector1); }
                if (inputConnector2 != null) { inputConnectors.Add(inputConnector2); }
                if (inputConnector3 != null) { inputConnectors.Add(inputConnector3); }

                int outstate1 = 0; int outstate2 = 0;
                foreach (Connector connector in inputConnectors)
                {
                    if (connector != null)
                    {
                        if (connector.TargetPortID.IndexOf("SPort") != -1)
                        {
                            inputState1 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("ClkPort") != -1)
                        {
                            inputState2 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("RPort") != -1)
                        {
                            inputState3 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                    }
                }
                
                if ((inputState1==1 && inputState3==1))
                {
                    outstate1 = 0; outstate2 = 0;
                }
                else if ((inputState1==1 && inputState3==0))
                {
                    outstate1 = 1; outstate2 = 0;
                }
                else if ((inputState1==0 && inputState3==1))
                {
                    outstate1 = 0; outstate2 = 1;
                }
                else
                {
                    skip=true;
                }
                if (!skip)
                {
                    node.AdditionalInfo["BinaryState1"] = outstate1;
                    node.AdditionalInfo["BinaryState2"] = outstate2;

                    Connector? con1 = node.Ports[3].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[3].OutEdges[0]) as Connector : null;
                    Connector? con2 = node.Ports[4].OutEdges.Count > 0 ? Diagram.GetObject(node.Ports[4].OutEdges[0]) as Connector : null;
                    Connector? outConnector1 = null;
                    Connector? outConnector2 = null;
                    if (con1 != null && con1.SourcePortID.IndexOf("q1Port") != -1)
                        outConnector2 = con1;
                    else if (con1 != null && con1.SourcePortID.IndexOf("qPort") !=-1)
                        outConnector1 = con1;

                    if (con2 !=null && con2.SourcePortID.IndexOf("q1Port") != -1)
                        outConnector2 = con2;
                    else if (con2 !=null && con2.SourcePortID.IndexOf("qPort") != -1)
                        outConnector1 = con2;

                    if (outstate1 == 1)
                    {
                        if (outConnector1 != null)
                        {
                            outConnector1.AdditionalInfo["BinaryState"] = 1;
                            outConnector1.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                        }
                    }
                    else
                    {
                        if (outConnector1 != null)
                        {
                            outConnector1.AdditionalInfo["BinaryState"] = 0;
                            outConnector1.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                        }
                    }

                    if (outstate2 == 1)
                    {
                        if (outConnector2 != null)
                        {
                            outConnector2.AdditionalInfo["BinaryState"] = 1;
                            outConnector2.Style = new ShapeStyle() { StrokeColor="#05DAC5", StrokeWidth = 2 };
                        }
                    }
                    else
                    {
                        if (outConnector2 != null)
                        {
                            outConnector2.AdditionalInfo["BinaryState"] = 0;
                            outConnector2.Style = new ShapeStyle() { StrokeColor="black", StrokeWidth = 2 };
                        }
                    }
                }
                
            }
        }
        /// <summary>
        /// Computes the output of other control elements associated with the specified node.
        /// </summary>
        /// <param name="node">The node for which the other control elements output is to be computed.</param>
        private void OtherControlOutput(Node node)
        {
            if (node.ID.IndexOf("Bus") != -1)
            {
                Connector? inputConnector1 = null;
                Connector? inputConnector2 = null;
                if (node.Ports[0].InEdges.Count>0)
                    inputConnector1 = Diagram.GetObject(node.Ports[0].InEdges[0]) as Connector;
                if (node.Ports[1].InEdges.Count>0)
                    inputConnector2 = Diagram.GetObject(node.Ports[1].InEdges[0]) as Connector;
                int inputState1 = 0; int inputState2 = 0;
                if(inputConnector1 != null && inputConnector2 != null)
                {
                    if(inputConnector1.AdditionalInfo != null )
                    {
                        inputState1=GetIntegerFromJSONData(inputConnector1);
                    }
                    if (inputConnector2.AdditionalInfo != null)
                    {
                        inputState2=GetIntegerFromJSONData(inputConnector2);
                    }
                    int state = 0;
                    if(inputState1==0 && inputState2==0)
                    {
                        state=0;
                    }
                    else if((inputState1==1 && inputState2==0) || (inputState1==0 && inputState2==1))
                    {
                        state=2;
                    }
                    else
                    {
                        state=1;
                    }
                    node.AdditionalInfo["BinaryState"]=state;
                    if(state==1)
                    {
                        UpdateConnectorStyle(node, state);
                    }
                    else if(state==2)
                    {
                        for (int i = 0; i<node.OutEdges.Count; i++)
                        {
                            Connector? connector = Diagram.GetObject(node.OutEdges[i]) as Connector;
                            if (connector != null)
                            {
                                connector.AdditionalInfo["BinaryState"] = 2;
                                connector.Style = new ShapeStyle() { StrokeColor = "grey", StrokeWidth = 2 };
                            }
                        }
                    }
                    else
                    {
                        UpdateConnectorStyle(node, state );
                    }
                }
                else if(inputConnector2==null && inputConnector1!=null)
                {
                    inputState1 = GetIntegerFromJSONData(inputConnector1);
                    int newState =inputState1;
                    node.AdditionalInfo["BinaryState"]=newState;
                    if(newState==1)
                    {
                        UpdateConnectorStyle(node, newState);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, newState );
                    }
                }
                else if (inputConnector1 == null && inputConnector2 != null)
                {
                    inputState1 = GetIntegerFromJSONData(inputConnector2);
                    int newState = inputState1;
                    node.AdditionalInfo["BinaryState"]=newState;
                    if (newState==1)
                    {
                        UpdateConnectorStyle(node, newState);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, newState);
                    }
                }
            }
            else if(node.ID.IndexOf("Pull") != -1)
            {
                Connector? inputconnector = null;
                if (node.Ports[0].InEdges.Count>0)
                {
                    inputconnector = Diagram.GetObject(node.Ports[0].InEdges[0]) as Connector;
                }
                
                if(inputconnector!=null)
                {
                    int state = GetIntegerFromJSONData(inputconnector);
                    node.AdditionalInfo["BinaryState"] = state;
                    if(state==1)
                    {
                        UpdateConnectorStyle(node, 0);
                    }
                    else
                    {
                        UpdateConnectorStyle(node, 1);
                    }
                }
            }
        }
        /// <summary>
        /// Computes the output of the control elements associated with the output of the specified node.
        /// </summary>
        private void OutputControlOutput(Node node)
        {
            if(node.ID.IndexOf("Bulb") != -1 && node is NodeGroup grpNode)
            { 
                Connector? inputConnector = null;
                Node? child = Diagram.GetObject(grpNode.Children[0]) as Node;
                if (child != null && child.AdditionalInfo.Count>0)
                {
                    if (child.Ports[0].InEdges.Count>0)
                        inputConnector = Diagram.GetObject(child.Ports[0].InEdges[0]) as Connector;
                }
                if(inputConnector!=null && inputConnector.AdditionalInfo != null && inputConnector.AdditionalInfo.Count > 0)
                {
                    int inputState = inputConnector.AdditionalInfo!=null ? GetIntegerFromJSONData(inputConnector) : 0;
                    if(inputState==0)
                    {
                        Node? child1 = Diagram.GetObject(grpNode.Children[2]) as Node;
                        Node? child2 = Diagram.GetObject(grpNode.Children[3]) as Node;
                        if(child1!=null)
                            child1.IsVisible= false;
                        if(child2!=null)
                            child2.IsVisible= false;
                        
                    }
                    else if (inputState==1)
                    {
                        Node? child1 = Diagram.GetObject(grpNode.Children[2]) as Node;
                        Node? child2 = Diagram.GetObject(grpNode.Children[3]) as Node;
                        if (child1!=null)
                        {
                            child1.IsVisible= true;
                            child1.Style=new ShapeStyle()
                            {
                                Fill="#05DAC5"
                            };
                        } 
                        if (child2!=null)
                        {
                            child2.IsVisible = true;
                            child2.Style=new ShapeStyle()
                            {
                                Fill="#05DAC5"
                            };
                        }
                    }
                    else
                    {
                        Node? child1 = Diagram.GetObject(grpNode.Children[2]) as Node;
                        Node? child2 = Diagram.GetObject(grpNode.Children[3]) as Node;
                        if (child1!=null)
                            child1.IsVisible= false;
                        if (child2!=null)
                            child2.IsVisible= false;
                        
                    }
                }
                else
                {
                    Node? child1 = Diagram.GetObject(grpNode.Children[2]) as Node;
                    Node? child2 = Diagram.GetObject(grpNode.Children[3]) as Node;
                    if (child1!=null)
                        child1.IsVisible= false;
                    if (child2!=null)
                        child2.IsVisible= false;
                }
            }
            else if(node.ID.IndexOf("Digit") != -1)
            {
                Connector? inputConn1 = null;
                Connector? inputConn2 = null;
                Connector? inputConn3 = null;
                Connector? inputConn4 = null;
                if (node.Ports.Count>0)
                {
                    if (node.Ports[0].InEdges.Count>0)
                        inputConn1 = Diagram.GetObject(node.Ports[0].InEdges[0]) as Connector;
                    if (node.Ports[1].InEdges.Count>0)
                        inputConn2 = Diagram.GetObject(node.Ports[1].InEdges[0]) as Connector;
                    if (node.Ports[2].InEdges.Count>0)
                        inputConn3 = Diagram.GetObject(node.Ports[2].InEdges[0]) as Connector;
                    if (node.Ports[3].InEdges.Count>0)
                        inputConn4 = Diagram.GetObject(node.Ports[3].InEdges[0]) as Connector;
                }
                
                int inputState1 = 0;int inputState2 = 0; int inputState3 = 0; int inputState4 = 0;
                DiagramObjectCollection<Connector> inputConnectors = new DiagramObjectCollection<Connector>();
                if (inputConn1 != null) { inputConnectors.Add(inputConn1); }
                if (inputConn2 != null) { inputConnectors.Add(inputConn2); }
                if (inputConn3 != null) { inputConnectors.Add(inputConn3); }
                if (inputConn4 != null) { inputConnectors.Add(inputConn4); }

                foreach (Connector connector in inputConnectors)
                {
                    if (connector != null)
                    {
                        if (connector.TargetPortID.IndexOf("DigitPort1") != -1)
                        {
                            inputState1 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("DigitPort2") != -1)
                        {
                            inputState2 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("DigitPort3") != -1)
                        {
                            inputState3 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                        else if (connector.TargetPortID.IndexOf("DigitPort4") != -1)
                        {
                            inputState4 = connector.AdditionalInfo != null ? GetIntegerFromJSONData(connector) : 0;
                        }
                    }
                }
                if(inputState1==0 && inputState2==0 && inputState3==0 && inputState4==0)
                {
                    if(node.Shape is PathShape element )
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M54.7838 50.672C50.9758 50.672 47.8958 49.0853 45.5438 45.912C43.2291 42.7387 42.0718 38.128 42.0718 32.08C42.0718 25.9947 43.2291 21.4213 45.5438 18.36C47.8958 15.2987 50.9758 13.768 54.7838 13.768C58.5918 13.768 61.6531 15.3173 63.9678 18.416C66.3198 21.4773 67.4958 26.032 67.4958 32.08C67.4958 38.128 66.3198 42.7387 63.9678 45.912C61.6531 49.0853 58.5918 50.672 54.7838 50.672ZM54.7838 44.288C55.7171 44.288 56.5571 43.952 57.3038 43.28C58.0504 42.5707 58.6291 41.3387 59.0398 39.584C59.4878 37.792 59.7118 35.2907 59.7118 32.08C59.7118 28.832 59.4878 26.3493 59.0398 24.632C58.6291 22.9147 58.0504 21.7387 57.3038 21.104C56.5571 20.4693 55.7171 20.152 54.7838 20.152C53.8504 20.152 53.0104 20.4693 52.2638 21.104C51.5171 21.7387 50.9198 22.9147 50.4718 24.632C50.0611 26.3493 49.8558 28.832 49.8558 32.08C49.8558 35.2907 50.0611 37.792 50.4718 39.584C50.9198 41.3387 51.5171 42.5707 52.2638 43.28C53.0104 43.952 53.8504 44.288 54.7838 44.288Z";
                        
                    } 
                }
                else if(inputState1==1 && inputState2==0 && inputState3==0 && inputState4==0)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M43.9199 50V43.336H51.6479V22.784H44.9839V17.688C46.9253 17.3147 48.5679 16.8667 49.9119 16.344C51.2933 15.8213 52.5999 15.1867 53.8319 14.44H59.8799V43.336H66.5439V50H43.9199Z";
                       
                    }
                }
                else if (inputState1==0 && inputState2==1 && inputState3==0 && inputState4==0)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M42.0719 50V44.904C45.0586 42.2533 47.7092 39.7707 50.0239 37.456C52.3759 35.1413 54.2052 32.9947 55.5119 31.016C56.8559 29.0373 57.5279 27.2267 57.5279 25.584C57.5279 23.8293 57.0799 22.504 56.1839 21.608C55.2879 20.6747 54.0559 20.208 52.4879 20.208C51.2186 20.208 50.0612 20.5813 49.0159 21.328C48.0079 22.0373 47.0559 22.8773 46.1599 23.848L41.8479 19.592C43.5652 17.6507 45.3199 16.1947 47.1119 15.224C48.9412 14.2533 51.1066 13.768 53.6079 13.768C55.9226 13.768 57.9572 14.2347 59.7119 15.168C61.5039 16.1013 62.8852 17.408 63.8559 19.088C64.8639 20.768 65.3679 22.7467 65.3679 25.024C65.3679 27.152 64.7892 29.2427 63.6319 31.296C62.5119 33.312 61.0186 35.328 59.1519 37.344C57.2852 39.36 55.3066 41.4133 53.2159 43.504C54.1119 43.392 55.1199 43.2987 56.2399 43.224C57.3599 43.112 58.3306 43.056 59.1519 43.056H67.1039V50H42.0719Z";
                        
                    }
                }
                else if (inputState1==1 && inputState2==1 && inputState3==0 && inputState4==0)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M53.6079 50.672C51.7039 50.672 49.9679 50.448 48.3999 50C46.8693 49.5893 45.5066 49.0107 44.3119 48.264C43.1173 47.5173 42.0906 46.64 41.2319 45.632L45.0399 40.424C46.1226 41.4693 47.3173 42.328 48.6239 43C49.9306 43.672 51.3306 44.008 52.8239 44.008C54.5413 44.008 55.9226 43.6533 56.9679 42.944C58.0506 42.1973 58.5919 41.152 58.5919 39.808C58.5919 38.7627 58.3306 37.8667 57.8079 37.12C57.2853 36.336 56.3333 35.7387 54.9519 35.328C53.6079 34.9173 51.6293 34.712 49.0159 34.712V28.888C51.1439 28.888 52.8239 28.6827 54.0559 28.272C55.2879 27.8613 56.1466 27.3013 56.6319 26.592C57.1546 25.8827 57.4159 25.0613 57.4159 24.128C57.4159 22.8587 57.0239 21.888 56.2399 21.216C55.4933 20.544 54.4293 20.208 53.0479 20.208C51.7786 20.208 50.6213 20.488 49.5759 21.048C48.5679 21.5707 47.5039 22.336 46.3839 23.344L42.2399 18.304C43.8826 16.8853 45.6186 15.784 47.4479 15C49.2773 14.1787 51.2746 13.768 53.4399 13.768C55.8666 13.768 57.9946 14.16 59.8239 14.944C61.6906 15.6907 63.1279 16.792 64.1359 18.248C65.1813 19.6667 65.7039 21.4213 65.7039 23.512C65.7039 25.3413 65.1813 26.9093 64.1359 28.216C63.1279 29.5227 61.6719 30.5867 59.7679 31.408V31.632C61.1119 32.0427 62.3253 32.64 63.4079 33.424C64.4906 34.1707 65.3306 35.1227 65.9279 36.28C66.5253 37.4373 66.8239 38.7813 66.8239 40.312C66.8239 42.5147 66.2079 44.4 64.9759 45.968C63.7813 47.4987 62.1759 48.6747 60.1599 49.496C58.1813 50.28 55.9973 50.672 53.6079 50.672Z";
                    }
                }
                else if (inputState1==0 && inputState2==0 && inputState3==1 && inputState4==0)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M55.1816 19.3926V36.5742H59.1338V40.1553H55.1816V46H51.0068V40.1553H38.9648V36.5371C40.0534 35.3125 41.1667 33.9889 42.3047 32.5664C43.4551 31.1315 44.556 29.6719 45.6074 28.1875C46.6712 26.6908 47.6546 25.194 48.5576 23.6973C49.4606 22.2005 50.2214 20.7656 50.8398 19.3926H55.1816ZM51.0068 36.5742V25.4043C49.6585 27.8288 48.3226 29.944 46.999 31.75C45.6878 33.556 44.457 35.1641 43.3066 36.5742H51.0068Z";
                    }
                }
                else if (inputState1==1 && inputState2==0 && inputState3==1 && inputState4==0)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M53.8321 50.672C51.8908 50.672 50.1361 50.448 48.5681 50C47.0374 49.552 45.6748 48.9547 44.4801 48.208C43.2854 47.4613 42.2214 46.6213 41.2881 45.688L44.9841 40.48C46.0668 41.4507 47.2428 42.2907 48.5121 43C49.7814 43.672 51.1628 44.008 52.6561 44.008C53.8508 44.008 54.8961 43.8027 55.7921 43.392C56.6881 42.944 57.3788 42.3093 57.8641 41.488C58.3868 40.6667 58.6481 39.6587 58.6481 38.464C58.6481 36.672 58.1254 35.3093 57.0801 34.376C56.0348 33.4427 54.6908 32.976 53.0481 32.976C52.0401 32.976 51.1814 33.1067 50.4721 33.368C49.7628 33.6293 48.8668 34.096 47.7841 34.768L44.0881 32.416L45.0961 14.44H65.2561V21.384H52.2081L51.5921 28.048C52.3014 27.7493 52.9548 27.544 53.5521 27.432C54.1494 27.2827 54.8214 27.208 55.5681 27.208C57.6214 27.208 59.5068 27.6 61.2241 28.384C62.9414 29.168 64.3041 30.3813 65.3121 32.024C66.3574 33.6667 66.8801 35.7387 66.8801 38.24C66.8801 40.8907 66.2641 43.1493 65.0321 45.016C63.8374 46.8453 62.2508 48.2453 60.2721 49.216C58.2934 50.1867 56.1468 50.672 53.8321 50.672Z";
                    }
                }
                else if (inputState1==0 && inputState2==1 && inputState3==1 && inputState4==0)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M55.7359 50.672C54.0186 50.672 52.3572 50.336 50.7519 49.664C49.1466 48.992 47.7092 47.9467 46.4399 46.528C45.1706 45.1093 44.1626 43.2987 43.4159 41.096C42.6692 38.856 42.2959 36.168 42.2959 33.032C42.2959 29.7093 42.6879 26.8347 43.4719 24.408C44.2559 21.9813 45.3199 19.984 46.6639 18.416C48.0079 16.848 49.5386 15.6907 51.2559 14.944C52.9732 14.16 54.7839 13.768 56.6879 13.768C59.0399 13.768 61.0746 14.1973 62.7919 15.056C64.5092 15.8773 65.9279 16.8667 67.0479 18.024L62.7359 22.952C62.1012 22.28 61.2799 21.7013 60.2719 21.216C59.2639 20.6933 58.2372 20.432 57.1919 20.432C55.8479 20.432 54.5972 20.824 53.4399 21.608C52.3199 22.392 51.4239 23.7173 50.7519 25.584C50.1172 27.4133 49.7999 29.896 49.7999 33.032C49.7999 35.9813 50.0612 38.296 50.5839 39.976C51.1439 41.6187 51.8719 42.7947 52.7679 43.504C53.6639 44.176 54.5972 44.512 55.5679 44.512C56.3892 44.512 57.1359 44.3067 57.8079 43.896C58.4799 43.4853 59.0212 42.8693 59.4319 42.048C59.8426 41.1893 60.0479 40.088 60.0479 38.744C60.0479 37.4747 59.8426 36.4667 59.4319 35.72C59.0212 34.9733 58.4612 34.432 57.7519 34.096C57.0799 33.76 56.2959 33.592 55.3999 33.592C54.4666 33.592 53.4959 33.872 52.4879 34.432C51.5172 34.992 50.6399 35.9813 49.8559 37.4L49.4639 32.024C50.3972 30.6427 51.5919 29.5973 53.0479 28.888C54.5039 28.1413 55.8106 27.768 56.9679 27.768C58.9839 27.768 60.7946 28.16 62.3999 28.944C64.0052 29.728 65.2559 30.9413 66.1519 32.584C67.0852 34.1893 67.5519 36.2427 67.5519 38.744C67.5519 41.208 67.0106 43.336 65.9279 45.128C64.8826 46.92 63.4639 48.3013 61.6719 49.272C59.8799 50.2053 57.9012 50.672 55.7359 50.672Z";
                    }
                }
                else if (inputState1==1 && inputState2==1 && inputState3==1 && inputState4==0)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M48.3999 49.9999C48.5865 47.0506 48.8852 44.3439 49.2959 41.8799C49.7065 39.3786 50.2852 37.0079 51.0319 34.7679C51.8159 32.5279 52.8052 30.3253 53.9999 28.1599C55.1945 25.9946 56.6692 23.7359 58.4239 21.3839H42.4639V14.4399H67.2159V19.4799C65.1252 22.0186 63.4079 24.3893 62.0639 26.5919C60.7572 28.7946 59.7492 31.0346 59.0399 33.3119C58.3305 35.5893 57.8079 38.0719 57.4719 40.7599C57.1359 43.4479 56.8745 46.5279 56.6879 49.9999H48.3999Z";
                    }
                }
                else if (inputState1==0 && inputState2==0 && inputState3==0 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M54.7281 50.6719C52.3761 50.6719 50.2481 50.2613 48.3441 49.4399C46.4774 48.6186 45.0027 47.4799 43.9201 46.0239C42.8747 44.5306 42.3521 42.8319 42.3521 40.9279C42.3521 39.4719 42.6134 38.2213 43.1361 37.1759C43.6961 36.0933 44.4241 35.1413 45.3201 34.3199C46.2534 33.4986 47.2801 32.7893 48.4001 32.1919V31.9679C47.0561 30.9226 45.9174 29.7279 44.9841 28.3839C44.0881 27.0399 43.6401 25.4346 43.6401 23.5679C43.6401 21.5519 44.1254 19.8159 45.0961 18.3599C46.0667 16.8666 47.4107 15.7279 49.1281 14.9439C50.8454 14.1599 52.8054 13.7679 55.0081 13.7679C58.2934 13.7679 60.9067 14.6639 62.8481 16.4559C64.8267 18.2106 65.8161 20.5626 65.8161 23.5119C65.8161 24.6319 65.5921 25.6959 65.1441 26.7039C64.7334 27.6746 64.1734 28.5519 63.4641 29.3359C62.7547 30.0826 61.9894 30.7359 61.1681 31.2959V31.5199C62.2881 32.1173 63.3147 32.8453 64.2481 33.7039C65.1814 34.5253 65.9281 35.5333 66.4881 36.7279C67.0481 37.8853 67.3281 39.2479 67.3281 40.8159C67.3281 42.6826 66.8054 44.3626 65.7601 45.8559C64.7147 47.3119 63.2401 48.4879 61.3361 49.3839C59.4694 50.2426 57.2667 50.6719 54.7281 50.6719ZM56.9681 29.2799C57.6774 28.4586 58.2187 27.6186 58.5921 26.7599C58.9654 25.9013 59.1521 25.0053 59.1521 24.0719C59.1521 23.1386 58.9841 22.3359 58.6481 21.6639C58.3121 20.9919 57.8081 20.4693 57.1361 20.0959C56.5014 19.6853 55.7361 19.4799 54.8401 19.4799C53.7574 19.4799 52.8241 19.8346 52.0401 20.5439C51.2561 21.2159 50.8641 22.2239 50.8641 23.5679C50.8641 24.5386 51.1254 25.3599 51.6481 26.0319C52.1707 26.7039 52.8801 27.3013 53.7761 27.8239C54.7094 28.3466 55.7734 28.8319 56.9681 29.2799ZM54.8961 44.9599C55.7921 44.9599 56.5947 44.8106 57.3041 44.5119C58.0507 44.1759 58.6294 43.6906 59.0401 43.0559C59.4507 42.3839 59.6561 41.5439 59.6561 40.5359C59.6561 39.4906 59.3387 38.6319 58.7041 37.9599C58.1067 37.2506 57.2294 36.6346 56.0721 36.1119C54.9521 35.5519 53.6454 34.9546 52.1521 34.3199C51.3681 35.0293 50.7147 35.8879 50.1921 36.8959C49.6694 37.8666 49.4081 38.8933 49.4081 39.9759C49.4081 41.0213 49.6507 41.9173 50.1361 42.6639C50.6587 43.4106 51.3307 43.9893 52.1521 44.3999C53.0107 44.7733 53.9254 44.9599 54.8961 44.9599Z";
                    }
                }
                else if (inputState1==1 && inputState2==0 && inputState3==0 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M52.8238 50.6719C50.4718 50.6719 48.4371 50.2426 46.7198 49.3839C45.0025 48.5253 43.5651 47.5359 42.4078 46.4159L46.7758 41.4879C47.3731 42.1599 48.1758 42.7573 49.1838 43.2799C50.2291 43.7653 51.2558 44.0079 52.2638 44.0079C53.6825 44.0079 54.9331 43.6159 56.0158 42.8319C57.1358 42.0479 58.0131 40.7413 58.6478 38.9119C59.3198 37.0453 59.6558 34.5439 59.6558 31.4079C59.6558 28.4586 59.3758 26.1626 58.8158 24.5199C58.2931 22.8399 57.5838 21.6639 56.6878 20.9919C55.8291 20.2826 54.8958 19.9279 53.8878 19.9279C53.1038 19.9279 52.3571 20.1333 51.6478 20.5439C50.9758 20.9546 50.4345 21.5706 50.0238 22.3919C49.6131 23.2133 49.4078 24.3146 49.4078 25.6959C49.4078 26.9279 49.6131 27.9359 50.0238 28.7199C50.4345 29.4666 50.9945 30.0079 51.7038 30.3439C52.4131 30.6799 53.2158 30.8479 54.1118 30.8479C55.0451 30.8479 55.9971 30.5679 56.9678 30.0079C57.9385 29.4106 58.8158 28.4213 59.5998 27.0399L59.9918 32.4159C59.0958 33.7226 57.9198 34.7679 56.4638 35.5519C55.0078 36.2986 53.6825 36.6719 52.4878 36.6719C50.4718 36.6719 48.6611 36.2799 47.0558 35.4959C45.4505 34.7119 44.1811 33.5173 43.2478 31.9119C42.3518 30.2693 41.9038 28.1973 41.9038 25.6959C41.9038 23.2319 42.4451 21.1039 43.5278 19.3119C44.6105 17.5199 46.0478 16.1573 47.8398 15.2239C49.6318 14.2533 51.5918 13.7679 53.7198 13.7679C55.4745 13.7679 57.1545 14.1039 58.7598 14.7759C60.3651 15.4479 61.7838 16.4933 63.0158 17.9119C64.2851 19.3306 65.2931 21.1599 66.0398 23.3999C66.8238 25.6026 67.2158 28.2719 67.2158 31.4079C67.2158 34.7306 66.8051 37.6053 65.9838 40.0319C65.1998 42.4586 64.1358 44.4559 62.7918 46.0239C61.4478 47.5919 59.9171 48.7679 58.1998 49.5519C56.4825 50.2986 54.6905 50.6719 52.8238 50.6719Z";
                    }
                }
                else if (inputState1==0 && inputState2==1 && inputState3==0 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M63.1416 46H58.2988L55.9053 39.2275H45.4404L43.1396 46H38.3154L48.2793 19.3926H53.252L63.1416 46ZM54.7363 35.6279L51.0439 25.0146C50.9326 24.6683 50.8151 24.1117 50.6914 23.3447H50.6172C50.5059 24.0498 50.3822 24.6064 50.2461 25.0146L46.5908 35.6279H54.7363Z";
                    }
                }
                else if (inputState1==1 && inputState2==1 && inputState3==0 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M43.6479 50V13.376H55.3519C57.8906 13.376 60.1306 13.6746 62.072 14.272C64.0506 14.832 65.5999 15.7653 66.7199 17.072C67.8773 18.3786 68.4559 20.152 68.4559 22.392C68.4559 24.2586 68.0079 25.92 67.1119 27.376C66.2533 28.7946 65.0026 29.784 63.36 30.344V30.568C65.5253 31.016 67.224 31.9306 68.4559 33.312C69.7253 34.656 70.3599 36.5413 70.3599 38.968C70.3599 41.4693 69.7439 43.5413 68.5119 45.184C67.3173 46.8266 65.656 48.04 63.528 48.824C61.3999 49.608 58.9919 50 56.3039 50H43.6479ZM50.1439 28.496H54.7919C57.3306 28.496 59.1786 28.0293 60.3359 27.096C61.4933 26.1626 62.072 24.912 62.072 23.344C62.072 21.552 61.4746 20.2826 60.2799 19.536C59.0853 18.7893 57.2933 18.416 54.9039 18.416H50.1439V28.496ZM50.1439 44.96H55.632C58.32 44.96 60.3919 44.4746 61.8479 43.504C63.304 42.496 64.0319 40.9653 64.0319 38.912C64.0319 36.9706 63.3226 35.5706 61.9039 34.712C60.4853 33.816 58.3946 33.368 55.632 33.368H50.1439V44.96Z";
                    }
                }
                else if (inputState1==0 && inputState2==0 && inputState3==1 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M58.3759 50.672C55.2772 50.672 52.4586 49.944 49.9199 48.488C47.4186 47.032 45.4212 44.904 43.9279 42.104C42.4719 39.2666 41.7439 35.832 41.7439 31.8C41.7439 28.8133 42.1732 26.144 43.0319 23.792C43.8906 21.4026 45.0852 19.3866 46.6159 17.744C48.1839 16.1013 49.9759 14.8506 51.9919 13.992C54.0079 13.1333 56.1732 12.704 58.4879 12.704C60.8026 12.704 62.8559 13.1706 64.6479 14.104C66.4399 15 67.9146 16.0826 69.0719 17.352L65.5999 21.328C64.6666 20.3573 63.6212 19.6293 62.4639 19.144C61.3439 18.6213 60.0746 18.36 58.6559 18.36C56.7146 18.36 54.9599 18.9013 53.3919 19.984C51.8612 21.0666 50.6479 22.5973 49.7519 24.576C48.8559 26.5546 48.4079 28.9066 48.4079 31.632C48.4079 34.432 48.8372 36.84 49.6959 38.856C50.5546 40.8346 51.7679 42.3653 53.3359 43.448C54.9039 44.4933 56.7332 45.016 58.8239 45.016C60.3919 45.016 61.7919 44.6986 63.0239 44.064C64.2559 43.4293 65.3759 42.5893 66.3839 41.544L69.6879 45.408C68.1946 47.1253 66.5146 48.432 64.6479 49.328C62.7812 50.224 60.6906 50.672 58.3759 50.672Z";
                    }
                }
                else if (inputState1==1 && inputState2==0 && inputState3==1 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M43.6479 50V13.376H53.448C57.1813 13.376 60.3733 14.048 63.024 15.392C65.6746 16.736 67.7093 18.752 69.128 21.44C70.5466 24.128 71.2559 27.488 71.2559 31.52C71.2559 35.552 70.5466 38.9493 69.128 41.712C67.7466 44.4373 65.7493 46.5093 63.136 47.928C60.5226 49.3093 57.4053 50 53.7839 50H43.6479ZM50.1439 44.736H52.9999C55.4639 44.736 57.5546 44.2693 59.2719 43.336C61.0266 42.3653 62.3519 40.9093 63.2479 38.968C64.1439 37.0266 64.5919 34.544 64.5919 31.52C64.5919 28.5333 64.1439 26.088 63.2479 24.184C62.3519 22.28 61.0266 20.88 59.2719 19.984C57.5546 19.088 55.4639 18.64 52.9999 18.64H50.1439V44.736Z";
                    }
                }
                else if (inputState1==0 && inputState2==1 && inputState3==1 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M43.6479 50V13.376H65.712V18.864H50.1439V28.216H63.3039V33.704H50.1439V44.512H66.2719V50H43.6479Z";
                    }
                }
                else if (inputState1==1 && inputState2==1 && inputState3==1 && inputState4==1)
                {
                    if (node.Shape is PathShape element)
                    {
                        (element as PathShape).Data="M28 0H80V65H28V56H12.9236C12.4425 59.1151 9.74979 61.5 6.5 61.5C2.91015 61.5 0 58.5899 0 55C0 51.4101 2.91015 48.5 6.5 48.5C9.74979 48.5 12.4425 50.8849 12.9236 54H28V41H12.9236C12.4425 44.1151 9.74979 46.5 6.5 46.5C2.91015 46.5 0 43.5899 0 40C0 36.4101 2.91015 33.5 6.5 33.5C9.74979 33.5 12.4425 35.8849 12.9236 39H28V26H12.9236C12.4425 29.1151 9.74979 31.5 6.5 31.5C2.91015 31.5 0 28.5899 0 25C0 21.4101 2.91015 18.5 6.5 18.5C9.74979 18.5 12.4425 20.8849 12.9236 24H28V11H12.9236C12.4425 14.1151 9.74979 16.5 6.5 16.5C2.91015 16.5 0 13.5899 0 10C0 6.41015 2.91015 3.5 6.5 3.5C9.74979 3.5 12.4425 5.88491 12.9236 9H28V0ZM78 2H30V63H78V2ZM6.5 15.5C9.53757 15.5 12 13.0376 12 10C12 6.96243 9.53757 4.5 6.5 4.5C3.46243 4.5 1 6.96243 1 10C1 13.0376 3.46243 15.5 6.5 15.5ZM6.5 30.5C9.53757 30.5 12 28.0376 12 25C12 21.9624 9.53757 19.5 6.5 19.5C3.46243 19.5 1 21.9624 1 25C1 28.0376 3.46243 30.5 6.5 30.5ZM6.5 45.5C9.53757 45.5 12 43.0376 12 40C12 36.9624 9.53757 34.5 6.5 34.5C3.46243 34.5 1 36.9624 1 40C1 43.0376 3.46243 45.5 6.5 45.5ZM6.5 60.5C9.53757 60.5 12 58.0376 12 55C12 51.9624 9.53757 49.5 6.5 49.5C3.46243 49.5 1 51.9624 1 55C1 58.0376 3.46243 60.5 6.5 60.5Z M43.6479 50V13.376H65.768V18.864H50.1439V29.224H63.4719V34.712H50.1439V50H43.6479Z";
                    }
                }
            }
        }
        /// <summary>
        /// This method is invoked when click event performed on switch node.
        /// </summary>
        /// <param name="node"></param>
        private void OnInputChanged(Node node)
        {
            if(node.ID.IndexOf("Switch") != -1 && node is NodeGroup grpNode)
            {
                if(grpNode.AdditionalInfo != null)
                {
                    int state = 0;
                    if (grpNode.AdditionalInfo.Count==0)
                    {
                        Node? child = Diagram.GetObject(grpNode.Children[0]) as Node;
                        if(child != null && child.AdditionalInfo.Count>0)
                        {
                            state = (int)child.AdditionalInfo["BinaryState"];
                        }
                    }
                    else
                        state = (int)grpNode.AdditionalInfo["BinaryState"];
                    if (state==1)
                    {
                        if (grpNode.AdditionalInfo.Count==0)
                        {
                            Node? child = Diagram.GetObject(grpNode.Children[0]) as Node;
                            if (child != null && child.AdditionalInfo.Count>0)
                            {
                                child.AdditionalInfo["BinaryState"] = 0;
                            }
                        }
                        else
                            grpNode.AdditionalInfo["BinaryState"]=0;
                        Node? child3 = Diagram.GetObject(grpNode.Children[3]) as Node;
                        Node? child2 = Diagram.GetObject(grpNode.Children[2]) as Node;
                        Node? child1 = Diagram.GetObject(grpNode.Children[1]) as Node;

                        if(child1 != null && child2!=null && child3!=null)
                        {
                            child3.IsVisible=false;
                            child2.IsVisible=true;
                            child1.Style=new ShapeStyle()
                            {
                                Fill="white"
                            };
                        }
                        
                    }
                    else if(state==0)
                    {
                        if (grpNode.AdditionalInfo.Count==0)
                        {
                            Node? child = Diagram.GetObject(grpNode.Children[0]) as Node;
                            if (child != null && child.AdditionalInfo.Count>0)
                            {
                                child.AdditionalInfo["BinaryState"] = 1;
                            }
                        }
                        else
                            grpNode.AdditionalInfo["BinaryState"]=1;
                        Node? child3 = Diagram.GetObject(grpNode.Children[3]) as Node;
                        Node? child2 = Diagram.GetObject(grpNode.Children[2]) as Node;
                        Node? child1 = Diagram.GetObject(grpNode.Children[1]) as Node;

                        if(child1!=null && child2!=null && child3!=null)
                        {
                            child3.IsVisible=true;
                            child2.IsVisible=false;
                            child3.Style=new ShapeStyle()
                            {
                                Fill="white",
                                StrokeColor="black",
                                StrokeWidth=2
                            };
                            child1.Style=new ShapeStyle()
                            {
                                Fill="#05DAC5"
                            };
                        }
                    }
                }
            }
            
            RunSimulation();
        }
        /// <summary>
        /// This method is used to get the interger value from the JSON data.
        /// </summary>
        private int GetIntegerFromJSONData(NodeBase connectorObj)
        {
            int num = 0;
            if (connectorObj.AdditionalInfo.TryGetValue("BinaryState", out var binaryStateObj))
            {
                if (binaryStateObj is JsonElement binaryStateElement && binaryStateElement.ValueKind == JsonValueKind.Number)
                {
                    num = binaryStateElement.GetInt32();
                }
                else
                {
                    num = (int)connectorObj.AdditionalInfo["BinaryState"];
                }
            }
            return num;
        }
        /// <summary>
        /// Invoke when the click action is performed on the diagram.
        /// </summary>
        /// <param name="args">The event arguments containing information about the click action.</param>
        private async void OnClick(ClickEventArgs args)
        {
            if(args !=null && IsCollectionChangeCompleted)
            {
                string button = args.Button.ToString();
                if(button == "Left")
                {
                    Diagram.BeginUpdate();
                    if (args.Element is Node clickedNode && clickedNode != null)
                    {
                        foreach (Node node1 in Diagram.Nodes)
                        {
                            if (node1 is NodeGroup grpNode && (grpNode.ID == clickedNode.ID || grpNode.Children.Contains(clickedNode.ID)))
                            {
                                OnInputChanged(grpNode);
                            }
                        }
                    }
                    else if(args.Element is DiagramSelectionSettings selectedNode && selectedNode.Nodes.Count>0)
                    {
                        if (selectedNode.Nodes[0].ID.IndexOf("Switch") != -1)
                        {
                            OnInputChanged(selectedNode.Nodes[0]);
                        }
                    }
                    if (args.Element is Node node && node.ID.IndexOf("Clock") != -1)
                    {
                        Parent.Toolbar.numerictext = "block";
                        Parent.Toolbar.StateChanged();
                    }
                    else if (args.Element is DiagramSelectionSettings selectedNode && selectedNode.Nodes.Count > 0)
                    {
                        if (selectedNode.Nodes[0].ID.IndexOf("Clock") != -1)
                        {
                            Parent.Toolbar.numerictext = "block";
                            Parent.Toolbar.StateChanged();
                        }
                    }
                    else if (Parent.Toolbar.numerictext != "none")
                    {
                        Parent.Toolbar.numerictext = "none";
                        Parent.Toolbar.StateChanged();
                    }
                    await Diagram.EndUpdate();
                }
            }
            
        }
    }
}
