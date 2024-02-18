using System.Drawing;
using System.Windows.Forms;

namespace FrEee.UI.WinForms.Controls;

public partial class GameButton : Button
{
	public GameButton()
	{
		InitializeComponent();
		UseVisualStyleBackColor = false;
		BackColor = Color.Black;
		ForeColor = Color.CornflowerBlue;
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		var g = pe.Graphics;

		// draw background
		var rect = new Rectangle(0, 0, Width, Height);
		g.FillRectangle(new SolidBrush(BackColor), rect);

		// draw border
		int penSize = 1;
		var rect1 = rect;
		rect1.X += penSize;
		rect1.Y += penSize;
		rect1.Width -= penSize * 2;
		rect1.Height -= penSize * 2;
		Pen pen1;
		if (Enabled)
			pen1 = new Pen(Color.Navy, penSize);
		else
			pen1 = new Pen(Color.DarkGray, penSize);
		g.DrawRectangle(pen1, rect1);
		var rect2 = rect1;
		rect2.X += penSize;
		rect2.Y += penSize;
		rect2.Width -= penSize * 2;
		rect2.Height -= penSize * 2;
		Pen pen2;
		if (Enabled)
			pen2 = new Pen(Color.CornflowerBlue, penSize);
		else
			pen2 = new Pen(Color.Silver, penSize);
		g.DrawRectangle(pen2, rect2);

		// draw image
		if (Image != null)
		{
			// TODO - image alignment and zoom options
			g.DrawImage(Image, rect2);
		}

		// draw text
		var sf = new StringFormat();
		switch (TextAlign)
		{
			case ContentAlignment.TopLeft:
				sf.Alignment = StringAlignment.Near;
				sf.LineAlignment = StringAlignment.Near;
				break;

			case ContentAlignment.MiddleLeft:
				sf.Alignment = StringAlignment.Near;
				sf.LineAlignment = StringAlignment.Center;
				break;

			case ContentAlignment.BottomLeft:
				sf.Alignment = StringAlignment.Near;
				sf.LineAlignment = StringAlignment.Far;
				break;

			case ContentAlignment.TopCenter:
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Near;
				break;

			case ContentAlignment.MiddleCenter:
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				break;

			case ContentAlignment.BottomCenter:
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Far;
				break;

			case ContentAlignment.TopRight:
				sf.Alignment = StringAlignment.Far;
				sf.LineAlignment = StringAlignment.Near;
				break;

			case ContentAlignment.MiddleRight:
				sf.Alignment = StringAlignment.Far;
				sf.LineAlignment = StringAlignment.Center;
				break;

			case ContentAlignment.BottomRight:
				sf.Alignment = StringAlignment.Far;
				sf.LineAlignment = StringAlignment.Far;
				break;
		}
		g.DrawString(Text, Font, new SolidBrush(ForeColor), rect2, sf);
	}
}