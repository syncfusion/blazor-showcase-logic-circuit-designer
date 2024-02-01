using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Diagram;
using Syncfusion.Blazor.Diagram.SymbolPalette;
using System.Xml.Linq;
using System.Reflection.Emit;
using Syncfusion.Blazor.PivotView;

namespace LogicCircuit
{
    public partial class DiagramSymbolpalette
    {
        /// <summary>
        /// This method used to render the Symbols in the Symbol Palette
        /// </summary>
        private void InitializePalettes()
        {
            InputSymbols = new DiagramObjectCollection<NodeBase>();
            Node switchOuter = new Node()
            {
                ID = "Outer Switch",
                Width = 78,
                Height = 60,
                OffsetX=140.5,
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
                Constraints = NodeConstraints.Default &~NodeConstraints.InConnect
            };
            InputSymbols.Add(switchOuter);
            Node switchInner = new Node()
            {
                ID = "Inner Switch",
                Width = 40,
                Height = 49,
                OffsetX=125.5,
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
            InputSymbols.Add(switchInner);

            Node switchOff = new Node()
            {
                ID = "SwitchOff",
                Width = 20,
                Height = 30,
                OffsetX=125.5,
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
            InputSymbols.Add(switchOff);
            Node switchOn = new Node()
            {
                ID = "SwitchOn",
                Width = 20,
                Height = 25,
                OffsetX=127.5,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            InputSymbols.Add(switchOn);
            NodeGroup groupNode = new NodeGroup();
            groupNode.ID="ToggleSwitch";
            
            groupNode.Children = new string[] { "Outer Switch", "Inner Switch", "SwitchOff", "SwitchOn" };
            groupNode.Tooltip= new DiagramTooltip()
            {
                Content="Toggle Switch"
            };
            groupNode.Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip )&~NodeConstraints.InConnect;
            InputSymbols.Add(groupNode);

            Node pushButtonOuterRect = new Node()
            {
                ID = "OuterRect PushBtn",
                Width = 68,
                Height = 50,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            InputSymbols.Add(pushButtonOuterRect);
            Node transparentnode = new Node()
            {
                ID = "transparentNode",
                Width = 51,
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
            InputSymbols.Add(transparentnode);
            Node pushBtnInner = new Node()
            {
                ID = "InnerCircle pushBtn",
                Width = 19,
                Height = 20,
                OffsetX=128,
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
            InputSymbols.Add(pushBtnInner);

            Node pushBtnOuter = new Node()
            {
                ID = "OuterCircle PushBtn",
                Width = 27,
                Height = 27,
                OffsetX=128,
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
            InputSymbols.Add(pushBtnOuter);
            
            NodeGroup PushButton = new NodeGroup();
            PushButton.ID ="PushButton";
            PushButton.Children = new string[] {"transparentNode","OuterRect PushBtn", "OuterCircle PushBtn", "InnerCircle pushBtn" };
            PushButton.Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect;
            PushButton.Tooltip=new DiagramTooltip()
            {
                Content = "Push Button"
            };
            InputSymbols.Add(PushButton);

            Node ClkOuterRect = new Node()
            {
                ID = "clkOuterRect",
                Width = 65,
                Height = 35,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            InputSymbols.Add(ClkOuterRect);
            Node ClkInnerPart = new Node()
            {
                ID = "clkInnerPart",
                Width = 30,
                Height = 20,
                OffsetX=130,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            InputSymbols.Add(ClkInnerPart);
            NodeGroup ClkButton = new NodeGroup();
            ClkButton.ID ="Clock";
            ClkButton.Children = new string[] { "clkOuterRect", "clkInnerPart" };
            
            ClkButton.Constraints = NodeConstraints.Default &~NodeConstraints.InConnect;
            InputSymbols.Add(ClkButton);

            Node HighConstant = new Node()
            {
                ID = "HighConstant",
                Width = 78,
                Height = 55,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M50 0H0V44H50V23H60.083C60.559 25.8377 63.027 28 66 28C69.3137 28 72 25.3137 72 22C72 18.6863 69.3137 16 66 16C63.027 16 60.559 18.1623 60.083 21H50V0ZM2 42V2H48V42H2ZM62 22C62 19.7909 63.7909 18 66 18C68.2091 18 70 19.7909 70 22C70 24.2091 68.2091 26 66 26C63.7909 26 62 24.2091 62 22ZM20.2759 16.728C21.0972 16.5414 21.6666 16.392 21.9839 16.28V28.068L18.6239 28.348V30H29.2919V28.348L26.0999 28.04V12.64L23.8879 12.332H23.8599C23.1692 12.9107 22.2732 13.4707 21.1719 14.012C20.0892 14.5347 19.0439 14.8987 18.0359 15.104L18.3439 17.064C18.8106 17.008 19.4546 16.896 20.2759 16.728Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Tooltip= new DiagramTooltip()
                {
                    Content="High Constant"
                },
            Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            InputSymbols.Add(HighConstant);

            Node LowConstant = new Node()
            {
                ID = "LowConstant",
                Width = 75,
                Height = 55,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M50 0H0V44H50V23H60.083C60.559 25.8377 63.027 28 66 28C69.3137 28 72 25.3137 72 22C72 18.6863 69.3137 16 66 16C63.027 16 60.559 18.1623 60.083 21H50V0ZM2 42V2H48V42H2ZM62 22C62 19.7909 63.7909 18 66 18C68.2091 18 70 19.7909 70 22C70 24.2091 68.2091 26 66 26C63.7909 26 62 24.2091 62 22ZM18.1081 29.452C19.6201 31.356 21.8974 32.308 24.9401 32.308C26.8254 32.308 28.4961 31.8506 29.9521 30.936C31.4081 30.0213 32.5374 28.7333 33.3401 27.072C34.1614 25.392 34.5721 23.4693 34.5721 21.304C34.5721 18.0373 33.7974 15.4986 32.2481 13.688C30.7174 11.8586 28.4121 10.944 25.3321 10.944C23.4654 10.944 21.8134 11.3826 20.3761 12.26C18.9388 13.1373 17.8188 14.388 17.0161 16.012C16.2321 17.636 15.8401 19.5213 15.8401 21.668C15.8401 24.9533 16.5961 27.548 18.1081 29.452ZM21.6361 14.92C22.4948 13.408 23.6894 12.652 25.2201 12.652C26.7694 12.652 27.9548 13.3706 28.7761 14.808C29.5974 16.2266 30.0081 18.392 30.0081 21.304C30.0081 24.3093 29.5788 26.6146 28.7201 28.22C27.8614 29.8066 26.6761 30.6 25.1641 30.6C23.6148 30.6 22.4201 29.844 21.5801 28.332C20.7588 26.8013 20.3481 24.5706 20.3481 21.64C20.3481 18.672 20.7774 16.432 21.6361 14.92Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Tooltip= new DiagramTooltip()
                {
                    Content="Low Constant"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            InputSymbols.Add(LowConstant);

            Node BulpFullPath = new Node()
            {
                ID = "FullPath",
                Width = 30,
                Height = 40,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            OutputSymbols.Add(BulpFullPath);
            Node BulpBlackPart = new Node()
            {
                ID = "BulpBlackpart",
                Width = 14,
                Height = 8,
                OffsetX=140,
                OffsetY=108,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            OutputSymbols.Add(BulpBlackPart);

            Node InnerBulpPart = new Node()
            {
                ID = "InnerBulpPart",
                Width = 14,
                Height = 10,
                OffsetX=140,
                OffsetY=97,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            OutputSymbols.Add(InnerBulpPart);
            Node OuterBulpPart = new Node()
            {
                ID = "OuterBulpPart",
                Width = 32,
                Height = 28,
                OffsetX=140,
                OffsetY=90,
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
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select &~NodeConstraints.InConnect
            };
            OutputSymbols.Add(OuterBulpPart);
            NodeGroup Bulp = new NodeGroup();
            Bulp.ID="Bulb";
            Bulp.Children = new string[] { "FullPath", "BulpBlackpart", "InnerBulpPart", "OuterBulpPart" };
            Bulp.Constraints = NodeConstraints.Default & ~NodeConstraints.InConnect;
            Bulp.Style=new ShapeStyle()
            {
                Fill="none"
            };
            OutputSymbols.Add(Bulp);

            Node FourBitDigit = new Node()
            {
                ID = "BitDigit",
                Width = 70,
                Height = 55,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M24 1V0C23.4477 0 23 0.447715 23 1L24 1ZM72 1H73C73 0.447715 72.5523 0 72 0V1ZM72 65V66C72.5523 66 73 65.5523 73 65H72ZM24 65H23C23 65.5523 23.4477 66 24 66V65ZM10 9C10 11.2091 8.20914 13 6 13V15C9.31371 15 12 12.3137 12 9H10ZM6 13C3.79086 13 2 11.2091 2 9H0C0 12.3137 2.68629 15 6 15V13ZM2 9C2 6.79086 3.79086 5 6 5V3C2.68629 3 0 5.68629 0 9H2ZM6 5C8.20914 5 10 6.79086 10 9H12C12 5.68629 9.31371 3 6 3V5ZM10 25C10 27.2091 8.20914 29 6 29V31C9.31371 31 12 28.3137 12 25H10ZM6 29C3.79086 29 2 27.2091 2 25H0C0 28.3137 2.68629 31 6 31V29ZM2 25C2 22.7909 3.79086 21 6 21V19C2.68629 19 0 21.6863 0 25H2ZM6 21C8.20914 21 10 22.7909 10 25H12C12 21.6863 9.31371 19 6 19V21ZM10 41C10 43.2091 8.20914 45 6 45V47C9.31371 47 12 44.3137 12 41H10ZM6 45C3.79086 45 2 43.2091 2 41H0C0 44.3137 2.68629 47 6 47V45ZM2 41C2 38.7909 3.79086 37 6 37V35C2.68629 35 0 37.6863 0 41H2ZM6 37C8.20914 37 10 38.7909 10 41H12C12 37.6863 9.31371 35 6 35V37ZM10 57C10 59.2091 8.20914 61 6 61V63C9.31371 63 12 60.3137 12 57H10ZM6 61C3.79086 61 2 59.2091 2 57H0C0 60.3137 2.68629 63 6 63V61ZM2 57C2 54.7909 3.79086 53 6 53V51C2.68629 51 0 53.6863 0 57H2ZM6 53C8.20914 53 10 54.7909 10 57H12C12 53.6863 9.31371 51 6 51V53ZM11 26H24V24H11V26ZM11 10H24V8H11V10ZM11 42H24V40H11V42ZM11 58H24V56H11V58ZM24 2H72V0H24V2ZM71 1V65H73V1H71ZM72 64H24V66H72V64ZM25 9V1H23V9H25ZM25 25V9H23V25H25ZM25 41V25H23V41H25ZM25 65V57H23V65H25ZM25 57V41H23V57H25Z M36 11H60L56 15H40L36 11Z M36 54H60L56 50H40L36 54Z M39 33L41 31H55L57 33L55 35H41L39 33Z M36 30V13L40 17V30L38 32L36 30Z M36 36V52L40 48V36L38 34L36 36Z M60 30V13L56 17V30L58 32L60 30Z M60 36V52L56 48V36L58 34L60 36Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000"
                },
                Tooltip= new DiagramTooltip()
                {
                    Content="4-Bit Digit"
                },
            Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~ NodeConstraints.InConnect
            };
            OutputSymbols.Add(FourBitDigit);

            Node OrGate = new Node()
            {
                ID = "ORGate",
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
                Width = 79,
                Height = 45,
                Tooltip=new DiagramTooltip()
                {
                    Content = "OR Gate"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~ NodeConstraints.InConnect
            };
            LogicSymbols.Add(OrGate);
            Node NorGate = new Node()
            {
                ID = "NORGate",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M30.5076 11L31.4744 10.7444L30.5076 11ZM30.7565 29L31.7303 29.2276L30.7565 29ZM26 1L26.0345 0.000594001C25.6575 -0.0124037 25.3053 0.187777 25.1237 0.518291C24.942 0.848804 24.9617 1.25342 25.1747 1.56469L26 1ZM26 40L25.1747 39.4353C24.9617 39.7466 24.942 40.1512 25.1237 40.4817C25.3053 40.8122 25.6575 41.0124 26.0345 40.9994L26 40ZM63.3762 29.3095L64.1636 29.926L64.1636 29.926L63.3762 29.3095ZM42.21 39.441L42.2445 40.4404L42.2445 40.4404L42.21 39.441ZM42.6846 1.57533L42.719 0.575923L42.6846 1.57533ZM63.3088 11.1159L64.0806 10.48L63.3088 11.1159ZM10 11C10 13.2091 8.20914 15 6 15V17C9.31371 17 12 14.3137 12 11H10ZM6 15C3.79086 15 2 13.2091 2 11H0C0 14.3137 2.68629 17 6 17V15ZM2 11C2 8.79086 3.79086 7 6 7V5C2.68629 5 0 7.68629 0 11H2ZM6 7C8.20914 7 10 8.79086 10 11H12C12 7.68629 9.31371 5 6 5V7ZM10 29C10 31.2091 8.20914 33 6 33V35C9.31371 35 12 32.3137 12 29H10ZM6 33C3.79086 33 2 31.2091 2 29H0C0 32.3137 2.68629 35 6 35V33ZM2 29C2 26.7909 3.79086 25 6 25V23C2.68629 23 0 25.6863 0 29H2ZM6 25C8.20914 25 10 26.7909 10 29H12C12 25.6863 9.31371 23 6 23V25ZM98 20C98 22.2091 96.2091 24 94 24V26C97.3137 26 100 23.3137 100 20H98ZM94 24C91.7909 24 90 22.2091 90 20H88C88 23.3137 90.6863 26 94 26V24ZM90 20C90 17.7909 91.7909 16 94 16V14C90.6863 14 88 16.6863 88 20H90ZM94 16C96.2091 16 98 17.7909 98 20H100C100 16.6863 97.3137 14 94 14V16ZM73 20C73 21.1046 72.1046 22 71 22V24C73.2091 24 75 22.2091 75 20H73ZM71 22C69.8954 22 69 21.1046 69 20H67C67 22.2091 68.7909 24 71 24V22ZM69 20C69 18.8954 69.8954 18 71 18V16C68.7909 16 67 17.7909 67 20H69ZM71 18C72.1046 18 73 18.8954 73 20H75C75 17.7909 73.2091 16 71 16V18ZM74 21H89V19H74V21ZM11 12H30.5076V10H11V12ZM11 30H30.7565V28H11V30ZM25.9655 1.99941L42.6501 2.57474L42.719 0.575923L26.0345 0.000594001L25.9655 1.99941ZM62.537 11.7518C65.4698 15.3112 66.7403 17.7536 66.7499 20.0979C66.7597 22.4494 65.5031 24.9712 62.5889 28.6929L64.1636 29.926C67.1034 26.1714 68.7627 23.1761 68.7499 20.0897C68.7371 16.9961 67.0468 14.0799 64.0806 10.48L62.537 11.7518ZM42.1755 38.4416L25.9655 39.0006L26.0345 40.9994L42.2445 40.4404L42.1755 38.4416ZM62.5889 28.6929C57.7614 34.8581 50.1044 38.1682 42.1755 38.4416L42.2445 40.4404C50.6565 40.1504 58.9106 36.6345 64.1636 29.926L62.5889 28.6929ZM42.6501 2.57474C50.3091 2.83884 57.7311 5.91909 62.537 11.7518L64.0806 10.48C58.8454 4.12625 50.8409 0.855987 42.719 0.575923L42.6501 2.57474ZM31.4744 10.7444C30.499 7.05472 28.9517 3.54311 26.8253 0.435311L25.1747 1.56469C27.1606 4.46711 28.6183 7.76579 29.5408 11.2556L31.4744 10.7444ZM26.8253 40.5647C29.1482 37.1698 30.7801 33.2928 31.7303 29.2276L29.7828 28.7724C28.8833 32.6208 27.3442 36.2645 25.1747 39.4353L26.8253 40.5647ZM31.7303 29.2276C33.1399 23.1963 33.055 16.7243 31.4744 10.7444L29.5408 11.2556C31.0374 16.9173 31.1184 23.0575 29.7828 28.7724L31.7303 29.2276Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Width = 79,
                Height = 45,
                Tooltip= new DiagramTooltip()
                {
                    Content="NOR Gate"
                },
            Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            LogicSymbols.Add(NorGate);
            Node AndGate = new Node()
            {
                ID = "ANDGate",
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
                Width = 79,
                Height = 45,
                Tooltip= new DiagramTooltip()
                {
                    Content="AND Gate"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip)&~NodeConstraints.InConnect
            };
            LogicSymbols.Add(AndGate);
            Node NandGate = new Node()
            {
                ID = "NANDGate",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M27 1V0C26.4477 0 26 0.447715 26 1L27 1ZM27 40H26C26 40.5523 26.4477 41 27 41V40ZM70 17V16H69V17H70ZM76 17H77V16H76V17ZM76 23V24H77V23H76ZM70 23H69V24H70V23ZM10 11C10 13.2091 8.20914 15 6 15V17C9.31371 17 12 14.3137 12 11H10ZM6 15C3.79086 15 2 13.2091 2 11H0C0 14.3137 2.68629 17 6 17V15ZM2 11C2 8.79086 3.79086 7 6 7V5C2.68629 5 0 7.68629 0 11H2ZM6 7C8.20914 7 10 8.79086 10 11H12C12 7.68629 9.31371 5 6 5V7ZM10 29C10 31.2091 8.20914 33 6 33V35C9.31371 35 12 32.3137 12 29H10ZM6 33C3.79086 33 2 31.2091 2 29H0C0 32.3137 2.68629 35 6 35V33ZM2 29C2 26.7909 3.79086 25 6 25V23C2.68629 23 0 25.6863 0 29H2ZM6 25C8.20914 25 10 26.7909 10 29H12C12 25.6863 9.31371 23 6 23V25ZM98 20C98 22.2091 96.2091 24 94 24V26C97.3137 26 100 23.3137 100 20H98ZM94 24C91.7909 24 90 22.2091 90 20H88C88 23.3137 90.6863 26 94 26V24ZM90 20C90 17.7909 91.7909 16 94 16V14C90.6863 14 88 16.6863 88 20H90ZM94 16C96.2091 16 98 17.7909 98 20H100C100 16.6863 97.3137 14 94 14V16ZM11 12H27V10H11V12ZM11.5 30H27V28H11.5V30ZM27 2H49.5V0H27V2ZM49.5 39H27V41H49.5V39ZM68 20.5C68 30.7173 59.7173 39 49.5 39V41C60.8218 41 70 31.8218 70 20.5H68ZM49.5 2C59.7173 2 68 10.2827 68 20.5H70C70 9.17816 60.8218 0 49.5 0V2ZM70 18H76V16H70V18ZM76 22H70V24H76V22ZM71 23V17H69V23H71ZM28 40V29H26V40H28ZM28 29V11H26V29H28ZM28 11V1H26V11H28ZM75 17V20H77V17H75ZM75 20V23H77V20H75ZM76 21H89V19H76V21Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Width = 79,
                Height = 45,
                Tooltip= new DiagramTooltip()
                {
                    Content="NAND Gate"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            LogicSymbols.Add(NandGate);
            Node BufferGate = new Node()
            {
                ID = "Buffer",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M30 1L30.4122 0.0888921C30.1027 -0.0511131 29.7433 -0.0244422 29.4579 0.159712C29.1724 0.343866 29 0.66032 29 1H30ZM30 40H29C29 40.3432 29.176 40.6623 29.4661 40.8456C29.7563 41.0288 30.1201 41.0504 30.4299 40.9029L30 40ZM10 20C10 22.2091 8.20914 24 6 24V26C9.31371 26 12 23.3137 12 20H10ZM6 24C3.79086 24 2 22.2091 2 20H0C0 23.3137 2.68629 26 6 26V24ZM2 20C2 17.7909 3.79086 16 6 16V14C2.68629 14 0 16.6863 0 20H2ZM6 16C8.20914 16 10 17.7909 10 20H12C12 16.6863 9.31371 14 6 14V16ZM98 20C98 22.2091 96.2091 24 94 24V26C97.3137 26 100 23.3137 100 20H98ZM94 24C91.7909 24 90 22.2091 90 20H88C88 23.3137 90.6863 26 94 26V24ZM90 20C90 17.7909 91.7909 16 94 16V14C90.6863 14 88 16.6863 88 20H90ZM94 16C96.2091 16 98 17.7909 98 20H100C100 16.6863 97.3137 14 94 14V16ZM72 21H89V19H72V21ZM30.4299 40.9029L72.4299 20.9029L71.5701 19.0971L29.5701 39.0971L30.4299 40.9029ZM72.4122 19.0889L30.4122 0.0888921L29.5878 1.91111L71.5878 20.9111L72.4122 19.0889ZM11 21H30V19H11V21ZM29 1V20H31V1H29ZM29 20V40H31V20H29Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Width = 79,
                Height = 45,
                Constraints = NodeConstraints.Default &~NodeConstraints.InConnect
            };
            LogicSymbols.Add(BufferGate);
            Node NotGate = new Node()
            {
                ID = "NOTGate",
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
                Width = 79,
                Height = 45,
                Tooltip= new DiagramTooltip()
                {
                    Content="NOT Gate"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            LogicSymbols.Add(NotGate);
            Node XorGate = new Node()
            {
                ID = "XORGate",
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
                Tooltip= new DiagramTooltip()
                {
                    Content="XOR Gate"
                },
                Width = 79,
                Height = 45,
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            LogicSymbols.Add(XorGate);
            Node XNorGate = new Node()
            {
                ID = "XNORGate",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M27 2L27.0345 1.00059L25.0579 0.932436L26.1747 2.56469L27 2ZM27 41L26.1747 40.4353L25.0579 42.0676L27.0345 41.9994L27 41ZM64.3762 30.3095L65.1636 30.926L65.1636 30.926L64.3762 30.3095ZM43.21 40.441L43.2445 41.4404L43.2445 41.4404L43.21 40.441ZM43.6846 2.57533L43.719 1.57592L43.6846 2.57533ZM64.3088 12.1159L65.0806 11.48L64.3088 12.1159ZM69 18V17H68V18H69ZM75 18H76V17H75V18ZM75 24V25H76V24H75ZM69 24H68V25H69V24ZM10 12C10 14.2091 8.20914 16 6 16V18C9.31371 18 12 15.3137 12 12H10ZM6 16C3.79086 16 2 14.2091 2 12H0C0 15.3137 2.68629 18 6 18V16ZM2 12C2 9.79086 3.79086 8 6 8V6C2.68629 6 0 8.68629 0 12H2ZM6 8C8.20914 8 10 9.79086 10 12H12C12 8.68629 9.31371 6 6 6V8ZM10 30C10 32.2091 8.20914 34 6 34V36C9.31371 36 12 33.3137 12 30H10ZM6 34C3.79086 34 2 32.2091 2 30H0C0 33.3137 2.68629 36 6 36V34ZM2 30C2 27.7909 3.79086 26 6 26V24C2.68629 24 0 26.6863 0 30H2ZM6 26C8.20914 26 10 27.7909 10 30H12C12 26.6863 9.31371 24 6 24V26ZM98 21C98 23.2091 96.2091 25 94 25V27C97.3137 27 100 24.3137 100 21H98ZM94 25C91.7909 25 90 23.2091 90 21H88C88 24.3137 90.6863 27 94 27V25ZM90 21C90 18.7909 91.7909 17 94 17V15C90.6863 15 88 17.6863 88 21H90ZM94 17C96.2091 17 98 18.7909 98 21H100C100 17.6863 97.3137 15 94 15V17ZM74.5 22H89V20H74.5V22ZM11 13H31V11H11V13ZM11 31H31V29H11V31ZM20.8253 41.5647C28.6947 30.0635 28.6947 12.9365 20.8253 1.43531L19.1747 2.56469C26.5782 13.385 26.5782 29.615 19.1747 40.4353L20.8253 41.5647ZM26.9655 2.99941L43.6501 3.57474L43.719 1.57592L27.0345 1.00059L26.9655 2.99941ZM63.537 12.7518C66.4698 16.3112 67.7403 18.7536 67.7499 21.0979C67.7597 23.4494 66.5031 25.9712 63.5889 29.6929L65.1636 30.926C68.1034 27.1714 69.7627 24.1761 69.7499 21.0897C69.7371 17.9961 68.0468 15.0799 65.0806 11.48L63.537 12.7518ZM43.1755 39.4416L26.9655 40.0006L27.0345 41.9994L43.2445 41.4404L43.1755 39.4416ZM27.8253 41.5647C35.6947 30.0635 35.6947 12.9365 27.8253 1.43531L26.1747 2.56469C33.5782 13.385 33.5782 29.615 26.1747 40.4353L27.8253 41.5647ZM63.5889 29.6929C58.7614 35.8581 51.1044 39.1682 43.1755 39.4416L43.2445 41.4404C51.6565 41.1504 59.9106 37.6345 65.1636 30.926L63.5889 29.6929ZM43.6501 3.57474C51.3091 3.83884 58.7311 6.91909 63.537 12.7518L65.0806 11.48C59.8454 5.12625 51.8409 1.85599 43.719 1.57592L43.6501 3.57474ZM69 19H75V17H69V19ZM74 18V24H76V18H74ZM75 23H69V25H75V23ZM70 24V18H68V24H70Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Tooltip= new DiagramTooltip()
                {
                    Content="XNOR Gate"
                },
                Width = 79,
                Height = 45,
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            LogicSymbols.Add(XNorGate);

            Node JKFlipFlop = new Node()
            {
                ID = "JKFlip-Flop",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M47 6C47 3.79086 48.7909 2 51 2C53.2091 2 55 3.79086 55 6C55 8.20914 53.2091 10 51 10C48.7909 10 47 8.20914 47 6ZM51 0C47.6863 0 45 2.68629 45 6C45 8.973 47.1623 11.441 50 11.917V19H23C22.4477 19 22 19.4477 22 20V35H11.917C11.441 32.1623 8.973 30 6 30C2.68629 30 0 32.6863 0 36C0 39.3137 2.68629 42 6 42C8.973 42 11.441 39.8377 11.917 37H22V51H11.917C11.441 48.1623 8.973 46 6 46C2.68629 46 0 48.6863 0 52C0 55.3137 2.68629 58 6 58C8.973 58 11.441 55.8377 11.917 53H22V67H11.917C11.441 64.1623 8.973 62 6 62C2.68629 62 0 64.6863 0 68C0 71.3137 2.68629 74 6 74C8.973 74 11.441 71.8377 11.917 69H22V84C22 84.5523 22.4477 85 23 85H50V92.583C47.1623 93.059 45 95.527 45 98.5C45 101.814 47.6863 104.5 51 104.5C54.3137 104.5 57 101.814 57 98.5C57 95.527 54.8377 93.059 52 92.583V85H79C79.5523 85 80 84.5523 80 84V62H90.083C90.559 64.8377 93.027 67 96 67C99.3137 67 102 64.3137 102 61C102 57.6863 99.3137 55 96 55C93.027 55 90.559 57.1623 90.083 60H80V46H90.083C90.559 48.8377 93.027 51 96 51C99.3137 51 102 48.3137 102 45C102 41.6863 99.3137 39 96 39C93.027 39 90.559 41.1623 90.083 44H80V20C80 19.4477 79.5523 19 79 19H52V11.917C54.8377 11.441 57 8.973 57 6C57 2.68629 54.3137 0 51 0ZM2 36C2 33.7909 3.79086 32 6 32C8.20914 32 10 33.7909 10 36C10 38.2091 8.20914 40 6 40C3.79086 40 2 38.2091 2 36ZM92 45C92 42.7909 93.7909 41 96 41C98.2091 41 100 42.7909 100 45C100 47.2091 98.2091 49 96 49C93.7909 49 92 47.2091 92 45ZM24 83V21H78V83H24ZM2 52C2 49.7909 3.79086 48 6 48C8.20914 48 10 49.7909 10 52C10 54.2091 8.20914 56 6 56C3.79086 56 2 54.2091 2 52ZM92 61C92 58.7909 93.7909 57 96 57C98.2091 57 100 58.7909 100 61C100 63.2091 98.2091 65 96 65C93.7909 65 92 63.2091 92 61ZM2 68C2 65.7909 3.79086 64 6 64C8.20914 64 10 65.7909 10 68C10 70.2091 8.20914 72 6 72C3.79086 72 2 70.2091 2 68ZM47 98.5C47 96.2909 48.7909 94.5 51 94.5C53.2091 94.5 55 96.2909 55 98.5C55 100.709 53.2091 102.5 51 102.5C48.7909 102.5 47 100.709 47 98.5ZM59.4067 25.5752L58.6904 27.5308H58.0181L58.5146 25.5752H59.4067ZM43.8193 32V29.7148H44.6587C45.3677 29.7148 45.9302 29.52 46.3462 29.1304C46.7622 28.7407 46.9702 28.2397 46.9702 27.6274C46.9702 27.0151 46.771 26.5405 46.3726 26.2036C45.9741 25.8667 45.4131 25.6982 44.6895 25.6982H42.7778V32H43.8193ZM43.8193 28.8623V26.5596H44.5796C45.4409 26.5596 45.8716 26.9302 45.8716 27.6714C45.8716 28.0493 45.7544 28.3423 45.52 28.5503C45.2856 28.7583 44.9443 28.8623 44.4961 28.8623H43.8193ZM29.2939 38.4858C29.6631 38.0698 29.8477 37.4883 29.8477 36.7412V32.6982H28.8018V36.5742C28.8018 37.6523 28.4531 38.1914 27.7559 38.1914C27.4922 38.1914 27.2769 38.1343 27.1099 38.02V39.0044C27.2944 39.0718 27.5303 39.1055 27.8174 39.1055C28.4326 39.1055 28.9248 38.8989 29.2939 38.4858ZM52.9116 32H51.6899L50.6836 30.3125C50.5928 30.1572 50.5034 30.0254 50.4155 29.917C50.3306 29.8057 50.2427 29.7148 50.1519 29.6445C50.064 29.5742 49.9673 29.5229 49.8618 29.4907C49.7563 29.4585 49.6377 29.4424 49.5059 29.4424H49.084V32H48.0425V25.6982H50.1167C50.4126 25.6982 50.6851 25.7334 50.9341 25.8037C51.1831 25.874 51.3999 25.981 51.5845 26.1245C51.769 26.2651 51.9126 26.4424 52.0151 26.6562C52.1206 26.8672 52.1733 27.1147 52.1733 27.3989C52.1733 27.6216 52.1396 27.8267 52.0723 28.0142C52.0078 28.1987 51.9141 28.3643 51.791 28.5107C51.6709 28.6543 51.5244 28.7773 51.3516 28.8799C51.1816 28.9824 50.9897 29.0615 50.7759 29.1172V29.1348C50.8901 29.1992 50.9897 29.271 51.0747 29.3501C51.1597 29.4263 51.2402 29.5054 51.3164 29.5874C51.3926 29.6694 51.4673 29.7632 51.5405 29.8687C51.6167 29.9712 51.7002 30.0913 51.791 30.229L52.9116 32ZM49.084 26.5464V28.5942H49.9541C50.1152 28.5942 50.2632 28.5693 50.3979 28.5195C50.5356 28.4697 50.6543 28.3979 50.7539 28.3042C50.8535 28.2104 50.9312 28.0962 50.9868 27.9614C51.0425 27.8267 51.0703 27.6758 51.0703 27.5088C51.0703 27.207 50.9751 26.9712 50.7847 26.8013C50.5942 26.6313 50.3203 26.5464 49.9629 26.5464H49.084ZM53.6499 32H57.1875V31.1167H54.6958V29.2534H56.8623V28.3701H54.6958V26.5859H57.0469V25.6982H53.6499V32ZM45.4014 79.1055C46.1074 79.1055 46.6978 78.9824 47.1724 78.7363V77.7695C46.7358 78.0508 46.2202 78.1914 45.6255 78.1914C44.981 78.1914 44.4609 77.9863 44.0654 77.5762C43.6699 77.1631 43.4722 76.6094 43.4722 75.915C43.4722 75.1855 43.6831 74.6025 44.105 74.166C44.5298 73.7295 45.0835 73.5112 45.7661 73.5112C46.2788 73.5112 46.7476 73.6372 47.1724 73.8892V72.8521C46.771 72.6792 46.2656 72.5928 45.6562 72.5928C44.71 72.5928 43.9263 72.9033 43.3052 73.5244C42.687 74.1455 42.3779 74.96 42.3779 75.9678C42.3779 76.9053 42.6533 77.6626 43.2041 78.2397C43.7549 78.8169 44.4873 79.1055 45.4014 79.1055ZM51.8569 79H48.3677V72.6982H49.4136V78.1167H51.8569V79ZM56.4185 79H57.6401L56.5195 77.229C56.4287 77.0913 56.3452 76.9712 56.269 76.8687C56.1958 76.7632 56.1211 76.6694 56.0449 76.5874C55.9688 76.5054 55.8882 76.4263 55.8032 76.3501C55.7183 76.271 55.6187 76.1992 55.5044 76.1348V76.1172C55.7183 76.0615 55.9102 75.9824 56.0801 75.8799C56.2529 75.7773 56.3994 75.6543 56.5195 75.5107C56.6426 75.3643 56.7363 75.1987 56.8008 75.0142C56.8682 74.8267 56.9019 74.6216 56.9019 74.3989C56.9019 74.1147 56.8491 73.8672 56.7437 73.6562C56.6411 73.4424 56.4976 73.2651 56.313 73.1245C56.1284 72.981 55.9116 72.874 55.6626 72.8037C55.4136 72.7334 55.1411 72.6982 54.8452 72.6982H52.771V79H53.8125V76.4424H54.2344C54.3662 76.4424 54.4849 76.4585 54.5903 76.4907C54.6958 76.5229 54.7925 76.5742 54.8804 76.6445C54.9712 76.7148 55.0591 76.8057 55.144 76.917C55.2319 77.0254 55.3213 77.1572 55.4121 77.3125L56.4185 79ZM53.8125 75.5942V73.5464H54.6914C55.0488 73.5464 55.3228 73.6313 55.5132 73.8013C55.7036 73.9712 55.7988 74.207 55.7988 74.5088C55.7988 74.6758 55.771 74.8267 55.7153 74.9614C55.6597 75.0962 55.582 75.2104 55.4824 75.3042C55.3828 75.3979 55.2642 75.4697 55.1265 75.5195C54.9917 75.5693 54.8438 75.5942 54.6826 75.5942H53.8125ZM59.4771 72.5752L58.7607 74.5308H58.0884L58.585 72.5752H59.4771ZM70.1401 47.8726C70.5093 48.0278 70.9209 48.1055 71.375 48.1055C71.5508 48.1055 71.7192 48.0938 71.8804 48.0703C72.0415 48.0469 72.1968 48.0146 72.3462 47.9736L73.4712 48.8481H75.062L73.2339 47.5298C73.6118 47.2456 73.9048 46.8706 74.1128 46.4048C74.3208 45.936 74.4248 45.3911 74.4248 44.77C74.4248 44.3013 74.353 43.8721 74.2095 43.4824C74.0688 43.0928 73.8696 42.7588 73.6118 42.4805C73.354 42.1992 73.0435 41.981 72.6802 41.8257C72.3169 41.6704 71.9155 41.5928 71.4761 41.5928C71.0015 41.5928 70.5723 41.6719 70.1885 41.8301C69.8047 41.9883 69.478 42.2124 69.2085 42.5024C68.9419 42.7925 68.7354 43.1426 68.5889 43.5527C68.4453 43.9629 68.3735 44.4199 68.3735 44.9238C68.3735 45.3955 68.4453 45.8262 68.5889 46.2158C68.7324 46.6055 68.9346 46.9409 69.1953 47.2222C69.4561 47.5005 69.771 47.7173 70.1401 47.8726ZM70.6323 42.6826C70.8726 42.5684 71.1362 42.5112 71.4233 42.5112C71.7192 42.5112 71.9844 42.564 72.2188 42.6694C72.4561 42.7749 72.6567 42.9287 72.8208 43.1309C72.9849 43.3301 73.1108 43.5762 73.1987 43.8691C73.2866 44.1621 73.3306 44.4961 73.3306 44.8711C73.3306 45.2373 73.2852 45.564 73.1943 45.8511C73.1035 46.1382 72.9731 46.3813 72.8032 46.5806C72.6362 46.7769 72.4326 46.9277 72.1924 47.0332C71.9521 47.1387 71.6812 47.1914 71.3794 47.1914C71.0952 47.1914 70.8359 47.1357 70.6016 47.0244C70.3672 46.9102 70.1665 46.752 69.9995 46.5498C69.8325 46.3447 69.7021 46.0986 69.6084 45.8115C69.5176 45.5244 69.4722 45.2065 69.4722 44.8579C69.4722 44.5063 69.519 44.187 69.6128 43.8999C69.7095 43.6128 69.8442 43.3667 70.0171 43.1616C70.1899 42.9536 70.395 42.7939 70.6323 42.6826ZM68.375 64.1055C67.9209 64.1055 67.5093 64.0278 67.1401 63.8726C66.771 63.7173 66.4561 63.5005 66.1953 63.2222C65.9346 62.9409 65.7324 62.6055 65.5889 62.2158C65.4453 61.8262 65.3735 61.3955 65.3735 60.9238C65.3735 60.4199 65.4453 59.9629 65.5889 59.5527C65.7354 59.1426 65.9419 58.7925 66.2085 58.5024C66.478 58.2124 66.8047 57.9883 67.1885 57.8301C67.5723 57.6719 68.0015 57.5928 68.4761 57.5928C68.9155 57.5928 69.3169 57.6704 69.6802 57.8257C70.0435 57.981 70.354 58.1992 70.6118 58.4805C70.8696 58.7588 71.0688 59.0928 71.2095 59.4824C71.353 59.8721 71.4248 60.3013 71.4248 60.77C71.4248 61.3911 71.3208 61.936 71.1128 62.4048C70.9048 62.8706 70.6118 63.2456 70.2339 63.5298L72.062 64.8481H70.4712L69.3462 63.9736C69.1968 64.0146 69.0415 64.0469 68.8804 64.0703C68.7192 64.0938 68.5508 64.1055 68.375 64.1055ZM68.4233 58.5112C68.1362 58.5112 67.8726 58.5684 67.6323 58.6826C67.395 58.7939 67.1899 58.9536 67.0171 59.1616C66.8442 59.3667 66.7095 59.6128 66.6128 59.8999C66.519 60.187 66.4722 60.5063 66.4722 60.8579C66.4722 61.2065 66.5176 61.5244 66.6084 61.8115C66.7021 62.0986 66.8325 62.3447 66.9995 62.5498C67.1665 62.752 67.3672 62.9102 67.6016 63.0244C67.8359 63.1357 68.0952 63.1914 68.3794 63.1914C68.6812 63.1914 68.9521 63.1387 69.1924 63.0332C69.4326 62.9277 69.6362 62.7769 69.8032 62.5806C69.9731 62.3813 70.1035 62.1382 70.1943 61.8511C70.2852 61.564 70.3306 61.2373 70.3306 60.8711C70.3306 60.4961 70.2866 60.1621 70.1987 59.8691C70.1108 59.5762 69.9849 59.3301 69.8208 59.1309C69.6567 58.9287 69.4561 58.7749 69.2188 58.6694C68.9844 58.564 68.7192 58.5112 68.4233 58.5112ZM72.9629 59.5308L73.6792 57.5752H72.7871L72.2905 59.5308H72.9629ZM32.0933 52.6577L28.1689 54.543V53.7124L31.043 52.4336V52.4072L28.1689 50.9834V50.1484L32.0933 52.1919V52.6577ZM31.1836 71H32.5459L29.8828 67.7217L32.3569 64.6982H31.1045L28.9907 67.4448C28.9263 67.5239 28.8765 67.5957 28.8413 67.6602H28.8237V64.6982H27.7778V71H28.8237V67.8975H28.8413C28.8589 67.9355 28.9087 68.0088 28.9907 68.1172L31.1836 71Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Width = 70,
                Height = 90,
                Tooltip= new DiagramTooltip()
                {
                    Content="JK Flip-Flop"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip)&~NodeConstraints.InConnect
            };
            FlipFlopSymbols.Add(JKFlipFlop);
            Node DFlipFlop = new Node()
            {
                ID = "DFlip-Flop",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M47 6C47 3.79086 48.7909 2 51 2C53.2091 2 55 3.79086 55 6C55 8.20914 53.2091 10 51 10C48.7909 10 47 8.20914 47 6ZM51 0C47.6863 0 45 2.68629 45 6C45 8.973 47.1623 11.441 50 11.917V20H23C22.4477 20 22 20.4477 22 21V39H11.917C11.441 36.1623 8.973 34 6 34C2.68629 34 0 36.6863 0 40C0 43.3137 2.68629 46 6 46C8.973 46 11.441 43.8377 11.917 41H22V55H11.917C11.441 52.1623 8.973 50 6 50C2.68629 50 0 52.6863 0 56C0 59.3137 2.68629 62 6 62C8.973 62 11.441 59.8377 11.917 57H22V73C22 73.5523 22.4477 74 23 74H50V82.583C47.1623 83.059 45 85.527 45 88.5C45 91.8137 47.6863 94.5 51 94.5C54.3137 94.5 57 91.8137 57 88.5C57 85.527 54.8377 83.059 52 82.583V74H79C79.5523 74 80 73.5523 80 73V57H90.083C90.559 59.8377 93.027 62 96 62C99.3137 62 102 59.3137 102 56C102 52.6863 99.3137 50 96 50C93.027 50 90.559 52.1623 90.083 55H80V41H90.083C90.559 43.8377 93.027 46 96 46C99.3137 46 102 43.3137 102 40C102 36.6863 99.3137 34 96 34C93.027 34 90.559 36.1623 90.083 39H80V21C80 20.4477 79.5523 20 79 20H52V11.917C54.8377 11.441 57 8.973 57 6C57 2.68629 54.3137 0 51 0ZM24 72V22H78V72H24ZM2 40C2 37.7909 3.79086 36 6 36C8.20914 36 10 37.7909 10 40C10 42.2091 8.20914 44 6 44C3.79086 44 2 42.2091 2 40ZM92 40C92 37.7909 93.7909 36 96 36C98.2091 36 100 37.7909 100 40C100 42.2091 98.2091 44 96 44C93.7909 44 92 42.2091 92 40ZM2 56C2 53.7909 3.79086 52 6 52C8.20914 52 10 53.7909 10 56C10 58.2091 8.20914 60 6 60C3.79086 60 2 58.2091 2 56ZM92 56C92 53.7909 93.7909 52 96 52C98.2091 52 100 53.7909 100 56C100 58.2091 98.2091 60 96 60C93.7909 60 92 58.2091 92 56ZM47 88.5C47 86.2909 48.7909 84.5 51 84.5C53.2091 84.5 55 86.2909 55 88.5C55 90.7091 53.2091 92.5 51 92.5C48.7909 92.5 47 90.7091 47 88.5ZM59.4067 25.5752L58.6904 27.5308H58.0181L58.5146 25.5752H59.4067ZM43.8193 29.7148V32H42.7778V25.6982H44.6895C45.4131 25.6982 45.9741 25.8667 46.3726 26.2036C46.771 26.5405 46.9702 27.0151 46.9702 27.6274C46.9702 28.2397 46.7622 28.7407 46.3462 29.1304C45.9302 29.52 45.3677 29.7148 44.6587 29.7148H43.8193ZM43.8193 26.5596V28.8623H44.4961C44.9443 28.8623 45.2856 28.7583 45.52 28.5503C45.7544 28.3423 45.8716 28.0493 45.8716 27.6714C45.8716 26.9302 45.4409 26.5596 44.5796 26.5596H43.8193ZM52.9116 32H51.6899L50.6836 30.3125C50.5928 30.1572 50.5034 30.0254 50.4155 29.917C50.3306 29.8057 50.2427 29.7148 50.1519 29.6445C50.064 29.5742 49.9673 29.5229 49.8618 29.4907C49.7563 29.4585 49.6377 29.4424 49.5059 29.4424H49.084V32H48.0425V25.6982H50.1167C50.4126 25.6982 50.6851 25.7334 50.9341 25.8037C51.1831 25.874 51.3999 25.981 51.5845 26.1245C51.769 26.2651 51.9126 26.4424 52.0151 26.6562C52.1206 26.8672 52.1733 27.1147 52.1733 27.3989C52.1733 27.6216 52.1396 27.8267 52.0723 28.0142C52.0078 28.1987 51.9141 28.3643 51.791 28.5107C51.6709 28.6543 51.5244 28.7773 51.3516 28.8799C51.1816 28.9824 50.9897 29.0615 50.7759 29.1172V29.1348C50.8901 29.1992 50.9897 29.271 51.0747 29.3501C51.1597 29.4263 51.2402 29.5054 51.3164 29.5874C51.3926 29.6694 51.4673 29.7632 51.5405 29.8687C51.6167 29.9712 51.7002 30.0913 51.791 30.229L52.9116 32ZM49.084 26.5464V28.5942H49.9541C50.1152 28.5942 50.2632 28.5693 50.3979 28.5195C50.5356 28.4697 50.6543 28.3979 50.7539 28.3042C50.8535 28.2104 50.9312 28.0962 50.9868 27.9614C51.0425 27.8267 51.0703 27.6758 51.0703 27.5088C51.0703 27.207 50.9751 26.9712 50.7847 26.8013C50.5942 26.6313 50.3203 26.5464 49.9629 26.5464H49.084ZM53.6499 32H57.1875V31.1167H54.6958V29.2534H56.8623V28.3701H54.6958V26.5859H57.0469V25.6982H53.6499V32ZM45.4014 69.1055C46.1074 69.1055 46.6978 68.9824 47.1724 68.7363V67.7695C46.7358 68.0508 46.2202 68.1914 45.6255 68.1914C44.981 68.1914 44.4609 67.9863 44.0654 67.5762C43.6699 67.1631 43.4722 66.6094 43.4722 65.915C43.4722 65.1855 43.6831 64.6025 44.105 64.166C44.5298 63.7295 45.0835 63.5112 45.7661 63.5112C46.2788 63.5112 46.7476 63.6372 47.1724 63.8892V62.8521C46.771 62.6792 46.2656 62.5928 45.6562 62.5928C44.71 62.5928 43.9263 62.9033 43.3052 63.5244C42.687 64.1455 42.3779 64.96 42.3779 65.9678C42.3779 66.9053 42.6533 67.6626 43.2041 68.2397C43.7549 68.8169 44.4873 69.1055 45.4014 69.1055ZM51.8569 69H48.3677V62.6982H49.4136V68.1167H51.8569V69ZM56.4185 69H57.6401L56.5195 67.229C56.4287 67.0913 56.3452 66.9712 56.269 66.8687C56.1958 66.7632 56.1211 66.6694 56.0449 66.5874C55.9688 66.5054 55.8882 66.4263 55.8032 66.3501C55.7183 66.271 55.6187 66.1992 55.5044 66.1348V66.1172C55.7183 66.0615 55.9102 65.9824 56.0801 65.8799C56.2529 65.7773 56.3994 65.6543 56.5195 65.5107C56.6426 65.3643 56.7363 65.1987 56.8008 65.0142C56.8682 64.8267 56.9019 64.6216 56.9019 64.3989C56.9019 64.1147 56.8491 63.8672 56.7437 63.6562C56.6411 63.4424 56.4976 63.2651 56.313 63.1245C56.1284 62.981 55.9116 62.874 55.6626 62.8037C55.4136 62.7334 55.1411 62.6982 54.8452 62.6982H52.771V69H53.8125V66.4424H54.2344C54.3662 66.4424 54.4849 66.4585 54.5903 66.4907C54.6958 66.5229 54.7925 66.5742 54.8804 66.6445C54.9712 66.7148 55.0591 66.8057 55.144 66.917C55.2319 67.0254 55.3213 67.1572 55.4121 67.3125L56.4185 69ZM53.8125 65.5942V63.5464H54.6914C55.0488 63.5464 55.3228 63.6313 55.5132 63.8013C55.7036 63.9712 55.7988 64.207 55.7988 64.5088C55.7988 64.6758 55.771 64.8267 55.7153 64.9614C55.6597 65.0962 55.582 65.2104 55.4824 65.3042C55.3828 65.3979 55.2642 65.4697 55.1265 65.5195C54.9917 65.5693 54.8438 65.5942 54.6826 65.5942H53.8125ZM59.4771 62.5752L58.7607 64.5308H58.0884L58.585 62.5752H59.4771ZM70.1401 42.8726C70.5093 43.0278 70.9209 43.1055 71.375 43.1055C71.5508 43.1055 71.7192 43.0938 71.8804 43.0703C72.0415 43.0469 72.1968 43.0146 72.3462 42.9736L73.4712 43.8481H75.062L73.2339 42.5298C73.6118 42.2456 73.9048 41.8706 74.1128 41.4048C74.3208 40.936 74.4248 40.3911 74.4248 39.77C74.4248 39.3013 74.353 38.8721 74.2095 38.4824C74.0688 38.0928 73.8696 37.7588 73.6118 37.4805C73.354 37.1992 73.0435 36.981 72.6802 36.8257C72.3169 36.6704 71.9155 36.5928 71.4761 36.5928C71.0015 36.5928 70.5723 36.6719 70.1885 36.8301C69.8047 36.9883 69.478 37.2124 69.2085 37.5024C68.9419 37.7925 68.7354 38.1426 68.5889 38.5527C68.4453 38.9629 68.3735 39.4199 68.3735 39.9238C68.3735 40.3955 68.4453 40.8262 68.5889 41.2158C68.7324 41.6055 68.9346 41.9409 69.1953 42.2222C69.4561 42.5005 69.771 42.7173 70.1401 42.8726ZM70.6323 37.6826C70.8726 37.5684 71.1362 37.5112 71.4233 37.5112C71.7192 37.5112 71.9844 37.564 72.2188 37.6694C72.4561 37.7749 72.6567 37.9287 72.8208 38.1309C72.9849 38.3301 73.1108 38.5762 73.1987 38.8691C73.2866 39.1621 73.3306 39.4961 73.3306 39.8711C73.3306 40.2373 73.2852 40.564 73.1943 40.8511C73.1035 41.1382 72.9731 41.3813 72.8032 41.5806C72.6362 41.7769 72.4326 41.9277 72.1924 42.0332C71.9521 42.1387 71.6812 42.1914 71.3794 42.1914C71.0952 42.1914 70.8359 42.1357 70.6016 42.0244C70.3672 41.9102 70.1665 41.752 69.9995 41.5498C69.8325 41.3447 69.7021 41.0986 69.6084 40.8115C69.5176 40.5244 69.4722 40.2065 69.4722 39.8579C69.4722 39.5063 69.519 39.187 69.6128 38.8999C69.7095 38.6128 69.8442 38.3667 70.0171 38.1616C70.1899 37.9536 70.395 37.7939 70.6323 37.6826ZM68.375 59.1055C67.9209 59.1055 67.5093 59.0278 67.1401 58.8726C66.771 58.7173 66.4561 58.5005 66.1953 58.2222C65.9346 57.9409 65.7324 57.6055 65.5889 57.2158C65.4453 56.8262 65.3735 56.3955 65.3735 55.9238C65.3735 55.4199 65.4453 54.9629 65.5889 54.5527C65.7354 54.1426 65.9419 53.7925 66.2085 53.5024C66.478 53.2124 66.8047 52.9883 67.1885 52.8301C67.5723 52.6719 68.0015 52.5928 68.4761 52.5928C68.9155 52.5928 69.3169 52.6704 69.6802 52.8257C70.0435 52.981 70.354 53.1992 70.6118 53.4805C70.8696 53.7588 71.0688 54.0928 71.2095 54.4824C71.353 54.8721 71.4248 55.3013 71.4248 55.77C71.4248 56.3911 71.3208 56.936 71.1128 57.4048C70.9048 57.8706 70.6118 58.2456 70.2339 58.5298L72.062 59.8481H70.4712L69.3462 58.9736C69.1968 59.0146 69.0415 59.0469 68.8804 59.0703C68.7192 59.0938 68.5508 59.1055 68.375 59.1055ZM68.4233 53.5112C68.1362 53.5112 67.8726 53.5684 67.6323 53.6826C67.395 53.7939 67.1899 53.9536 67.0171 54.1616C66.8442 54.3667 66.7095 54.6128 66.6128 54.8999C66.519 55.187 66.4722 55.5063 66.4722 55.8579C66.4722 56.2065 66.5176 56.5244 66.6084 56.8115C66.7021 57.0986 66.8325 57.3447 66.9995 57.5498C67.1665 57.752 67.3672 57.9102 67.6016 58.0244C67.8359 58.1357 68.0952 58.1914 68.3794 58.1914C68.6812 58.1914 68.9521 58.1387 69.1924 58.0332C69.4326 57.9277 69.6362 57.7769 69.8032 57.5806C69.9731 57.3813 70.1035 57.1382 70.1943 56.8511C70.2852 56.564 70.3306 56.2373 70.3306 55.8711C70.3306 55.4961 70.2866 55.1621 70.1987 54.8691C70.1108 54.5762 69.9849 54.3301 69.8208 54.1309C69.6567 53.9287 69.4561 53.7749 69.2188 53.6694C68.9844 53.564 68.7192 53.5112 68.4233 53.5112ZM72.9629 54.5308L73.6792 52.5752H72.7871L72.2905 54.5308H72.9629ZM27.7778 43V36.6982H29.5972C31.9204 36.6982 33.082 37.7222 33.082 39.77C33.082 40.7427 32.7598 41.5249 32.1152 42.1167C31.4707 42.7056 30.6064 43 29.5225 43H27.7778ZM28.8193 37.5859V42.1167H29.6543C30.3896 42.1167 30.9609 41.9146 31.3682 41.5103C31.7783 41.106 31.9834 40.5347 31.9834 39.7964C31.9834 38.3228 31.2202 37.5859 29.6938 37.5859H28.8193ZM28.1689 58.543L32.0933 56.6577V56.1919L28.1689 54.1484V54.9834L31.043 56.4072V56.4336L28.1689 57.7124V58.543Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Width = 65,
                Height = 90,
                Tooltip= new DiagramTooltip()
                {
                    Content="D Flip-Flop"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            FlipFlopSymbols.Add(DFlipFlop);
            Node TFlipFlop = new Node()
            {
                ID = "TFlip-Flop",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M47 6C47 3.79086 48.7909 2 51 2C53.2091 2 55 3.79086 55 6C55 8.20914 53.2091 10 51 10C48.7909 10 47 8.20914 47 6ZM51 0C47.6863 0 45 2.68629 45 6C45 8.973 47.1623 11.441 50 11.917V19H23C22.4477 19 22 19.4477 22 20V38H11.917C11.441 35.1623 8.973 33 6 33C2.68629 33 0 35.6863 0 39C0 42.3137 2.68629 45 6 45C8.973 45 11.441 42.8377 11.917 40H22V54H11.917C11.441 51.1623 8.973 49 6 49C2.68629 49 0 51.6863 0 55C0 58.3137 2.68629 61 6 61C8.973 61 11.441 58.8377 11.917 56H22V72C22 72.5523 22.4477 73 23 73H50V80.583C47.1623 81.059 45 83.527 45 86.5C45 89.8137 47.6863 92.5 51 92.5C54.3137 92.5 57 89.8137 57 86.5C57 83.527 54.8377 81.059 52 80.583V73H79C79.5523 73 80 72.5523 80 72V56H90.083C90.559 58.8377 93.027 61 96 61C99.3137 61 102 58.3137 102 55C102 51.6863 99.3137 49 96 49C93.027 49 90.559 51.1623 90.083 54H80V40H90.083C90.559 42.8377 93.027 45 96 45C99.3137 45 102 42.3137 102 39C102 35.6863 99.3137 33 96 33C93.027 33 90.559 35.1623 90.083 38H80V20C80 19.4477 79.5523 19 79 19H52V11.917C54.8377 11.441 57 8.973 57 6C57 2.68629 54.3137 0 51 0ZM24 71V21H78V71H24ZM2 39C2 36.7909 3.79086 35 6 35C8.20914 35 10 36.7909 10 39C10 41.2091 8.20914 43 6 43C3.79086 43 2 41.2091 2 39ZM92 39C92 36.7909 93.7909 35 96 35C98.2091 35 100 36.7909 100 39C100 41.2091 98.2091 43 96 43C93.7909 43 92 41.2091 92 39ZM2 55C2 52.7909 3.79086 51 6 51C8.20914 51 10 52.7909 10 55C10 57.2091 8.20914 59 6 59C3.79086 59 2 57.2091 2 55ZM92 55C92 52.7909 93.7909 51 96 51C98.2091 51 100 52.7909 100 55C100 57.2091 98.2091 59 96 59C93.7909 59 92 57.2091 92 55ZM47 86.5C47 84.2909 48.7909 82.5 51 82.5C53.2091 82.5 55 84.2909 55 86.5C55 88.7091 53.2091 90.5 51 90.5C48.7909 90.5 47 88.7091 47 86.5ZM59.4067 25.5752L58.6904 27.5308H58.0181L58.5146 25.5752H59.4067ZM43.8193 29.7148V32H42.7778V25.6982H44.6895C45.4131 25.6982 45.9741 25.8667 46.3726 26.2036C46.771 26.5405 46.9702 27.0151 46.9702 27.6274C46.9702 28.2397 46.7622 28.7407 46.3462 29.1304C45.9302 29.52 45.3677 29.7148 44.6587 29.7148H43.8193ZM43.8193 26.5596V28.8623H44.4961C44.9443 28.8623 45.2856 28.7583 45.52 28.5503C45.7544 28.3423 45.8716 28.0493 45.8716 27.6714C45.8716 26.9302 45.4409 26.5596 44.5796 26.5596H43.8193ZM52.9116 32H51.6899L50.6836 30.3125C50.5928 30.1572 50.5034 30.0254 50.4155 29.917C50.3306 29.8057 50.2427 29.7148 50.1519 29.6445C50.064 29.5742 49.9673 29.5229 49.8618 29.4907C49.7563 29.4585 49.6377 29.4424 49.5059 29.4424H49.084V32H48.0425V25.6982H50.1167C50.4126 25.6982 50.6851 25.7334 50.9341 25.8037C51.1831 25.874 51.3999 25.981 51.5845 26.1245C51.769 26.2651 51.9126 26.4424 52.0151 26.6562C52.1206 26.8672 52.1733 27.1147 52.1733 27.3989C52.1733 27.6216 52.1396 27.8267 52.0723 28.0142C52.0078 28.1987 51.9141 28.3643 51.791 28.5107C51.6709 28.6543 51.5244 28.7773 51.3516 28.8799C51.1816 28.9824 50.9897 29.0615 50.7759 29.1172V29.1348C50.8901 29.1992 50.9897 29.271 51.0747 29.3501C51.1597 29.4263 51.2402 29.5054 51.3164 29.5874C51.3926 29.6694 51.4673 29.7632 51.5405 29.8687C51.6167 29.9712 51.7002 30.0913 51.791 30.229L52.9116 32ZM49.084 26.5464V28.5942H49.9541C50.1152 28.5942 50.2632 28.5693 50.3979 28.5195C50.5356 28.4697 50.6543 28.3979 50.7539 28.3042C50.8535 28.2104 50.9312 28.0962 50.9868 27.9614C51.0425 27.8267 51.0703 27.6758 51.0703 27.5088C51.0703 27.207 50.9751 26.9712 50.7847 26.8013C50.5942 26.6313 50.3203 26.5464 49.9629 26.5464H49.084ZM53.6499 32H57.1875V31.1167H54.6958V29.2534H56.8623V28.3701H54.6958V26.5859H57.0469V25.6982H53.6499V32ZM45.4014 67.1055C46.1074 67.1055 46.6978 66.9824 47.1724 66.7363V65.7695C46.7358 66.0508 46.2202 66.1914 45.6255 66.1914C44.981 66.1914 44.4609 65.9863 44.0654 65.5762C43.6699 65.1631 43.4722 64.6094 43.4722 63.915C43.4722 63.1855 43.6831 62.6025 44.105 62.166C44.5298 61.7295 45.0835 61.5112 45.7661 61.5112C46.2788 61.5112 46.7476 61.6372 47.1724 61.8892V60.8521C46.771 60.6792 46.2656 60.5928 45.6562 60.5928C44.71 60.5928 43.9263 60.9033 43.3052 61.5244C42.687 62.1455 42.3779 62.96 42.3779 63.9678C42.3779 64.9053 42.6533 65.6626 43.2041 66.2397C43.7549 66.8169 44.4873 67.1055 45.4014 67.1055ZM51.8569 67H48.3677V60.6982H49.4136V66.1167H51.8569V67ZM56.4185 67H57.6401L56.5195 65.229C56.4287 65.0913 56.3452 64.9712 56.269 64.8687C56.1958 64.7632 56.1211 64.6694 56.0449 64.5874C55.9688 64.5054 55.8882 64.4263 55.8032 64.3501C55.7183 64.271 55.6187 64.1992 55.5044 64.1348V64.1172C55.7183 64.0615 55.9102 63.9824 56.0801 63.8799C56.2529 63.7773 56.3994 63.6543 56.5195 63.5107C56.6426 63.3643 56.7363 63.1987 56.8008 63.0142C56.8682 62.8267 56.9019 62.6216 56.9019 62.3989C56.9019 62.1147 56.8491 61.8672 56.7437 61.6562C56.6411 61.4424 56.4976 61.2651 56.313 61.1245C56.1284 60.981 55.9116 60.874 55.6626 60.8037C55.4136 60.7334 55.1411 60.6982 54.8452 60.6982H52.771V67H53.8125V64.4424H54.2344C54.3662 64.4424 54.4849 64.4585 54.5903 64.4907C54.6958 64.5229 54.7925 64.5742 54.8804 64.6445C54.9712 64.7148 55.0591 64.8057 55.144 64.917C55.2319 65.0254 55.3213 65.1572 55.4121 65.3125L56.4185 67ZM53.8125 63.5942V61.5464H54.6914C55.0488 61.5464 55.3228 61.6313 55.5132 61.8013C55.7036 61.9712 55.7988 62.207 55.7988 62.5088C55.7988 62.6758 55.771 62.8267 55.7153 62.9614C55.6597 63.0962 55.582 63.2104 55.4824 63.3042C55.3828 63.3979 55.2642 63.4697 55.1265 63.5195C54.9917 63.5693 54.8438 63.5942 54.6826 63.5942H53.8125ZM59.4771 60.5752L58.7607 62.5308H58.0884L58.585 60.5752H59.4771ZM70.1401 41.8726C70.5093 42.0278 70.9209 42.1055 71.375 42.1055C71.5508 42.1055 71.7192 42.0938 71.8804 42.0703C72.0415 42.0469 72.1968 42.0146 72.3462 41.9736L73.4712 42.8481H75.062L73.2339 41.5298C73.6118 41.2456 73.9048 40.8706 74.1128 40.4048C74.3208 39.936 74.4248 39.3911 74.4248 38.77C74.4248 38.3013 74.353 37.8721 74.2095 37.4824C74.0688 37.0928 73.8696 36.7588 73.6118 36.4805C73.354 36.1992 73.0435 35.981 72.6802 35.8257C72.3169 35.6704 71.9155 35.5928 71.4761 35.5928C71.0015 35.5928 70.5723 35.6719 70.1885 35.8301C69.8047 35.9883 69.478 36.2124 69.2085 36.5024C68.9419 36.7925 68.7354 37.1426 68.5889 37.5527C68.4453 37.9629 68.3735 38.4199 68.3735 38.9238C68.3735 39.3955 68.4453 39.8262 68.5889 40.2158C68.7324 40.6055 68.9346 40.9409 69.1953 41.2222C69.4561 41.5005 69.771 41.7173 70.1401 41.8726ZM70.6323 36.6826C70.8726 36.5684 71.1362 36.5112 71.4233 36.5112C71.7192 36.5112 71.9844 36.564 72.2188 36.6694C72.4561 36.7749 72.6567 36.9287 72.8208 37.1309C72.9849 37.3301 73.1108 37.5762 73.1987 37.8691C73.2866 38.1621 73.3306 38.4961 73.3306 38.8711C73.3306 39.2373 73.2852 39.564 73.1943 39.8511C73.1035 40.1382 72.9731 40.3813 72.8032 40.5806C72.6362 40.7769 72.4326 40.9277 72.1924 41.0332C71.9521 41.1387 71.6812 41.1914 71.3794 41.1914C71.0952 41.1914 70.8359 41.1357 70.6016 41.0244C70.3672 40.9102 70.1665 40.752 69.9995 40.5498C69.8325 40.3447 69.7021 40.0986 69.6084 39.8115C69.5176 39.5244 69.4722 39.2065 69.4722 38.8579C69.4722 38.5063 69.519 38.187 69.6128 37.8999C69.7095 37.6128 69.8442 37.3667 70.0171 37.1616C70.1899 36.9536 70.395 36.7939 70.6323 36.6826ZM68.375 58.1055C67.9209 58.1055 67.5093 58.0278 67.1401 57.8726C66.771 57.7173 66.4561 57.5005 66.1953 57.2222C65.9346 56.9409 65.7324 56.6055 65.5889 56.2158C65.4453 55.8262 65.3735 55.3955 65.3735 54.9238C65.3735 54.4199 65.4453 53.9629 65.5889 53.5527C65.7354 53.1426 65.9419 52.7925 66.2085 52.5024C66.478 52.2124 66.8047 51.9883 67.1885 51.8301C67.5723 51.6719 68.0015 51.5928 68.4761 51.5928C68.9155 51.5928 69.3169 51.6704 69.6802 51.8257C70.0435 51.981 70.354 52.1992 70.6118 52.4805C70.8696 52.7588 71.0688 53.0928 71.2095 53.4824C71.353 53.8721 71.4248 54.3013 71.4248 54.77C71.4248 55.3911 71.3208 55.936 71.1128 56.4048C70.9048 56.8706 70.6118 57.2456 70.2339 57.5298L72.062 58.8481H70.4712L69.3462 57.9736C69.1968 58.0146 69.0415 58.0469 68.8804 58.0703C68.7192 58.0938 68.5508 58.1055 68.375 58.1055ZM68.4233 52.5112C68.1362 52.5112 67.8726 52.5684 67.6323 52.6826C67.395 52.7939 67.1899 52.9536 67.0171 53.1616C66.8442 53.3667 66.7095 53.6128 66.6128 53.8999C66.519 54.187 66.4722 54.5063 66.4722 54.8579C66.4722 55.2065 66.5176 55.5244 66.6084 55.8115C66.7021 56.0986 66.8325 56.3447 66.9995 56.5498C67.1665 56.752 67.3672 56.9102 67.6016 57.0244C67.8359 57.1357 68.0952 57.1914 68.3794 57.1914C68.6812 57.1914 68.9521 57.1387 69.1924 57.0332C69.4326 56.9277 69.6362 56.7769 69.8032 56.5806C69.9731 56.3813 70.1035 56.1382 70.1943 55.8511C70.2852 55.564 70.3306 55.2373 70.3306 54.8711C70.3306 54.4961 70.2866 54.1621 70.1987 53.8691C70.1108 53.5762 69.9849 53.3301 69.8208 53.1309C69.6567 52.9287 69.4561 52.7749 69.2188 52.6694C68.9844 52.564 68.7192 52.5112 68.4233 52.5112ZM72.9629 53.5308L73.6792 51.5752H72.7871L72.2905 53.5308H72.9629ZM31.8208 36.5859H30.0103V42H28.9644V36.5859H27.1582V35.6982H31.8208V36.5859ZM28.1689 57.543L32.0933 55.6577V55.1919L28.1689 53.1484V53.9834L31.043 55.4072V55.4336L28.1689 56.7124V57.543Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Width = 65,
                Height = 90,
                Tooltip= new DiagramTooltip()
                {
                    Content="T Flip-Flop"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            FlipFlopSymbols.Add(TFlipFlop);
            Node SRFlipFlop = new Node()
            {
                ID = "SRFlip-Flop",
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M23 0C22.4477 0 22 0.447715 22 1V16H11.917C11.441 13.1623 8.973 11 6 11C2.68629 11 0 13.6863 0 17C0 20.3137 2.68629 23 6 23C8.973 23 11.441 20.8377 11.917 18H22V32H11.917C11.441 29.1623 8.973 27 6 27C2.68629 27 0 29.6863 0 33C0 36.3137 2.68629 39 6 39C8.973 39 11.441 36.8377 11.917 34H22V48H11.917C11.441 45.1623 8.973 43 6 43C2.68629 43 0 45.6863 0 49C0 52.3137 2.68629 55 6 55C8.973 55 11.441 52.8377 11.917 50H22V65C22 65.5523 22.4477 66 23 66H79C79.5523 66 80 65.5523 80 65V43H90.083C90.559 45.8377 93.027 48 96 48C99.3137 48 102 45.3137 102 42C102 38.6863 99.3137 36 96 36C93.027 36 90.559 38.1623 90.083 41H80V27H90.083C90.559 29.8377 93.027 32 96 32C99.3137 32 102 29.3137 102 26C102 22.6863 99.3137 20 96 20C93.027 20 90.559 22.1623 90.083 25H80V1C80 0.447715 79.5523 0 79 0H23ZM78 64H24V2H78V64ZM2 17C2 14.7909 3.79086 13 6 13C8.20914 13 10 14.7909 10 17C10 19.2091 8.20914 21 6 21C3.79086 21 2 19.2091 2 17ZM92 26C92 23.7909 93.7909 22 96 22C98.2091 22 100 23.7909 100 26C100 28.2091 98.2091 30 96 30C93.7909 30 92 28.2091 92 26ZM2 33C2 30.7909 3.79086 29 6 29C8.20914 29 10 30.7909 10 33C10 35.2091 8.20914 37 6 37C3.79086 37 2 35.2091 2 33ZM92 42C92 39.7909 93.7909 38 96 38C98.2091 38 100 39.7909 100 42C100 44.2091 98.2091 46 96 46C93.7909 46 92 44.2091 92 42ZM2 49C2 46.7909 3.79086 45 6 45C8.20914 45 10 46.7909 10 49C10 51.2091 8.20914 53 6 53C3.79086 53 2 51.2091 2 49ZM27.4878 18.6729V19.7495C27.5757 19.8052 27.6914 19.855 27.835 19.8989C27.9785 19.9429 28.1309 19.9795 28.292 20.0088C28.4531 20.041 28.6099 20.0645 28.7622 20.0791C28.9175 20.0967 29.0479 20.1055 29.1533 20.1055C29.4697 20.1055 29.77 20.0747 30.0542 20.0132C30.3384 19.9517 30.5889 19.8521 30.8057 19.7144C31.0254 19.5767 31.1997 19.3979 31.3286 19.1782C31.4575 18.9585 31.522 18.689 31.522 18.3696C31.522 18.1323 31.478 17.9214 31.3901 17.7368C31.3052 17.5493 31.188 17.3794 31.0386 17.2271C30.8921 17.0747 30.7207 16.9355 30.5244 16.8096C30.3311 16.6836 30.1245 16.5635 29.9048 16.4492C29.6909 16.3555 29.502 16.269 29.3379 16.1899C29.1768 16.1079 29.0391 16.0244 28.9248 15.9395C28.8135 15.8545 28.7285 15.7607 28.6699 15.6582C28.6113 15.5557 28.582 15.4341 28.582 15.2935C28.582 15.144 28.6201 15.0166 28.6963 14.9111C28.7725 14.8057 28.8706 14.7192 28.9907 14.6519C29.1138 14.5845 29.2515 14.5361 29.4038 14.5068C29.5591 14.4775 29.7144 14.4629 29.8696 14.4629C30.4321 14.4629 30.8921 14.5918 31.2495 14.8496V13.8169C30.9478 13.6675 30.4673 13.5928 29.8081 13.5928C29.5181 13.5928 29.2339 13.6279 28.9556 13.6982C28.6772 13.7656 28.4282 13.8711 28.2085 14.0146C27.9917 14.1553 27.8159 14.3354 27.6812 14.5552C27.5493 14.772 27.4834 15.0283 27.4834 15.3242C27.4834 15.5645 27.5215 15.7739 27.5977 15.9526C27.6768 16.1313 27.7837 16.2925 27.9185 16.436C28.0562 16.5767 28.2173 16.7056 28.4019 16.8228C28.5864 16.9399 28.7856 17.0557 28.9995 17.1699C29.1987 17.2607 29.3848 17.3486 29.5576 17.4336C29.7334 17.5156 29.8857 17.6021 30.0146 17.6929C30.1436 17.7837 30.2432 17.8848 30.3135 17.9961C30.3867 18.1045 30.4233 18.2334 30.4233 18.3828C30.4233 18.6611 30.3149 18.8735 30.0981 19.02C29.8843 19.1665 29.5576 19.2397 29.1182 19.2397C28.9922 19.2397 28.8545 19.2266 28.7051 19.2002C28.5557 19.1738 28.4062 19.1372 28.2568 19.0903C28.1074 19.0405 27.9653 18.9805 27.8306 18.9102C27.6987 18.8398 27.5845 18.7607 27.4878 18.6729ZM70.1401 28.8726C70.5093 29.0278 70.9209 29.1055 71.375 29.1055C71.5508 29.1055 71.7192 29.0938 71.8804 29.0703C72.0415 29.0469 72.1968 29.0146 72.3462 28.9736L73.4712 29.8481H75.062L73.2339 28.5298C73.6118 28.2456 73.9048 27.8706 74.1128 27.4048C74.3208 26.936 74.4248 26.3911 74.4248 25.77C74.4248 25.3013 74.353 24.8721 74.2095 24.4824C74.0688 24.0928 73.8696 23.7588 73.6118 23.4805C73.354 23.1992 73.0435 22.981 72.6802 22.8257C72.3169 22.6704 71.9155 22.5928 71.4761 22.5928C71.0015 22.5928 70.5723 22.6719 70.1885 22.8301C69.8047 22.9883 69.478 23.2124 69.2085 23.5024C68.9419 23.7925 68.7354 24.1426 68.5889 24.5527C68.4453 24.9629 68.3735 25.4199 68.3735 25.9238C68.3735 26.3955 68.4453 26.8262 68.5889 27.2158C68.7324 27.6055 68.9346 27.9409 69.1953 28.2222C69.4561 28.5005 69.771 28.7173 70.1401 28.8726ZM70.6323 23.6826C70.8726 23.5684 71.1362 23.5112 71.4233 23.5112C71.7192 23.5112 71.9844 23.564 72.2188 23.6694C72.4561 23.7749 72.6567 23.9287 72.8208 24.1309C72.9849 24.3301 73.1108 24.5762 73.1987 24.8691C73.2866 25.1621 73.3306 25.4961 73.3306 25.8711C73.3306 26.2373 73.2852 26.564 73.1943 26.8511C73.1035 27.1382 72.9731 27.3813 72.8032 27.5806C72.6362 27.7769 72.4326 27.9277 72.1924 28.0332C71.9521 28.1387 71.6812 28.1914 71.3794 28.1914C71.0952 28.1914 70.8359 28.1357 70.6016 28.0244C70.3672 27.9102 70.1665 27.752 69.9995 27.5498C69.8325 27.3447 69.7021 27.0986 69.6084 26.8115C69.5176 26.5244 69.4722 26.2065 69.4722 25.8579C69.4722 25.5063 69.519 25.187 69.6128 24.8999C69.7095 24.6128 69.8442 24.3667 70.0171 24.1616C70.1899 23.9536 70.395 23.7939 70.6323 23.6826ZM68.375 45.1055C67.9209 45.1055 67.5093 45.0278 67.1401 44.8726C66.771 44.7173 66.4561 44.5005 66.1953 44.2222C65.9346 43.9409 65.7324 43.6055 65.5889 43.2158C65.4453 42.8262 65.3735 42.3955 65.3735 41.9238C65.3735 41.4199 65.4453 40.9629 65.5889 40.5527C65.7354 40.1426 65.9419 39.7925 66.2085 39.5024C66.478 39.2124 66.8047 38.9883 67.1885 38.8301C67.5723 38.6719 68.0015 38.5928 68.4761 38.5928C68.9155 38.5928 69.3169 38.6704 69.6802 38.8257C70.0435 38.981 70.354 39.1992 70.6118 39.4805C70.8696 39.7588 71.0688 40.0928 71.2095 40.4824C71.353 40.8721 71.4248 41.3013 71.4248 41.77C71.4248 42.3911 71.3208 42.936 71.1128 43.4048C70.9048 43.8706 70.6118 44.2456 70.2339 44.5298L72.062 45.8481H70.4712L69.3462 44.9736C69.1968 45.0146 69.0415 45.0469 68.8804 45.0703C68.7192 45.0938 68.5508 45.1055 68.375 45.1055ZM68.4233 39.5112C68.1362 39.5112 67.8726 39.5684 67.6323 39.6826C67.395 39.7939 67.1899 39.9536 67.0171 40.1616C66.8442 40.3667 66.7095 40.6128 66.6128 40.8999C66.519 41.187 66.4722 41.5063 66.4722 41.8579C66.4722 42.2065 66.5176 42.5244 66.6084 42.8115C66.7021 43.0986 66.8325 43.3447 66.9995 43.5498C67.1665 43.752 67.3672 43.9102 67.6016 44.0244C67.8359 44.1357 68.0952 44.1914 68.3794 44.1914C68.6812 44.1914 68.9521 44.1387 69.1924 44.0332C69.4326 43.9277 69.6362 43.7769 69.8032 43.5806C69.9731 43.3813 70.1035 43.1382 70.1943 42.8511C70.2852 42.564 70.3306 42.2373 70.3306 41.8711C70.3306 41.4961 70.2866 41.1621 70.1987 40.8691C70.1108 40.5762 69.9849 40.3301 69.8208 40.1309C69.6567 39.9287 69.4561 39.7749 69.2188 39.6694C68.9844 39.564 68.7192 39.5112 68.4233 39.5112ZM72.9629 40.5308L73.6792 38.5752H72.7871L72.2905 40.5308H72.9629ZM32.0933 33.6577L28.1689 35.543V34.7124L31.043 33.4336V33.4072L28.1689 31.9834V31.1484L32.0933 33.1919V33.6577ZM31.4253 52H32.647L31.5264 50.229C31.4355 50.0913 31.3521 49.9712 31.2759 49.8687C31.2026 49.7632 31.1279 49.6694 31.0518 49.5874C30.9756 49.5054 30.895 49.4263 30.8101 49.3501C30.7251 49.271 30.6255 49.1992 30.5112 49.1348V49.1172C30.7251 49.0615 30.917 48.9824 31.0869 48.8799C31.2598 48.7773 31.4062 48.6543 31.5264 48.5107C31.6494 48.3643 31.7432 48.1987 31.8076 48.0142C31.875 47.8267 31.9087 47.6216 31.9087 47.3989C31.9087 47.1147 31.856 46.8672 31.7505 46.6562C31.6479 46.4424 31.5044 46.2651 31.3198 46.1245C31.1353 45.981 30.9185 45.874 30.6694 45.8037C30.4204 45.7334 30.1479 45.6982 29.8521 45.6982H27.7778V52H28.8193V49.4424H29.2412C29.373 49.4424 29.4917 49.4585 29.5972 49.4907C29.7026 49.5229 29.7993 49.5742 29.8872 49.6445C29.978 49.7148 30.0659 49.8057 30.1509 49.917C30.2388 50.0254 30.3281 50.1572 30.4189 50.3125L31.4253 52ZM28.8193 48.5942V46.5464H29.6982C30.0557 46.5464 30.3296 46.6313 30.52 46.8013C30.7104 46.9712 30.8057 47.207 30.8057 47.5088C30.8057 47.6758 30.7778 47.8267 30.7222 47.9614C30.6665 48.0962 30.5889 48.2104 30.4893 48.3042C30.3896 48.3979 30.271 48.4697 30.1333 48.5195C29.9985 48.5693 29.8506 48.5942 29.6895 48.5942H28.8193Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Width = 73,
                Height = 78,
                Tooltip= new DiagramTooltip()
                {
                    Content="SR Flip-Flop"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            FlipFlopSymbols.Add(SRFlipFlop);

            Node Label = new Node()
            {
                ID = "Label",
                Shape=new TextShape()
                {
                    Type=NodeShapes.Text,
                    Content="Text",
                   
                },
                Style=new TextStyle()
                {
                    Fill="black",
                    StrokeColor="black",
                    StrokeWidth=2,
                    Color = "white"
                    
                },
                Width = 50,
                Height = 40,
                Constraints = NodeConstraints.Default &~NodeConstraints.InConnect

            };
            OtherSymbols.Add(Label);
            Node BusShape = new Node()
            {
                ID = "Bus",
                Width = 80,
                Height = 45,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M31 0C30.4477 0 30 0.447715 30 1V12H11.917C11.441 9.16229 8.973 7 6 7C2.68629 7 0 9.68629 0 13C0 16.3137 2.68629 19 6 19C8.973 19 11.441 16.8377 11.917 14H30V30H11.917C11.441 27.1623 8.973 25 6 25C2.68629 25 0 27.6863 0 31C0 34.3137 2.68629 37 6 37C8.973 37 11.441 34.8377 11.917 32H30V44C30 44.5523 30.4477 45 31 45H71C71.5523 45 72 44.5523 72 44V23H88.083C88.559 25.8377 91.027 28 94 28C97.3137 28 100 25.3137 100 22C100 18.6863 97.3137 16 94 16C91.027 16 88.559 18.1623 88.083 21H72V1C72 0.447715 71.5523 0 71 0H31ZM32 43V2H70V43H32ZM2 13C2 10.7909 3.79086 9 6 9C8.20914 9 10 10.7909 10 13C10 15.2091 8.20914 17 6 17C3.79086 17 2 15.2091 2 13ZM90 22C90 19.7909 91.7909 18 94 18C96.2091 18 98 19.7909 98 22C98 24.2091 96.2091 26 94 26C91.7909 26 90 24.2091 90 22ZM2 31C2 28.7909 3.79086 27 6 27C8.20914 27 10 28.7909 10 31C10 33.2091 8.20914 35 6 35C3.79086 35 2 33.2091 2 31ZM56.4823 26.02C56.769 26.08 57.0923 26.11 57.4523 26.11C58.099 26.11 58.629 26 59.0423 25.78C59.4623 25.56 59.7623 25.2833 59.9423 24.95C60.129 24.6166 60.2223 24.2666 60.2223 23.9C60.2223 23.5266 60.1256 23.2033 59.9323 22.93C59.739 22.65 59.509 22.4233 59.2423 22.25C58.9756 22.0766 58.6556 21.9 58.2823 21.72C57.8223 21.4933 57.4856 21.32 57.2723 21.2C57.059 21.0733 56.8756 20.92 56.7223 20.74C56.5756 20.56 56.5023 20.3466 56.5023 20.1C56.5023 19.7933 56.6023 19.55 56.8023 19.37C57.009 19.1833 57.3123 19.09 57.7123 19.09C57.9323 19.09 58.1323 19.12 58.3123 19.18C58.4923 19.2333 58.6123 19.29 58.6723 19.35L58.8823 20.58H59.6823L59.7423 18.83C59.6556 18.8166 59.5256 18.7866 59.3523 18.74C59.059 18.66 58.7856 18.5966 58.5323 18.55C58.2856 18.5033 57.999 18.48 57.6723 18.48C57.119 18.48 56.649 18.57 56.2623 18.75C55.8823 18.9233 55.5956 19.1633 55.4023 19.47C55.209 19.77 55.1123 20.1066 55.1123 20.48C55.1123 21.08 55.2923 21.5566 55.6523 21.91C56.019 22.2566 56.479 22.5566 57.0323 22.81C57.439 22.99 57.759 23.15 57.9923 23.29C58.2256 23.4233 58.409 23.5766 58.5423 23.75C58.6756 23.9166 58.7423 24.1166 58.7423 24.35C58.7423 24.7033 58.629 24.98 58.4023 25.18C58.1823 25.3733 57.849 25.47 57.4023 25.47C57.1157 25.47 56.859 25.4333 56.6323 25.36C56.4123 25.2866 56.259 25.2033 56.1723 25.11L55.9523 23.82H55.1423L55.0523 25.67C55.1323 25.6766 55.289 25.7133 55.5223 25.78C55.8756 25.88 56.1956 25.96 56.4823 26.02ZM41.1799 19.32L40.3999 19.19V18.57H41.8899C42.1166 18.57 42.4099 18.5566 42.7699 18.53C42.8566 18.5233 42.9599 18.5166 43.0799 18.51C43.1999 18.5033 43.3399 18.5 43.4999 18.5C44.5066 18.5 45.2099 18.6433 45.6099 18.93C46.0166 19.21 46.2199 19.63 46.2199 20.19C46.2199 20.6833 46.0699 21.0966 45.7699 21.43C45.4766 21.7566 45.0899 21.9566 44.6099 22.03C45.2699 22.0233 45.7866 22.1733 46.1599 22.48C46.5399 22.7866 46.7299 23.2133 46.7299 23.76C46.7299 24.4533 46.4866 25.01 45.9999 25.43C45.5132 25.85 44.6999 26.06 43.5599 26.06C43.2599 26.06 42.9832 26.05 42.7299 26.03C42.6632 26.03 42.5499 26.0233 42.3899 26.01C42.2299 26.0033 42.0666 26 41.8999 26H40.3899V25.4L41.1799 25.3V19.32ZM42.6299 21.81C42.7032 21.8166 42.8532 21.82 43.0799 21.82H43.5499C43.9566 21.82 44.2599 21.6933 44.4599 21.44C44.6599 21.1866 44.7599 20.83 44.7599 20.37C44.7599 19.93 44.6566 19.61 44.4499 19.41C44.2432 19.21 43.8999 19.11 43.4199 19.11C43.2932 19.11 43.0299 19.1233 42.6299 19.15V21.81ZM42.6299 25.34C42.7832 25.4 43.0766 25.43 43.5099 25.43C44.0766 25.43 44.4866 25.2966 44.7399 25.03C44.9932 24.7633 45.1199 24.3766 45.1199 23.87C45.1199 23.3833 45.0032 23.0366 44.7699 22.83C44.5432 22.6166 44.1799 22.51 43.6799 22.51C43.1199 22.51 42.7699 22.5166 42.6299 22.53V25.34ZM50.9007 26.08C49.8873 26.08 49.134 25.8266 48.6407 25.32C48.1473 24.8133 47.9007 24.0166 47.9007 22.93V19.32L47.2407 19.19V18.57H50.2607V19.19L49.4107 19.33V23.06C49.4107 24.6466 49.974 25.44 51.1007 25.44C51.654 25.44 52.064 25.2366 52.3307 24.83C52.5973 24.4166 52.7307 23.84 52.7307 23.1V19.33L51.8707 19.19V18.57H54.3707V19.19L53.6907 19.32V23.02C53.6907 24.06 53.4373 24.83 52.9307 25.33C52.4307 25.83 51.754 26.08 50.9007 26.08Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Constraints = NodeConstraints.Default &~NodeConstraints.InConnect
            };
            OtherSymbols.Add(BusShape);
            Node PullUp = new Node()
            {
                ID = "PullUp",
                Width = 73,
                Height = 45,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M32 0C31.4477 0 31 0.447715 31 1V22H11.917C11.441 19.1623 8.973 17 6 17C2.68629 17 0 19.6863 0 23C0 26.3137 2.68629 29 6 29C8.973 29 11.441 26.8377 11.917 24H31V46C31 46.5523 31.4477 47 32 47H71V24H88.083C88.559 26.8377 91.027 29 94 29C97.3137 29 100 26.3137 100 23C100 19.6863 97.3137 17 94 17C91.027 17 88.559 19.1623 88.083 22H71V0H32ZM33 45V2H69V45H33ZM54.8685 16L51 10.1972L47.1315 16H50V18.2792L44.6838 20.0513L44.8356 21.9864L52.093 23.196L44.7575 25.0299V26.9701L52.8769 29L44.7575 31.0299L44.8356 32.9864L50 33.8471V39H52V32.1529L49.907 31.804L57.2425 29.9701V28.0299L49.1231 26L57.2425 23.9701L57.1644 22.0136L49.1358 20.6755L52 19.7208V16H54.8685ZM51 13.8028L51.1315 14H50.8685L51 13.8028ZM2 23C2 20.7909 3.79086 19 6 19C8.20914 19 10 20.7909 10 23C10 25.2091 8.20914 27 6 27C3.79086 27 2 25.2091 2 23ZM90 23C90 20.7909 91.7909 19 94 19C96.2091 19 98 20.7909 98 23C98 25.2091 96.2091 27 94 27C91.7909 27 90 25.2091 90 23ZM39.4041 12.312C39.7561 12.232 40.0001 12.168 40.1361 12.12V17.172L38.6961 17.292V18H43.2681V17.292L41.9001 17.16V10.56L40.9521 10.428H40.9401C40.6441 10.676 40.2601 10.916 39.7881 11.148C39.3241 11.372 38.8761 11.528 38.4441 11.616L38.5761 12.456C38.7761 12.432 39.0521 12.384 39.4041 12.312Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Tooltip= new DiagramTooltip()
                {
                    Content="Pull Up"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            OtherSymbols.Add(PullUp);
            Node PullDown = new Node()
            {
                ID = "PullDown",
                Width = 73,
                Height = 45,
                Shape=new PathShape()
                {
                    Type=NodeShapes.Path,
                    Data="M70 0H31V22H11.917C11.441 19.1623 8.973 17 6 17C2.68629 17 0 19.6863 0 23C0 26.3137 2.68629 29 6 29C8.973 29 11.441 26.8377 11.917 24H31V47H70C70.5523 47 71 46.5523 71 46V24H88.083C88.559 26.8377 91.027 29 94 29C97.3137 29 100 26.3137 100 23C100 19.6863 97.3137 17 94 17C91.027 17 88.559 19.1623 88.083 22H71V1C71 0.447715 70.5523 0 70 0ZM33 45V2H69V45H33ZM52 18.8471V12H50V17.1529L44.8356 18.0136L44.7575 19.9701L52.8769 22L44.7575 24.0299V25.9701L52.093 27.804L44.8356 29.0136L44.6838 30.9487L50 32.7208V35H47.1315L51 40.8028L54.8685 35H52V31.2792L49.1358 30.3245L57.1644 28.9864L57.2425 27.0299L49.1231 25L57.2425 22.9701V21.0299L49.907 19.196L52 18.8471ZM2 23C2 20.7909 3.79086 19 6 19C8.20914 19 10 20.7909 10 23C10 25.2091 8.20914 27 6 27C3.79086 27 2 25.2091 2 23ZM90 23C90 20.7909 91.7909 19 94 19C96.2091 19 98 20.7909 98 23C98 25.2091 96.2091 27 94 27C91.7909 27 90 25.2091 90 23ZM62.1219 38.0959C61.4979 38.0959 60.9639 37.9679 60.5199 37.7119C60.0799 37.4559 59.7199 37.1079 59.4399 36.6679C59.1639 36.2239 58.9619 35.7219 58.8339 35.1619C58.7059 34.5979 58.6439 34.0099 58.6479 33.3979C58.6599 32.4979 58.8259 31.7179 59.1459 31.0579C59.4699 30.3939 59.9019 29.8819 60.4419 29.5219C60.9819 29.1619 61.5899 28.9819 62.2659 28.9819C62.8979 28.9819 63.4319 29.1019 63.8679 29.3419C64.3079 29.5779 64.6619 29.9039 64.9299 30.3199C65.2019 30.7359 65.3979 31.2159 65.5179 31.7599C65.6379 32.2999 65.6959 32.8739 65.6919 33.4819C65.6839 34.4099 65.5259 35.2199 65.2179 35.9119C64.9139 36.5999 64.4939 37.1359 63.9579 37.5199C63.4219 37.9039 62.8099 38.0959 62.1219 38.0959ZM62.1699 36.6919C62.6859 36.6959 63.0779 36.4419 63.3459 35.9299C63.6179 35.4179 63.7539 34.6219 63.7539 33.5419C63.7539 32.9019 63.6979 32.3459 63.5859 31.8739C63.4739 31.4019 63.3019 31.0359 63.0699 30.7759C62.8379 30.5159 62.5459 30.3859 62.1939 30.3859C61.6779 30.3859 61.2819 30.6339 61.0059 31.1299C60.7339 31.6259 60.5979 32.3659 60.5979 33.3499C60.5979 33.9859 60.6539 34.5559 60.7659 35.0599C60.8819 35.5599 61.0559 35.9559 61.2879 36.2479C61.5199 36.5399 61.8139 36.6879 62.1699 36.6919Z"
                },
                Style=new ShapeStyle()
                {
                    Fill="#000000",
                    StrokeWidth=0,
                },
                Tooltip=new DiagramTooltip()
                {
                    Content="Pull Down"
                },
                Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) &~NodeConstraints.InConnect
            };
            OtherSymbols.Add(PullDown);
        }

    }
}
