// Updated by XamlIntelliSenseFileGenerator 4/26/2023 4:25:18 PM
#pragma checksum "..\..\EmployeeHome.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0D8A6E5F4987D1CA37C71A0253B37C929E2885BE90002F8F692AF94F878B4FFF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GUI;
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


namespace GUI
{


    /// <summary>
    /// EmployeeHome
    /// </summary>
    public partial class EmployeeHome : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 35 "..\..\EmployeeHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock user;

#line default
#line hidden


#line 83 "..\..\EmployeeHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock time;

#line default
#line hidden


#line 130 "..\..\EmployeeHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock amountOrders;

#line default
#line hidden


#line 138 "..\..\EmployeeHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock totalOrders;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GUI;component/employeehome.xaml", System.UriKind.Relative);

#line 1 "..\..\EmployeeHome.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UI = ((GUI.EmployeeHome)(target));
                    return;
                case 2:
                    this.user = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 3:
                    this.time = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 4:

#line 89 "..\..\EmployeeHome.xaml"
                    ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.BtnOrder_Click);

#line default
#line hidden
                    return;
                case 5:

#line 99 "..\..\EmployeeHome.xaml"
                    ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.BtnShowBill_Click);

#line default
#line hidden
                    return;
                case 6:

#line 110 "..\..\EmployeeHome.xaml"
                    ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LogOut_MouseLeftButtonDown);

#line default
#line hidden
                    return;
                case 7:
                    this.amountOrders = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 8:
                    this.totalOrders = ((System.Windows.Controls.TextBlock)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window UI;
    }
}

