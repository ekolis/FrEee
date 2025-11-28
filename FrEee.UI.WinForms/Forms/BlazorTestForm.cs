using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FrEee.UI.Blazor.Views;
using FrEee.UI.WinForms.Controls;
using FrEee.UI.WinForms.Controls.Blazor;

namespace FrEee.UI.WinForms.Forms
{
	public partial class BlazorTestForm : Form
    {
        public BlazorTestForm()
        {
            InitializeComponent();
        }

        private T ShowControl<T>() where T : Control, new()
        {
            panel.Controls.Clear();
            var c = new T();
            c.Dock = DockStyle.Fill;
            panel.Controls.Add(c);
            return c;
        }

        private void btnProgressBar_Click(object sender, EventArgs e)
        {
            ShowControl<GameProgressBar>();
        }

        private void btnResourceDisplay_Click(object sender, EventArgs e)
        {
            ShowControl<Controls.ResourceDisplay>();
        }

        private void btnPieChart_Click(object sender, EventArgs e)
        {
            var chart = ShowControl<PieChart>();
            chart.Entries = new HashSet<PieChartViewModel<int>.Entry>
            {
                new("Fred", Color.Red, 15),
                new("Ginger", Color.Blue, 14),
                new("Penny", Color.Green, 12),
                new("Pepper", Color.Yellow, 10),
                new("Salt", Color.Pink, 7),
            };
        }
    }
}
