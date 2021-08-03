using System;
using System.Collections.Generic;
using System.IO;
using QRCoder;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;

namespace Danmakux
{

    public static class ListHelper
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }

    partial class Program
    {

        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (args.Length < 1)
                return;
            string graphFile = args[0];
            if (!File.Exists(graphFile))
                return;

            GraphicHelper helper = new GraphicHelper(graphFile);
            
            helper.Reset();
            int originalX = 0;
            {
                TextProperty prop = new TextProperty();
                prop.duration = 7;
                prop.scale = 0.6f;
                prop.fillColor = "0xff6666";
                prop.alpha = 0.0f;
                Random w = new Random();
                int filt = 0;

                helper.AddText("水に横たう手持ち花火が", "w1_", "w1", prop, (p, noChar, noStroke) => { },
                    (motion, p, noChar, noStroke) =>
                    {
                        var offset = 0.0f * noStroke;
                        var curr = filt;
                        const int xDistance = 80;
                        var rndY = w.Next(-2, 2);
                        filt++;
                        motion.Apply(offset + 0.3f * noChar, new TextProperty()
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

                File.WriteAllText("result_script.txt", helper.GetResult());
            }
            //500
            Intro_Author(helper);

            #region kaeshi

            

            {//15.533 // 16000
                helper.Reset();
                helper.Comment("踵返し夏は溶け-15533");
                helper.DefParent("s1", null, new TextProperty()
                {
                    x = 10,
                    y = 10,
                    duration = 20,
                    scale = 0.18f,
                    alpha = 0.5f
                });
                
                TextProperty prop = new TextProperty();
                prop.duration = 10;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "踵返し夏は溶け", 3, "　手を引く影　", "s1", "s1", prop, 1.5f);
                
                
                helper.DefParent("z1", null, new TextProperty()
                {
                    x = 10,
                    y = 70,
                    duration = 20,
                    scale = 0.5f,
                });
                
                prop.x = 0;
                prop.y = 0;
                prop.duration = 11;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;
                Fadein_1.Func2(helper, "踵返し夏は溶け", "z1", "z1", prop, 4.5f, 0.5f);
                helper.DefParent("z2", null, new TextProperty()
                {
                    x = 10,
                    y = 80,
                    duration = 20,
                    scale = 0.5f,
                });
                Fadein_1.Func2(helper, "手を引く影", "z2", "z2", prop, 1.5f, 4.0f);
                
                File.WriteAllText("qr_1.txt", helper.GetResult());
            }
            #endregion

            #region nomichi

            

            {
                //22800
                helper.Reset();
                helper.Comment("日暮れの道-22800");
                helper.DefParent("s2", null, new TextProperty()
                {
                    x = 73,
                    y = 42,
                    duration = 20,
                    scale = 0.15f,
                    alpha = 0.5f
                });
                TextProperty prop = new TextProperty();
                prop.duration = 12;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "畦道　日暮れの道", 4.5f, "　優しい笑顔の　", "s2", "s2", prop, 2.5f);
                
                helper.DefParent("z3", null, new TextProperty()
                {
                    x = 70,
                    y = 25,
                    duration = 20,
                    scale = 0.5f,
                });
                
                
                Fadein_1.Func3(helper, "畦道　日暮れの道", "z3", "z3", prop, 4.5f, new List<float>
                {
                    .177f,
                    .543f,
                    1.438f,
                    1.891f,
                    2.105f,
                    2.469f,
                    2.813f,
                    3.000f,
                    3.955f
                });
                
                helper.DefParent("z4", null, new TextProperty()
                {
                    x = 70,
                    y = 32,
                    duration = 20,
                    scale = 0.5f,
                });
                
                Fadein_1.Func3(helper, "優しい笑顔の", "z4", "z4", prop, 1.26f, new List<float>
                {
                    4.497f,
                    5.234f,
                    5.692f,
                    5.932f,
                    6.386f,
                    6.936f,
                    7.310f
                });
                
                
                File.WriteAllText("qr_2.txt", helper.GetResult());
            }
            
            #endregion

            #region yumemichi

            

            {
                //29500 / 29800(txt)
                helper.Reset();
                helper.Comment("yumemichi-29500");
                helper.DefParent("s3", null, new TextProperty()
                {
                    x = 10,
                    y = 60,
                    duration = 20,
                    scale = 0.13f,
                });
                TextProperty prop = new TextProperty();
                prop.duration = 12;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "夢路は遠く果て", 2.2f, "　遥か唄の底　",1.9f ,"　水に横たう　", "s3", "s3", prop, 1.3f);
                
                File.WriteAllText("qr_3.txt", helper.GetResult());
                
                helper.Reset();
                
                helper.Comment("夢路は遠く果て-29800");
                
                helper.DefParent("z5", null, new TextProperty()
                {
                    x = 10,
                    y = 20,
                    duration = 20,
                    scale = 0.5f,
                });
                
                //由于特效也要时间，就提前0.2s吧？
                Fadein_1.Func4(helper, "夢路は遠く果て", "z5", "z5", prop, 3.5f, new List<float>
                {//Begin:30.0f
                    .8f,
                    1.044f,
                    1.313f,
                    1.445f,
                    1.760f,
                    2.046f,
                    2.519f,
                    2.634f,
                    2.921f,
                });
                
                helper.DefParent("z6", null, new TextProperty()
                {
                    x = 10,
                    y = 27.5f,
                    duration = 20,
                    scale = 0.5f,
                });
                
                Fadein_1.Func4(helper, "遥か唄の底", "z6", "z6", prop, 3.5f, new List<float>
                {//Begin:30.0f
                    2.995f,
                    3.500f,
                    3.756f,
                    3.952f,
                    4.209f,
                    4.594f,
                });
                
                helper.DefParent("z7", null, new TextProperty()
                {
                    x = 10,
                    y = 35,
                    duration = 20,
                    scale = 0.5f,
                });
                
                Fadein_1.Func4(helper, "水に横たう", "z7", "z7", prop, 3.5f, new List<float>
                {//Begin:30.0f
                    4.940f,
                    5.380f,
                    5.600f,
                    6.000f,
                    6.390f,
                    6.600f
                });
                
                File.WriteAllText("txt_3.txt", helper.GetResult());
            }
            
            #endregion

            #region hanabi

            

            {
                //Start at 35.700 /36000(txt)
                helper.Reset();
                helper.Comment("hanabi 35700");
                helper.DefParent("s4", null, new TextProperty()
                {
                    x = 80,
                    y = 10,
                    duration = 20,
                    scale = 0.15f,
                });
                TextProperty prop = new TextProperty();
                prop.duration = 14;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "手持ち花火が　　　", 3.2f, "今沈んでった　　　",1.9f ,"土へと還るみたいに", "s4", "s4", prop, 3.2f);
                
                
                File.WriteAllText("qr_4.txt", helper.GetResult());
                
                helper.Reset();
                
                helper.Comment("手持ち花火が 36000");
                
                helper.DefParent("z8", null, new TextProperty()
                {
                    x = 78,
                    y = 60,
                    duration = 20,
                    scale = 0.5f,
                });
                
                //由于特效也要时间，就提前0.2s吧？
                Fadein_1.Func5(helper, "手持ち花火が", "z8", "z8", prop, 3.5f, new List<float>
                {//Begin:36.0f
                    0.621f,
                    0.862f,
                    1.092f,
                    1.344f,
                    1.815f,
                    2.063f,
                    2.548f,
                });
                
                helper.DefParent("z9", null, new TextProperty()
                {
                    x = 78,
                    y = 67.5f,
                    duration = 20,
                    scale = 0.5f,
                });
                
                //由于特效也要时间，就提前0.2s吧？
                Fadein_1.Func5(helper, "今しずんでった", "z9", "z9", prop, 3.5f, new List<float>
                {//Begin:36.0f
                    3.015f,
                    4.118f, 
                    4.318f,
                    4.549f,
                    4.810f,
                    4.924f,
                    5.062f,
                    5.327f
                });
                
                helper.DefParent("A1", null, new TextProperty()
                {
                    x = 78,
                    y = 75,
                    duration = 20,
                    scale = 0.5f,
                });
                
                //由于特效也要时间，就提前0.2s吧？
                Fadein_1.Func5(helper, "土へと還るみたいに", "A1", "A1", prop, 2.8f, new List<float>
                {//Begin:36.0f
                    5.378f,
                    5.912f,
                    6.146f,
                    6.380f,
                    7.355f,
                    8.061f,
                    8.312f,
                    8.553f,
                    8.790f,
                });
                
                
                File.WriteAllText("text_4.txt", helper.GetResult());
            }
            
            #endregion
            
            #region Nokorunonara-46800~
            {
                //Start at 46.800
                helper.Reset();
                const float allBegin = 46.800f;
                const float allEnd = 53.689f;
                const string txtColor = "0x030303";
                Action<string> applyDuration = (string parent) =>
                {
                    helper.DefMotion(parent, motion =>
                    {
                        motion.Apply(allEnd - allBegin);
                        motion.Apply(0.05f, new TextProperty {alpha = 0});
                        motion.Apply(0.05f, new TextProperty {alpha = 1});
                        motion.Apply(0.05f, new TextProperty {alpha = 0});
                        motion.Apply(0.05f, new TextProperty {alpha = 1});
                        motion.Apply(0.05f, new TextProperty {alpha = 0});
                        motion.Apply(0.05f, new TextProperty {alpha = 0.5f});
                        motion.Apply(0.05f, new TextProperty {alpha = 0});
                        motion.Apply(0.05f, new TextProperty {alpha = 0.5f});
                        motion.Apply(0.05f, new TextProperty {alpha = 0});
                    });
                };
                #region Nokorunonara
                helper.Comment("残るのなら-46800");
                helper.DefParent("s5", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 0.21f,
                    scale = 1.0f
                });

                {
                    const float strokeOffset = 0.002f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("残", "s5", "s5", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 1;
                        p.y = -75;
                        p.rotateX = 0;
                        p.rotateY = 0;
                        p.rotateZ = 0;
                        p.scale = 1.0f;
                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(strokeOffset * noStroke);

                        motion.Apply(0.2f, new TextProperty
                        {
                            y = 100,
                            scale = 1.2f
                        }, "cubic-bezier(0,.6,.2,1)");
                    });
                }
                
                

                var seg1 = new LinearLineSegment(
                    new PointF(500, -700), new PointF(700, 700),
                    new PointF(-200, 700));
                var seg2 = new LinearLineSegment(
                    new PointF(500, -700), new PointF(-700, -700),
                    new PointF(-700, 700), new PointF(-200, 700));
                ClipHelper.ClipChar(helper, seg1, "残", "攒");
                ClipHelper.ClipChar(helper, seg2, "残", "撰");
                
                helper.DefParent("s6", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                applyDuration("s6");
                {
                    const int charDistance = 50;
                    const int rotateDiff = 25;
                    const float strokeOffset = 0.002f;
                    const float charOffset = 0f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("攒", "s6", "s6", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.2f);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = -150
                        },"cubic-bezier(0,1,0,1)", true);
                        
                        motion.Apply(10, isBackup: true);

                        motion.Apply(1f, new TextProperty
                        {
                            x = 200
                        }, "cubic-bezier(0,.5,0,1)");
                        
                        motion.ForceSetBackup(true);
                    });
                }
                
                
                helper.DefParent("s7", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                {
                    const int charDistance = 50;
                    const int rotateDiff = 25;
                    const float strokeOffset = 0.002f;
                    const float charOffset = 0f;

                    
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("撰", "s7", "s7", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.2f);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = 200,
                            alpha = 0
                        }, "cubic-bezier(0,.3,.1,1)");


                        motion.ForceSetBackup(true);
                    });
                }
                //Fin:53.400
                helper.DefParent("s8", null, new TextProperty()
                {
                    x = 75,
                    y = 45,
                    duration = 10f,
                    scale = 1.0f
                });
                applyDuration("s8");
                {
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 0.3f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    Fadein_1.Func8(helper, "█るのなら", "s8", "s8", prop, 1.0f, 6.4f,new List<float>{
                        47.104f - 46.800f,
                        47.157f - 46.800f,
                        47.400f - 46.800f,
                        47.667f - 46.800f,
                        47.910f - 46.800f
                    });
                }
                #endregion

                #region kioku

                const float kiBegin = 48.859f;
                const float kiDelay = kiBegin - allBegin;
                helper.DefParent("s9", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = kiDelay + 0.21f,
                    scale = 1.0f
                });
                {
                    const float strokeOffset = 0.002f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 0.0f;
                    helper.AddText("記", "s9", "s9", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.y = -75;
                        p.rotateX = 0;
                        p.rotateY = 0;
                        p.rotateZ = 0;
                        p.scale = 1.0f;
                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(kiDelay);
                        motion.Apply(0.001f, new TextProperty { alpha = 1});
                        motion.Apply(strokeOffset * noStroke);

                        motion.Apply(0.2f, new TextProperty
                        {
                            y = 100,
                            scale = 1.2f
                        }, "cubic-bezier(0,.6,.2,1)");
                    });
                }
                
                
                var seg3 = new LinearLineSegment(
                    new PointF(200, -700), new PointF(700, -700), 
                    new PointF(700, 700), new PointF(-500, 700));
                var seg4 = new LinearLineSegment(
                    new PointF(200, -700), new PointF(-700, -700),
                    new PointF(-700, 700), new PointF(-500, 700));
                ClipHelper.ClipChar(helper, seg4, "記", "基");
                ClipHelper.ClipChar(helper, seg3, "記", "集");
                
                
                helper.DefParent("sa", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                
                applyDuration("sa");
                {
                    const float strokeOffset = 0.002f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("基", "sa", "sa", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.2f + kiDelay);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = - 25
                        },"cubic-bezier(0,1,0,1)", true);
                        
                        motion.Apply(10, isBackup: true);

                        motion.Apply(1f, new TextProperty
                        {
                            x = 350
                        }, "cubic-bezier(0,.5,0,1)");
                        
                        motion.ForceSetBackup(true);
                    });
                }
                
                
                helper.DefParent("sb", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                {
                    const float strokeOffset = 0.002f;
                    
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("集", "sb", "sb", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.2f + kiDelay);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = 200,
                            alpha = 0
                        }, "cubic-bezier(0,.3,.1,1)");


                        motion.ForceSetBackup(true);
                    });
                }

                //Fin:53.400
                helper.DefParent("sc", null, new TextProperty()
                {
                    x = 80,
                    y = 60,
                    duration = 10f,
                    scale = 1.0f
                });
                
                applyDuration("sc");
                {
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 0.3f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    Fadein_1.Func9(helper, "█憶だけじゃ嫌だと", "sc", "sc", prop, 1.0f, 6.4f,new List<float>{
                        49.129f - 46.800f,
                        49.588f - 46.800f,
                        50.561f - 46.800f,
                        51.024f - 46.800f,
                        51.466f - 46.800f,
                        51.681f - 46.800f,
                        51.994f - 46.800f,
                        52.952f - 46.800f,
                        53.437f - 46.800f
                    });
                }
                #endregion
                File.WriteAllText("text_5.txt", helper.GetResult());
            }
            
            #endregion
            
            
            #region negaiikoto-54387~
            {
                //Start at 46.800
                helper.Reset();
                helper.Comment("沙汰止んだ願い事-54387");
                const float allBegin = 54.287f;
                const float allEnd = 60.628f;
                const string txtColor = "0x030303";
                Action<string> applyDuration = (string parent) =>
                {
                    helper.DefMotion(parent, motion =>
                    {
                        motion.Apply(allEnd - allBegin);
                        motion.Apply(0.05f, new TextProperty {alpha = 0});
                    });
                };
                #region satarann
                helper.DefParent("s5", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 0.21f,
                    scale = 1.0f
                });

                {
                    const float strokeOffset = 0.002f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("沙", "s5", "s5", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 1;
                        p.y = -75;
                        p.rotateX = 0;
                        p.rotateY = 0;
                        p.rotateZ = 0;
                        p.scale = 1.0f;
                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(strokeOffset * noStroke);

                        motion.Apply(0.1f, new TextProperty
                        {
                            y = 100,
                            scale = 1.2f
                        }, "cubic-bezier(0,.6,.2,1)");
                    });
                }
                
                

                var seg1 = new LinearLineSegment(
                    new PointF(500, -700), new PointF(700, 700),
                    new PointF(-200, 700));
                var seg2 = new LinearLineSegment(
                    new PointF(500, -700), new PointF(-700, -700),
                    new PointF(-700, 700), new PointF(-200, 700));
                ClipHelper.ClipChar(helper, seg1, "沙", "莎");
                ClipHelper.ClipChar(helper, seg2, "沙", "啥");
                
                helper.DefParent("s6", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                applyDuration("s6");
                {
                    const int charDistance = 50;
                    const int rotateDiff = 25;
                    const float strokeOffset = 0.002f;
                    const float charOffset = 0f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("莎", "s6", "s6", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.1f);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = -150
                        },"cubic-bezier(0,1,0,1)", true);
                        
                        motion.Apply(10, isBackup: true);

                        motion.Apply(1f, new TextProperty
                        {
                            x = -400
                        }, "cubic-bezier(0,.5,0,1)");
                        
                        motion.ForceSetBackup(true);
                    });
                }
                
                
                helper.DefParent("s7", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                {
                    const int charDistance = 50;
                    const int rotateDiff = 25;
                    const float strokeOffset = 0.002f;
                    const float charOffset = 0f;

                    
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("啥", "s7", "s7", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.1f);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = 200,
                            alpha = 0
                        }, "cubic-bezier(0,.3,.1,1)");


                        motion.ForceSetBackup(true);
                    });
                }
                //Fin:53.400
                helper.DefParent("s8", null, new TextProperty()
                {
                    x = 15,
                    y = 48,
                    duration = 10f,
                    scale = 1.0f
                });
                applyDuration("s8");
                {
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 0.3f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    Fadein_1.Func8(helper, "█汰止んだ願い事", "s8", "s8", prop, 1.0f, 6.4f,new List<float>{
                        54.487f - allBegin,
                        54.630f - allBegin,
                        54.881f - allBegin,
                        54.986f - allBegin,
                        55.221f - allBegin,
                        55.585f - allBegin,
                        56.290f - allBegin,
                        56.784f - allBegin,
                    });
                }
                #endregion

                #region kioku

                const float kiBegin = 57.751f;
                const float kiDelay = kiBegin - allBegin;
                helper.DefParent("s9", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = kiDelay + 0.21f,
                    scale = 1.0f
                });
                {
                    const float strokeOffset = 0.002f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 0.0f;
                    helper.AddText("石", "s9", "s9", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.y = -75;
                        p.rotateX = 0;
                        p.rotateY = 0;
                        p.rotateZ = 0;
                        p.scale = 1.0f;
                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(kiDelay);
                        motion.Apply(0.001f, new TextProperty { alpha = 1});
                        motion.Apply(strokeOffset * noStroke);

                        motion.Apply(0.2f, new TextProperty
                        {
                            y = 100,
                            scale = 1.2f
                        }, "cubic-bezier(0,.6,.2,1)");
                    });
                }
                
                
                var seg3 = new LinearLineSegment(
                    new PointF(200, -700), new PointF(700, -700), 
                    new PointF(700, 700), new PointF(-500, 700));
                var seg4 = new LinearLineSegment(
                    new PointF(200, -700), new PointF(-700, -700),
                    new PointF(-700, 700), new PointF(-500, 700));
                ClipHelper.ClipChar(helper, seg4, "石", "⑩");
                ClipHelper.ClipChar(helper, seg3, "石", "势");
                
                
                helper.DefParent("sa", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                
                applyDuration("sa");
                {
                    const float strokeOffset = 0.002f;
                        
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("⑩", "sa", "sa", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.2f + kiDelay);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = - 25
                        },"cubic-bezier(0,1,0,1)", true);
                        
                        motion.Apply(10, isBackup: true);

                        motion.Apply(1f, new TextProperty
                        {
                            x = -250
                        }, "cubic-bezier(0,.5,0,1)");
                        
                        motion.ForceSetBackup(true);
                    });
                }
                
                
                helper.DefParent("sb", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 10f,
                    scale = 1.0f
                });
                {
                    const float strokeOffset = 0.002f;
                    
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 1.0f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    helper.AddText("势", "sb", "sb", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 0;
                        p.y = 100;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(.2f + kiDelay);
                        motion.Apply(strokeOffset * noStroke, new TextProperty
                        {
                            alpha = 1
                        });

                        motion.Apply(1f, new TextProperty
                        {
                            y = 200,
                            alpha = 0
                        }, "cubic-bezier(0,.3,.1,1)");


                        motion.ForceSetBackup(true);
                    });
                }

                //Fin:53.400
                helper.DefParent("sc", null, new TextProperty()
                {
                    x = 20,
                    y = 60,
                    duration = 10f,
                    scale = 1.0f
                });
                
                applyDuration("sc");
                {
                    TextProperty prop = new TextProperty();
                    prop.duration = 14;
                    prop.scale = 0.3f;
                    prop.fillColor = txtColor;
                    prop.alpha = 1.0f;
                    Fadein_1.Func9(helper, "█の下に", "sc", "sc", prop, 1.0f, 6.4f,new List<float>{
                        57.951f - allBegin,
                        58.464f - allBegin,
                        58.707f - allBegin,
                        59.428f - allBegin
                    });
                }
                #endregion
                File.WriteAllText("text_6.txt", helper.GetResult());
            }
            
            #endregion

            #region omoidasu

            {
                //Start at 60.389
                helper.Reset();
                helper.Comment("思い出す-60.389");
                const float allBegin = 60.389f;
                const float allEnd = 61.560f;
                const string backgroundColor = "0xefefef";
                const string foregroundColor = "0x030303";
                
                helper.DefParent("A", null, new TextProperty()
                {
                    x = 40,
                    y = 50,
                    duration = allEnd - allBegin,
                    scale = 1.0f,
                });
                
                TextProperty prop = new TextProperty();
                prop.duration = 46.1f;
                prop.y = 0f;
                prop.fillColor = backgroundColor;
                prop.scale = 0.6f;
                prop.alpha = 1.0f;

                helper.AddText("█", $"ABG", "A", new TextProperty
                {
                    scale = 50,
                    fillColor = backgroundColor,
                });

                prop.fillColor = foregroundColor;
                prop.scale = 0.6f;
                
                Fadein_1.Func7(helper, "思い出す", "A", "A", prop, 0f, 0.8f, 1f, new List<float>
                {
                    60.389f - allBegin,
                    60.870f - allBegin - 0.1f,
                    61.094f - allBegin - 0.1f,
                    61.341f - allBegin - 0.1f,
                });
                prop.scale = 8f;
                Fadein_1.Func10(helper, "思い出す", "B", "A", prop, 0.02f, new List<float>
                {
                    60.389f - allBegin,
                    60.870f - allBegin - 0.1f,
                    61.094f - allBegin - 0.15f,
                    61.341f - allBegin - 0.2f,
                });
                
                
                File.WriteAllText("tween03.txt", helper.GetResult());
            }


            #endregion

            #region Background

            

            {
                helper.Reset();
                ImageHelper img = new ImageHelper(@"D:\4.svg");
                ImageHelper img2 = new ImageHelper(@"D:\2.svg");
                ImageHelper border = new ImageHelper(@"D:\border.svg");
                ImageHelper border1 = new ImageHelper(@"D:\part-1-border.svg");
                ImageHelper border2 = new ImageHelper(@"D:\part-2-border.svg");
                ImageHelper border3 = new ImageHelper(@"D:\part-3-border.svg");
                const float part3Time = 30.771f;
                
                helper.DefParent("s7", null, new TextProperty()
                {
                    x = 50,
                    y = 50f,
                    duration = 46.1f,
                    scale = 1f
                });
                
                helper.DefMotion("s7", motion =>
                {
                    motion.Apply(12.8f)
                        .Apply(1f, new TextProperty {scale = 0.8f}, "cubic-bezier(0,.3,.7,1)")//
                        .Apply(0.001f, new TextProperty{scale = 1})
                        .Apply(1f, new TextProperty{scale = 0.8f}, "cubic-bezier(0,.3,.7,1)")
                            .Apply(0.001f, new TextProperty{scale = 1});
                });
                
                /*
                 * set s7{} 4.8s
                    then set s7{scale=0.8} 1s
                    then set s7{scale=1.0} 0.001s
                    then set s7{scale=0.8} 1s
                    需要对父节点做scale才能比较好达到最终的效果
                    
                 */
                
                var rnd = new Random(123);
                
                TextProperty prop = new TextProperty();
                prop.duration = 46.1f;
                prop.x = 0f;
                prop.scale = 1.2f;
                prop.alpha = 1.0f;
                img.AddImage(helper, "s7", prop, motion =>
                {
                    motion.Apply(999.0f, null, isBackup: true);
                    IntroMotion(motion, rnd, 6);
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = 330, y = 100});
                    //14.8
                    motion.Apply(2.9f, new TextProperty()
                    {x = 280 },"cubic-bezier(0,.6,.6,1)");
                    motion.Apply(2.9001f, new TextProperty()
                        {}, isBackup: true);

                    //23.0
                    Fadein_1.RandomMove(motion, rnd, 23.0f - 17.7f, 280, 100, 10f, 10f, false);
                    Fadein_1.RandomRot(motion, rnd, 23.0f - 17.7f, 0, 1f, true);
                    //motion.Apply(23.0f - 17.7f, isBackup: true);
                    //motion.Apply(23.0f - 17.7f, isBackup: false);
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = -300});
                    motion.Apply(2.9f, new TextProperty()
                        {x = -250 },"cubic-bezier(0,.6,.6,1)");
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = 100}, isBackup: true);
                    motion.Apply(2.9000f, new TextProperty()
                        {}, isBackup: true);
                    
                    //Part3
                    Fadein_1.RandomMove(motion, rnd, part3Time - 23.0f - 2.9f, -250, 100, 10f, 10f, false);
                    Fadein_1.RandomRot(motion, rnd, part3Time - 23.0f - 2.9f, 0, 1f, true);
                    //motion.Apply(part3Time - 23.0f - 2.9f);
                    //motion.Apply(part3Time - 23.0f - 2.9f, isBackup: true);
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = -50});
                    motion.Apply(0.001f, new TextProperty()
                        {scale = 1.6f});
                    motion.Apply(2.9f, new TextProperty()
                        {scale = 1.1f},"cubic-bezier(0,.7,.5,1)");
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = 130}, isBackup: true);
                    motion.Apply(2.9001f, isBackup: true);
                    
                    
                    Fadein_1.RandomMove(motion, rnd, 10f, -50, 100, 10f, 10f, false);
                    Fadein_1.RandomRot(motion, rnd, 10f, 0, 1f, true);
                    
                    motion.ForceSetBackup(true);
                },xalias:"S7",fixBorder: true,width:120);

                rnd = new Random(123);
                
                img2.AddImage(helper, "s7", prop, motion =>
                {
                    motion.Apply(999.0f, null, isBackup: true);
                    IntroMotion(motion, rnd, 6);
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = 330, y = 100});
                    //14.8
                    motion.Apply(2.9f, new TextProperty()
                    {x = 280 },"cubic-bezier(0,.6,.6,1)");
                    motion.Apply(2.9001f, new TextProperty()
                        {}, isBackup: true);

                    //23.0
                    Fadein_1.RandomMove(motion, rnd, 23.0f - 17.7f, 280, 100, 10f, 10f, false);
                    Fadein_1.RandomRot(motion, rnd, 23.0f - 17.7f, 0, 1f, true);
                    //motion.Apply(23.0f - 17.7f, isBackup: true);
                    //motion.Apply(23.0f - 17.7f, isBackup: false);
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = -300});
                    motion.Apply(2.9f, new TextProperty()
                        {x = -250 },"cubic-bezier(0,.6,.6,1)");
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = 100}, isBackup: true);
                    motion.Apply(2.9000f, new TextProperty()
                        {}, isBackup: true);
                    
                    //Part3
                    Fadein_1.RandomMove(motion, rnd, part3Time - 23.0f - 2.9f, -250, 100, 10f, 10f, false);
                    Fadein_1.RandomRot(motion, rnd, part3Time - 23.0f - 2.9f, 0, 1f, true);
                    //motion.Apply(part3Time - 23.0f - 2.9f);
                    //motion.Apply(part3Time - 23.0f - 2.9f, isBackup: true);
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = -50});
                    motion.Apply(0.001f, new TextProperty()
                        {scale = 1.6f});
                    motion.Apply(2.9f, new TextProperty()
                        {scale = 1.1f},"cubic-bezier(0,.7,.5,1)");
                    
                    motion.Apply(0.001f, new TextProperty()
                        {x = 130}, isBackup: true);
                    motion.Apply(2.9001f, isBackup: true);
                    
                    
                    Fadein_1.RandomMove(motion, rnd, 10f, -50, 100, 10f, 10f, false);
                    Fadein_1.RandomRot(motion, rnd, 10f, 0, 1f, true);
                    
                    motion.ForceSetBackup(true);
                },fixBorder: false,width:120);
                
                
                helper.DefParent("bd1", null, new TextProperty()
                {
                    x = 53,//53
                    y = 47.6f,
                    duration = 46.1f,
                    zIndex = 3,
                    scale = 1.2f
                });
                border1.AddImage(helper, "bd1", new TextProperty
                {
                    duration = 23.0f,
                    zIndex = 3,
                    x = 50,
                    alpha = 0
                }, motion =>
                {
                    motion.Apply(14.8f);//14.8
                    motion.Apply(0.001f, new TextProperty{alpha = 1});
                    motion.Apply(2.5f, new TextProperty
                        {
                            x = 0
                        }, "cubic-bezier(0,.6,.6,1)");
                    motion.Apply(23.0f - 14.8f - 2.5f);
                    motion.Apply(0.001f, new TextProperty
                    {alpha = 0});
                });
                
                helper.DefParent("bd2", null, new TextProperty()
                {
                    x = 53,//53
                    y = 47.6f,
                    duration = 46.1f,
                    zIndex = 2,
                    scale = 1.2f
                });
                border2.AddImage(helper, "bd2", new TextProperty
                {
                    duration = part3Time,
                    zIndex = 2,
                    x = -150,
                    alpha = 0
                }, motion =>
                {
                    motion.Apply(23.0f);//14.8
                    motion.Apply(0.001f, new TextProperty{alpha = 1});
                    motion.Apply(2.5f, new TextProperty
                    {
                        x = -100
                    }, "cubic-bezier(0,.6,.6,1)");
                    motion.Apply(part3Time - 23.0f - 2.5f);
                    motion.Apply(0.001f, new TextProperty {alpha = 0});
                });
                
                helper.DefParent("bd3", null, new TextProperty()
                {
                    x = 53,//53
                    y = 47.6f,
                    duration = 46.1f,
                    zIndex = 2,
                    scale = 1.2f
                });
                //
                border3.AddImage(helper, "bd3", new TextProperty
                {
                    duration = 46.1f,
                    zIndex = 2,
                    x = 0,
                    alpha = 0,
                    scale = 1.5f
                }, motion =>
                {
                    motion.Apply(part3Time);
                    motion.Apply(0.001f, new TextProperty{alpha = 1});
                    motion.Apply(0.001f, new TextProperty{x = -50});
                    motion.Apply(2.5f, new TextProperty
                    {
                        scale = 1
                    }, "cubic-bezier(0,.4,.7,1)");
                });
                
                helper.DefParent("BD", null, new TextProperty()
                {
                    x = 53,
                    y = 47.6f,
                    duration = 46.1f,
                    zIndex = 3,
                    scale = 1.2f
                });
                border.AddImage(helper, "BD", new TextProperty
                {
                    duration = 46.1f,
                    zIndex = 3
                });
                
                File.WriteAllText("img.txt", helper.GetResult());
            }
            
            #endregion

            #region Tween01-30384
            {
                helper.Reset();
                //30.384 - 200ms
                helper.Comment("Tween1-30384");
                helper.DefParent("tw1", null, new TextProperty()
                {
                    x = 20,//53
                    y = 30f,
                    duration = 30.871f - 30.384f + 0.2f,
                    zIndex = 3,
                });
                
                TextProperty prop = new TextProperty();
                prop.duration = 46.1f;
                prop.y = 0f;
                prop.fillColor = "0xefefef";
                prop.scale = 0.6f;
                prop.alpha = 1.0f;

                helper.AddText("█", $"wBG", "tw1", new TextProperty
                {
                    scale = 50,
                    fillColor = "0x020202",
                });

                Fadein_1.Func6(helper, "　　　　　　　　　　　　　　　　Mum", "wA", "tw1", prop, 0.2f, 1f);
                prop.y = 50f;
                Fadein_1.Func6(helper, "　　　　　　　　adj", "wB", "tw1", prop, 0.2f, 1f);
                prop.y = 100f;
                Fadein_1.Func6(helper, "　　　Not verbalizing　　silent", "wC", "tw1", prop, 0.2f, 1f);
                prop.y = 150f;
                Fadein_1.Func6(helper, "　　　　　　　　interj", "wD", "tw1", prop, 0.2f, 1f);
                prop.y = 200f;
                Fadein_1.Func6(helper, "　　　Used as a command to stop speaking", "wE", "tw1", prop, 0.2f, 0.5f);
                prop.y = 500f;
                
                File.WriteAllText("tween1.txt", helper.GetResult());
            }
            #endregion
            
            #region Tween02-45266
            {
                // 这个月结束得好快
                helper.Reset();
                helper.Comment("Tween02-45266");
                //45.265 - 200ms
                helper.DefParent("tw1", null, new TextProperty()
                {
                    x = 20,//53
                    y = 30f,
                    duration = 46.217f - 45.265f + 0.2f,
                    zIndex = 3,
                });
                
                TextProperty prop = new TextProperty();
                prop.duration = 46.1f;
                prop.y = 0f;
                prop.fillColor = "0xfdfdfd";
                prop.scale = 0.6f;
                prop.alpha = 1.0f;

                helper.AddText("█", $"wBG", "tw1", new TextProperty
                {
                    scale = 50,
                    fillColor = "0x010101",
                });

                Fadein_1.Func6(helper, "I want to see you", "wA", "tw1", prop, 0.2f, 1f);
                prop.y = 50f;
                Fadein_1.Func6(helper, "Hana to nare", "wB", "tw1", prop, 0.2f, 1f);
                prop.y = 100f;
                Fadein_1.Func6(helper, "Loop", "wC", "tw1", prop, 0.2f, 1f);
                prop.y = 150f;
                Fadein_1.Func6(helper, "PaIII  REVOLUTION", "wD", "tw1", prop, 0.2f, 1f);
                prop.y = 200f;
                Fadein_1.Func6(helper, "Ziqqurat", "wE", "tw1", prop, 0.2f, 0.5f);
                prop.y = 250f;
                Fadein_1.Func6(helper, "Blank", "wF", "tw1", prop, 0.2f, 0.5f);
                
                helper.DefParent("tw2", null, new TextProperty()
                {
                    x = 60,//53
                    y = 30f,
                    duration = 46.217f - 45.265f + 0.2f,
                    zIndex = 3,
                });
                prop.y = 0f;
                Fadein_1.Func6(helper, "Satellite Mind", "wG", "tw2", prop, 0.2f, 1f);
                prop.y = 50f;
                Fadein_1.Func6(helper, "Sharari", "wH", "tw2", prop, 0.2f, 1f);
                prop.y = 100f;
                Fadein_1.Func6(helper, "Nigo ame", "wI", "tw2", prop, 0.2f, 1f);
                prop.y = 150f;
                Fadein_1.Func6(helper, "Black or White", "wJ", "tw2", prop, 0.2f, 1f);
                prop.y = 200f;
                Fadein_1.Func6(helper, "Coppelia", "wK", "tw2", prop, 0.2f, 0.5f);
                prop.y = 250f;
                Fadein_1.Func6(helper, "PaIII  SENSATION", "wL", "tw2", prop, 0.2f, 0.5f);
                
                File.WriteAllText("tween2.txt", helper.GetResult());
            }
            #endregion

            #region Namiwo

            {
                helper.Reset();
                const float allBegin = 61.381f;
                const float allEnd = 75.000f;
                helper.Comment("Namiwo 61.381");

                const string foregroundColor = "0x030303";
                //所有的Parent，注意在使用全Parent的场景下，Y方向的定位从之前的16:9浏览器比例变成了1:1（错）
                helper.DefScreenParent("Z", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = allEnd - allBegin + 1.000f
                }, motion =>
                {
                    
                    //手动管理Backup 和本身
                    //移动在Backup层，旋转在正常层
                    const float Nogoru = 46.200f;
                    const float Satarann = 54.000f;
                    
                    motion.Comment("Start");
                    
                    const float Nami = 61.470f;
                    motion.Apply(Nami - allBegin);
                    motion.Apply(0.001f, new TextProperty {rotateZ = 60});
                    motion.Apply(1, new TextProperty {rotateZ = 0}, "cubic-bezier(0,.5,.2,1)");

                    const float Natsu = 63.000f;
                    motion.Apply(Natsu - Nami - 1f);
                    
                    motion.Comment("Natsu");
                    motion.Apply(2f, new TextProperty {scale = 1.05f}, "cubic-bezier(.7,0,.1,1)");
                    motion.Apply(0.5f);
                    motion.Apply(0.001f, new TextProperty {rotateZ = 15});
                    motion.Apply(0.001f, new TextProperty {scale = 0.90f});

                    const float Saii = 69.259f - 1.0f;
                    
                    
                    motion.Apply(Saii - Natsu - 2.5f + 0.1f);//莫名的抢跑，手动修的
                    
                    motion.Comment("Saii");
                    
                    motion.Apply(1f, new TextProperty {rotateZ = -20}, "cubic-bezier(.1,0,1,.4)");
                    motion.Apply(0.001f, new TextProperty {rotateZ = -30});
                    motion.Apply(1f, new TextProperty {rotateZ = 0}, "cubic-bezier(0,.5,.2,1)");
                });
                

                #region FirstSentance-AB

                
                const float EndAB = 63.100f;
                //在以ScreenParent为parent时，坐标系变为了 -50 - 50 的形式。
                // 注意，在实际行为中y方向的比例为单行的字符大小，不确定原因
                helper.DefParent("A", "Z", new TextProperty()
                {
                    x = 13,
                    y = -175,
                    duration = EndAB - allBegin + 1.000f,
                    scale = 0.7f,
                });

                TextProperty prop = new TextProperty();
                prop.duration = 46.1f;
                prop.y = 0f;
                prop.fillColor = foregroundColor;
                prop.scale = 0.6f;
                prop.alpha = 1.0f;
                
                Fadein_1.Func11(helper, "涙をそっと", "A", "A", prop, (float)Math.PI * 0.5f, 0.8f, 
                    EndAB - allBegin - 0.500f, new List<float>
                {
                    61.581f - allBegin - 0.200f,
                    61.996f - allBegin - 0.200f,
                    62.059f - allBegin - 0.200f,
                    62.150f - allBegin - 0.200f,
                    62.307f - allBegin - 0.200f,
                }, new List<float>
                {
                    1.0f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                });
                helper.DefParent("B", "Z", new TextProperty()
                {
                    x = -13,
                    y = -175,
                    duration = EndAB - allBegin + 1.000f,
                    scale = 0.7f,
                });
                Fadein_1.Func11(helper, "拭い", "B", "B", prop, (float)Math.PI * 0.5f, 0.8f, 
                    EndAB - allBegin - 0.500f, new List<float>
                {
                    62.424f - allBegin - 0.600f,
                    62.896f - allBegin - 0.600f,
                }, new List<float>
                {
                    1.0f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                });
                
                #endregion

                #region SecondSentence_CD

                prop.duration = 46.1f;
                prop.y = 0f;
                prop.fillColor = foregroundColor;
                prop.scale = 0.6f;
                prop.alpha = 1.0f;
                
                //在以ScreenParent为parent时，坐标系变为了 -50 - 50 的形式。
                // 注意，在实际行为中y方向的比例为单行的字符大小，不确定原因
                helper.DefParent("C", "Z", new TextProperty()
                {
                    x = 12,
                    y = -175,
                    duration = 66f - allBegin,
                    scale = 0.7f,
                });
                
                Fadein_1.Func11(helper, "舐めた温度は", "C", "C", prop, (float)Math.PI * 0.5f, 0.7f, 
                    65.408f - allBegin - 0.400f, new List<float>
                {
                    63.137f - allBegin - 0.400f,
                    63.264f - allBegin - 0.400f,
                    63.377f - allBegin - 0.400f,
                    63.642f - allBegin - 0.400f,
                    64.221f - allBegin - 0.400f,
                    64.339f - allBegin - 0.400f,

                }, new List<float>
                {
                    1.0f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                });
                helper.DefParent("D", "Z", new TextProperty()
                {
                    x = -12,
                    y = -175,
                    duration = 66f - allBegin,
                    scale = 0.7f,
                });
                Fadein_1.Func11(helper, "暑いまま", "D", "D", prop, (float)Math.PI * 0.5f, 0.7f,
                    65.408f - allBegin - 0.400f, new List<float>
                {
                    64.612f - allBegin - 0.600f,
                    65.088f - allBegin - 0.600f,
                    65.180f - allBegin - 0.600f,
                    65.308f - allBegin - 0.600f,
                }, new List<float>
                {
                    1.0f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                });

                #endregion

                #region ThirdSentence_EF

                
                const float EndEF = 69.100f;
                prop.duration = 46.1f;
                prop.y = 0f;
                prop.fillColor = foregroundColor;
                prop.scale = 0.6f;
                prop.alpha = 1.0f;
                
                //在以ScreenParent为parent时，坐标系变为了 -50 - 50 的形式。
                // 注意，在实际行为中y方向的比例为单行的字符大小，不确定原因
                helper.DefParent("E", "Z", new TextProperty()
                {
                    x = -10,
                    y = -275,
                    duration = EndEF - allBegin + 1.000f,
                    scale = 0.7f,
                });
                
                Fadein_1.Func11(helper, "いくら泣いても", "E", "E", prop, (float)Math.PI * 0.5f, 0.7f, 
                    EndEF - allBegin - 0.300f, new List<float>
                    {
                        65.675f - allBegin - 0.200f,
                        65.902f - allBegin - 0.200f,
                        66.144f - allBegin - 0.200f,
                        66.261f - allBegin - 0.200f,
                        66.518f - allBegin - 0.200f,
                        66.752f - allBegin - 0.200f,
                        67.112f - allBegin - 0.200f,

                    }, new List<float>
                    {
                        0.6f, 0.6f, 0.6f, 1.0f, 0.6f, 0.6f, 0.6f
                    });
                helper.DefParent("F", "Z", new TextProperty()
                {
                    x = -13,
                    y = -145,
                    duration = EndEF - allBegin + 1.000f,
                    scale = 0.7f,
                });
                Fadein_1.Func11(helper, "冷めないままで", "F", "F", prop, (float)Math.PI * 0.5f, 0.7f,
                    EndEF - allBegin  - 0.300f, new List<float>
                    {
                        67.346f - allBegin - 0.300f,
                        67.581f - allBegin - 0.300f,
                        67.823f - allBegin - 0.300f,
                        68.072f - allBegin - 0.300f,
                        68.299f - allBegin - 0.300f,
                        68.556f - allBegin - 0.400f,
                        68.768f - allBegin - 0.400f,

                    }, new List<float>
                    {
                        1.0f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                    });

                #endregion
                
                
                #region SecondSentence_GH
                const float EndGH = 75.000f;
                prop.duration = 46.1f;
                prop.y = 0f;
                prop.fillColor = foregroundColor;
                prop.scale = 0.6f;
                prop.alpha = 1.0f;
                
                //在以ScreenParent为parent时，坐标系变为了 -50 - 50 的形式。
                // 注意，在实际行为中y方向的比例为单行的字符大小，不确定原因
                helper.DefParent("G", "Z", new TextProperty()
                {
                    x = 11,
                    y = -175,
                    duration = EndGH - allBegin + 2.000f,
                    scale = 0.7f,
                });
                
                Fadein_1.Func11(helper, "咲いた台詞が", "G", "G", prop, (float)Math.PI * 0.5f, 0.7f, 
                    EndGH - allBegin - 0.300f, new List<float>
                    {
                        69.485f - allBegin - 0.200f,
                        69.749f - allBegin - 0.200f,
                        69.998f - allBegin - 0.200f,
                        70.232f - allBegin - 0.200f,
                        70.694f - allBegin - 0.200f,
                        70.929f - allBegin - 0.200f,
                    }, new List<float>
                    {
                        1.0f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                    });
                helper.DefParent("H", "Z", new TextProperty()
                {
                    x = -11,
                    y = -275,
                    duration = EndGH - allBegin + 2.000f,
                    scale = 0.7f,
                });
                Fadein_1.Func11(helper, "いつまでも浮かぶよ", "H", "H", prop, (float)Math.PI * 0.5f, 0.7f,
                    EndGH - allBegin - 0.300f, new List<float>
                    {
                        71.420f - allBegin - 0.300f,
                        71.917f - allBegin - 0.300f,
                        72.380f - allBegin - 0.300f,
                        72.849f - allBegin - 0.300f,
                        73.348f - allBegin - 0.300f,
                        73.817f - allBegin - 0.300f,
                        74.066f - allBegin - 0.300f,
                        74.301f - allBegin - 0.300f,
                        74.792f - allBegin - 0.300f,

                    }, new List<float>
                    {
                        1.0f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f
                    });

                #endregion
                
                prop = new TextProperty();
                prop.duration = allEnd - allBegin;
                prop.x = 5.5f - 50f;
                prop.y = 120f - 50f;
                prop.scale = 1.0f;
                prop.alpha = 1.0f;

                {
                    const float Ikura = 65.000f;
                    const float Saii = 69.259f - 0.5f;
                    helper.DefParent("p", null, new TextProperty()
                    {
                        x = 20,//53
                        y = 30f,
                        duration = Saii - allBegin + 1,
                        zIndex = 3,
                    });
                

                    helper.AddText("█", $"p", "p", new TextProperty
                    {
                        scale = 50,
                        fillColor = "0xffffff",
                        alpha = 0,
                    }, null, (motion, _, _, _) =>
                    {
                        motion.Apply(Ikura - allBegin);
                        motion.Apply(0.5f, new TextProperty {alpha = 1});
                        motion.Apply(0.5f, new TextProperty {alpha = 0});
                        motion.Apply(Saii - Ikura - 1);
                        motion.Apply(0.5f, new TextProperty {alpha = 1});
                        motion.Apply(0.5f, new TextProperty {alpha = 0});
                    });
                }
                
                File.WriteAllText("text7.txt", helper.GetResult());
            }
            #endregion

            #region Background_Part2

            {
                helper.Reset();
                const float allBegin = 45.000f;
                const float allEnd = 94.000f;
                const float borderEnd = 60.000f;
                
                helper.Comment("bg2 45.000f");
                var rnd = new Random();

                const string foregroundColor = "0xfefefe";
                //所有的Parent，注意在使用全Parent的场景下，Y方向的定位从之前的16:9浏览器比例变成了1:1（错）
                helper.DefScreenParent("Z", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    alpha = 0,
                    duration = allEnd - allBegin
                }, motion =>
                {
                    //手动管理Backup 和本身
                    //移动在Backup层，旋转在正常层
                    const float Nogoru = 46.200f;
                    const float Satarann = 54.000f;
                    motion.Apply(999.0f, null, isBackup: true);
                    motion.Apply(Nogoru - allBegin);
                    motion.Apply(Nogoru - allBegin, isBackup: true);
                    
                    motion.Apply(0.001f, new TextProperty {alpha = 1, scale = 0.80f}, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {alpha = 1, y = 5}, isBackup: true);
                    
                    motion.Comment("Start");
                    motion.Apply(0.001f, new TextProperty {alpha = 1});
                    motion.Apply(1f, new TextProperty()
                    {
                        x = -7 - 50f,
                    },"cubic-bezier(0,1,0,1)", isBackup: true);
                    //motion.Apply(Satarann - Nogoru -  1, isBackup: true);
                    Fadein_1.RandomMove(motion, rnd, Satarann - Nogoru -  1, -7 - 50f, 5, 0.3f, 15f
                        , isBackup: true);
                    
                    motion.Apply(1f, new TextProperty()
                    {
                        x = 9 - 50f,
                    },"cubic-bezier(.7,0,.1,1)", isBackup: true);
                    
                    const float Nami = 61.470f;
                    Fadein_1.RandomMove(motion, rnd, Nami - Satarann - 1, 9 - 50f, 5, 0.3f, 15f
                        , isBackup: true);
                    //motion.Apply(Nami - Satarann - 1, isBackup: true);
                    //在这部分没有旋转
                    //motion.Apply(Nami - Nogoru);
                    Fadein_1.RandomRot(motion, rnd, Nami - Nogoru, 0, 1.3f);
                    
                    
                    motion.Comment("Nami");
                    motion.Apply(0.001f, new TextProperty {scale = 1.2f}, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {x=3 - 50f}, isBackup: true);
                    motion.Apply(1, new TextProperty {scale = 0.8f}, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {rotateZ = 60});
                    motion.Apply(1, new TextProperty {rotateZ = 0}, "cubic-bezier(0,.5,.2,1)");

                    const float Natsu = 63.000f;
                    //motion.Apply(Natsu - Nami - 1f);
                    Fadein_1.RandomRot(motion, rnd, Natsu - Nami - 1f, 0, 1.3f);
                    
                    Fadein_1.RandomMove(motion, rnd, Natsu - Nami - 1f, 3 - 50f, 5, 0.3f, 15f
                        , isBackup: true);
                    //motion.Apply(Natsu - Nami - 1f, isBackup: true);
                    
                    motion.Comment("Natsu");
                    motion.Apply(2f, new TextProperty {scale = 0.95f}, "cubic-bezier(.7,0,.1,1)"
                        , isBackup: true);
                    motion.Apply(0.5f, isBackup: true);
                    motion.Apply(1f);
                    motion.Apply(1.5f, new TextProperty {y = 70 - 50f}, "cubic-bezier(.3,.1,.1,.4)");
                    //这里需要一个白色渐变

                    motion.Apply(0.001f, new TextProperty {y = 0 - 50f});
                    motion.Apply(0.001f, new TextProperty {rotateZ = 15});
                    motion.Apply(0.001f, new TextProperty {x = 13 - 50, y = 292 - 50}, isBackup: true);
                    motion.Apply(1f, new TextProperty {y = 402 - 50f},"cubic-bezier(0,.7,.3,1)", isBackup: true);
                    motion.Apply(1f);

                    const float Saii = 69.259f - 1.0f;
                    
                    
                    Fadein_1.RandomRot(motion, rnd, Saii - Natsu - 3.5f, 15, 1.3f);
                    //motion.Apply(Saii - Natsu - 3.5f);
                    //motion.Apply(Saii - Natsu - 3.5f, isBackup: true);
                    
                    Fadein_1.RandomMove(motion, rnd, Saii - Natsu - 3.5f, 13 - 50, 402 - 50f, 0.3f, 15f
                        , isBackup: true);
                    
                    motion.Comment("Saii");
                    
                    motion.Apply(1f, new TextProperty {rotateZ = -20}, "cubic-bezier(.1,0,1,.4)");
                    motion.Apply(1f, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {rotateZ = -30});
                    motion.Apply(0.001f, new TextProperty {x = 4 - 50, y = 550 - 50}, isBackup: true);
                    motion.Apply(1f, new TextProperty {rotateZ = 0}, "cubic-bezier(0,.5,.2,1)");
                    motion.Apply(1f, isBackup: true);
                    
                    
                    //-10 -3

                    const float Rot = 75.000f;
                    
                    Fadein_1.RandomRot(motion, rnd, Rot - Saii - 2f, 0, 1.3f);
                    //motion.Apply(Rot - Saii - 2f);
                    
                    Fadein_1.RandomMove(motion, rnd, Rot - Saii - 2f, 4 - 50, 550 - 50, 0.3f, 15f
                        , isBackup: true);
                    //motion.Apply(Rot - Saii - 2f, isBackup: true);
                    
                    motion.Comment("Rot");
                    motion.Apply(0.001f, new TextProperty {x = 0, y = 0});
                    motion.Apply(0.001f, new TextProperty {x = 4 - 50, y = -200 - 50}, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {scale = 1.00f}, isBackup: true);
                    motion.Apply(1f, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {x = 4 - 50, y = 100 - 50}, isBackup: true);
                    motion.Apply(1f, isBackup: true);
                    
                    motion.Apply(0.001f, new TextProperty {rotateZ = 30});
                    motion.Apply(1f, new TextProperty {rotateZ = 0}, "cubic-bezier(0,.5,.2,1)");
                    motion.Apply(0.001f, new TextProperty {rotateZ = -30});
                    motion.Apply(1f, new TextProperty {rotateZ = 0}, "cubic-bezier(0,.5,.2,1)");
                    
                    //=======
                    
                    motion.Apply(0.001f, new TextProperty {alpha = 1, scale = 0.80f}, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {alpha = 1, y = 55}, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {alpha = 1});
                    motion.Apply(1f, new TextProperty()
                    {
                        x = -7 - 50f,
                    },"cubic-bezier(0,1,0,1)", isBackup: true);
                    
                    const float finalRot = 90.300f;
                    
                    motion.Comment("finalRot");
                    Fadein_1.RandomRot(motion, rnd, finalRot - Rot - 2f, 0, 2);
                    Fadein_1.RandomMove(motion, rnd, finalRot - Rot - 3f, -7 - 50f, 55, 0.3f, 15f
                        , isBackup: true);
                    motion.Apply(0.001f, new TextProperty {x = 0, y = 0});
                    motion.Apply(0.001f, new TextProperty {x = 0.5f - 50, y = 5}, isBackup: true);
                    motion.Apply(0.001f, new TextProperty {rotateZ = -45});
                    motion.Apply(2.9f, new TextProperty {rotateZ = 0}, "cubic-bezier(0,.5,.2,1)");
                    motion.Apply(0.001f, new TextProperty {scale=1.00f}, isBackup: true);
                    motion.Apply(2.9f, new TextProperty {scale=0.80f}, isBackup: true);
                    
                    motion.Apply(0.3f, new TextProperty {rotateX = 90}, "cubic-bezier(0,.8,0,1)");

                });
                
                
                ImageHelper img = new ImageHelper(@"D:\2.svg");
                ImageHelper imgBackground = new ImageHelper(@"D:\4.svg");
                ImageHelper border = new ImageHelper(@"D:\border.svg");
                
                TextProperty prop = new TextProperty();
                prop.duration = allEnd - allBegin;
                prop.x = 5.5f - 50f;
                prop.y = 120f - 50f;
                prop.scale = 1.0f;
                prop.alpha = 1.0f;
                //prop.zIndex = -2;
                imgBackground.AddImage(helper, "Z", prop, width: 200);
                //prop.zIndex = -1;
                img.AddImage(helper, "Z", prop,xalias:"z", fixBorder: false, width: 200);

                {
                    const float Ikura = 65.000f;
                    const float Saii = 69.259f - 0.5f;
                    helper.DefParent("B", null, new TextProperty()
                    {
                        x = 20,//53
                        y = 30f,
                        duration = Saii - allBegin + 1,
                        zIndex = 3,
                    });
                

                    helper.AddText("█", $"B", "B", new TextProperty
                    {
                        scale = 50,
                        fillColor = "0xffffff",
                        alpha = 0,
                    }, null, (motion, _, _, _) =>
                    {
                        motion.Apply(Ikura - allBegin);
                        motion.Apply(0.5f, new TextProperty {alpha = 1});
                        motion.Apply(0.5f, new TextProperty {alpha = 0});
                        motion.Apply(Saii - Ikura - 1);
                        motion.Apply(0.5f, new TextProperty {alpha = 1});
                        motion.Apply(0.5f, new TextProperty {alpha = 0});
                    });
                }

                helper.DefScreenParent("M", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    alpha = 1,
                    duration = allEnd - allBegin
                }, motion =>
                {
                    motion.Apply(borderEnd - allBegin);
                    motion.Apply(0.001f, new TextProperty
                    {
                        scale = 1.2f
                    });
                });
                
                prop.x = 0f - 50f;
                prop.scale = 1.2f;
                prop.y = -37f - 50f;
                border.AddImage(helper, "M", prop, fixBorder: false);
                
                File.WriteAllText("bg_2.txt", helper.GetResult());
            }
            #endregion
        }

        private static void IntroMotion(MotionHelper motion, Random rnd,int round = 6)
        {
            motion.Apply(0.001f, new TextProperty { x = 300 ,y = 90}, isBackup: true);
            motion.Apply(0.8f, new TextProperty()
            {
                x = 100
            }, "cubic-bezier(0,.6,.1,1)", isBackup: true);
            motion.Apply(0.8f, null);

            int prevRot = 0;
            for (int i = 0; i < round; i++)
            {
                int nextRot = rnd.Next(-7, 7);
                motion.Apply(2.0f, new TextProperty()
                {
                    rotateZ = nextRot
                }, "cubic-bezier(.5,0,.5,1)", true);
                prevRot = nextRot;
            }

            motion.Comment("Begin");
            motion.Apply(0.001f, new TextProperty()
                {rotateZ = 0}, isBackup: true);
            motion.Apply(0.001f, new TextProperty()
            {
                y = -150,
                x = 50
            }, isBackup: true); //设定旋转中心,需要计算一下隐含的+50
            motion.Apply(1f, new TextProperty()
            {
            }, isBackup: true);
            motion.Apply(0.001f, new TextProperty()
            {
                y = 10,
                x = 80
            }, isBackup: true); //设定旋转中心,需要计算一下隐含的+50
            motion.Apply(1f, new TextProperty()
            {
            }, isBackup: true);
            
            motion.Apply(0.001f, new TextProperty()
                {y = 0, x = 0}, isBackup: true);
            motion.Apply(0.001f, new TextProperty()
                {rotateZ = 0}, isBackup: true);


            for (int i = 0; i < round; i++)
            {
                int x = rnd.Next(-5, 5);
                int y = rnd.Next(-5, 5);
                motion.Apply(2.0f, new TextProperty()
                {
                    x = x,
                    y = y
                }, "cubic-bezier(.2,0,.8,1)");
            }

            motion.Apply(0.001f, new TextProperty
            {
                x = 0,
                y = 0
            });
            motion.Apply(0.001f, new TextProperty
            {
                scale = 2.0f
            });

            motion.Apply(0.001f, new TextProperty()
                {y = 450, x = 0}); //设定旋转中心在屏幕中在的位置
            motion.Apply(0.001f, new TextProperty()
            {
                rotateZ = -30
            });
            motion.Apply(1f, new TextProperty()
                {rotateZ = 0}, "cubic-bezier(.2,0,.8,1)");
            motion.Apply(0.001f, new TextProperty()
                {rotateZ = 30});
            motion.Apply(0.001f, new TextProperty()
                {y = -150}); //设定旋转中心在屏幕中在的位置
            motion.Apply(1f, new TextProperty()
            {
                rotateZ = 0
            }, "cubic-bezier(.2,0,.8,1)");
            
            motion.Apply(0.001f, new TextProperty()
                {scale = 1.0f}); //恢复
            motion.Apply(0.001f, new TextProperty()
                {y = 0}); //恢复
        }

        /*
         *  Bitmap bmp = new Bitmap(50,50);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.Green, 0, 0, 50, 50);
            g.Dispose();
            bmp.Save("filepath", System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
         */
    }
}