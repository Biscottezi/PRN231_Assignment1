#pragma checksum "..\..\..\MemberWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4B2EA331A8216AF5EF45D1542F478B6254E9B63B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SalesWPFApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace SalesWPFApp {
    
    
    /// <summary>
    /// MemberWindow
    /// </summary>
    public partial class MemberWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbWelcome;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvOrderHistory;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbId;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbEmail;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbPassword;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbCompany;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbCity;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbCountry;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdate;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvOrderDetail;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/eStoreClient;V1.0.0.0;component/memberwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MemberWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lbWelcome = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lvOrderHistory = ((System.Windows.Controls.ListView)(target));
            
            #line 32 "..\..\..\MemberWindow.xaml"
            this.lvOrderHistory.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.lvOrderHistory_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lbId = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lbEmail = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lbPassword = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lbCompany = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lbCity = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lbCountry = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.btnUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 107 "..\..\..\MemberWindow.xaml"
            this.btnUpdate.Click += new System.Windows.RoutedEventHandler(this.btnUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lvOrderDetail = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

