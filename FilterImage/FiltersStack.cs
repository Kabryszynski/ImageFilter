using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace FilterImage
{
    class FiltersStack
    {
        private List<Filter> filters;
        public delegate void afterUpdateEventHandler();
        public event afterUpdateEventHandler afterUpdate;

        public FiltersStack() {
            this.filters = new List<Filter>();
        }


        public FiltersStack Add(Filter filter) {
            this.filters.Add(filter);

            if (afterUpdate != null)
            {
                afterUpdate();
            }

            return this;
        }


        public FiltersStack Remove(Filter filter) {
            filters.Remove(filter);

            if (afterUpdate != null)
            {
                afterUpdate();
            }

            return this;
        }


        public FiltersStack Remove(int filterIndex) {
            filters.RemoveAt(filterIndex);

            if (afterUpdate != null)
            {
                afterUpdate();
            }

            return this;
        }


        public List<Filter> getFilters() {
            return filters;
        }
        

        public byte[] apply(byte[] pixels, int width, int height) {

            foreach (Filter filter in filters)
            {
                Debug.WriteLine("Filter: {0}", filter.getName());
                pixels = filter.apply(pixels, width, height);
            }

            return pixels;
        }
         
    }
}
