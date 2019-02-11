using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class TraitPicker : UserControl
	{
		public TraitPicker()
		{
			InitializeComponent();
		}

		public IEnumerable<Trait> CheckedTraits
		{
			get { return Traits.Where(t => IsTraitChecked(t)); }
		}

		public IEnumerable<Trait> Traits
		{
			get
			{
				return traits;
			}
			set
			{
				traits = value;
				Bind();
			}
		}

		private IEnumerable<Trait> traits;

		public void Bind()
		{
			pnlTraits.Controls.Clear();
			if (Traits != null)
			{
				foreach (var trait in Traits)
				{
					var chk = new CheckBox();
					chk.Text = "(" + trait.Cost + " pts) " + trait.Name;
					chk.Tag = trait;
					chk.CheckedChanged += chk_CheckedChanged;
					toolTip.SetToolTip(chk, trait.Description);
					pnlTraits.Controls.Add(chk);
				}
			}
		}

		public bool IsTraitChecked(Trait trait)
		{
			if (!Traits.Contains(trait))
				return false;
			var chk = pnlTraits.Controls.OfType<CheckBox>().Single(c => c.Tag == trait);
			return chk.Checked;
		}

		public void SetTraitChecked(Trait trait, bool state)
		{
			if (!Traits.Contains(trait))
				throw new Exception(trait + " is not in this trait picker.");
			var chk = pnlTraits.Controls.OfType<CheckBox>().Single(c => c.Tag == trait);
			chk.Checked = state;
		}

		private void chk_CheckedChanged(object sender, EventArgs e)
		{
			var chk = (CheckBox)sender;
			if (TraitToggled != null)
				TraitToggled(this, (Trait)chk.Tag, chk.Checked);
		}

		private void pnlTraits_SizeChanged(object sender, EventArgs e)
		{
			foreach (Control c in pnlTraits.Controls)
				c.Width = pnlTraits.Width - 32;
		}

		public event TraitToggledDelegate TraitToggled;

		public delegate void TraitToggledDelegate(TraitPicker picker, Trait trait, bool state);
	}
}