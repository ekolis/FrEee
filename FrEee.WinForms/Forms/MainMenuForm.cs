using FrEee.Game;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Forms
{
    public partial class MainMenuForm : Form
    {
        private static MainMenuForm _instance;
        public static MainMenuForm GetInstance()
        {
            return _instance ?? (_instance = new MainMenuForm());
        }

        private MainMenuForm()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(Properties.Resources.FrEeeSplash);
            Icon = new Icon(Properties.Resources.FrEeeIcon);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                tblButtonPanel.Enabled = !IsBusy;
                progressBar1.Visible = IsBusy;
            }
        }

        #region Button click handlers

        private void btnQuickStart_Click(object sender, EventArgs e)
        {
            IsBusy = true;

            // TODO: THIS SHOULD BE MOVED
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNewWithExceptionHandling(() =>
            {
                // load the stock mod
                Mod.Load(null);

                // create the game
                var galtemp = Mod.Current.GalaxyTemplates.PickRandom();
                var gsu = new GameSetup
                {
                    GalaxyTemplate = galtemp,
                    StarSystemCount = 50,
                    GalaxySize = new System.Drawing.Size(40, 30)
                };
                gsu.Empires.Add(new Empire { Name = "Jraenar Empire", Color = Color.Red, EmperorTitle = "Master General", EmperorName = "Jar-Nolath" });
                gsu.Empires.Add(new Empire { Name = "Eee Consortium", Color = Color.Cyan });
                gsu.Empires.Add(new Empire { Name = "Drushocka Empire", Color = Color.Green });
                gsu.Empires.Add(new Empire { Name = "Norak Ascendancy", Color = Color.Blue });
                gsu.Empires.Add(new Empire { Name = "Abbidon Enclave", Color = Color.Orange });
                gsu.CreateGalaxy();

                // test saving the game
                var sw = new StreamWriter("save.gam");
                sw.Write(Galaxy.Current.SerializeGameState());
                sw.Close();

                // test loading the game
                var sr = new StreamReader("save.gam");
                Galaxy.Current = Galaxy.DeserializeGameState(sr);
                sr.Close();

                // test redacting fogged info
                Galaxy.Current.CurrentEmpire = Galaxy.Current.Empires[0];
                Galaxy.Current.Redact();

                // test saving the player's view
                sw = new StreamWriter("p1.gam");
                sw.Write(Galaxy.Current.SerializeGameState());
                sw.Close();
            })
                .ContinueWithWithExceptionHandling(t =>
                {
                    var game = new GameForm(Galaxy.Current);
                    game.Show();
                    game.FormClosed += (s, args) =>
                    {
                        game.Dispose();
                        Show();
                        IsBusy = false;
                    };
                    Hide();
                }, scheduler);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion
    }
}
