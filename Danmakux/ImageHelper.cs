using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Danmakux
{
    public class ImageHelper
    {

        private string MoveToCenter(string svgInput)
        {
            var xOffset = Width / 2;
            var yOffset = Height / 2;
            
            StringBuilder strBuilder = new StringBuilder();
            ClipHelper.SvgVisitor(svgInput, (cmd, x, y, c1X, c1Y, c2X, c2Y) =>
            {
                strBuilder.Append(cmd);
                x = (x - xOffset) * 1;
                c1X = (c1X - xOffset) * 1;
                c2X = (c2X - xOffset) * 1;
                y = (y - yOffset) * 1;
                c1Y = (c1Y - yOffset) * 1;
                c2Y = (c2Y - yOffset) * 1;
                switch (cmd)
                {
                    case "Z":
                        break;
                    case "M":
                    case "L":
                        strBuilder.Append($"{x:F2} {y:F2}");
                        break;
                    case "Q":
                        //c1 == c2
                        strBuilder.Append($" {c1X:F2} {c1Y:F2}");
                        strBuilder.Append($" {x:F2} {y:F2}");
                        break;
                    case "C":
                        strBuilder.Append($" {c1X:F2} {c1Y:F2}");
                        strBuilder.Append($" {c2X:F2} {c2Y:F2}");
                        strBuilder.Append($" {x:F2} {y:F2}");
                        break;
                    default:
                        throw new InvalidDataException();
                }
            });
            return strBuilder.ToString();
        }
        
        public ImageHelper(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode svgInfo = doc.GetElementsByTagName("svg")[0];
            Width = float.Parse(svgInfo.Attributes["width"].Value.Replace("pt",""));
            Height = float.Parse(svgInfo.Attributes["height"].Value.Replace("pt",""));
            foreach (XmlNode node in doc.GetElementsByTagName("path"))
            {
                var color = node?.Attributes["fill"]?.Value;
                var path = node?.Attributes["d"]?.Value;
                var opacity = node?.Attributes["opacity"]?.Value;
                if (!string.IsNullOrEmpty(color) && !string.IsNullOrEmpty(path))
                {
                    path = MoveToCenter(path);
                    FillDatas.Add(new FillData()
                    {
                        FillColor = color?.Replace("#", "0x"),
                        Path = path,
                        Opacity = opacity
                    });
                }
            }
        }

        public void AddImage(GraphicHelper helper, string parent, TextProperty prop,
            Action<MotionHelper> onProcessMotion = null)
        {
            var alias = parent;
            var builder = helper.Builder;
            var containerName = $"{alias}_i";
            var subContainerName = $"{alias}_iX";
            builder.Append(
                "def text zb {content = \"　\" fontSize = 10% anchorX=.5 anchorY=.5 duration=999s x=50% y=50%}");
            builder.Append($"let {subContainerName} = zb{{parent=\"{containerName}\"");
            //这里的原因是因为scale属性一般是放在第二层，用于缩放本身的内容。如果anchor属性放在第一层可能无法取得预期的效果？
            //感觉大概率会导致单个笔画的位置出错，先试试吧
            if (prop.anchorX != null) builder.Append($",anchorX={prop.anchorX}");
            if (prop.anchorY != null) builder.Append($",anchorY={prop.anchorY}");
            builder.Append($"}}");
            builder.Append($"let {containerName} = zb{{parent=\"{parent}\"");
                    
            //这里直接集成GraphicHelper的zb定义。
            if (prop.x != null) builder.Append($",x={prop.x + 50}%");
            if (prop.y != null) builder.Append($",y={prop.y + 50}%");
            if (prop.rotateX != null) builder.Append($",rotateX={prop.rotateX}");
            if (prop.rotateY != null) builder.Append($",rotateY={prop.rotateY}");
            if (prop.rotateZ != null) builder.Append($",rotateZ={prop.rotateZ}");
            if (prop.scale != null) builder.Append($",scale={prop.scale}");
            if (prop.zIndex != null) builder.Append($",zIndex={prop.zIndex}");
            if (prop.duration != null) builder.Append($",duration={prop.duration}s");
            if (prop.alpha != null) builder.Append($",alpha={prop.alpha}");
                    
            builder.Append("}\n");
            if (onProcessMotion != null)
            {
                var motion = new MotionHelper(builder, containerName, subContainerName, "");
                onProcessMotion(motion);
                motion.ProcessBackupLayer();
            }

            builder.Append(
                $"def path {alias}_b {{width=100% x={-Width / 2 + 50:F1}% y={-Height / 2 + 50:F1}% viewBox=\"{-Width / 2:F1} {-Height / 2:F1} {Width:F1} {Height:F1}\" fillAlpha = 1 duration=999s}}\n");
            
            int seq = 0;
            foreach (var fillItem in FillDatas)
            {
                var strokeName = $"{alias}_{seq}";
                builder.Append($"let {strokeName}={alias}_b{{parent=\"{subContainerName}\" alpha={fillItem.Opacity} fillColor={fillItem.FillColor}");
                builder.Append($" d=\"{fillItem.Path}\"}}\n");
                seq++;
            }
            
        }

        public class FillData
        {
            public string FillColor { get; set; }
            public string Path { get; set; }
            
            public string Opacity { get; set; }
        }
        
        public float Width { get; set; }
        public float Height { get; set; }

        public List<FillData> FillDatas { get; } = new List<FillData>();
    }
}