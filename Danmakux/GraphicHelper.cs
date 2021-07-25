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
            "def path pb {scale=0.1 borderWidth = 1 borderColor = 0xff11ff borderAlpha = 0.8 fillColor = 0x00a1d6 fillAlpha = 0.8 duration=30s}\ndef text cb {content = \"　\" fontSize = 10% anchorX = 0.5 anchorY = 0.5 rotateX = 180 duration=30s}\n\n";

        private static Regex mediansPattern1 = new Regex(@"(.*),""medians"":\[\[\[.*\]\]\](.*)");

        private StringBuilder builder = new StringBuilder();
        private List<char> preparedTxt = new List<char>();
        
        public GraphicHelper(string dataPath)
        {
            string[] data = File.ReadAllLines(dataPath);
            
            foreach (var graph in data)
            {
                var ignoreMedian = mediansPattern1.Replace(graph, "$1$2");
                var newGraphic = Newtonsoft.Json.JsonConvert.DeserializeObject<GraphicInfo>(ignoreMedian);
                graphicData.Add(newGraphic.Character.First(),newGraphic);
            }
        }

        public void Reset()
        {
            builder.Clear();
            preparedTxt.Clear();
            builder.Append(prev_template);
        }

        public void DefParent(string alias, string parent, TextProperty prop)
        {
            builder.Append($"def text {alias} {{");
            builder.Append($"content=\"　\" ");
            builder.Append($"anchorX=.5 ");
            builder.Append($"anchorY=.5 ");
            builder.Append($"fontSize=10% ");
            if (!string.IsNullOrEmpty(parent)) builder.Append($"parent=\"{parent}\" ");
            if (prop != null)
            {
                if (prop.x != null) builder.Append($",x={prop.x}%");
                if (prop.y != null) builder.Append($",y={prop.y}%");
                if (prop.rotateX != null) builder.Append($",rotateX={prop.rotateX}");
                if (prop.rotateY != null) builder.Append($",rotateY={prop.rotateY}");
                if (prop.rotateZ != null) builder.Append($",rotateZ={prop.rotateZ}");
                if (prop.scale != null) builder.Append($",scale={prop.scale}");
                if (prop.zIndex != null) builder.Append($",zIndex={prop.zIndex}");
                if (prop.duration != null) builder.Append($",duration={prop.duration}s");
                if (prop.alpha != null) builder.Append($",alpha={prop.alpha}");
                    
                builder.Append("}\n");
            }
        }

        public void AddText(string txt, string alias, string parent, TextProperty prop, 
            Action<TextProperty, int, int> onNextPart = null,
            Action<MotionHelper, TextProperty, int, int> onProcessMotion = null)
        {
            if (txt.Length == 0)
                return;
            bool isFirst = true;
            int chSeq = -1;
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
                            builder.Append($"let p{chIndex}_{partIndex}=pb{{d=\"{chGraphic.Strokes[partIndex]}\",alpha=0}}\n");
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
                    
                    builder.Append($"let {strokeName}=p{chIndex}_{partIndex}{{parent=\"{subContainerName}\" alpha=1");
                    if (prop.borderAlpha != null) builder.Append($" borderAlpha={prop.borderAlpha}");
                    if (prop.borderWidth != null) builder.Append($" borderWidth={prop.borderWidth}");
                    if (!string.IsNullOrEmpty(prop.borderColor)) builder.Append($" borderColor={prop.borderColor}");
                    if (!string.IsNullOrEmpty(prop.fillColor)) builder.Append($" fillColor={prop.fillColor}");
                    if (prop.fillAlpha != null) builder.Append($" borderWidth={prop.fillAlpha}");
                    builder.Append("}");
                    builder.Append($"let {subContainerName} = cb{{parent=\"{containerName}\"");
                    //这里的原因是因为scale属性一般是放在第二层，用于缩放本身的内容。如果anchor属性放在第一层可能无法取得预期的效果？
                    //感觉大概率会导致单个笔画的位置出错，先试试吧
                    if (prop.anchorX != null) builder.Append($",anchorX={prop.anchorX}");
                    if (prop.anchorY != null) builder.Append($",anchorY={prop.anchorY}");
                    builder.Append($"}}");
                    builder.Append($"let {containerName} = cb{{parent=\"{parent}\"");
                    
                    if (prop.x != null) builder.Append($",x={prop.x}%");
                    if (prop.y != null) builder.Append($",y={prop.y}%");
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
                        var motion = new MotionHelper(builder, containerName, subContainerName);
                        onProcessMotion(motion, prop, chSeq, partIndex);
                        motion.ProcessBackupLayer();
                    }
                }

                //let c_3_1 = p_ni1{parent = "b_3_1"} let b_3_1 = cb{parent = "c",x = 20%, y = -30%, rotateY = -30, alpha = 0}
                //set b_3_1 {} 0.1s then set b_3_1 {x = 20%, y = 0%, rotateY = 0, alpha = 1} 1s, "ease-out" then set b_3_1{} 2s
                //then set b_3_1 {x = 20%, y = 150%, rotateY = 30, alpha = 0} 2s, "ease-in"

            }
        }

        public string GetResult()
        {
            return builder.ToString();
        }
    }
}