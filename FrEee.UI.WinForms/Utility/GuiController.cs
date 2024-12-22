using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.UI.WinForms.Forms;
using FrEee.UI.WinForms.Objects;

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
		return Type.GetType(screen + "Form");
	}
}
