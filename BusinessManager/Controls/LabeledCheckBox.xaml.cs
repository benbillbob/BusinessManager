using System;
using System.Windows;
using System.Windows.Controls;

namespace BusinessManager.Controls
{
	public partial class LabeledCheckBox : UserControl
	{
		public LabeledCheckBox()
		{
			InitializeComponent();
		}

		public String Label
		{
			get { return (String)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabeledCheckBox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public String Value
		{
			get { return (String)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(bool), typeof(LabeledCheckBox), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
	}
}
