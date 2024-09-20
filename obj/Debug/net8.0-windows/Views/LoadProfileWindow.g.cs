﻿#pragma checksum "..\..\..\..\Views\LoadProfileWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D338382CD820CB809C9F45B29612A405F6532F05"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SRCClient.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace SRCClient.Views {
    
    
    /// <summary>
    /// LoadProfileWindow
    /// </summary>
    public partial class LoadProfileWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 194 "..\..\..\..\Views\LoadProfileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ProfilesBorder;
        
        #line default
        #line hidden
        
        
        #line 200 "..\..\..\..\Views\LoadProfileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ProfileScrollBar;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\..\..\Views\LoadProfileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ProfileStack;
        
        #line default
        #line hidden
        
        
        #line 212 "..\..\..\..\Views\LoadProfileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NewProfileButton;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\..\Views\LoadProfileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImportProfileButton;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\..\..\Views\LoadProfileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteAllProfilesButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SRCClient;component/views/loadprofilewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\LoadProfileWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 180 "..\..\..\..\Views\LoadProfileWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BorderMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 189 "..\..\..\..\Views\LoadProfileWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButtonClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ProfilesBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.ProfileScrollBar = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 5:
            this.ProfileStack = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.NewProfileButton = ((System.Windows.Controls.Button)(target));
            
            #line 222 "..\..\..\..\Views\LoadProfileWindow.xaml"
            this.NewProfileButton.Click += new System.Windows.RoutedEventHandler(this.NewProfileButtonClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ImportProfileButton = ((System.Windows.Controls.Button)(target));
            
            #line 233 "..\..\..\..\Views\LoadProfileWindow.xaml"
            this.ImportProfileButton.Click += new System.Windows.RoutedEventHandler(this.ImportProfileButtonClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DeleteAllProfilesButton = ((System.Windows.Controls.Button)(target));
            
            #line 247 "..\..\..\..\Views\LoadProfileWindow.xaml"
            this.DeleteAllProfilesButton.Click += new System.Windows.RoutedEventHandler(this.DeleteAllProfilesButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

