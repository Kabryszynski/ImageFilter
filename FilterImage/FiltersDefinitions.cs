using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterImage
{
    class FiltersDefinitions
    {
     
        private static List<Filter> filters = new List<Filter>();
        public enum FiltersDef {
            TRESHOLD,
            INVERT,
            BRIGHTNESS,
            BLUR_3x3,
            SHARP_SOBEL_HORIZONTAL_3x3,
            EDGES_SOBEL_HORIZONTAL_3x3,
            LAPLACE
        };

        public static List<String> getFiltersNames() {
            return filters.Select(f => f.getName()).ToList();
        }


        public static Filter getFilter(FiltersDef filterDef) {
            Filter filter = null;
            String filterName = "";

            switch (filterDef) {
                case FiltersDef.TRESHOLD:
                    filterName = "Treshold";
                    break;
                case FiltersDef.BLUR_3x3:
                    filterName = "Blur_3x3";
                    break;
                case FiltersDef.SHARP_SOBEL_HORIZONTAL_3x3:
                    filterName = "SharpSobelHorizontal_3x3";
                    break;
                case FiltersDef.EDGES_SOBEL_HORIZONTAL_3x3:
                    filterName = "EdgesSobelHorizontal_3x3";
                    break;
                case FiltersDef.INVERT:
                    filterName = "Invert";
                    break;
                case FiltersDef.BRIGHTNESS:
                    filterName = "Brightness";
                    break;
                case FiltersDef.LAPLACE:
                    filterName = "Laplace_3x3";
                    break;
                default:
                    break;
            }

            filter = filters.Find(f => f.getName() == filterName);
            
            return filter;
        }


        public static Filter getFilter(String filterName)
        {
            Filter filter = null;
            filter = filters.Find(f => f.getName() == filterName);

            return filter;
        }


        public static void prepareFiltersDefinitions() {

            LinearFilter treshold_Filter = new LinearFilter("Treshold", (byte R, byte G, byte B, byte A) => {
                byte R_new, G_new, B_new, A_new;
                byte value = (byte)(Filter.brightness(R, G, B) < 70 ? 0 : 255);

                R_new = value;
                G_new = value;
                B_new = value;
                A_new = A;

                return new byte[4] { R_new, G_new, B_new, A_new };
            });
            filters.Add(treshold_Filter);


            LinearFilter brightness_Filter = new LinearFilter("Brightness", (byte R, byte G, byte B, byte A) => {
                byte R_new, G_new, B_new, A_new;
                byte value = (byte) Filter.brightness(R, G, B);

                R_new = value;
                G_new = value;
                B_new = value;
                A_new = A;

                return new byte[4] { R_new, G_new, B_new, A_new };
            });
            filters.Add(brightness_Filter);


            LinearFilter invert_Filter = new LinearFilter("Invert", (byte R, byte G, byte B, byte A) => {
                byte R_new, G_new, B_new, A_new;

                R_new = (byte) (255 - R);
                G_new = (byte) (255 - G);
                B_new = (byte) (255 - B);
                A_new = A;

                return new byte[4] { R_new, G_new, B_new, A_new };
            });
            filters.Add(invert_Filter);


            LinearFilter ChannelR_Filter = new LinearFilter("Channel R", (byte R, byte G, byte B, byte A) => {
                byte R_new, G_new, B_new, A_new;

                R_new = G_new = B_new = R;
                A_new = A;

                return new byte[4] { R_new, G_new, B_new, A_new };
            });
            filters.Add(ChannelR_Filter);


            LinearFilter ChannelG_Filter = new LinearFilter("Channel G", (byte R, byte G, byte B, byte A) => {
                byte R_new, G_new, B_new, A_new;

                R_new = G_new = B_new = G;
                A_new = A;

                return new byte[4] { R_new, G_new, B_new, A_new };
            });
            filters.Add(ChannelG_Filter);


            LinearFilter ChannelB_Filter = new LinearFilter("Channel B", (byte R, byte G, byte B, byte A) => {
                byte R_new, G_new, B_new, A_new;

                R_new = G_new = B_new = B;
                A_new = A;

                return new byte[4] { R_new, G_new, B_new, A_new };
            });
            filters.Add(ChannelB_Filter);


            LinearFilter whiteToTransparent = new LinearFilter("WhiteToTransparent", (byte R, byte G, byte B, byte A) => {
                byte A_new;

                A_new = (byte) ( (R == 255 && G == 255 && B == 255) ? 0 : A);

                return new byte[4] { R, G, B, A_new };
            });
            filters.Add(whiteToTransparent);


            LinearFilter blackToTransparent = new LinearFilter("BlackToTransparent", (byte R, byte G, byte B, byte A) => {
                byte A_new;

                A_new = (byte)((R == 0 && G == 0 && B == 0) ? 0 : A);

                return new byte[4] { R, G, B, A_new };
            });
            filters.Add(blackToTransparent);


            double[,] blur3x3_Filter_matrix = new double[,] {
                    {1.0/9, 1.0/9, 1.0/9},
                    {1.0/9, 1.0/9, 1.0/9},
                    {1.0/9, 1.0/9, 1.0/9}
                };
            MatrixFilter blur3x3_Filter = new MatrixFilter("Blur_3x3", blur3x3_Filter_matrix);
            filters.Add(blur3x3_Filter);


            double[,] blur5x5_Filter_matrix = new double[,] {
                    {1.0/25, 1.0/25, 1.0/25, 1.0/25, 1.0/25},
                    {1.0/25, 1.0/25, 1.0/25, 1.0/25, 1.0/25},
                    {1.0/25, 1.0/25, 1.0/25, 1.0/25, 1.0/25},
                    {1.0/25, 1.0/25, 1.0/25, 1.0/25, 1.0/25},
                    {1.0/25, 1.0/25, 1.0/25, 1.0/25, 1.0/25}
                };
            MatrixFilter blur5x5_Filter = new MatrixFilter("Blur_5x5", blur5x5_Filter_matrix);
            filters.Add(blur5x5_Filter);

            double[,] blur7x7_Filter_matrix = new double[,] {
                    {1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49},
                    {1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49},
                    {1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49},
                    {1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49},
                    {1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49},
                    {1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49},
                    {1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49, 1.0/49}
                };
            MatrixFilter blur7x7_Filter = new MatrixFilter("Blur_7x7", blur7x7_Filter_matrix);
            filters.Add(blur7x7_Filter);


            double[,] sharp1_Filter_matrix = new double[,] {
                    { 1, -2,  1},
                    {-2,  9, -2},
                    { 1, -2,  1}
                };
            MatrixFilter sharp1_3x3_Filter = new MatrixFilter("Sharp1_3x3", sharp1_Filter_matrix);
            filters.Add(sharp1_3x3_Filter);


            double[,] sharp2_Filter_matrix = new double[,] {
                    { 0, -1,  0},
                    {-1, 5, -1},
                    { 0, -1,  0}
                };
            MatrixFilter sharp2_3x3_Filter = new MatrixFilter("Sharp2_3x3", sharp2_Filter_matrix);
            filters.Add(sharp2_3x3_Filter);


            double[,] sharp3_Filter_matrix = new double[,] {
                    {-1, -1,  0},
                    {-1,  1,  1},
                    { 0,  1,  1}
                };
            MatrixFilter sharp3_3x3_Filter = new MatrixFilter("Sharp3_3x3", sharp3_Filter_matrix);
            filters.Add(sharp3_3x3_Filter);


            double[,] laplace_Filter_matrix = new double[,] {
                    { 0,  1,  0},
                    { 1, -4,  1},
                    { 0,  1,  0}
                };
            MatrixFilter laplace_3x3_Filter = new MatrixFilter("Laplace_3x3", laplace_Filter_matrix);
            filters.Add(laplace_3x3_Filter);

            double[,] edges1_Filter_matrix = new double[,] {
                    { 0, -1,  0},
                    {-1,  4, -1},
                    { 0, -1,  0}
                };
            MatrixFilter edges1_3x3_Filter = new MatrixFilter("Edges1_3x3", edges1_Filter_matrix);
            filters.Add(edges1_3x3_Filter);


            double[,] edges2_Filter_matrix = new double[,] {
                    {-1, -1, -1},
                    {-1,  8, -1},
                    {-1, -1, -1}
                };
            MatrixFilter edges2_3x3_Filter = new MatrixFilter("Edges2_3x3", edges2_Filter_matrix);
            filters.Add(edges2_3x3_Filter);


            double[,] edges3_Filter_matrix = new double[,] {
                    { 1, -2,  1},
                    {-2,  4, -2},
                    { 1, -2,  1}
                };
            MatrixFilter edges3_3x3_Filter = new MatrixFilter("Edges3_3x3", edges3_Filter_matrix);
            filters.Add(edges3_3x3_Filter);


            double[,] edges4_Filter_matrix = new double[,] {
                    { 0, -1,  0},
                    { 0,  1,  0},
                    { 0,  0,  0}
                };
            MatrixFilter edges4_3x3_Filter = new MatrixFilter("Edges4_3x3", edges4_Filter_matrix);
            filters.Add(edges4_3x3_Filter);

            double[,] sobelVertical_Filter_matrix = new double[,] {
                    {-1,  0,  1},
                    {-2,  1,  2},
                    {-1,  0,  1}
                };
            MatrixFilter sobelVertical_3x3_Filter = new MatrixFilter("SharpSobelVertical_3x3", sobelVertical_Filter_matrix);
            filters.Add(sobelVertical_3x3_Filter);

            double[,] sobelHorizontal_Filter_matrix = new double[,] {
                    {-1, -2, -1},
                    { 0,  1,  0},
                    { 1,  2,  1}
                };
            MatrixFilter sobelHorizontal_3x3_Filter = new MatrixFilter("SharpSobelHorizontal_3x3", sobelHorizontal_Filter_matrix);
            filters.Add(sobelHorizontal_3x3_Filter);

            
            double[,] edgesHorizontal_Filter_matrix = new double[,] {
                    {-1, -2, -1},
                    { 0,  0,  0},
                    { 1,  2,  1}
                };
            MatrixFilter edgesHorizontal_3x3_Filter = new MatrixFilter("EdgesSobelHorizontal_3x3", edgesHorizontal_Filter_matrix);
            filters.Add(edgesHorizontal_3x3_Filter);


            double[,] edgesVertical_Filter_matrix = new double[,] {
                    {-1,  0,  1},
                    {-2,  0,  2},
                    {-1,  0,  1}
                };
            MatrixFilter edgesVertical_3x3_Filter = new MatrixFilter("EdgesSobelVertical_3x3", edgesVertical_Filter_matrix);
            filters.Add(edgesVertical_3x3_Filter);
        }

    }
}
