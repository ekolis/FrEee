using FrEee.Objects.Civilization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class AptitudePicker : UserControl
	{
		public AptitudePicker()
		{
			InitializeComponent();
			Values = Aptitude.All.ToDictionary(a => a, a => 100);
			Bind();
		}

		/// <summary>
		/// The empire point cost of choosing all the selected aptitude values.
		/// </summary>
		public int Cost
		{
			get
			{
				if (Values == null)
					return 0;
				return Values.Sum(kvp => kvp.Key.GetCost(kvp.Value));
			}
		}

		public IDictionary<Aptitude, int> Values { get; set; }

		public void Bind()
		{
			pnl.Controls.Clear();
			pnl.ColumnCount = 3;
			pnl.ColumnStyles.Clear();
			pnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
			pnl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			pnl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			Normalize();
			int row = 0;
			if (Values != null)
			{
				pnl.RowCount = Values.Count;
				pnl.RowStyles.Clear();
				foreach (var kvp in Values)
				{
					pnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));

					var txtName = new Label();
					txtName.Text = kvp.Key.Name;
					txtName.ForeColor = Color.CornflowerBlue;
					txtName.TextAlign = ContentAlignment.MiddleLeft;
					txtName.AutoSize = false;
					txtName.Width = 200;
					toolTip.SetToolTip(txtName, kvp.Key.Description);
					//txtName.Dock = DockStyle.Fill;
					pnl.Controls.Add(txtName, 0, row);

					var spnValue = new NumericUpDown();
					spnValue.Minimum = kvp.Key.MinPercent;
					spnValue.Maximum = kvp.Key.MaxPercent;
					spnValue.Value = kvp.Value;
					spnValue.Tag = kvp.Key;
					spnValue.ValueChanged += spnValue_ValueChanged;
					toolTip.SetToolTip(spnValue, kvp.Key.Description);
					pnl.Controls.Add(spnValue, 1, row);

					var txtCost = new Label();
					txtCost.Name = "txtCost_" + kvp.Key.Name.Replace(' ', '_');
					txtCost.Text = kvp.Key.GetCost(kvp.Value) + " pts";
					txtCost.TextAlign = ContentAlignment.MiddleLeft;
					toolTip.SetToolTip(txtCost, kvp.Key.Description);
					pnl.Controls.Add(txtCost, 2, row);

					row++;
				}
			}
		}

		public int GetValue(Aptitude apt)
		{
			if (Values == null)
				return 100;
			return Values[apt];
		}

		/// <summary>
		/// Clamps values to within the allowed range.
		/// </summary>
		public void Normalize()
		{
			if (Values != null)
			{
				foreach (var kvp in Values.ToArray())
				{
					if (kvp.Value > kvp.Key.MaxPercent)
						Values[kvp.Key] = kvp.Key.MaxPercent;
					if (kvp.Value < kvp.Key.MinPercent)
						Values[kvp.Key] = kvp.Key.MinPercent;
				}
			}
		}

		public void SetValue(Aptitude apt, int value)
		{
			if (Values == null)
				Values = Aptitude.All.ToDictionary(a => a, a => 100);
			var spn = pnl.Controls.OfType<NumericUpDown>().Single(c => c.Tag == apt);
			try
			{
				spn.Value = value;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new Exception("Unable to set {0} to {1}. It must be between {2} and {3}.".F(apt, value, spn.Minimum, spn.Maximum), ex);
			}
		}

		private void spnValue_ValueChanged(object sender, EventArgs e)
		{
			var spn = (NumericUpDown)sender;
			var apt = (Aptitude)spn.Tag;
			var txtCost = pnl.Controls.OfType<Label>().Single(l => l.Name == "txtCost_" + apt.Name.Replace(' ', '_'));
			if (Values == null)
				Values = Aptitude.All.ToDictionary(a => a, a => 100);
			Values[apt] = (int)spn.Value;
			txtCost.Text = apt.GetCost(Values[apt]) + " pts";
			if (AptitudeValueChanged != null)
				AptitudeValueChanged(this, apt, Values[apt]);
		}

		public event AptitudeValueChangedDelegate AptitudeValueChanged;

		public delegate void AptitudeValueChangedDelegate(AptitudePicker ap, Aptitude aptitude, int newValue);
	}
}