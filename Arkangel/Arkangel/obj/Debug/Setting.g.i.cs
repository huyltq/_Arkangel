﻿#pragma checksum "..\..\Setting.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78279FA40D7FCB5151F948BB5D98F962261B1E0A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Arkangel;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Arkangel {
    
    
    /// <summary>
    /// Setting
    /// </summary>
    public partial class Setting : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_quit;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Keystroke;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_browserText;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Webcam;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_browserWebcam;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Scrshot;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_browerScrshot;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbb_keystroke;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_profilepath;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_profile;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Arkangel;component/setting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Setting.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.bt_quit = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\Setting.xaml"
            this.bt_quit.Click += new System.Windows.RoutedEventHandler(this.bt_quit_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tb_Keystroke = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.bt_browserText = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\Setting.xaml"
            this.bt_browserText.Click += new System.Windows.RoutedEventHandler(this.bt_browserText_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tb_Webcam = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.bt_browserWebcam = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\Setting.xaml"
            this.bt_browserWebcam.Click += new System.Windows.RoutedEventHandler(this.bt_browserWebcam_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tb_Scrshot = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.bt_browerScrshot = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\Setting.xaml"
            this.bt_browerScrshot.Click += new System.Windows.RoutedEventHandler(this.bt_browerScrshot_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cbb_keystroke = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.tb_profilepath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.bt_profile = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\Setting.xaml"
            this.bt_profile.Click += new System.Windows.RoutedEventHandler(this.bt_profile_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

