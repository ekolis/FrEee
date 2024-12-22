using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Extensions;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;
using FrEee.UI.WinForms.Controls;
using FrEee.UI.Blazor.Views;
using Excubo.Blazor.Canvas.Contexts;

namespace FrEee.UI.WinForms.Forms;

public partial class LogForm
	: BlazorForm
{
	/// <summary>
	/// Creates a log form.
	/// </summary>
	/// <param name="mainGameForm"></param>
	/// <param name="battle">The battle whose log we should display, or null to display the turn log.</param>
	public LogForm(MainGameForm mainGameForm, IList<LogMessage> log)
	{
		InitializeComponent();
		this.mainGameForm = mainGameForm;

		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); } catch { }

		ShowInTaskbar = !mainGameForm.HasLogBeenShown;

		VM.Messages = log;
		VM.OnBattleSelected += VM_OnBattleSelected;
		VM.OnHullSelected += VM_OnHullSelected;
		VM.OnMessageSelected += VM_OnMessageSelected;
		VM.OnSpaceObjectSelected += VM_OnSpaceObjectSelected;
		VM.OnStarSystemSelected += VM_OnStarSystemSelected;
		VM.OnTechnologySelected += VM_OnTechnologySelected;
	}

	private void VM_OnTechnologySelected(object? sender, Technology e)
	{
		// go to research screen
		mainGameForm.ShowResearchForm();
		Close();
	}

	private void VM_OnStarSystemSelected(object? sender, StarSystem e)
	{
		mainGameForm.SelectStarSystem(e);
		Close();
	}

	private void VM_OnSpaceObjectSelected(object? sender, ISpaceObject e)
	{
		// go to space object
		mainGameForm.SelectSpaceObject(e);
		Close();
	}

	private void VM_OnMessageSelected(object? sender, IMessage e)
	{
		var form = new DiplomacyForm(e);
		mainGameForm.ShowChildForm(form);
		Close();
	}

	private void VM_OnHullSelected(object? sender, IHull? e)
	{
		// go to design screen and create a new design using this hull
		if (e is null)
		{
			mainGameForm.ShowVehicleDesignForm(new VehicleDesignForm());
		}
		else
		{
			mainGameForm.ShowVehicleDesignForm(new VehicleDesignForm(e));
		}
		Close();
	}

	private void VM_OnBattleSelected(object? sender, IBattle e)
	{
		// show battle
		var form = new BattleResultsForm(e);
		this.ShowChildForm(form);
	}

	protected override Type BlazorComponentType { get; } = typeof(HistoryLog);

	protected override HistoryLogViewModel VM { get; } = new();

	private MainGameForm mainGameForm;
}