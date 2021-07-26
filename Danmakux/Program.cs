using System;
using System.Collections.Generic;
using System.IO;
using QRCoder;

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

            Intro_Author(helper);
            {
                helper.Reset();
                helper.DefParent("s1", null, new TextProperty()
                {
                    x = 10,
                    y = 10,
                    duration = 20,
                    scale = 0.13f,
                });
                
                TextProperty prop = new TextProperty();
                prop.duration = 10;
                prop.scale = 0.6f;
                prop.fillColor = "0xff6666";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "踵返し夏は溶け", 3, "　手を引く影　", "s1", "s1", prop, 2.0f);
                
                
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
                prop.scale = 1f;
                prop.fillColor = "0xff6666";
                prop.alpha = 0.0f;
                Fadein_1.Func2(helper, "踵返し夏は溶け", "z1", "z1", prop, 5.5f, 0.5f);
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
            {
                
                helper.Reset();
                helper.DefParent("s2", null, new TextProperty()
                {
                    x = 70,
                    y = 60,
                    duration = 20,
                    scale = 0.13f,
                });
                TextProperty prop = new TextProperty();
                prop.duration = 12;
                prop.scale = 0.6f;
                prop.fillColor = "0xeeeeee";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "畦道　日暮れの道", 4.5f, "　優しい笑顔の　", "s2", "s2", prop, 2.5f);
                
                helper.DefParent("z3", null, new TextProperty()
                {
                    x = 70,
                    y = 10,
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
                    y = 25,
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

            {

                helper.Reset();
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
                prop.fillColor = "0xeeeeee";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "夢路は遠く果て", 2.2f, "　遥か唄の底　",1.9f ,"　水に横たう　", "s3", "s3", prop, 1.3f);
                
                File.WriteAllText("qr_3.txt", helper.GetResult());
                
                helper.Reset();
                
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

            {
                //Start at 35.700
                helper.Reset();
                helper.DefParent("s4", null, new TextProperty()
                {
                    x = 70,
                    y = 5,
                    duration = 20,
                    scale = 0.13f,
                });
                TextProperty prop = new TextProperty();
                prop.duration = 14;
                prop.scale = 0.6f;
                prop.fillColor = "0xeeeeee";
                prop.alpha = 0.0f;

                QRCodeHelper.GenQRCode(helper, "手持ち花火が　　　", 3.2f, "今沈んでった　　　",1.9f ,"土へと還るみたいに", "s4", "s4", prop, 3.2f);
                
                
                File.WriteAllText("qr_4.txt", helper.GetResult());
                
                helper.Reset();
                
                helper.DefParent("z8", null, new TextProperty()
                {
                    x = 70,
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
                    x = 70,
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
                    x = 70,
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

            {
                //Start at 48.800
                helper.Reset();
                helper.DefParent("s5", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 0.4f,
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
                    prop.fillColor = "0xeeeeee";
                    prop.alpha = 1.0f;
                    helper.AddText("残", "s5", "s5", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 1;
                        p.y = -150;
                        p.rotateX = 0;
                        p.rotateY = 0;
                        p.rotateZ = 0;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(strokeOffset * noStroke);

                        motion.Apply(0.3f, new TextProperty
                        {
                            y = 0
                        }, "cubic-bezier(0,.6,.2,1)");
                    });
                }
                
                helper.DefParent("s6", null, new TextProperty()
                {
                    x = 50,
                    y = 50,
                    duration = 0.4f,
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
                    prop.fillColor = "0xeeeeee";
                    prop.alpha = 1.0f;
                    helper.AddText("残", "s6", "s6", prop, (p, noChar, noStroke) =>
                    {
                        p.x = 0;
                        p.alpha = 1;
                        p.y = -150;
                        p.rotateX = 0;
                        p.rotateY = 0;
                        p.rotateZ = 0;
                        p.scale = 1.0f;

                    }, (motion, p, noChar, noStroke) =>
                    {
                        motion.Apply(strokeOffset * noStroke);

                        motion.Apply(0.3f, new TextProperty
                        {
                            y = 0,
                            scale = 2.0f
                        }, "cubic-bezier(0,.6,.2,1)");
                    });
                }
                
                File.WriteAllText("text_5.txt", helper.GetResult());
            }
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