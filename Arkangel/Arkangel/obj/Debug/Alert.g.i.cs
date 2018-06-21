﻿#pragma checksum "..\..\Alert.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AD4BDE2427C35D2FA8A79E9347B344407634EB57"
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
using Dragablz;
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
    /// Alert
    /// </summary>
    public partial class Alert : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridCursor;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMain;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView keyword_list;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_add;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_delete;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_sendMail;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_scrShot;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\Alert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_OK;
        
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
            System.Uri resourceLocater = new System.Uri("/Arkangel;component/alert.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Alert.xaml"
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
            this.GridCursor = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.GridMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.keyword_list = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.bt_add = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\Alert.xaml"
            this.bt_add.Click += new System.Windows.RoutedEventHandler(this.bt_add_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bt_delete = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\Alert.xaml"
            this.bt_delete.Click += new System.Windows.RoutedEventHandler(this.bt_delete_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb_sendMail = ((System.Windows.Controls.CheckBox)(target));
            
            #line 53 "..\..\Alert.xaml"
            this.cb_sendMail.Click += new System.Windows.RoutedEventHandler(this.cb_sendMail_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cb_scrShot = ((System.Windows.Controls.CheckBox)(target));
            
            #line 54 "..\..\Alert.xaml"
            this.cb_scrShot.Click += new System.Windows.RoutedEventHandler(this.cb_scrShot_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bt_OK = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\Alert.xaml"
            this.bt_OK.Click += new System.Windows.RoutedEventHandler(this.bt_OK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

