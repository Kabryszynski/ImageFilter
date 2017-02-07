using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace FilterImage
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private WriteableBitmap WriteableBitmapMainImage;
        private byte[] orygPixels;
        private WriteableBitmap OryginalWriteableBitmapMainImage;

        private List<String> FiltersList = new List<String>();
        private String SelectedFilter;
        private FiltersStack filtersStack;


        public MainPage()
        {
            this.InitializeComponent();
            FiltersDefinitions.prepareFiltersDefinitions();

            filtersStack = new FiltersStack();
            filtersStack.Add(FiltersDefinitions.getFilter(FiltersDefinitions.FiltersDef.BRIGHTNESS));
            filtersStack.Add(FiltersDefinitions.getFilter(FiltersDefinitions.FiltersDef.EDGES_SOBEL_HORIZONTAL_3x3));
            filtersStack.Add(FiltersDefinitions.getFilter(FiltersDefinitions.FiltersDef.TRESHOLD));

            filtersStack.afterUpdate += updateUsedFiltersPanel;
            updateUsedFiltersPanel();

            FiltersList = FiltersDefinitions.getFiltersNames();
            SelectedFilter = FiltersList.First();
        }

        private async void saveFile_click(object sender, RoutedEventArgs e)
        {

            if (!chosenImage())
            {
                var dialog = new MessageDialog("Brak zdjęcia do zapisu");
                await dialog.ShowAsync();

                return;
            }


            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.FileTypeChoices.Add("JPG File", new List<string>() { ".jpg" });
            savePicker.SuggestedFileName = fileName_textBlock.Text.Substring(0, fileName_textBlock.Text.Length-4) + "_contours";

            StorageFile savefile = await savePicker.PickSaveFileAsync();
            if (savefile == null)
                return;

            IRandomAccessStream stream = await savefile.OpenAsync(FileAccessMode.ReadWrite);
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);

            Stream pixelStream = WriteableBitmapMainImage.PixelBuffer.AsStream();
            byte[] pixels = new byte[pixelStream.Length];
            await pixelStream.ReadAsync(pixels, 0, pixels.Length);

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)WriteableBitmapMainImage.PixelWidth, (uint)WriteableBitmapMainImage.PixelHeight, 96.0, 96.0, pixels);
            await encoder.FlushAsync();
        }



        private async void openFile_click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".bmp");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

                    OryginalWriteableBitmapMainImage = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
                    WriteableBitmapMainImage = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);

                    OryginalLayer_Image.Source = OryginalWriteableBitmapMainImage;
                    FilteredLayer_Image.Source = WriteableBitmapMainImage;

                    BitmapTransform transform = new BitmapTransform()
                    {
                        ScaledWidth = Convert.ToUInt32(WriteableBitmapMainImage.PixelWidth),
                        ScaledHeight = Convert.ToUInt32(WriteableBitmapMainImage.PixelHeight)
                    };
                    

                    PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Straight,
                        transform,
                        ExifOrientationMode.IgnoreExifOrientation,
                        ColorManagementMode.DoNotColorManage);

                    byte[] sourcePixels = pixelData.DetachPixelData();
                    orygPixels = (byte[]) sourcePixels.Clone();

                    // copy pixels to filtered layer
                    using (Stream stream = WriteableBitmapMainImage.PixelBuffer.AsStream())
                    {
                        await stream.WriteAsync(sourcePixels, 0, sourcePixels.Length);
                        fileName_textBlock.Text = file.Name;
                    }

                    // copy pixels to oryginal layer
                    using (Stream stream = OryginalWriteableBitmapMainImage.PixelBuffer.AsStream())
                    {
                        await stream.WriteAsync(orygPixels, 0, sourcePixels.Length);
                    }

                }

                WriteableBitmapMainImage.Invalidate();
                OryginalWriteableBitmapMainImage.Invalidate();
            }
        }

        private async void filter_click(object sender, RoutedEventArgs e){
            if (!chosenImage()) {
                var dialog= new MessageDialog("Wybirz obraz do przetworzenia");
                await dialog.ShowAsync();
                return;
            }
   
            byte[] pixels = filtersStack.apply( (byte[]) orygPixels.Clone(), WriteableBitmapMainImage.PixelWidth, WriteableBitmapMainImage.PixelHeight);

            using (Stream stream = WriteableBitmapMainImage.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(pixels, 0, pixels.Length);
            }

            WriteableBitmapMainImage.Invalidate();
        }


        private bool chosenImage() {
            return WriteableBitmapMainImage != null;
        }

        private void Advanced_Click(object sender, RoutedEventArgs e)
        {
            if (GridRow_Advance.MaxHeight == 0)
            {
          //      GridRow_Simple.MaxHeight = Double.PositiveInfinity;
                GridRow_Advance.MaxHeight = Double.PositiveInfinity;

            }
            else {
                GridRow_Advance.MaxHeight = 0;

                //      GridRow_Simple.MaxHeight = 0;
            }
        }


       


        private void UseFilter_Click(object sender, RoutedEventArgs e)
        {
            var filterName = Filters_ComboBox.SelectedValue as string;
            if (filterName == null) { return; };

            Filter selectedfilter = FiltersDefinitions.getFilter(filterName);

            if (selectedfilter != null) {
                filtersStack.Add(selectedfilter);

                if (autoPreview_Switch.IsOn) {
                    filter_click(sender, e);
                }
            }
        }


        private void removeUsedFilter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string btnName = btn.Name.ToString();

            int filterIndex = Convert.ToInt16(Regex.Match(btnName, @"^.*_(\d+)_.*$").Groups[1].Value);

            filtersStack.Remove(filterIndex);

            if (autoPreview_Switch.IsOn)
            {
                filter_click(sender, e);
            }
        }


        private void updateUsedFiltersPanel() {
            Button prevBtn = null;
            int counter = 0;
            UsedFilters_Panel.Children.Clear();

            foreach (var filter in filtersStack.getFilters())
            {
                // update usedFiltersPanel
                Button but = new Button()
                {
                    Name = "filter_" + counter + "_" + filter.getName(),
                    Content = filter.getName().Substring(0, 1),
                    Style = (Style)this.Resources["UsedFilter"]
                };
                but.Click += removeUsedFilter_Click;

                if (prevBtn != null)
                {
                    RelativePanel.SetRightOf(but, prevBtn);
                }

                UsedFilters_Panel.Children.Add(but);

                if (filter.GetType() == typeof(MatrixFilter))
                {
                    Match mat_dim = Regex.Match(filter.getName(), "_(.*)$");

                    TextBlock tb_mat = new TextBlock()
                    {
                        Text = mat_dim.Groups[1].Value,
                        Style = (Style)this.Resources["UsedFilter_matrix"]
                    };

                    RelativePanel.SetAlignRightWith(tb_mat, but);
                    RelativePanel.SetAlignBottomWith(tb_mat, but);

                    UsedFilters_Panel.Children.Add(tb_mat);
                }




                prevBtn = but;
                counter++;
            }

        }

        private void AutoPreview_Toggle(object sender, RoutedEventArgs e)
        {
            if (autoPreview_Switch.IsOn)
            {
                filter_click(sender, e);
            }
        }

        private void changeFilterLayerOpacity_Change(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider == null) return;

            FilteredLayer_Image.Opacity = (100 - slider.Value)/100;
        }
    }

    
}
