using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using System;
using System.Drawing;

namespace FrEee.WinForms.Forms;

public partial class DebugForm : GameForm
{
	public DebugForm()
	{
		InitializeComponent();
		try
		{
			// why is the script engine remembering my imports? oh well, it's handy :P
			PythonScriptEngine.EvaluateExpression<object>("from FrEee.Modding import Mod");
			rtbOutput.AppendText("Imported Mod.\n");
			rtbOutput.AppendText("Mod.Current is currently: " + OrNil(Mod.Current) + "\n");

			PythonScriptEngine.EvaluateExpression<object>("from FrEee.Objects.Space import Galaxy");
			rtbOutput.AppendText("Imported Galaxy.\n");
			rtbOutput.AppendText("Galaxy.Current is currently: " + OrNil(Galaxy.Current) + "\n");

			PythonScriptEngine.EvaluateExpression<object>("from FrEee.Objects.Civilization import Empire");
			rtbOutput.AppendText("Imported Empire.\n");
			rtbOutput.AppendText("Empire.Current is currently: " + OrNil(Empire.Current) + "\n");

			if (Mod.Current != null)
			{
				PythonScriptEngine.EvaluateExpression<object>(Mod.Current.GlobalScript.FullText);
				rtbOutput.AppendText("Ran mod global script.\n");
			}
		}
		catch (Exception ex)
		{
			rtbOutput.AppendText("Error initializing debug console.\n");
			rtbOutput.AppendText(ex + "\n");
		}
	}

	private void btnSubmit_Click(object sender, EventArgs e)
	{
		rtbOutput.SelectionColor = Color.Green;
		rtbOutput.AppendText(">" + txtCommand.Text + "\n");
		rtbOutput.SelectionColor = rtbOutput.ForeColor;
		try
		{
			var result = PythonScriptEngine.EvaluateExpression<object>("str(" + txtCommand.Text + ")");
			rtbOutput.AppendText(OrNil(result));
			rtbOutput.AppendText("\n");
		}
		catch (Exception ex)
		{
			rtbOutput.AppendText("Exception has occurred.\n");
			rtbOutput.AppendText(ex + "\n");
		}
		rtbOutput.ScrollToCaret();
		txtCommand.Text = "";
	}

	private string OrNil(object o)
	{
		if (o == null)
			return "<nil>";
		else
			return o.ToString();
	}
}