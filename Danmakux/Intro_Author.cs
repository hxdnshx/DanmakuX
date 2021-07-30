using System;
using System.IO;

namespace Danmakux
{
    partial class Program
    {
        public static void Intro_Author(GraphicHelper helper)
        {
            helper.Reset();

            {
                helper.DefParent("i0", null, new TextProperty()
                {
                    x = 78,
                    y = 40,
                    duration = 20,
                    scale = 1.5f
                });

                TextProperty prop = new TextProperty();
                prop.duration = 15f;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;
                prop.rotateX = 0;
                prop.rotateY = 0;
                prop.rotateZ = 0;
                Random w = new Random();

                helper.AddText("Mum", "i_0", "i0", prop, (p, noChar, noStroke) => { }, (motion, p, noChar, noStroke) =>
                {
                    var offset = 0.1f * noStroke;
                    const int xDistance = 35;
                    const int fdDistance = 45;
                    var rndY = w.Next(-2, 2);
                    var num = noChar + 1;
                    motion.Apply(0.01f, new TextProperty()
                        {
                            x = xDistance * noChar + fdDistance * (int) Math.Cos(num * 90 * Math.PI / 180),
                            y = 0 + fdDistance * (int) Math.Sin(num * 90 * Math.PI / 180)
                        })
                        .Apply(0.01f,
                            new TextProperty
                            {
                                alpha = 1
                            })
                        .Apply(5f + offset,
                            new TextProperty
                            {
                                y = 0,
                                x = xDistance * noChar
                            }, "cubic-bezier(0,1,0,.9)")
                        .Apply(7f)
                        .Apply(1,
                            new TextProperty
                            {
                                alpha = 0,
                                x = xDistance * noChar + 400,
                                y = 100,
                                rotateX = 360 - 25 * noChar
                            }, "ease-in");
                });

                //File.WriteAllText("result_script_Mum.txt", helper.GetResult());
            }
            {
                helper.DefParent("i1", null, new TextProperty()
                {
                    x = 74.5f,
                    y = 50,
                    duration = 13,
                    scale = 0.3f
                });

                TextProperty prop = new TextProperty();
                prop.duration = 15f;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;
                prop.rotateX = 0;
                prop.rotateY = 0;
                prop.rotateZ = 0;
                Random w = new Random();

                helper.AddText("MUSIC  YONOSUKE", "i_1", "i1", prop, (p, noChar, noStroke) => { },
                    (motion, p, noChar, noStroke) =>
                    {
                        var offset = 0.1f * noStroke;
                        var charTimeOffset = 0.02f * noChar + 0.005f * noStroke;
                        const int xDistance = 35;
                        const int fdDistance = 45;
                        var rndY = w.Next(-2, 2);
                        var num = noStroke;
                        int charOffset = ((noChar % 2) - 1) * 50;
                        int strokeOffset = ((noStroke % 2) - 1) * 50;
                        motion.Apply(.5f + charTimeOffset, new TextProperty()
                            {
                                x = xDistance * noChar + charOffset,
                                y = 0 + strokeOffset
                            })
                            .Apply(0.45f,
                                new TextProperty
                                {
                                    alpha = 1,
                                    y = 0 + (int) (strokeOffset * 0.2)
                                }, "cubic-bezier(0,1,0,.9)")
                            .Apply(0.25f,
                                new TextProperty
                                {
                                    y = 0,
                                    alpha = 0
                                })
                            .Apply(0.25f,
                                new TextProperty
                                {
                                    x = xDistance * noChar + (int) (charOffset * 0.5),
                                    alpha = 1
                                }, "cubic-bezier(0,1,0,.9)")
                            .Apply(0.25f,
                                new TextProperty
                                {
                                    x = xDistance * noChar + (int) (charOffset * 0.125),
                                    alpha = 0
                                })
                            .Apply(0.25f,
                                new TextProperty
                                {
                                    x = xDistance * noChar + (int) (charOffset * 0.0625),
                                    alpha = 1
                                }, "cubic-bezier(0,1,0,.9)")
                            .Apply(9.5f - charTimeOffset,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                })
                            .Apply(1,
                                new TextProperty
                                {
                                    alpha = 0,
                                    x = xDistance * noChar + 100 * noChar,
                                    y = 100,
                                }, "ease-in");
                    });
            }


            {
                helper.DefParent("i2", null, new TextProperty()
                {
                    x = 73.5f,
                    y = 55,
                    duration = 13,
                    scale = 0.3f
                });

                TextProperty prop = new TextProperty();
                prop.duration = 15f;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;
                prop.rotateX = 0;
                prop.rotateY = 0;
                prop.rotateZ = 0;
                Random w = new Random();

                helper.AddText("LYRICS  GYUNIKU", "i_2", "i2", prop, (p, noChar, noStroke) => { },
                    (motion, p, noChar, noStroke) =>
                    {
                        var offset = 0.1f * noStroke;
                        var charTimeOffset = 0.04f * noChar + 0.005f * noStroke;
                        const int xDistance = 35;
                        const int fdDistance = 45;
                        var rndY = w.Next(-2, 2);
                        var num = noStroke;
                        int charOffset = ((noChar % 2) * 2 - 1) * 60;
                        int strokeOffset = ((noStroke % 2) - 1) * 50;
                        motion.Apply(2.5f + charTimeOffset, new TextProperty()
                            {
                                x = xDistance * noChar + charOffset,
                                y = 0
                            })
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.1f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(1.1f,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                }, "cubic-bezier(0,.5,.3,1)")
                            .Apply(7f - charTimeOffset,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                })
                            .Apply(1,
                                new TextProperty
                                {
                                    alpha = 0,
                                    x = xDistance * noChar +
                                        (float) (200 * Math.Cos((noChar * 3 + noStroke) * 30 * Math.PI / 180)),
                                    y = 100 +
                                        (float) (200 * Math.Sin((noChar * 3 + noStroke) * 30 * Math.PI / 180)),
                                }, "ease-in");
                    });
            }

            {
                helper.DefParent("i3", null, new TextProperty()
                {
                    x = 67f,
                    y = 60,
                    duration = 13,
                    scale = 0.3f
                });

                TextProperty prop = new TextProperty();
                prop.duration = 15f;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;
                prop.rotateX = 0;
                prop.rotateY = 0;
                prop.rotateZ = 0;
                Random w = new Random();

                helper.AddText("ILLUSTRATION  HEREMIA", "i_3", "i3", prop, (p, noChar, noStroke) => { },
                    (motion, p, noChar, noStroke) =>
                    {
                        var offset = 0.1f * noStroke;
                        var charTimeOffset = 0.04f * noChar + 0.005f * noStroke;
                        const int xDistance = 35;
                        const int fdDistance = 45;
                        var rndY = w.Next(-2, 2);
                        var num = noStroke;
                        int charOffset = ((noChar % 2) * 2 - 1) * 60;
                        int strokeOffset = ((noStroke % 2) - 1) * 50;
                        motion.Apply(4.5f + charTimeOffset, new TextProperty()
                            {
                                x = xDistance * noChar + charOffset,
                                y = 0
                            })
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.1f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(1.1f,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                }, "cubic-bezier(0,.5,.3,1)")
                            .Apply(5f - charTimeOffset,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                })
                            .Apply(1,
                                new TextProperty
                                {
                                    alpha = 0,
                                    x = xDistance * noChar +
                                        (float) (200 * Math.Cos(((noChar * 3 + noStroke) * 30 % 180) * Math.PI / 180)),
                                    y = 100 +
                                        (float) (200 * Math.Sin(((noChar * 3 + noStroke) * 30 % 180) * Math.PI / 180)),
                                }, "ease-in");
                    });
            }


            {
                helper.DefParent("i4", null, new TextProperty()
                {
                    x = 74.5f,
                    y = 65,
                    duration = 13,
                    scale = 0.3f
                });

                TextProperty prop = new TextProperty();
                prop.duration = 15f;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;
                prop.rotateX = 0;
                prop.rotateY = 0;
                prop.rotateZ = 0;
                Random w = new Random();

                helper.AddText("MOVIE  TAISUKE", "i_4", "i4", prop, (p, noChar, noStroke) => { },
                    (motion, p, noChar, noStroke) =>
                    {
                        var offset = 0.1f * noStroke;
                        var charTimeOffset = 0.04f * noChar + 0.005f * noStroke;
                        const int xDistance = 35;
                        const int fdDistance = 45;
                        var rndY = w.Next(-2, 2);
                        var num = noStroke;
                        int charOffset = ((noChar % 2) * 2 - 1) * 60;
                        int strokeOffset = ((noStroke % 2) - 1) * 50;
                        motion.Apply(6.5f + charTimeOffset, new TextProperty()
                            {
                                x = xDistance * noChar + charOffset,
                                y = 0
                            })
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.1f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(1.1f,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                }, "cubic-bezier(0,.5,.3,1)")
                            .Apply(3f - charTimeOffset,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                })
                            .Apply(1,
                                new TextProperty
                                {
                                    alpha = 0,
                                    x = xDistance * noChar +
                                        (float) (200 * Math.Cos(((noChar * 3 + noStroke) * 30 % 180) * Math.PI / 180)),
                                    y = 100 +
                                        (float) (200 * Math.Sin(((noChar * 3 + noStroke) * 30 % 180) * Math.PI / 180)),
                                }, "ease-in");
                    });
            }
            {
                helper.DefParent("i5", null, new TextProperty()
                {
                    x = 68f,
                    y = 70,
                    duration = 13,
                    scale = 0.3f
                });

                TextProperty prop = new TextProperty();
                prop.duration = 15f;
                prop.scale = 0.6f;
                prop.fillColor = "0x030303";
                prop.alpha = 0.0f;
                prop.rotateX = 0;
                prop.rotateY = 0;
                prop.rotateZ = 0;
                Random w = new Random();

                helper.AddText("VOCALTUNING  OSAMU", "i_5", "i5", prop, (p, noChar, noStroke) => { },
                    (motion, p, noChar, noStroke) =>
                    {
                        var offset = 0.1f * noStroke;
                        var charTimeOffset = 0.04f * noChar + 0.005f * noStroke;
                        const int xDistance = 35;
                        const int fdDistance = 45;
                        var rndY = w.Next(-2, 2);
                        var num = noStroke;
                        int charOffset = ((noChar % 2) * 2 - 1) * 60;
                        int strokeOffset = ((noStroke % 2) - 1) * 50;
                        motion.Apply(8.5f + charTimeOffset, new TextProperty()
                            {
                                x = xDistance * noChar + charOffset,
                                y = 0
                            })
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.05f,
                                new TextProperty {alpha = 0}, "cubic-bezier(0,1,0,1)")
                            .Apply(0.1f,
                                new TextProperty {alpha = 1}, "cubic-bezier(0,1,0,1)")
                            .Apply(1.1f,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                }, "cubic-bezier(0,.5,.3,1)")
                            .Apply(1f - charTimeOffset,
                                new TextProperty
                                {
                                    x = xDistance * noChar,
                                    alpha = 1
                                })
                            .Apply(1,
                                new TextProperty
                                {
                                    alpha = 0,
                                    x = xDistance * noChar +
                                        (float) (200 * Math.Cos(((noChar * 3 + noStroke) * 30 % 240) * Math.PI / 180)),
                                    y = 100 +
                                        (float) (200 * Math.Sin(((noChar * 3 + noStroke) * 30 % 240) * Math.PI / 180)),
                                }, "ease-in");
                    });

                File.WriteAllText("result_script_Mum.txt", helper.GetResult());
            }
        }
    }
}