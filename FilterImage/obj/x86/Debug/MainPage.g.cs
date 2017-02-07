﻿#pragma checksum "C:\Users\krzys\documents\visual studio 2015\Projects\FilterImage\FilterImage\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "85089E97C6CA9E63013F6669C33752EF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilterImage
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_Primitives_Selector_SelectedItem(global::Windows.UI.Xaml.Controls.Primitives.Selector obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.SelectedItem = value;
            }
        };

        private class MainPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::FilterImage.MainPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ComboBox obj10;

            public MainPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 10:
                        this.obj10 = (global::Windows.UI.Xaml.Controls.ComboBox)target;
                        break;
                    default:
                        break;
                }
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // MainPage_obj1_Bindings

            public void SetDataRoot(global::FilterImage.MainPage newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::FilterImage.MainPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_FiltersList(obj.FiltersList, phase);
                        this.Update_SelectedFilter(obj.SelectedFilter, phase);
                    }
                }
            }
            private void Update_FiltersList(global::System.Collections.Generic.List<global::System.String> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj10, obj, null);
                }
            }
            private void Update_SelectedFilter(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Primitives_Selector_SelectedItem(this.obj10, obj, null);
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.UsedFilter = (global::Windows.UI.Xaml.Style)(target);
                }
                break;
            case 3:
                {
                    this.UsedFilter_matrix = (global::Windows.UI.Xaml.Style)(target);
                }
                break;
            case 4:
                {
                    this.GridRow_Advance = (global::Windows.UI.Xaml.Controls.RowDefinition)(target);
                }
                break;
            case 5:
                {
                    this.OpenFile_Button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 68 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.OpenFile_Button).Click += this.openFile_click;
                    #line default
                }
                break;
            case 6:
                {
                    this.SaveFile_Button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 74 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SaveFile_Button).Click += this.saveFile_click;
                    #line default
                }
                break;
            case 7:
                {
                    this.fileName_textBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8:
                {
                    this.OryginalLayer_Image = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 9:
                {
                    this.FilteredLayer_Image = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 10:
                {
                    this.Filters_ComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 11:
                {
                    this.SelectFilter_Button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 113 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectFilter_Button).Click += this.UseFilter_Click;
                    #line default
                }
                break;
            case 12:
                {
                    this.autoPreview_Switch = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                    #line 124 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ToggleSwitch)this.autoPreview_Switch).Toggled += this.AutoPreview_Toggle;
                    #line default
                }
                break;
            case 13:
                {
                    this.filterLayerOpacity_Slider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 143 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)this.filterLayerOpacity_Slider).ValueChanged += this.changeFilterLayerOpacity_Change;
                    #line default
                }
                break;
            case 14:
                {
                    this.UsedFilters_Panel = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 15:
                {
                    global::Windows.UI.Xaml.Controls.Button element15 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 158 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element15).Click += this.Advanced_Click;
                    #line default
                }
                break;
            case 16:
                {
                    this.Filter_Button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 164 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Filter_Button).Click += this.filter_click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    MainPage_obj1_Bindings bindings = new MainPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

