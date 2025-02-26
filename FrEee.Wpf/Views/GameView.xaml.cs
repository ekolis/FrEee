﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrEee.Utility;
using FrEee.Wpf.ViewModels;
using DynamicViewModel;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Interaction logic for GameView.xaml
	/// </summary>
	public partial class GameView
	{
		public GameView()
		{
			InitializeComponent();
			Galaxy = new GameViewModel(Game.Objects.Space.Galaxy.Current);
		}

		public GameViewModel Galaxy
		{
			get
			{
				return (GameViewModel)DataContext;
			}
			set
			{
				DataContext = value;
			}
		}
	}
}
