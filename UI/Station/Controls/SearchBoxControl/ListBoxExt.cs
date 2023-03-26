using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdionFA.UI.Station.Controls.SearchBoxControl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AdionFA.UI.Station.Controls.SearchTextBoxControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AdionFA.UI.Station.Controls.SearchTextBoxControl;assembly=AdionFA.UI.Station.Controls.SearchTextBoxControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ListBoxExt/>
    ///
    /// </summary>
    public class ListBoxExt : ListBox
    {
        public enum ItemStyles
        {
            NormalStyle,
            CheckBoxStyle,
            RadioBoxStyle
        }

        #region Properties

        ItemStyles extendedStyle;
        public ItemStyles ExtendedStyle
        {
            get => extendedStyle; 
            set
            {
                extendedStyle = value;

                // load resources
                ResourceDictionary resDict = new ResourceDictionary();
                resDict.Source = new Uri("pack://application:,,,/AdionFA.UI.Station.Controls.SearchBoxControl;component/Themes/ListBoxExt.xaml");
                if (resDict.Source == null)
                    throw new SystemException();

                switch (value)
                {
                    case ItemStyles.NormalStyle:
                        this.Style = (Style)resDict["NormalListBox"];
                        break;
                    case ItemStyles.CheckBoxStyle:
                        this.Style = (Style)resDict["CheckListBox"];
                        break;
                    case ItemStyles.RadioBoxStyle:
                        this.Style = (Style)resDict["RadioListBox"];
                        break;
                }
            }
        }

        #endregion

        #region Ctor

        static ListBoxExt()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxExt), new FrameworkPropertyMetadata(typeof(ListBoxExt)));
        }

        public ListBoxExt() : base()
        {
            ExtendedStyle = ItemStyles.NormalStyle;
        }

        public ListBoxExt(ItemStyles style) : base()
        {
            ExtendedStyle = style;
        }

        #endregion
    }
}
