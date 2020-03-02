using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    /// <summary>
    /// A place for
    /// TODO 1d,2d,3d Noise Generator funtions
    /// TODO Role a Dice
    /// Toss a Coin.
    /// </summary>
    class PatternGenerator
    {

        static protected Random rnd = new Random();

        void GenerateWhiteNoise(int imageWidth, int imageHeight)
        {
            //imageWidth = imageHeight = 512;
            //const int kNumColors = 3;
            //rockColors[kNumColors] = {
            //    { 0.4078, 0.4078, 0.3764 },
            //    { 0.7606, 0.6274, 0.6313 },
            //    { 0.8980, 0.9372, 0.9725 } };
            //std::ofstream ofs( "./rockpattern.ppm" );
            //ofs << "P6\n" << imageWidth << " " << imageHeight << "\n255\n";
            //for (int j = 0; j < imageWidth; ++j)
            //{
            //    for (int i = 0; i < imageHeight; ++i)
            //    {
            //        unsigned colorIndex = std::min(unsigned(drand48() * kNumColors), kNumColors - 1);
            //        ofs << uchar(rockColors[colorIndex][0] * 255) <<
            //               uchar(rockColors[colorIndex][1] * 255) <<
            //               uchar(rockColors[colorIndex][2] * 255);
            //    }
            //}
            //ofs.close();
        }

        // Abstract Random Generator for Coin Toss
        public static bool RollCoin()
        {
            if (rnd.Next() % 2 == 0)
                return true;
            else
                return false;
        }

    }

}
