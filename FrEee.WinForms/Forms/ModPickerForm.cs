using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;
using System.IO;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Utility;
using System.Reflection;

namespace FrEee.WinForms.Forms
{
	public partial class ModPickerForm : Form
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

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private IList<ModInfo> modinfos = new List<ModInfo>();

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

		public string ModPath { get; private set; }

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
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
	}
}
