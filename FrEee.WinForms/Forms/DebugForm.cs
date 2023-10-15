using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class DebugForm : GameForm
	{
		public DebugForm()
		{
			InitializeComponent();
			try
			{
				// why is the script engine remembering my imports? oh well, it's handy :P
				PythonScriptEngine.EvaluateExpression<object>("from FrEee import The");
				PythonScriptEngine.EvaluateExpression<object>("from FrEee.Modding import Mod");
				rtbOutput.AppendText("Imported Mod.\n");
				rtbOutput.AppendText("The.Mod is currently: " + OrNil(The.Mod) + "\n");

				PythonScriptEngine.EvaluateExpression<object>("from FrEee.Objects.Space import Galaxy");
				rtbOutput.AppendText("Imported Galaxy.\n");
				rtbOutput.AppendText("The.Game is currently: " + OrNil(The.Game) + "\n");

				PythonScriptEngine.EvaluateExpression<object>("from FrEee.Objects.Civilization import Empire");
				rtbOutput.AppendText("Imported Empire.\n");
				rtbOutput.AppendText("Empire.Current is currently: " + OrNil(Empire.Current) + "\n");

				if (The.Mod != null)
				{
					PythonScriptEngine.EvaluateExpression<object>(The.Mod.GlobalScript.FullText);
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
}
