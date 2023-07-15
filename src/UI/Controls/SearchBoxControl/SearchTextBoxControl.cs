using AdionFA.UI.Controls.SearchBoxControl.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace AdionFA.UI.Controls.SearchBoxControl
{
    public class SearchEventArgs : RoutedEventArgs
    {
        string keyword = "";
        public string Keyword
        {
            get => keyword;
            set => keyword = value;
        }

        List<string> sections = new List<string>();
        public List<string> Sections
        {
            get => sections;
            set => sections = value;
        }

        public SearchEventArgs() : base()
        {

        }

        public SearchEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {

        }
    }

    public enum SectionsStyles
    {
        NormalStyle,
        CheckBoxStyle,
        RadioBoxStyle
    }

    public class SearchTextBoxControl : TextBox
    {
        #region Properties

        public static DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(SearchTextBoxControl));
        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty); set => SetValue(LabelTextProperty, value);
        }

        public static DependencyProperty LabelTextColorProperty =
            DependencyProperty.Register("LabelTextColor", typeof(Brush), typeof(SearchTextBoxControl));
        public Brush LabelTextColor
        {
            get => (Brush)GetValue(LabelTextColorProperty); set => SetValue(LabelTextColorProperty, value);
        }

        static DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterReadOnly("HasText", typeof(bool), typeof(SearchTextBoxControl), new PropertyMetadata());
        public static DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;
        public bool HasText
        {
            get => (bool)GetValue(HasTextProperty); private set => SetValue(HasTextPropertyKey, value);
        }

        static DependencyPropertyKey IsMouseLeftButtonDownPropertyKey =
            DependencyProperty.RegisterReadOnly("IsMouseLeftButtonDown", typeof(bool), typeof(SearchTextBoxControl), new PropertyMetadata());
        public static DependencyProperty IsMouseLeftButtonDownProperty = IsMouseLeftButtonDownPropertyKey.DependencyProperty;
        public bool IsMouseLeftButtonDown
        {
            get => (bool)GetValue(IsMouseLeftButtonDownProperty); private set => SetValue(IsMouseLeftButtonDownPropertyKey, value);
        }

        public static DependencyProperty SectionsListProperty =
            DependencyProperty.Register("SectionsList", typeof(List<string>), typeof(SearchTextBoxControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        public List<string> SectionsList
        {
            get => (List<string>)GetValue(SectionsListProperty); set => SetValue(SectionsListProperty, value);
        }

        public static DependencyProperty SectionsSelectedProperty =
            DependencyProperty.Register("SectionsSelected", typeof(List<string>), typeof(SearchTextBoxControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        public List<string> SectionsSelected
        {
            get => (List<string>)GetValue(SectionsSelectedProperty); set => SetValue(SectionsSelectedProperty, value);
        }

        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent("Search", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchTextBoxControl));

        #endregion

        #region Controls

        private Popup listPopup = new Popup();
        private ListBoxExt listSection = null;
        private ListBoxExt listPreviousItem = null;

        #endregion

        #region Ctor

        static SearchTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBoxControl), new FrameworkPropertyMetadata(typeof(SearchTextBoxControl)));
        }

        public SearchTextBoxControl()
            : base()
        {
        }

        #endregion

        #region Events

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MouseLeave += new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                if (!listPopup.IsMouseOver)
                    HidePopup();
            });

            //Search////////////////////////////////////////////////////////////////////////////////////////////////
            var iconBorder = GetTemplateChild("PART_SearchIconBorder") as Border;
            if (iconBorder != null)
            {
                iconBorder.MouseLeftButtonDown += new MouseButtonEventHandler((object obj, MouseButtonEventArgs e) =>
                {
                    IsMouseLeftButtonDown = true;
                    Text = string.Empty;
                });

                iconBorder.MouseLeftButtonUp += new MouseButtonEventHandler((object obj, MouseButtonEventArgs e) =>
                {
                    if (!IsMouseLeftButtonDown) return;

                    if (HasText)
                    {
                        RaiseSearchEvent();
                    }

                    IsMouseLeftButtonDown = false;
                });
                iconBorder.MouseLeave += new MouseEventHandler((object obj, MouseEventArgs e) => IsMouseLeftButtonDown = false);
                iconBorder.MouseDown += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) => HidePopup());
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            //Filter////////////////////////////////////////////////////////////////////////////////////////////
            var size = 0;
            if (ShowSectionButton)
            {
                iconBorder = GetTemplateChild("PART_FilterIconBorder") as Border;
                if (iconBorder != null)
                {
                    iconBorder.MouseDown += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) =>
                    {
                        if (SectionsList == null)
                            return;
                        if (SectionsList.Count != 0)
                            ShowPopup(listSection);
                    });
                }
                size = 15;
            }
            var iconChoose = GetTemplateChild("FilterIcon") as Image;
            if (iconChoose != null)
                iconChoose.Width = iconChoose.Height = size;
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Previous search///////////////////////////////////////////////////////////////////////////////////
            //iconBorder = GetTemplateChild("PART_PreviousItem") as Border;
            //if (iconBorder != null)
            //    iconBorder.MouseDown += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) => 
            //    {
            //        if (listPreviousItem.Items.Count != 0)
            //            ShowPopup(listPreviousItem);
            //    });
            ////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // if users click on the editing area, the pop up will be hidden
            var sourceType = e.OriginalSource.GetType();
            if (sourceType != typeof(Image))
                HidePopup();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Text = "";
            }
            else if ((e.Key == Key.Return || e.Key == Key.Enter))
            {
                RaiseSearchEvent();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void RaiseSearchEvent()
        {
            if (!string.IsNullOrEmpty(Text) && !listPreviousItem.Items.Contains(this.Text))
                listPreviousItem.Items.Add(this.Text);


            var args = new SearchEventArgs(SearchEvent);
            args.Keyword = this.Text;
            if (listSection != null)
            {
                args.Sections = (List<string>)listSection.SelectedItems.Cast<string>().ToList();
                SectionsSelected = Array.Empty<string>().ToList();
                foreach (var item in listSection.SelectedItems.Cast<string>().ToList())
                {
                    SectionsSelected.Add(item);
                }
            }
            RaiseEvent(args);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            HasText = Text.Length != 0;
            HidePopup();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (!HasText)
                this.LabelText = i18n.SearchRecent;
            listPopup.StaysOpen = false;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (!HasText)
                this.LabelText = string.Empty;
            listPopup.StaysOpen = true;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            BuildPopup();
        }

        public event RoutedEventHandler OnSearch
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        #endregion

        #region Popup

        bool showSectionButton = true;
        public bool ShowSectionButton
        {
            get => showSectionButton; set => showSectionButton = value;
        }

        SectionsStyles itemStyle = SectionsStyles.CheckBoxStyle;
        public SectionsStyles SectionsStyle
        {
            get => itemStyle; set => itemStyle = value;
        }

        private void BuildPopup()
        {
            // initialize the pop up
            listPopup.PopupAnimation = PopupAnimation.Fade;
            listPopup.Placement = PlacementMode.Relative;
            listPopup.PlacementTarget = this;
            listPopup.PlacementRectangle = new Rect(0, this.ActualHeight, 30, 30);
            listPopup.Width = this.ActualWidth;
            // initialize the sections' list
            if (ShowSectionButton)
            {
                listSection = new ListBoxExt((int)itemStyle + ListBoxExt.ItemStyles.NormalStyle);

                //////////////////////////////////////////////////////////////////////////
                listSection.Items.Clear();
                // add items into the list
                // is there any smarter way?
                if (SectionsList != null)
                    foreach (var item in SectionsList)
                        listSection.Items.Add(item);
                //////////////////////////////////////////////////////////////////////////

                listSection.Width = this.Width;
                listSection.MouseLeave += new MouseEventHandler((object sender, MouseEventArgs e) => HidePopup());
            }

            // initialize the previous items' list
            listPreviousItem = new ListBoxExt();
            listPreviousItem.MouseLeave += new MouseEventHandler((object sender, MouseEventArgs e) => HidePopup());
            listPreviousItem.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) =>
            {
                // fetch the previous keyword into the text box
                Text = listPreviousItem.SelectedItems[0].ToString();
                // close the pop up
                HidePopup();

                Focus();
                SelectionStart = this.Text.Length;
            });
            listPreviousItem.Width = this.Width;
        }

        private void HidePopup()
        {
            listPopup.IsOpen = false;
        }

        private void ShowPopup(UIElement item)
        {
            listPopup.StaysOpen = true;
            listPopup.Child = item;
            listPopup.IsOpen = true;
        }

        #endregion
    }
}
