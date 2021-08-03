using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Danmakux
{
    public class GraphicHelper
    {
        public Dictionary<char, GraphicInfo> graphicData = new Dictionary<char, GraphicInfo>();

        
        private static string prev_template =
            "def path pb {width=10% viewBox=\"-512.0 -512.0 1024 1024\" fillColor = 0x00a1d6 fillAlpha = 1 duration=30s}\ndef text cb {content = \"　\" fontSize = 10% anchorX=.5 anchorY=.5 duration=30s x=50% y=50%}\n\n";

        private static Regex mediansPattern1 = new Regex(@"(.*),""medians"":\[\[\[.*\]\]\](.*)");

        internal StringBuilder Builder = new StringBuilder();
        private List<char> preparedTxt = new List<char>();

        private static (float, float) GetBoundingBoxOffset(GraphicInfo info)
        {
            /*
             * 在 make mea hanzi 的原文档中是这样说明边界的：
             * strokes: List of SVG path data for each stroke of this character, ordered by proper stroke order. Each stroke is laid out on a 1024x1024 size coordinate system where:
                The upper-left corner is at position (0, 900).
                The lower-right corner is at position (1024, -124).
                Note that the y-axes DECREASES as you move downwards, which is strage! To display these paths properly, you should hide render them as follows:
             * 如果计算边界的话，会将单个汉字在整个方框中的位置丢失。
             */
            float xMax = 1024;
            float xMin = 0;
            float yMax = 900;
            float yMin = -124;
            info.Width = xMax - xMin;
            info.Height = yMax - yMin;
            return ((xMax + xMin) / 2, (yMax + yMin) / 2);
            
        }

        public static (float, float) GetRealBoundingBox(GraphicInfo info)
        {
            float xMax = Single.MinValue;
            float xMin = Single.MaxValue;
            float yMax = Single.MinValue;
            float yMin = Single.MaxValue;
            foreach (var stroke in info.Strokes)
            {
                ClipHelper.SvgVisitor(stroke, (_, x, y, _, _, _, _) =>
                {
                    if (x > xMax) xMax = x;
                    if (x < xMin) xMin = x;
                    if (y > yMax) yMax = y;
                    if (y < yMin) yMin = y;
                });
            }
            
            return ((xMax + xMin) / 2, (yMax + yMin) / 2);
        }

        private static void MoveToCenter(GraphicInfo info)
        {
            var (xOffset,yOffset) = GetBoundingBoxOffset(info);
            List<string> result = new List<string>();
            foreach (var stroke in info.Strokes)
            {
                StringBuilder strBuilder = new StringBuilder();
                ClipHelper.SvgVisitor(stroke, (cmd, x, y, c1X, c1Y, c2X, c2Y) =>
                {
                    strBuilder.Append(cmd);
                    x = (x - xOffset) * 1;
                    c1X = (c1X - xOffset) * 1;
                    c2X = (c2X - xOffset) * 1;
                    y = (y - yOffset) * -1;
                    c1Y = (c1Y - yOffset) * -1;
                    c2Y = (c2Y - yOffset) * -1;
                    switch (cmd)
                    {
                        case "Z":
                            break;
                        case "M":
                        case "L":
                            strBuilder.Append($"{x:F1} {y:F1}");
                            break;
                        case "Q":
                            //c1 == c2
                            strBuilder.Append($" {c1X:F1} {c1Y:F1}");
                            strBuilder.Append($" {x:F1} {y:F1}");
                            break;
                        case "C":
                            strBuilder.Append($" {c1X:F1} {c1Y:F1}");
                            strBuilder.Append($" {c2X:F1} {c2Y:F1}");
                            strBuilder.Append($" {x:F1} {y:F1}");
                            break;
                        default:
                            throw new InvalidDataException();
                    }
                });
                result.Add(strBuilder.ToString());
                strBuilder.Clear();
            }

            info.Strokes = result;
        }
        
        public GraphicHelper(string dataPath)
        {
            string[] data = File.ReadAllLines(dataPath);
            
            foreach (var graph in data)
            {
                var ignoreMedian = mediansPattern1.Replace(graph, "$1$2");
                var newGraphic = Newtonsoft.Json.JsonConvert.DeserializeObject<GraphicInfo>(ignoreMedian);
                MoveToCenter(newGraphic);
                graphicData.Add(newGraphic.Character.First(),newGraphic);
            }
        }

        public void Reset()
        {
            Builder.Clear();
            preparedTxt.Clear();
            Builder.Append(prev_template);
        }

        public void DefParent(string alias, string parent, TextProperty prop)
        {
            Builder.Append($"def text {alias} {{");
            Builder.Append($"content=\"　\" ");
            Builder.Append($"anchorX=.5 ");
            Builder.Append($"anchorY=.5 ");
            Builder.Append($"fontSize=10% ");
            if (!string.IsNullOrEmpty(parent)) Builder.Append($"parent=\"{parent}\" ");
            if (prop != null)
            {
                if (prop.x != null) Builder.Append($",x={prop.x}%");
                if (prop.y != null) Builder.Append($",y={prop.y}%");
                if (prop.rotateX != null) Builder.Append($",rotateX={prop.rotateX}");
                if (prop.rotateY != null) Builder.Append($",rotateY={prop.rotateY}");
                if (prop.rotateZ != null) Builder.Append($",rotateZ={prop.rotateZ}");
                if (prop.scale != null) Builder.Append($",scale={prop.scale}");
                if (prop.zIndex != null) Builder.Append($",zIndex={prop.zIndex}");
                if (prop.duration != null) Builder.Append($",duration={prop.duration}s");
                if (prop.alpha != null) Builder.Append($",alpha={prop.alpha}");
                if (prop.anchorX != null) Builder.Append($",anchorX={prop.anchorX}");
                if (prop.anchorY != null) Builder.Append($",anchorY={prop.anchorY}");
                    
                Builder.Append("}\n");
            }
        }

        public const string ScreenTxt = @"　　　　　　　　　　　　　　　　\n　\n　\n　\n　\n　\n　\n　\n　";
        public void DefScreenParent(string alias, string parent, TextProperty prop, 
            Action<MotionHelper> onProcessMotion = null, string txt = ScreenTxt)
        {
            string subAlias = $"{alias}X";
            
            
            Builder.Append($"def text {subAlias} {{");//?.Replace("　","█")
            Builder.Append($"content=\"{txt??"　"}\" ");
            //不需要anchor
            //Builder.Append($"anchorX=.5 ");
            //Builder.Append($"anchorY=.5 ");
            Builder.Append($"fontSize={100f/16f}% ");
            if (!string.IsNullOrEmpty(parent)) Builder.Append($"parent=\"{parent}\" ");
            
            if (prop != null)
            {
                if (prop.x != null) Builder.Append($",x={prop.x}%");
                if (prop.y != null) Builder.Append($",y={prop.y}%");
                if (prop.rotateX != null) Builder.Append($",rotateX={prop.rotateX}");
                if (prop.rotateY != null) Builder.Append($",rotateY={prop.rotateY}");
                if (prop.rotateZ != null) Builder.Append($",rotateZ={prop.rotateZ}");
                if (prop.scale != null) Builder.Append($",scale={prop.scale}%");
                if (prop.zIndex != null) Builder.Append($",zIndex={prop.zIndex}");
                if (prop.duration != null) Builder.Append($",duration={prop.duration}s");
                if (prop.alpha != null) Builder.Append($",alpha={prop.alpha}");
                if (prop.anchorX != null) Builder.Append($",anchorX={prop.anchorX}");
                if (prop.anchorY != null) Builder.Append($",anchorY={prop.anchorY}");
                    
                Builder.Append("}\n");
            }
            
            Builder.Append($"def text {alias} {{");
            Builder.Append($"content=\"{txt??"　"}\" ");
            //不需要anchor
            //Builder.Append($"anchorX=.5 ");
            //Builder.Append($"anchorY=.5 ");
            Builder.Append($"fontSize={100f/16f}% ");
            Builder.Append($",x=0%");
            Builder.Append($",y=0%,");
            if (prop.duration != null) Builder.Append($"duration={prop.duration}s,");
            Builder.Append($"parent=\"{subAlias}\" ");
            
            Builder.Append("}\n");
            
            

            if (onProcessMotion != null)
            {
                
                var motion = new MotionHelper(Builder, subAlias, alias, null);
                onProcessMotion?.Invoke(motion);
                motion.ProcessBackupLayer();
            }
        }

        public class StrokeContext
        {
            public GraphicInfo Info { get; set; }
        }

        public void Comment(string txt)
        {
            Builder.Append("//");
            Builder.Append(txt);
            Builder.Append("\n");
        }

        public void AddText(string txt, string alias, string parent, TextProperty prop,
            Action<TextProperty, int, int> onNextPart = null,
            Action<MotionHelper, TextProperty, int, int> onProcessMotion = null)
        {
            AddText(txt, alias, parent, prop, onNextPart,
                ((helper, property, arg3, arg4, arg5) => onProcessMotion?.Invoke(helper, property, arg3, arg4)));
        }
        public void AddText(string txt, string alias, string parent, TextProperty prop, 
            Action<TextProperty, int, int> onNextPart,
            Action<MotionHelper, TextProperty, int, int, StrokeContext> onProcessMotion)
        {
            if (txt.Length == 0)
                return;
            bool isFirst = true;
            int chSeq = -1;
            StrokeContext ctx = new StrokeContext();
            foreach (var ch in txt)
            {
                chSeq++;
                int chIndex = preparedTxt.IndexOf(ch);
                GraphicInfo chGraphic;
                if (chIndex == -1)
                {
                    if (graphicData.TryGetValue(ch, out chGraphic))
                    {
                        chIndex = preparedTxt.Count;
                        preparedTxt.Add(ch);
                        for (int partIndex = 0; partIndex < chGraphic.Strokes.Count; partIndex++)
                            Builder.Append($"let p{chIndex}_{partIndex}=pb{{d=\"{chGraphic.Strokes[partIndex]}\",alpha=0}}\n");
                        //TODO：对应每一个笔画计算中点，使其支持XY轴旋转。
                    }
                    else
                        continue;//No Char Available
                }

                chGraphic = graphicData[ch];

                for (int partIndex = 0; partIndex < chGraphic.Strokes.Count; partIndex++)
                {
                    onNextPart?.Invoke(prop, chSeq, partIndex);
                    var strokeName = $"{alias}_{chSeq}_c{partIndex}";
                    var containerName = $"{alias}_{chSeq}_t{partIndex}";
                    var subContainerName = $"{alias}_{chSeq}_b{partIndex}";
                    
                    Builder.Append($"let {strokeName}=p{chIndex}_{partIndex}{{parent=\"{subContainerName}\" alpha=1");
                    //builder.Append($"viewBox=\"{-chGraphic.Width / 2:F1} {-chGraphic.Height / 2:F1} {chGraphic.Width:F0} {chGraphic.Height:F0}\"");
                    if (prop.borderAlpha != null) Builder.Append($" borderAlpha={prop.borderAlpha}");
                    if (prop.borderWidth != null) Builder.Append($" borderWidth={prop.borderWidth}");
                    if (!string.IsNullOrEmpty(prop.borderColor)) Builder.Append($" borderColor={prop.borderColor}");
                    if (!string.IsNullOrEmpty(prop.fillColor)) Builder.Append($" fillColor={prop.fillColor}");
                    if (prop.fillAlpha != null) Builder.Append($" borderWidth={prop.fillAlpha}");
                    //if (prop.width != null) builder.Append($" width={prop.width}%");
                    //if (prop.height != null) builder.Append($" height={prop.height}%");
                    Builder.Append("}");
                    Builder.Append($"let {subContainerName} = cb{{parent=\"{containerName}\"");
                    //这里的原因是因为scale属性一般是放在第二层，用于缩放本身的内容。如果anchor属性放在第一层可能无法取得预期的效果？
                    //感觉大概率会导致单个笔画的位置出错，先试试吧
                    if (prop.anchorX != null) Builder.Append($",anchorX={prop.anchorX}");
                    if (prop.anchorY != null) Builder.Append($",anchorY={prop.anchorY}");
                    Builder.Append($"}}");
                    Builder.Append($"let {containerName} = cb{{parent=\"{parent}\"");
                    
                    //实际上因为anchor 移动的是文字，定位点依然在右上角，所以这里手动加上50%
                    if (prop.x != null) Builder.Append($",x={prop.x + 50}%");
                    if (prop.y != null) Builder.Append($",y={prop.y + 50}%");
                    if (prop.rotateX != null) Builder.Append($",rotateX={prop.rotateX}");
                    if (prop.rotateY != null) Builder.Append($",rotateY={prop.rotateY}");
                    if (prop.rotateZ != null) Builder.Append($",rotateZ={prop.rotateZ}");
                    if (prop.scale != null) Builder.Append($",scale={prop.scale}");
                    if (prop.zIndex != null) Builder.Append($",zIndex={prop.zIndex}");
                    if (prop.duration != null) Builder.Append($",duration={prop.duration}s");
                    if (prop.alpha != null) Builder.Append($",alpha={prop.alpha}");
                    
                    Builder.Append("}\n");
                    if (onProcessMotion != null)
                    {
                        var motion = new MotionHelper(Builder, containerName, subContainerName, strokeName);
                        ctx.Info = chGraphic;
                        onProcessMotion(motion, prop, chSeq, partIndex, ctx);
                        motion.ProcessBackupLayer();
                    }
                }

                //let c_3_1 = p_ni1{parent = "b_3_1"} let b_3_1 = cb{parent = "c",x = 20%, y = -30%, rotateY = -30, alpha = 0}
                //set b_3_1 {} 0.1s then set b_3_1 {x = 20%, y = 0%, rotateY = 0, alpha = 1} 1s, "ease-out" then set b_3_1{} 2s
                //then set b_3_1 {x = 20%, y = 150%, rotateY = 30, alpha = 0} 2s, "ease-in"

            }
        }

        public void DefMotion(string dst, Action<MotionHelper> onProcessMotion)
        {
            var motion = new MotionHelper(Builder, dst, null, null);
            onProcessMotion?.Invoke(motion);
            motion.ProcessBackupLayer();
        }

        public string GetResult()
        {
            return Builder.ToString();
        }
    }
}