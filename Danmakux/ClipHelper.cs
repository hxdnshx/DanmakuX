using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;

namespace Danmakux
{
    public static class ClipHelper
    {
        private static Regex svgPattern = new Regex(@"([A-Z])([ \t0-9\.,-]*)");
        
        /// <summary>
        /// 假定input是 X 方向向右， Y 方向向下空间里，大小 1024 x 1024 的一个路径，将 clipPath中的部分裁剪掉。
        /// 实际上 ImageSharp 的实现中，会转成点的集合，这样来看应该不需要特别在意对于之前 G / C 属性的还原
        /// </summary>
        /// <param name="inputSvg"></param>
        /// <param name="clipPath"></param>
        /// <returns></returns>
        public static List<string> Clip(string inputSvg, IPath clipPath)
        {
            PathBuilder builder = new PathBuilder();
            var pointList = svgPattern.Matches(inputSvg);
            float prevX = Single.NaN;
            float prevY = Single.NaN;
            var beginX = Single.NaN;
            var beginY = Single.NaN;
            foreach (Match match in pointList)
            {
                var pointType = match.Groups[1].Value;
                var pointArgs = match.Groups[2].Value.Trim().Split(new char[] {' ', ','},
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var currX = Single.NaN;
                var currY = Single.NaN;
                var ctl1X = Single.NaN;
                var ctl2X = Single.NaN;
                var ctl1Y = Single.NaN;
                var ctl2Y = Single.NaN;
                if (pointArgs.Length >= 6)
                {
                    //C CTLX1 CTLY1 CTLX2 CTLY2 X Y
                    ctl1X = float.Parse(pointArgs[0]);
                    ctl1Y = float.Parse(pointArgs[1]);
                    ctl2X = float.Parse(pointArgs[2]);
                    ctl2Y = float.Parse(pointArgs[3]);
                    currX = float.Parse(pointArgs[4]);
                    currY = float.Parse(pointArgs[5]);
                }
                else if (pointArgs.Length >= 4)
                {
                    //Q CTLX CTLY X Y
                    ctl1X = ctl2X = float.Parse(pointArgs[0]);
                    ctl1Y = ctl2Y = float.Parse(pointArgs[1]);
                    currX = float.Parse(pointArgs[2]);
                    currY = float.Parse(pointArgs[3]);
                }
                else if (pointArgs.Length >= 2)
                {
                    //L AAA BBB
                    currX = float.Parse(pointArgs[0]);
                    currY = float.Parse(pointArgs[1]);
                } 
                switch (pointType)
                {
                    case "Z":
                        if (float.IsNaN(prevY) || float.IsNaN(beginX))
                            throw new InvalidDataException();
                        builder.AddLine(prevX, prevY, beginX, beginY);
                        builder.CloseFigure();
                        break;
                    case "M":
                        if (float.IsNaN(currX) || float.IsNaN(currY))
                            throw new InvalidDataException();
                        builder.StartFigure();
                        beginX = currX;
                        beginY = currY;
                        break;
                    case "L":
                        if (float.IsNaN(prevX) || float.IsNaN(currY))
                            throw new InvalidDataException();
                        builder.AddLine(prevX, prevY, currX, currY);
                        break;
                    case "Q":
                    case "C":
                        if (float.IsNaN(prevX) || float.IsNaN(currY) || float.IsNaN(ctl2Y))
                            throw new InvalidDataException();
                        builder.AddBezier(new PointF(prevX, prevY), new PointF(ctl1X, ctl1Y),
                            new PointF(ctl2X, ctl2Y), new PointF(currX, currY));
                        break;
                }
                prevX = currX;
                prevY = currY;
            }

            var inputPath = builder.Build();
            var result = inputPath.Clip(clipPath);
            bool isFirst = true;
            var returnValue = result.Flatten().Select(path =>
            {
                StringBuilder resultStr = new StringBuilder();
                foreach (var element in path.Points.ToArray())
                {
                    resultStr.Append($"{(isFirst ? "M" : "L")} {(int)element.X} {(int)element.Y}");
                    isFirst = false;
                }

                resultStr.Append("Z");
                
                return resultStr.ToString();
            }).ToList();
            /*
            returnValue.AddRange(clipPath.Flatten().Select(path =>
            {
                StringBuilder resultStr = new StringBuilder();
                foreach (var element in path.Points.ToArray())
                {
                    resultStr.Append($"{(isFirst ? "M" : "L")} {(int)element.X} {(int)element.Y}");
                    isFirst = false;
                }

                return resultStr.ToString();
            }));
            */
            
            return returnValue;
        }

        public static void TestCase(GraphicHelper helper)
        {
            var graphic = helper.graphicData['残'];
            string origValue = string.Join(" ", graphic.Strokes);
            File.WriteAllText("origTxt.txt", origValue);
            var poly = new Polygon(new LinearLineSegment(
                new PointF(0, 0), new PointF(400, 0), 
                new PointF(0, 1024)));
            string resultValue = String.Join(" ", graphic.Strokes.Select(
                stroke => string.Join(" ", Clip(stroke, poly))));
            
            File.WriteAllText("resultTxt.txt", resultValue);
        }

        public static void ClipChar(GraphicHelper helper, string str)
        {
            var poly = new Polygon(new LinearLineSegment(
                new PointF(-400, -1024), new PointF(800, -1024), 
                new PointF(-400, 2048)));
            foreach (var ch in str)
            {
                var graphic = helper.graphicData[ch];
                List<string> resultStroke = new List<string>();
                foreach (var stroke in graphic.Strokes)
                {
                    resultStroke.AddRange(Clip(stroke, poly));
                }

                graphic.Strokes = resultStroke;
            }
        }
    }
}