using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrEee.Wpf.Views
{
	public class View : ContentControl
	{
		public View()
		{
			Background = Brushes.Black;
		}

		/// <summary>
		/// Shows a view, either reusing an old shell or creating a new one.
		/// </summary>
		/// <param name="shell">The shell to reuse, or null to create a new one.</param>
		/// <returns>The shell created.</returns>
		public Shell Show(Shell shell = null)
		{
			shell = shell ?? new Shell(this);
			shell.Show();
			return shell;
		}

		/// <summary>
		/// Shows a view in a modal shell.
		/// </summary>
		/// <returns>The dialog result returned, if any.</returns>
		public bool? ShowDialog()
		{
			return new Shell(this).ShowDialog();
		}

		/// <summary>
		/// The shell (if any) which directly contains this view.
		/// </summary>
		public Shell Shell
		{
			get
			{
				return Parent as Shell;
			}
		}



		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(string), typeof(View), new PropertyMetadata("FrEee"));


	}
}