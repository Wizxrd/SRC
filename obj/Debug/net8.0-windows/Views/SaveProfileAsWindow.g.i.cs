﻿#pragma checksum "..\..\..\..\Views\SaveProfileAsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "98DB3780FDB4CA2705FF994C550C0A34E8D26540"
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
    /// SaveProfileAsWindow
    /// </summary>
    public partial class SaveProfileAsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 66 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseSaveProfileAsButton;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProfileNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveProfileButton;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
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
            System.Uri resourceLocater = new System.Uri("/SRCClient;V0.0.3.0;component/views/saveprofileaswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
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
            
            #line 57 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BorderMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CloseSaveProfileAsButton = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
            this.CloseSaveProfileAsButton.Click += new System.Windows.RoutedEventHandler(this.CloseButtonClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ProfileNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.SaveProfileButton = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
            this.SaveProfileButton.Click += new System.Windows.RoutedEventHandler(this.SaveProfileButtonClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\..\Views\SaveProfileAsWindow.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.CloseButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

