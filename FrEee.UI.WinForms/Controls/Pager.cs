using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.UI.WinForms.Controls;

public partial class Pager : UserControl
{
	public Pager()
	{
		InitializeComponent();
		SizeChanged += GamePager_SizeChanged;
	}

	public IList<Control> Content
	{
		get
		{
			return content;
		}
		set
		{
			content = value;
			if (Content != null && Content.Count > 0)
				CurrentPage = 0;
			else
				CurrentPage = -1;
		}
	}

	public int CurrentPage
	{
		get
		{
			return currentPage;
		}
		set
		{
			currentPage = value;
			RefreshContent();
		}
	}

	public bool ShowPager
	{
		get { return tableLayoutPanel1.RowStyles[0].Height != 0; }
		set { tableLayoutPanel1.RowStyles[0].Height = value ? 20 : 0; }
	}

	private IList<Control> content;

	private int currentPage;

	public void RefreshContent()
	{
		pnlContent.Controls.Clear();
		if (CurrentPage >= 0 && Content != null && CurrentPage < Content.Count)
		{
			lblPager.Text = "Page " + (CurrentPage + 1) + " of " + Content.Count;
			pnlContent.Controls.Add(Content[CurrentPage]);
			//Content[CurrentPage].Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			//Content[CurrentPage].Width = pnlContent.Width;
			Content[CurrentPage].Dock = DockStyle.Fill;
		}
		else
		{
			lblPager.Text = "";
		}
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		if (Content.Count > 0 && CurrentPage < Content.Count - 1)
			CurrentPage++;
	}

	private void btnPrev_Click(object sender, EventArgs e)
	{
		if (CurrentPage > 0)
			CurrentPage--;
	}

	private void GamePager_SizeChanged(object sender, EventArgs e)
	{
		btnPrev.Font = new Font(btnPrev.Font.FontFamily, btnPrev.Height / 3, btnPrev.Font.Style);
		btnNext.Font = new Font(btnNext.Font.FontFamily, btnNext.Height / 3, btnNext.Font.Style);
	}
}