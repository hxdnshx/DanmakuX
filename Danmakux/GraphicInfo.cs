using System.Collections.Generic;
using Newtonsoft.Json;

namespace Danmakux
{
    public class GraphicInfo
    {
        [JsonProperty("character")]
        public string Character { get; set; }
        [JsonProperty("strokes")]
        public List<string> Strokes { get; set; }

        public struct Loc
        {
            [JsonProperty("x")]
            public int X { get; set; }
            
            [JsonProperty("Y")]
            public int Y { get; set; }
        }
    }
}