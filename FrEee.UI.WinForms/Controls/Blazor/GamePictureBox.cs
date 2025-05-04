using FrEee.UI.Blazor.Views;
using FrEee.UI.WinForms.Forms;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlazorImageDisplay = FrEee.UI.Blazor.Views.ImageDisplay;

namespace FrEee.UI.WinForms.Controls.Blazor;

public partial class GamePictureBox : BlazorControl, ISupportInitialize
{
	public GamePictureBox()
	{
		InitializeComponent();

		// set up view model
		VM.OnClick = () =>
		{
			// HACK: https://github.com/MicrosoftEdge/WebView2Feedback/issues/3028#issuecomment-1461207168
			Task.Delay(0).ContinueWith(_ => MainGameForm.Instance.Invoke(() =>
			{
				OnClick(new EventArgs());
			}));
		};
	}

	protected override Type BlazorComponentType { get; } = typeof(BlazorImageDisplay);

	protected override ImageDisplayViewModel VM { get; } = new();

	#region viewmodel property wrappers for winforms
	public Image Image
	{
		get => VM.Image;
		set => VM.Image = value;
	}
	#endregion

	/// <summary>
	/// Shows a full-size version of the picture in its own window.
	/// </summary>
	/// <param name="text">The title for the form.</param>
	public void ShowFullSize(string text)
	{
		if (Image != null)
		{
			var pic = new PictureBox();
			pic.Image = Image;
			pic.Size = Image.Size;
			pic.BackColor = Color.Black;
			pic.SizeMode = PictureBoxSizeMode.Zoom;
			this.FindForm().ShowChildForm(pic.CreatePopupForm(text));
		}
	}

	// TODO: remove this stupid ISupportInitialize interface and anything that references GamePictureBox as it
	public void BeginInit() { }
	public void EndInit() { }

	[Obsolete]
	public PictureBoxSizeMode SizeMode { get; set; }

	public override Color BackColor
	{
		get => base.BackColor;
		set
		{
			base.BackColor = value;
			VM.BackgroundColor = value;
		}
	}
}
