using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
	public sealed partial class CurrencyPage : Page
	{
		// Conversion rates
		const double USD_EUR = 0.85189982;
		const double USD_GBP = 0.72872436;
		const double USD_INR = 74.257327;

		const double EUR_USD = 1.1739732;
		const double EUR_GBP = 0.8556672;
		const double EUR_INR = 87.00755;

		const double GBP_USD = 1.371907;
		const double GBP_EUR = 1.1686692;
		const double GBP_INR = 101.68635;

		const double INR_USD = 0.011492628;
		const double INR_EUR = 0.013492774;
		const double INR_GBP = 0.0098339397;

		// Unicode for currency symbols
		// "€" - \u20AC
		// "£" - \u20AC
		// "₹" - \u20B9

		public CurrencyPage()
		{
			this.InitializeComponent();
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));

		}

		private async void calcCurButton_Click(object sender, RoutedEventArgs e)
		{
			double amount;

			// Exception handling for amount
			try
			{
				amount = double.Parse(amtTextBox.Text);
			}
			catch (Exception theException)
			{
				var dialogMessage = new MessageDialog("Please enter a valid amount to convert");
				await dialogMessage.ShowAsync();
				amtTextBox.Focus(FocusState.Programmatic);
				amtTextBox.SelectAll();
				return;
			}

			// Validate amount to be converted is greater than 0
			if (amount < 0)
			{
				var dialogMessage = new MessageDialog("Please enter a positive amount to convert");
				await dialogMessage.ShowAsync();
				amtTextBox.Focus(FocusState.Programmatic);
				amtTextBox.SelectAll();
				return;
			}
						
			// Method call to convert
			convert(amount, fromComboBox.SelectedIndex, toComboBox.SelectedIndex);

		}
		/// <summary>
		/// Method to convert "amount" from currency at "from" index to the currency at "to" index
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		private void convert(double amount, int from, int to)
		{
			double converted;

			// Selection based on 'from' currency
			switch (from)
			{
				case 0:
					{
						// Converting USD to one of the currencies based on 'to'
						switch (to)
						{
							case 0:
								converted = amount;
								entryTextBlock.Text = "$" + amount.ToString("N") + " US Dollar(s) = ";
								resTextBlock.Text = "$ " + converted.ToString("N") + " US Dollar(s)";
								convTextBlock.Text = "1 USD = 1.0 USD";
								break;
							case 1:
								converted = amount * USD_EUR;
								entryTextBlock.Text = "$" + amount.ToString("N") + " US Dollar(s) = ";
								resTextBlock.Text = "€" + converted.ToString("N") + " Euro(s)";
								convTextBlock.Text = "1 USD = " + USD_EUR.ToString() + " Euros\n1 Euro = " + EUR_USD.ToString() + " USD";
								break;
							case 2:
								converted = amount * USD_GBP;
								entryTextBlock.Text = "$" + amount.ToString("N") + " US Dollar(s) = ";
								resTextBlock.Text = "£" + converted.ToString("N") + " Pound(s)";
								convTextBlock.Text = "1 USD = " + USD_GBP.ToString() + " Pounds\n1 Pound = " + GBP_USD.ToString() + " USD";
								break;
							case 3:
								converted = amount * USD_INR;
								entryTextBlock.Text = "$" + amount.ToString("N") + " US Dollar(s) = ";
								resTextBlock.Text = "₹" + converted.ToString("N") + " Rupee(s)";
								convTextBlock.Text = "1 USD = " + USD_INR.ToString() + " Rupees\n1 Rupee = " + INR_USD.ToString() + " USD";
								break;
							default:
								resTextBlock.Text = "";
								break;
						}
					}
					break;

				case 1:
					{
						// Converting EUR to one of the currencies based on 'to'
						switch (to)
						{
							case 0:
								converted = amount * EUR_USD;
								entryTextBlock.Text = "€" + amount.ToString("N") + " Euro(s) = ";
								resTextBlock.Text = "$" + converted.ToString("N") + " US Dollar(s)";
								convTextBlock.Text = "1 Euro = " + EUR_USD.ToString() + " USD\n1 USD = " + USD_EUR.ToString() + " Euros";
								break;
							case 1:
								converted = amount;
								entryTextBlock.Text = "€" + amount.ToString("N") + " Euro(s) = ";
								resTextBlock.Text = "€" + converted.ToString("N") + " Euro(s)";
								convTextBlock.Text = "1 Euro = 1.0 Euro";
								break;
							case 2:
								converted = amount * EUR_GBP;
								entryTextBlock.Text = "€" + amount.ToString("N") + " Euro(s) = ";
								resTextBlock.Text = "£" + converted.ToString("N") + " Pound(s)";
								convTextBlock.Text = "1 Euro = " + EUR_GBP.ToString() + " Pounds\n1 Pound = " + GBP_EUR.ToString() + " Euros";
								break;
							case 3:
								converted = amount * EUR_INR;
								entryTextBlock.Text = "€" + amount.ToString("N") + " Euro(s) = ";
								resTextBlock.Text = "₹" + converted.ToString("N") + " Rupee(s)";
								convTextBlock.Text = "1 Euro = " + EUR_INR.ToString() + " Rupees\n1 Rupee = " + INR_EUR.ToString() + " Euros";
								break;
							default:
								resTextBlock.Text = "";
								break;
						}
					}
					break;

				case 2:
					{
						// Converting GBP to one of the currencies based on 'to'
						switch (to)
						{
							case 0:
								converted = amount * GBP_USD;
								entryTextBlock.Text = "£" + amount.ToString("N") + " Pound(s) = ";
								resTextBlock.Text = "$" + converted.ToString("N") + " US Dollar(s)";
								convTextBlock.Text = "1 Pound = " + GBP_USD.ToString() + " USD\n1 USD = " + USD_GBP.ToString() + " Pounds";
								break;
							case 1:
								converted = amount * GBP_EUR;
								entryTextBlock.Text = "£" + amount.ToString("N") + " Pound(s) = ";
								resTextBlock.Text = "€" + converted.ToString("N") + " Euro(s)";
								convTextBlock.Text = "1 Pound = " + GBP_EUR.ToString() + " Euros\n1 Euro = " + EUR_GBP.ToString() + " Pounds";
								break;
							case 2:
								converted = amount;
								convTextBlock.Text = "£" + amount.ToString("N") + " Pound(s) = ";
								resTextBlock.Text = "£" + converted.ToString("N") + " Pound(s)";
								convTextBlock.Text = "1 Pound = 1.0 Pound";
								break;
							case 3:
								converted = amount * GBP_INR;
								entryTextBlock.Text = "£" + amount.ToString("N") + " Pound(s) = ";
								resTextBlock.Text = "₹" + converted.ToString("N") + " Rupee(s)";
								convTextBlock.Text = "1 Pound = " + GBP_INR.ToString() + " Rupees\n1 Rupee = " + INR_GBP.ToString() + " Pounds";
								break;
							default:
								resTextBlock.Text = "";
								break;
						}
					}
					break;

				case 3:
					{
						// Converting INR to one of the currencies based on 'to'
						switch (to)
						{
							case 0:
								converted = amount * INR_USD;
								entryTextBlock.Text = "₹" + amount.ToString("N") + " Rupee(s) = ";
								resTextBlock.Text = "$" + converted.ToString("N") + " US Dollar(s)";
								convTextBlock.Text = "1 Rupee = " + INR_USD.ToString() + " USD\n1 USD = " + USD_INR.ToString() + " Rupees";
								break;
							case 1:
								converted = amount * INR_EUR;
								entryTextBlock.Text = "₹" + amount.ToString("N") + " Rupee(s) = ";
								resTextBlock.Text = "€" + converted.ToString("N") + " Euro(s)";
								convTextBlock.Text = "1 Rupee = " + INR_EUR.ToString() + " Euros\n1 Euro = " + EUR_INR.ToString() + " Rupees";
								break;
							case 2:
								converted = amount * INR_GBP;
								entryTextBlock.Text = "₹" + amount.ToString("N") + " Rupee(s) = ";
								resTextBlock.Text = "£" + converted.ToString("N") + " Pound(s)";
								convTextBlock.Text = "1 Rupee = " + INR_GBP.ToString() + " Pounds\n1 Pound = " + GBP_INR.ToString() + " Rupees";
								break;
							case 3:
								converted = amount;
								entryTextBlock.Text = "₹" + amount.ToString("N") + " Rupee(s) = ";
								resTextBlock.Text = "₹" + converted.ToString("N") + " Rupee(s)";
								convTextBlock.Text = "1 Rupee = 1.0 Rupee";
								break;
							default:
								resTextBlock.Text = "";
								break;
						}
					}
					break;

				default:
					resTextBlock.Text = "";
					break;
			}
		}
	}
}
