using System;
using System.Windows;
using System.Windows.Controls;

namespace BusinessManager.Controls
{
	public partial class LabeledTextBox : UserControl
	{
		public LabeledTextBox()
		{
			InitializeComponent();
		}

		public String Label
		{
			get { return (String)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabeledTextBox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public String Value
		{
			get { return (String)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(LabeledTextBox), new FrameworkPropertyMetadata("",FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
	}
}
