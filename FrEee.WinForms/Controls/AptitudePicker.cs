using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game.Objects.Civilization;

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
					//txtName.Dock = DockStyle.Fill;
					pnl.Controls.Add(txtName, 0, row);

					var spnValue = new NumericUpDown();
					spnValue.Minimum = kvp.Key.MinPercent;
					spnValue.Maximum = kvp.Key.MaxPercent;
					spnValue.Value = kvp.Value;
					spnValue.Tag = kvp.Key;
					spnValue.ValueChanged += spnValue_ValueChanged;
					pnl.Controls.Add(spnValue, 1, row);

					var txtCost = new Label();
					txtCost.Name = "txtCost_" + kvp.Key.Name.Replace(' ', '_');
					txtCost.Text = GetCost(kvp.Key, kvp.Value) + " pts";
					txtCost.TextAlign = ContentAlignment.MiddleLeft;
					pnl.Controls.Add(txtCost, 2, row);

					row++;
				}
			}
		}

		void spnValue_ValueChanged(object sender, EventArgs e)
		{
			var spn = (NumericUpDown)sender;
			var apt = (Aptitude)spn.Tag;
			var txtCost = pnl.Controls.OfType<Label>().Single(l => l.Name == "txtCost_" + apt.Name.Replace(' ', '_'));
			Values[apt] = (int)spn.Value;
			txtCost.Text = GetCost(apt, Values[apt]) + " pts";
			if (AptitudeValueChanged != null)
				AptitudeValueChanged(this, apt, Values[apt]);
		}

		public delegate void AptitudeValueChangedDelegate(AptitudePicker ap, Aptitude aptitude, int newValue);

		public event AptitudeValueChangedDelegate AptitudeValueChanged;

		/// <summary>
		/// Clamps values to within the allowed range.
		/// </summary>
		public void Normalize()
		{
			foreach (var kvp in Values.ToArray())
			{
				if (kvp.Value > kvp.Key.MaxPercent)
					Values[kvp.Key] = kvp.Key.MaxPercent;
				if (kvp.Value < kvp.Key.MinPercent)
					Values[kvp.Key] = kvp.Key.MinPercent;
			}
		}

		/// <summary>
		/// The empire point cost of choosing all the selected aptitude values.
		/// </summary>
		public int Cost
		{
			get
			{
				return Values.Sum(kvp => GetCost(kvp.Key, kvp.Value));
			}
		}

		private int GetCost(Aptitude apt, int val)
		{
			if (val > 100)
			{
				var high = 100 + apt.Threshold;
				if (val > high)
					return (val - high) * apt.HighCost + (high - 100) * apt.Cost;
				else
					return (val - 100) * apt.Cost;
			}
			else if (val < 100)
			{
				var low = 100 - apt.Threshold;
				if (val < low)
					return (val - low) * apt.LowCost + (low - 100) * apt.Cost;
				else
					return (val - 100) * apt.Cost;
			}
			else
				return 0;
		}

		public int GetValue(Aptitude apt)
		{
			return Values[apt];
		}

		public void SetValue(Aptitude apt, int value)
		{
			var spn = pnl.Controls.OfType<NumericUpDown>().Single(c => c.Tag == apt);
			spn.Value = value;
		}
	}
}
