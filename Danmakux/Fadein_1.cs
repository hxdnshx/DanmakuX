using System;
using System.Collections.Generic;

namespace Danmakux
{
    public class Fadein_1
    {
        static void Func(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, float duration)
        {
            
            int originalX = 0;
            Random w = new Random();
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x += w.Next(-10, 10);
                if (noChar % 2 == 0)
                    p.x = originalX + 40;
                else
                    p.x = originalX - 40;
                p.rotateY += 10;
                p.rotateY *= -1;
                p.y = 45 * noChar;
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = 0.08f * noStroke ;
                motion.Apply(offset + 0.2f * noChar)
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 1, rotateY = 0, 
                            x = originalX
                        }, "ease-out")
                    .Apply(2,new TextProperty())
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 0, rotateY = p.rotateY, 
                            x = (noChar % 2 == 0) ? (originalX + 40) : (originalX - 40)
                        }, "ease-in");
            });
        }

        public static void Func2(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float duration, float delay)
        {
            const int charDistance = 90;
            const int rotateDiff = 25;
            const float strokeOffset = 0.07f;
            const float charOffset = 0.23f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = -200 + charDistance * noChar;
                p.y = -100;
                p.alpha = 0;
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke ;
                motion.Apply(delay);
                motion.Apply(offset +  charOffset * noChar,new TextProperty()
                    {
                        x = -200 + charDistance * noChar,
                        rotateX = -360 + rotateDiff * noChar
                    })
                    .Apply(1, 
                    new TextProperty
                    {
                        alpha = 1,
                        x = p.x + 200,
                        y = 0,
                        rotateX = 0
                    }, "ease-out")
                    .Apply(duration - strokeOffset * noStroke * 0.6f)
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 0, 
                            x = p.x + 400,
                            y = 100,
                            rotateX = 360 - rotateDiff * noChar
                        }, "ease-in");
            });
            //说明：带Y轴旋转的从左侧渐入
        }

        private static Random _rng = new Random();
        
        //说明：横向的笔画渐入（带时间轴
        public static void Func3(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float duration, List<float> timeline)
        {
            const int charDistance = 50;
            const int rotateDiff = 25;
            const float strokeOffset = 0.07f;
            const float charOffset = 0f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = -200 + charDistance * noChar;
                p.alpha = 0;
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke ;
                motion.Apply(timeline[noChar]);
                motion.Apply(offset +  charOffset * noChar,new TextProperty()
                    {
                        x = -200 + charDistance * noChar + _rng.Next(-70, 70)
                    })
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 1,
                            x = charDistance * noChar,
                            y = 0,
                            rotateX = 0
                        }, "cubic-bezier(0,.6,.2,1)")
                    .Apply(duration - timeline[noChar] * 0.1f - strokeOffset * noStroke * 0.5f)
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 0, 
                            x = 200 + charDistance * noChar + _rng.Next(-70, 70)
                        }, "cubic-bezier(.2,0,.3,1)");
            });
        }
        
        //说明：横向的拉伸渐入（带时间轴
        public static void Func4(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float duration, List<float> timeline)
        {
            const int charDistance = 50;
            const int rotateDiff = 25;
            const float strokeOffset = 0.04f;
            const float charOffset = 0f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = charDistance * noChar;
                p.alpha = 0;
                p.rotateX = 90;//这个设定的是第一层
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke ;
                motion.Apply(timeline[noChar]);
                motion.Apply(offset +  charOffset * noChar,new TextProperty()
                    {
                        rotateX = 90, // Hidden
                        scale = 5, //这个设定的是第二层
                        alpha = 1
                    })
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 1,
                            rotateX = 0,
                            scale = 1
                        }, "cubic-bezier(0,.6,.2,1)")
                    .Apply(duration - timeline[noChar] * 0.1f)// - strokeOffset * noStroke * 0.6f
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 0,
                            rotateX = -90,
                            scale = 2f,
                        }, "cubic-bezier(0,.6,.4,1)");
            });
        }
        
        //说明：竖向的拉伸渐入（带时间轴
        public static void Func5(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float duration, List<float> timeline)
        {
            const int charDistance = 50;
            const int rotateDiff = 25;
            const float strokeOffset = 0.04f;
            const float charOffset = 0f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = charDistance * noChar;
                p.alpha = 0;
                p.rotateX = 0;
                p.rotateZ = 0;
                p.rotateY = 160;//这个设定的是第一层
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke ;
                motion.Apply(timeline[noChar]);
                motion.Apply(offset +  charOffset * noChar,new TextProperty()
                    {
                        rotateY = 160, // Hidden
                        scale = 5, //这个设定的是第二层
                        alpha = 0
                    })
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 1,
                            rotateY = 0,
                            scale = 1
                        }, "cubic-bezier(0,.6,.2,1)")
                    .Apply(duration - timeline[noChar] * 0.1f)// - strokeOffset * noStroke * 0.6f
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 0,
                            rotateY = -90,
                            scale = 2f,
                        }, "cubic-bezier(0,.6,.4,1)");
            });
        }

        static void Func3()
        {
            /*
             * TextProperty prop = new TextProperty();
            prop.x = -100;
            prop.y = -100;
            prop.duration = 7;
            prop.scale = 0.6f;
            prop.fillColor = "0xff6666";
            prop.alpha = 0.0f;
            Random w = new Random();
            int filt = 0;
            
            helper.AddText("水に横たう手持ち花火が","w1_", "w1", prop, (p, noChar, noStroke) =>
            { }, (motion, p, noChar, noStroke) =>
            {
                var offset = 0.0f * noStroke ;
                var curr = filt;
                const int xDistance = 80;
                var rndY = w.Next(-2, 2);
                filt++;
                motion.Apply(offset + 0.3f * noChar,new TextProperty()
                    {
                        x = xDistance * noChar + 200 * ((filt & 1) == 0 ? -1 : 1),
                        y = 75 * rndY,
                        rotateX = 0,
                        rotateY = 0
                    })
                    .Apply(0.4f, 
                    new TextProperty
                    {
                        alpha = 1,
                        x = xDistance * noChar + ((filt % 8) - 4) * 10
                    }, "ease-out")
                    .Apply(0.5f, 
                        new TextProperty
                        {
                            y = 0,
                            x = xDistance * noChar
                        }, "cubic-bezier(.5,0,1,.5)")
                    .Apply(2 - 0.035f * noStroke)
                    .Apply(1, 
                        new TextProperty
                        {
                            alpha = 0, 
                            x = xDistance * noChar + 400,
                            y = 100,
                            rotateX = 360 - 25 * noChar
                        }, "ease-in");
            });
            
            
            说明：将每个字组装起来
             */
        }
    }
}