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

		public void Hide()
		{
			if (Shell != null)
				Shell.Hide();
		}

		/// <summary>
		/// Hides this view, shows another view modally, when that one is done shows this one again.
		/// </summary>
		/// <param name="view"></param>
		protected void SwitchTo(View view)
		{
			Hide();
			view.ShowDialog();
			Show();
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

		public object ViewModel
		{
			get { return (object)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(object), typeof(View), new PropertyMetadata(null));

		public void Close()
		{
			Shell.Close();
		}

		/// <summary>
		/// Allow closing of the shll containing this view?
		/// </summary>
		public bool AllowClose
		{
			get; set;
		} = true;

		public void ForceClose()
		{
			var c = AllowClose;
			AllowClose = true;
			Close();
			AllowClose = c;
		}

		/// <summary>
		/// Does this view cause the app to shut down when it is closed?
		/// </summary>
		public bool CausesShutdown
		{
			get; set;
		} = false;
	}
}