using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Processes.Combat;
using FrEee.UI.WinForms.Forms;
using FrEee.UI.WinForms.Objects;
using FrEee.Utility;
using FrEee.Vehicles;
using Screen = FrEee.Utility.Screen;

namespace FrEee.UI.WinForms.Utility;

public class GuiController
	: IGuiController
{
	public void Close(Screen screen)
	{
		GetForm(screen)?.Close();
	}

	public void CloseGame()
	{
		if (ClientSettings.Instance.QuitToMainMenu)
		{
			ReturnToMainMenu();
		}
		else
		{
			Exit();
		}
	}

	public void Exit()
	{
		Application.Exit();
	}

	public void Focus(ISpaceObject context)
	{
		MainGameForm.SelectSpaceObject(context);
		Close(Screen.Log); // if it's open...
	}

	public void Focus(StarSystem context)
	{
		MainGameForm.SelectStarSystem(context);
		Close(Screen.Log); // if it's open...
	}

	public void Focus(Technology context)
	{
		Show(Screen.Research);
		// TODO: focus the technology
		Close(Screen.Log); // if it's open...
	}

	public void Focus(IHull context)
	{
		new VehicleDesignForm(context).Show();
		Close(Screen.Log); // if it's open...
	}

	public void Focus(IBattle context)
	{
		new BattleResultsForm(context).Show();
		Close(Screen.Log); // if it's open...
	}

	public void Focus(IMessage context)
	{
		new DiplomacyForm(context).Show();
		Close(Screen.Log);
	}

	public void Hide(Screen screen)
	{
		GetForm(screen)?.Hide();
	}

	public void ReturnToMainMenu()
	{
		foreach (Form form in Application.OpenForms)
		{
			if (form is MainMenuForm)
			{
				form.Show();
			}
			else
			{
				form.Close();
			}
		}
	}

	public void Show(Screen screen)
	{
		var form = GetForm(screen);
		if (form is null)
		{
			form = (Form)Activator.CreateInstance(GetFormType(screen));
		}
		form.Show();
	}

	private Form? GetForm(Screen screen)
	{
		var type = GetFormType(screen);
		return Application.OpenForms.Cast<Form>().Where(q => q.GetType() == type).SingleOrDefault();
	}

	private Type GetFormType(Screen screen)
	{
		var tn = typeof(MainGameForm).FullName.Replace("MainGame", screen.ToString());
		return Type.GetType(tn);
	}

	private MainGameForm MainGameForm => MainGameForm.Instance;
}
