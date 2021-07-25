using System;
using System.Collections.Generic;
using System.Text;
using QRCoder;

namespace Danmakux
{
    class QRCodeHelper
    {
        private static QRCodeGenerator _gen = new QRCodeGenerator();
        private static Random qrRandom = new Random();
        private static Encoding chn = Encoding.Default;

        public static void GenQRCode(GraphicHelper helper, string str, float wait, string secondStr, string alias, string parent, TextProperty prop, float duration, float delay = 1f)
        {
            

            var list = new List<KeyValuePair<int, int>>();
            var list2 = new List<KeyValuePair<int, int>>();
            
            int totalRow = 0;
            {
                var codeData = _gen.CreateQrCode(chn.GetBytes(str), QRCodeGenerator.ECCLevel.L);
                AsciiQRCode qrCode = new AsciiQRCode(codeData);
                string qrCodeAsAsciiArt = qrCode.GetGraphic(1, "X", "O");
                {
                    foreach (var line in qrCodeAsAsciiArt.Split('\n'))
                    {
                        int col = 0;
                        foreach (var ch in line)
                        {
                            if (ch == 'X')
                            {
                                list.Add(new KeyValuePair<int, int>(totalRow, col));
                            }

                            col++;
                        }

                        totalRow++;
                    }
                }
            }

            {
                int row = 0;
                var codeData = _gen.CreateQrCode(chn.GetBytes(secondStr), QRCodeGenerator.ECCLevel.L);
                AsciiQRCode qrCode = new AsciiQRCode(codeData);
                string qrCodeAsAsciiArt = qrCode.GetGraphic(1, "X", "O");
                {
                    foreach (var line in qrCodeAsAsciiArt.Split('\n'))
                    {
                        int col = 0;
                        foreach (var ch in line)
                        {
                            if (ch == 'X')
                            {
                                list2.Add(new KeyValuePair<int, int>(row, col));
                            }

                            col++;
                        }

                        row++;
                    }
                }
            }
            

            prop.alpha = 0;
            int currentIndex = 0;
            list.Shuffle();
            list2.Shuffle();
            foreach (var item in list)
            {
                var col = item.Value;
                var row = item.Key;
                var offsetCol = col + qrRandom.Next(totalRow / 2, totalRow) * (qrRandom.Next(0, 2) * 2 - 1);
                var offsetRow = row + qrRandom.Next(totalRow / 2, totalRow) * (qrRandom.Next(0, 2) * 2 - 1);
                bool isRowFirst = qrRandom.Next(0, 2) == 0;
                var index = currentIndex;
                helper.AddText("█", $"{alias}_{currentIndex}", parent, prop, null, 
                    (motion, p, noChar, noStroke) =>
                    {
                        const int defaultScale = 50;
                        var offset = index * 0.003f;
                        motion.Apply(offset + delay, new TextProperty()
                        {
                            x = offsetRow * defaultScale,
                            y = offsetCol * defaultScale
                        });
                        for (int i = 0; i < 2; i++)
                        {
                            if (isRowFirst)
                                offsetRow = row;
                            else
                                offsetCol = col;
                            motion.Apply(0.3f, new TextProperty()
                            {
                                x = offsetRow * defaultScale,
                                y = offsetCol * defaultScale,
                                alpha = 1
                            }, i == 0 ? "linear":"cubic-bezier(0,.8,.4,1)");

                            isRowFirst = !isRowFirst;
                        }
                        motion.Apply(wait);

                        if (index < list2.Count)
                        {
                            row = list2[index].Key;
                            col = list2[index].Value;
                            for (int i = 0; i < 2; i++)
                            {
                                if (isRowFirst)
                                    offsetRow = row;
                                else
                                    offsetCol = col;
                                motion.Apply(0.3f, new TextProperty()
                                {
                                    x = offsetRow * defaultScale,
                                    y = offsetCol * defaultScale,
                                    alpha = 1
                                }, i == 0 ? "linear":"cubic-bezier(0,.8,.4,1)");

                                isRowFirst = !isRowFirst;
                            }
                            motion.Apply(duration);
                        }

                        motion.Apply(0.06f, new TextProperty() {alpha = 0});
                        motion.Apply(0.06f, new TextProperty() {alpha = 1});
                        motion.Apply(0.06f, new TextProperty() {alpha = 0});
                        motion.Apply(0.06f, new TextProperty() {alpha = 1});
                        motion.Apply(0.06f, new TextProperty() {alpha = 0});
                    });
                currentIndex++;
            }
        }
        
        public static void GenQRCode(GraphicHelper helper, string str, float wait, string secondStr, float wait2, string thirdStr, 
            string alias, string parent, TextProperty prop, float duration, float delay = 1f)
        {
            

            var list = new List<KeyValuePair<int, int>>();
            var list2 = new List<KeyValuePair<int, int>>();
            var list3 = new List<KeyValuePair<int, int>>();
            
            int totalRow = 0;
            {
                var codeData = _gen.CreateQrCode(chn.GetBytes(str), QRCodeGenerator.ECCLevel.L);
                AsciiQRCode qrCode = new AsciiQRCode(codeData);
                string qrCodeAsAsciiArt = qrCode.GetGraphic(1, "X", "O");
                {
                    foreach (var line in qrCodeAsAsciiArt.Split('\n'))
                    {
                        int col = 0;
                        foreach (var ch in line)
                        {
                            if (ch == 'X')
                            {
                                list.Add(new KeyValuePair<int, int>(totalRow, col));
                            }

                            col++;
                        }

                        totalRow++;
                    }
                }
            }

            {
                int row = 0;
                var codeData = _gen.CreateQrCode(chn.GetBytes(secondStr), QRCodeGenerator.ECCLevel.L);
                AsciiQRCode qrCode = new AsciiQRCode(codeData);
                string qrCodeAsAsciiArt = qrCode.GetGraphic(1, "X", "O");
                {
                    foreach (var line in qrCodeAsAsciiArt.Split('\n'))
                    {
                        int col = 0;
                        foreach (var ch in line)
                        {
                            if (ch == 'X')
                            {
                                list2.Add(new KeyValuePair<int, int>(row, col));
                            }

                            col++;
                        }

                        row++;
                    }
                }
            }
            
            {
                int row = 0;
                var codeData = _gen.CreateQrCode(chn.GetBytes(thirdStr), QRCodeGenerator.ECCLevel.L);
                AsciiQRCode qrCode = new AsciiQRCode(codeData);
                string qrCodeAsAsciiArt = qrCode.GetGraphic(1, "X", "O");
                {
                    foreach (var line in qrCodeAsAsciiArt.Split('\n'))
                    {
                        int col = 0;
                        foreach (var ch in line)
                        {
                            if (ch == 'X')
                            {
                                list3.Add(new KeyValuePair<int, int>(row, col));
                            }

                            col++;
                        }

                        row++;
                    }
                }
            }
            

            prop.alpha = 0;
            int currentIndex = 0;
            list.Shuffle();
            list2.Sort((a, b) => { return (b.Key + b.Value) - (a.Key + a.Value);});
            list2.Reverse();
            list3.Sort((a, b) => { return (a.Key + a.Value) - (b.Key + b.Value);});
            foreach (var item in list)
            {
                var col = item.Value;
                var row = item.Key;
                var offsetCol = col + qrRandom.Next(totalRow / 2, totalRow) * (qrRandom.Next(0, 2) * 2 - 1);
                var offsetRow = row + qrRandom.Next(totalRow / 2, totalRow) * (qrRandom.Next(0, 2) * 2 - 1);
                bool isRowFirst = qrRandom.Next(0, 2) == 0;
                var index = currentIndex;
                helper.AddText("█", $"{alias}_{currentIndex}", parent, prop, null, 
                    (motion, p, noChar, noStroke) =>
                    {
                        const int defaultScale = 50;
                        var offset = index * 0.003f;
                        motion.Apply(offset + delay, new TextProperty()
                        {
                            x = offsetRow * defaultScale,
                            y = offsetCol * defaultScale
                        });
                        motion.Apply(0.3f, new TextProperty()
                        {
                            x = row * defaultScale,
                            y = col * defaultScale,
                            alpha = 1
                        }, "cubic-bezier(0,.8,.4,1)");
                        motion.Apply(wait);

                        if (index < list2.Count)
                        {
                            row = list2[list2.Count - 1 - index].Key;
                            col = list2[list2.Count - 1 - index].Value;
                            motion.Apply(0.3f, new TextProperty()
                            {
                                x = row * defaultScale,
                                y = col * defaultScale,
                                alpha = 1
                            }, "cubic-bezier(0,.8,.4,1)");
                        }

                        motion.Apply(wait2);
                        
                        if (index < list3.Count)
                        {
                            row = list3[index].Key;
                            col = list3[index].Value;
                            motion.Apply(0.3f, new TextProperty()
                            {
                                x = row * defaultScale,
                                y = col * defaultScale,
                                alpha = 1
                            }, "cubic-bezier(0,.8,.4,1)");
                            motion.Apply(duration);
                        }

                        motion.Apply(0.06f, new TextProperty() {alpha = 0});
                        motion.Apply(0.06f, new TextProperty() {alpha = 1});
                        motion.Apply(0.06f, new TextProperty() {alpha = 0});
                    });
                currentIndex++;
            }
        }
    }
}