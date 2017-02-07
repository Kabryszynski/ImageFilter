using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterImage
{
    class Filter
    {
        private String name { get; set; }

        public Filter(String name) {
            this.name = name;
        }

        public String getName() {
            return this.name;
        }

        virtual public byte[] apply(byte[] pixels, int width, int height) {
            // TODO: rewrite... Fitler to abstract class?
            byte[] mockPix = new byte[1];
            Debug.WriteLine("Mock");
            return mockPix;
        }

        public static byte brightness(byte B, byte G, byte R){
            return (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);
        }
    }



    class LinearFilter : Filter {

        private Func<byte, byte, byte, byte, byte[]> filterFunction;

        public LinearFilter(String name, Func<byte, byte, byte, byte, byte[]> filterFunc) : base(name: name) {
            this.filterFunction = filterFunc;
        }

        public override byte[] apply(byte[] pixels, int width, int height)
        {
            return LinearFilter.applyFunctionForEachPixel(pixels, width, height, this.filterFunction);
        }

        private static byte[] applyFunctionForEachPixel(byte[] pixels, int width, int height, Func<byte, byte, byte, byte, byte[]> filterFunc) {
            int index = 0;
            byte R, G, B, A;
            byte[] RGBA_filtered = new byte[4];

            for (int y = 0; y < height; y++){
                for (int x = 0; x < width; x++)
                {
                    index = (y * width + x) * 4;
                    R = pixels[index + 2];
                    G = pixels[index + 1];
                    B = pixels[index + 0];
                    A = pixels[index + 3];

                    RGBA_filtered = filterFunc(R, G, B, A);

                    pixels[index + 0] = RGBA_filtered[2];  // B
                    pixels[index + 1] = RGBA_filtered[1];  // G
                    pixels[index + 2] = RGBA_filtered[0];  // R
                    pixels[index + 3] = RGBA_filtered[3];  // A
                }
            }

            return pixels;
        }

    }



    class MatrixFilter : Filter {

        private double[,] matrix;

        public MatrixFilter(String name, double[,] matrix) : base(name: name) {
            this.matrix = matrix;
        }

        public override byte[] apply(byte[] pixels, int width, int height)
        {
            return MatrixFilter.applyMatrix(pixels, width, height, this.matrix);
        }

        private static byte[] applyMatrix(byte[] pixels, int width, int height, double[,] matrix) {
            int index = 0;  // active point index
            int index_tl = 0;  // index top left
            int index_inner = 0;  // index inner matrix to calculate new value

            // params to normalize by scale
            /*
            double minR, maxR, minG, maxG, minB, maxB;
            minR = minG = minB = double.MaxValue;
            maxR = maxG = maxB = double.MinValue;
            */

            double[] doublePixels = new double[pixels.Length];  // sometimes pixels go outside <0,255> range or are decimals

            double valueR = 0, valueG = 0, valueB = 0;
            int size = matrix.GetLength(0);
            int halfSize = (int)Math.Floor((double)size / 2);

            for (int y = halfSize; y < height - halfSize; y++)
            {
                for (int x = halfSize; x < width - halfSize; x++)
                {
                    index = (y * width + x) * 4;
                    index_tl = index - (width * halfSize + halfSize) * 4;
                    valueR = valueG = valueB = 0;

                    for (int j = 0; j < size; j++)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            index_inner = index_tl + 4 * (j * width + i);

                            valueB += pixels[index_inner + 0] * matrix[j,i];
                            valueG += pixels[index_inner + 1] * matrix[j,i];
                            valueR += pixels[index_inner + 2] * matrix[j,i];
                        }
                    }

                    // params to normalize by scale
                    /*
                    minR = valueR < minR ? valueR : minR;
                    maxR = valueR > maxR ? valueR : maxR;
                    minG = valueG < minG ? valueG : minG;
                    maxG = valueG > maxG ? valueG : maxG;
                    minB = valueB < minB ? valueB : minB;
                    maxB = valueB > maxB ? valueB : maxB;
                    */

                    doublePixels[index + 0] = valueB;
                    doublePixels[index + 1] = valueG;
                    doublePixels[index + 2] = valueR;
                }
            }


            // normalize by scale R, G and B Channels - Doent't work correctly (apply blur => sherpen)
           /*
            for (int i = 0; i < width * height * 4; i+=4) {
                pixels[i + 0] = (byte)( (doublePixels[i + 0] - minB) * (255.0 - 0) / (maxB - minB) + 0);
                pixels[i + 1] = (byte)( (doublePixels[i + 1] - minG) * (255.0 - 0) / (maxG - minG) + 0);
                pixels[i + 2] = (byte)( (doublePixels[i + 2] - minR) * (255.0 - 0) / (maxR - minR) + 0);
            }
            */

            // normalize by cut outside of the range
            for (int i = 0; i < width * height * 4; i += 4)
            {
                pixels[i + 0] = (byte) Math.Min(Math.Max(doublePixels[i + 0], (byte)0), (byte)255);
                pixels[i + 1] = (byte) Math.Min(Math.Max(doublePixels[i + 1], (byte)0), (byte)255);
                pixels[i + 2] = (byte) Math.Min(Math.Max(doublePixels[i + 2], (byte)0), (byte)255);
            }
            
            return pixels;
        }

    }
}
