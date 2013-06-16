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
	public partial class TraitPicker : UserControl
	{
		public TraitPicker()
		{
			InitializeComponent();
		}

		private IEnumerable<Trait> traits;
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
					pnlTraits.Controls.Add(chk);

					var lbl = new Label();
					lbl.Text = trait.Description;
					lbl.Padding = new Padding(40, 0, 0, 0);
					pnlTraits.Controls.Add(lbl);
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

		public IEnumerable<Trait> CheckedTraits
		{
			get { return Traits.Where(t => IsTraitChecked(t)); }
		}
	}
}
