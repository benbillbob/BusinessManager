using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BusinessManager.Controls
{
	/// <summary>
	/// Interaction logic for LabeledControl.xaml
	/// </summary>
	public partial class LabeledTextBox : UserControl, INotifyPropertyChanged
	{
		public LabeledTextBox()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabeledTextBox), new FrameworkPropertyMetadata("Label"));

		public string Label
		{
			get 
			{ 
				return ((string)(base.GetValue(LabeledTextBox.LabelProperty))); 
			}
			set 
			{
				base.SetValue(LabeledTextBox.LabelProperty, value);
				OnPropertyChanged("Label");
			}
		}

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(LabeledTextBox), new FrameworkPropertyMetadata("Value"));

		public string Value
		{
			get 
			{ 
				return ((string)(base.GetValue(LabeledTextBox.ValueProperty))); 
			}
			set 
			{
				base.SetValue(LabeledTextBox.ValueProperty, value);
				OnPropertyChanged("Value");
			}
		}

		void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
