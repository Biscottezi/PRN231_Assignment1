﻿#pragma checksum "..\..\..\OrderPopup.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EAFB0175B79F568C72A066D7B08AE35E90DCB249"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
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
    /// OrderPopup
    /// </summary>
    public partial class OrderPopup : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTitle;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbOrderId;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOrderId;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbMemberId;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMemberId;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbOrderDate;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOrderDate;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbRequiredDate;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRequiredDate;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbShippedDate;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtShippedDate;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbFreight;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFreight;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateOrder;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddDetail;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemoveDetail;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\OrderPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvOrderDetails;
        
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
            System.Uri resourceLocater = new System.Uri("/eStoreClient;component/orderpopup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\OrderPopup.xaml"
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
            this.lbTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lbOrderId = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.txtOrderId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.lbMemberId = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.txtMemberId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.lbOrderDate = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.txtOrderDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.lbRequiredDate = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.txtRequiredDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.lbShippedDate = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.txtShippedDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.lbFreight = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.txtFreight = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.btnCreateOrder = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\OrderPopup.xaml"
            this.btnCreateOrder.Click += new System.Windows.RoutedEventHandler(this.btnCreateOrder_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.btnAddDetail = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\OrderPopup.xaml"
            this.btnAddDetail.Click += new System.Windows.RoutedEventHandler(this.btnAddDetail_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.btnRemoveDetail = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\..\OrderPopup.xaml"
            this.btnRemoveDetail.Click += new System.Windows.RoutedEventHandler(this.btnRemoveDetail_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.lvOrderDetails = ((System.Windows.Controls.ListView)(target));
            
            #line 99 "..\..\..\OrderPopup.xaml"
            this.lvOrderDetails.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lvOrderDetails_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
