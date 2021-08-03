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
        
        //说明：横向向右弹出。
        public static void Func6(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float delay, float duration)
        {
            const int charDistance = 20;
            const float strokeOffset = 0.002f;
            const float charOffset = 0.006f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = 0;
                p.alpha = 0;
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke  + charOffset * noChar ;
                motion.Apply(delay);
                motion.Apply(0.001f, new TextProperty {alpha = 1});
                motion.Apply(duration + offset, new TextProperty()
                {
                    x = charDistance * noChar
                },"cubic-bezier(0,.8,.2,1)");
            });
        }
        
        //说明：根据包围盒进行检测，如果是长方形则从对应的横向/纵向进入。
        //TODO: 带时间轴
        public static void Func7(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float direction, float effect, float duration, List<float> timeline)
        {
            int charDistance = (int)(100f * prop.scale??1.0);
            const float strokeOffset = 0.04f;
            const float charOffset = 0.1f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = 0;
                p.alpha = 0;
            }, (motion, p, noChar, noStroke, ctx) =>
            {
                var graphic = ctx.Info;
                var (width, height) = GraphicHelper.GetRealBoundingBox(graphic);
                width++;
                height++;
                var ratio = width / height;
                var offset = strokeOffset * noStroke  + timeline[noChar] ;
                var dstX = (float)(noChar * charDistance * Math.Cos(direction));
                var dstY = (float)(noChar * charDistance * Math.Sin(direction));
                var index = ((noChar * 5 + noStroke) % 2) * 2 - 1;
                
                motion.Apply(offset);
                motion.Apply(0.001f, new TextProperty {alpha = 1});
                if (ratio < 0.5) //竖线
                {
                    motion.Apply(0.001f, new TextProperty {y=index * 1000 + dstY, x = dstX});
                    motion.Apply(effect * 0.15f, new TextProperty()
                    {
                        y = index * 100 + dstY 
                    });
                    motion.Apply(effect * 0.85f, new TextProperty()
                    {
                        y = dstY
                    },"cubic-bezier(0,1,0,1)");
                }
                else if (ratio < 1.5) //一般
                {
                    motion.Apply(0.001f, new TextProperty {y=dstY, x = dstX});
                    motion.Apply(0.001f, new TextProperty {scale = 0,rotateZ = 720});
                    motion.Apply(effect, new TextProperty()
                    {
                        scale = prop.scale,
                        rotateZ = 0
                    },"cubic-bezier(0,1,0,1)");
                }
                else
                {//横线
                    motion.Apply(0.001f, new TextProperty {y=dstY, x = index * 1000 + dstX});
                    motion.Apply(effect * 0.15f, new TextProperty()
                    {
                        x = index * 100 + dstX
                    });
                    motion.Apply(effect * 0.85f, new TextProperty()
                    {
                        x = dstX
                    },"cubic-bezier(0,1,0,1)");
                }
                motion.Apply(duration - offset);
                motion.Apply(0.001f, new TextProperty {alpha = 0});
            });
        }
        
        //说明：横向向右弹出。带时间轴
        public static void Func8(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float effect, float duration, List<float> timeline)
        {
            const int charDistance = 30;
            const float strokeOffset = 0.04f;
            const float charOffset = 0f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = 0;
                p.alpha = 0;
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke ;
                motion.Apply(timeline[noChar] + offset);
                motion.Apply(0.001f, new TextProperty {alpha = 1});
                motion.Apply(effect + offset, new TextProperty()
                {
                    x = charDistance * noChar
                },"cubic-bezier(0,.8,.2,1)");
                motion.Apply(duration);
            });
        }

        public static void RandomMove(MotionHelper motion, Random rnd, float duration, float dstX, float dstY,
            float distanceX = 1f, float distanceY = 200f, bool isBackup = false)
        {
            for (;;)
            {
                if (duration <= 0)
                    break;
                float current = 1.0f;
                if (duration < 2.0f)
                {
                    distanceX = 0;
                    distanceY = 0;
                    current = duration;
                }

                float angle = (float) ((rnd.NextDouble() - 0.5f) * Math.PI);
                motion.Apply(current, new TextProperty
                {
                    x = dstX + distanceX * (float)Math.Cos(angle),
                    y = dstY + distanceY * (float)Math.Sin(angle),
                }, "cubic-bezier(.5,0,.5,1)", isBackup: isBackup);
                duration -= current;
            }
        }
        
        public static void RandomRot(MotionHelper motion, Random rnd, float duration, float dstRot,
             float distance = 3, bool isBackup = false)
        {
            for (;;)
            {
                if (duration <= 0)
                    break;
                float current = 0.75f;
                if (duration < 1.5f)
                {
                    distance = 0;
                    current = duration;
                }

                float value = (float)((rnd.NextDouble() - 0.5f) * distance);
                motion.Apply(current, new TextProperty
                {
                    rotateZ = (dstRot + value)
                }, "cubic-bezier(.5,0,.5,1)", isBackup: isBackup);
                duration -= current;
            }
        }
        
        
        //说明：横向向左弹出。带时间轴
        public static void Func9(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float effect, float duration, List<float> timeline)
        {
            const int charDistance = -30;
            const float strokeOffset = 0.04f;
            const float charOffset = 0f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = 0;
                p.alpha = 0;
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke ;
                motion.Apply(timeline[noChar] + offset);
                motion.Apply(0.001f, new TextProperty {alpha = 1});
                motion.Apply(effect + offset, new TextProperty()
                {
                    x = charDistance * noChar
                },"cubic-bezier(0,.8,.2,1)");
                motion.Apply(duration);
            });
        }
        
        //说明：横向笔划闪现。带时间轴
        public static void Func10(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float effect, List<float> timeline)
        {
            const int charDistance = 90;
            const int strokeDistance = 30;
            const float strokeOffset = 0.02f;
            const float charOffset = 0f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.x = charDistance * noChar + strokeDistance * noStroke;
                p.alpha = 0;
            }, (motion, p, noChar, noStroke) =>
            {
                var offset = strokeOffset * noStroke ;
                motion.Apply(timeline[noChar] + offset);
                motion.Apply(0.001f, new TextProperty {alpha = 1});
                motion.Apply(effect, new TextProperty()
                {
                    x = 30 + charDistance * noChar + strokeDistance * noStroke
                },"cubic-bezier(0,.8,.2,1)");   
                motion.Apply(0.001f, new TextProperty {alpha = 0});
            });
        }
        
        
        //说明：根据包围盒进行检测，如果是长方形则从对应的横向/纵向进入。
        //带字体大小分别设定，和行距处理的版本
        public static void Func11(GraphicHelper helper, string str,string alias, string parent, TextProperty prop, 
            float direction, float effect, float duration, List<float> timeline, List<float> fontSize)
        {
            const int lineSpacing = 25;
            int charLocation = 0;//(int)(lineSpacing + 100f * prop.scale??1.0);
            const float strokeOffset = 0.03f;
            const float charOffset = 0.1f;
            helper.AddText(str, alias, parent, prop, (p, noChar, noStroke) =>
            {
                p.alpha = 0;
                p.scale = fontSize[noChar];
                if (noChar > 0 && noStroke == 0)
                {
                    charLocation += (int) (lineSpacing + 0.5f * 50f * fontSize[noChar - 1] 
                                                       + 50f * fontSize[noChar]);
                }
            }, (motion, p, noChar, noStroke, ctx) =>
            {
                var graphic = ctx.Info;
                var (width, height) = GraphicHelper.GetRealBoundingBox(graphic);
                width++;
                height++;
                var ratio = width / height;
                var offset = strokeOffset * noStroke  + timeline[noChar] ;
                var dstX = (float)(charLocation * Math.Cos(direction));
                var dstY = (float)(charLocation * Math.Sin(direction));
                var index = ((noChar * 5 + noStroke) % 2) * 2 - 1;
                
                motion.Apply(offset);
                motion.Apply(0.001f, new TextProperty {alpha = 1});
                if (ratio < 0.5) //竖线
                {
                    motion.Apply(0.001f, new TextProperty {y=index * 1000 + dstY, x = dstX});
                    motion.Apply(effect * 0.15f, new TextProperty()
                    {
                        y = index * 100 + dstY 
                    });
                    motion.Apply(effect * 0.85f, new TextProperty()
                    {
                        y = dstY
                    },"cubic-bezier(0,1,0,1)");
                }
                else if (ratio < 1.5) //一般
                {
                    motion.Apply(0.001f, new TextProperty {y=dstY, x = dstX});
                    motion.Apply(0.001f, new TextProperty {scale = 0,rotateZ = 720});
                    motion.Apply(effect, new TextProperty()
                    {
                        scale = prop.scale,
                        rotateZ = 0
                    },"cubic-bezier(0,1,0,1)");
                }
                else
                {//横线
                    motion.Apply(0.001f, new TextProperty {y=dstY, x = index * 1000 + dstX});
                    motion.Apply(effect * 0.15f, new TextProperty()
                    {
                        x = index * 100 + dstX
                    });
                    motion.Apply(effect * 0.85f, new TextProperty()
                    {
                        x = dstX
                    },"cubic-bezier(0,1,0,1)");
                }
                motion.Apply(duration - offset - 0.100f);
                motion.Apply(0.001f, new TextProperty {alpha = 0});
                motion.Apply(0.02f, new TextProperty {alpha = 1});
                motion.Apply(0.02f, new TextProperty {alpha = 0});
                motion.Apply(0.02f, new TextProperty {alpha = 1});
                motion.Apply(0.02f, new TextProperty {alpha = 0});
                motion.Apply(0.02f, new TextProperty {alpha = .5f});
                motion.Apply(0.02f, new TextProperty {alpha = 0});
                motion.Apply(0.02f, new TextProperty {alpha = .5f});
                motion.Apply(0.02f, new TextProperty {alpha = 0});
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