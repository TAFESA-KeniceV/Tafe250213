using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgagePage : Page
	{
		public MortgagePage()
		{
			this.InitializeComponent();
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));

		}

		private async void calcButton_Click(object sender, RoutedEventArgs e)
		{
			/*
			double principal = 0.0;
			double interest = 0.0;
			double noMonths = 0.0;

		//principal parse
			try
			{
				double.Parse(principalTextBox.Text);
			}
			catch
			{
				var dialogMessage = new MessageDialog("Please enter decimal number into principal text box.");
				await dialogMessage.ShowAsync();
				return;
			}

			//interest parse
			try
			{
				double.Parse(monInTextBox.Text);
			}
			catch
			{
				var dialogMessage = new MessageDialog("Please enter decimal number into interest text box.");
				await dialogMessage.ShowAsync();
				return;
			}

			//months parse
			try
			{
				double.Parse(monthsTextBox.Text);
			}
			catch
			{
				var dialogMessage = new MessageDialog("Please enter number into months text box.");
				await dialogMessage.ShowAsync();
				return;
			}
			*/
			double yearlyIntrestRate = double.Parse(anInTextBox.Text);
			double principleBorrow = double.Parse(principalTextBox.Text);
			int Years = int.Parse(yearsTextBox.Text);
			int andMonths = int.Parse(monthsTextBox.Text);

			double monthlyIntrestRate = yearlyIntrestRate / 12.0;
			monthlyIntrestRate = monthlyIntrestRate * 0.01;

			int numberOfPayments = Years * 12 + andMonths;

			double numerator = principleBorrow * Math.Pow(1 + monthlyIntrestRate, numberOfPayments) * monthlyIntrestRate;
			double denominator = Math.Pow(1 + monthlyIntrestRate, numberOfPayments) - 1;
			double monthlyRepayment = numerator / denominator;

			monInTextBox.Text = monthlyIntrestRate.ToString();
			monthlyRepayTextBox.Text = numberOfPayments.ToString();
		}
	}
}
