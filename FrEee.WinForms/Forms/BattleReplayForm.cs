using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility;
using System.IO;
using FrEee.WinForms.Interfaces;
using FrEee.Game.Objects.Combat.Grid;

namespace FrEee.WinForms.Forms
{
	public partial class BattleReplayForm : Form, IBindable<Battle>
	{
		public BattleReplayForm(bool victory)
		{
			InitializeComponent();
		}

		public Battle Battle { get; private set; }

		public void Bind(Battle data)
		{
			Battle = data;
			Bind();
		}

		public void Bind()
		{
			
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
