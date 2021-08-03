namespace Danmakux
{
    public class TextProperty
    {
        public float? x  {get; set;}
        public float? y  {get; set;}
        public string fillColor { get; set; }
        public float? fillAlpha  {get; set;}
        public string borderColor  {get; set;}
        public float? borderAlpha  {get; set;}
        public float? borderWidth  {get; set;}
        public int? rotateX  {get; set;}
        public int? rotateY  {get; set;}
        public float? rotateZ  {get; set;}
        public float? scale  {get; set;}
        public int? zIndex  {get; set;}
        public float? duration  {get; set;}
        public float? alpha  {get; set;}
        
        public float? anchorX { get; set; }
        
        public float? anchorY { get; set; }
        
        //别看了，这个不支持不锁定比例的拉伸，我为什么不用scale呢？
        /*
        public float? width { get; set; }
        
        public float? height { get; set; }
        */
    }
}