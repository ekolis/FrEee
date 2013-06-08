using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.WinForms.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class SearchBox : UserControl
	{
		public SearchBox()
		{
			InitializeComponent();
			resultsForm = new SearchBoxResultsForm();
			resultsForm.ObjectSelected += resultsForm_ObjectSelected;
			ResultsPopupHeight = 128;

			textBox.GotFocus += textBox1_GotFocus;
			textBox.MouseUp += textBox1_MouseUp;
			textBox.Leave += textBox1_Leave;
		}

		private void SearchBox_Load(object sender, EventArgs e)
		{
			var form = this.FindForm();
			form.LocationChanged += form_LocationChanged;
			form.Resize += form_Resize;
		}

		void form_Resize(object sender, EventArgs e)
		{
			var form = (Form)sender;
			if (form.WindowState == FormWindowState.Minimized)
				resultsForm.Hide();
		}

		void form_LocationChanged(object sender, EventArgs e)
		{
			PlaceResultsForm();
		}

		private void PlaceResultsForm()
		{
			var screenPos = PointToScreen(Location);
			resultsForm.Left = screenPos.X;
			resultsForm.Top = screenPos.Y + Height;
			resultsForm.Width = Width;
			resultsForm.Height = ResultsPopupHeight;
		}

		void resultsForm_ObjectSelected(SearchBoxResultsForm sender, ISpaceObject sobj)
		{
			SelectedObject = sobj;
		}

		public delegate void ObjectSelectedDelegate(SearchBox sender, ISpaceObject sobj);

		public event ObjectSelectedDelegate ObjectSelected;

		private SearchBoxResultsForm resultsForm;

		private IEnumerable<ISpaceObject> objectsToSearch;
		public IEnumerable<ISpaceObject> ObjectsToSearch
		{
			get
			{
				return objectsToSearch;
			}
			set
			{
				objectsToSearch = value;
			}
		}

		/// <summary>
		/// The current star system. Items in this system will be shown first.
		/// </summary>
		public StarSystem StarSystem
		{
			get;
			set;
		}

		public ISpaceObject SelectedObject
		{
			get;
			private set;
		}

		public int ResultsPopupHeight
		{
			get;
			set;
		}

		public void ShowResults()
		{
			var results = ObjectsToSearch.Where(o => o.Name.ToLower().Contains(textBox.Text.ToLower())).OrderBy(o => o.FindStarSystem() == StarSystem ? 0 : 1);
			resultsForm.Results = results;
			if (!resultsForm.Visible)
				resultsForm.Show(this);
			PlaceResultsForm();
		}

		public void HideResults()
		{
			resultsForm.Hide();
		}

		private void textBox_SizeChanged(object sender, EventArgs e)
		{
			Height = textBox.Height;
		}

		private void SearchBox_SizeChanged(object sender, EventArgs e)
		{
			textBox.Width = Width;
			resultsForm.Width = Width;
		}

		private void SearchBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				// select first item
				SelectedObject = resultsForm.Results.First();
			}
			else if (e.KeyCode == Keys.Escape)
			{
				// hide results
				HideResults();
			}
			else
			{
				// update results
				ShowResults();
			}
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			// update results
			ShowResults();
		}

		private void SearchBox_Leave(object sender, EventArgs e)
		{
			HideResults();
		}

		private void textBox_Leave(object sender, EventArgs e)
		{
			HideResults();
		}

		private void textBox_Enter(object sender, EventArgs e)
		{
			textBox.SelectAll();
		}

		private void SearchBox_Enter(object sender, EventArgs e)
		{
			textBox.SelectAll();
		}

		// http://stackoverflow.com/questions/97459/automatically-select-all-text-on-focus-in-winforms-textbox
		private bool alreadyFocused;

		void textBox1_Leave(object sender, EventArgs e)
		{
			alreadyFocused = false;
		}


		void textBox1_GotFocus(object sender, EventArgs e)
		{
			// Select all text only if the mouse isn't down.
			// This makes tabbing to the textbox give focus.
			if (MouseButtons == MouseButtons.None)
			{
				this.textBox.SelectAll();
				alreadyFocused = true;
			}
		}

		void textBox1_MouseUp(object sender, MouseEventArgs e)
		{
			// Web browsers like Google Chrome select the text on mouse up.
			// They only do it if the textbox isn't already focused,
			// and if the user hasn't selected all text.
			if (!alreadyFocused && this.textBox.SelectionLength == 0)
			{
				alreadyFocused = true;
				this.textBox.SelectAll();
			}
		}
	}
}
