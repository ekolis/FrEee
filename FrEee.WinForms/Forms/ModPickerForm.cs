using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Utility;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class ModPickerForm : GameForm
	{
		public ModPickerForm()
		{
			InitializeComponent();

			// load modinfos
			var stock = new Mod();
			var loader = new ModInfoLoader(null);
			loader.Load(stock);
			modinfos.Add(stock.Info);
			if (Directory.Exists("Mods"))
			{
				foreach (var folder in Directory.GetDirectories("Mods"))
				{
					loader.ModPath = Path.GetFileName(folder);
					var mod = new Mod();
					loader.Load(mod);
					modinfos.Add(mod.Info);
				}
			}

			// populate list
			lstMods.Initialize(32, 32);
			foreach (var info in modinfos)
			{
				Image img;
				if (info.Folder == null)
					img = Pictures.GetCachedImage("MODICON");
				else
					img = Pictures.GetCachedImage(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Mods", info.Folder, "MODICON"));
				lstMods.AddItemWithImage(null, info.Name, info, img);
			}

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }
		}

		public string ModPath { get; private set; }
		private IList<ModInfo> modinfos = new List<ModInfo>();

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			LoadMod(ModPath);
			var original = The.Mod.Copy();
			Func<bool> save = () =>
			{
				// mod is already in memory
				return true;
			};
			Func<bool> cancel = () =>
			{
				if (MessageBox.Show("Discard work on this mod?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					// undo
					The.Mod = original;
					return true;
				}
				else
					return false; // don't close the form
			};
			var form = new EditorForm(The.Mod, save, cancel);
			Hide();
			this.ShowChildForm(form);
			Show();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		// TODO - put this in a utility class somewhere
		private void LoadMod(string modPath)
		{
			var status = new Status
			{
				Progress = 0d,
				Message = "Initializing",
				Exception = null,
			};
			Thread t = new Thread(new ThreadStart(() =>
			{
				try
				{
					status.Message = "Loading mod";
					Mod.Load(modPath, true, status, 1d);
					this.Invoke(new Action(delegate ()
					{
						if (Mod.Errors.Any())
							this.ShowChildForm(new ModErrorsForm());
					}));
				}
				catch (Exception ex)
				{
					status.Exception = ex;
				}
			}));
			t.Name = "Mod Loading";

			this.ShowChildForm(new StatusForm(t, status));

			Text = "FrEee - " + The.Mod.Info.Name;
		}

		private void lstMods_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				var info = (ModInfo)e.Item.Tag;
				txtName.Text = info.Name;
				txtVersion.Text = info.Version;
				txtFolder.Text = info.Folder ?? "(N/A)";
				txtAuthor.Text = info.Author;
				txtEmail.Text = info.Email;
				txtWebsite.Text = info.Website;
				txtDescription.Text = info.Description;
				ModPath = info.Folder;
			}
		}

		private void lstMods_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstMods.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				var info = (ModInfo)item.Tag;
				ModPath = info.Folder;
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void txtEmail_Click(object sender, EventArgs e)
		{
			Process.Start("mailto:" + txtEmail.Text);
		}

		private void txtWebsite_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(txtWebsite.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Could not launch " + txtWebsite.Text + ".");
			}
		}
	}
}