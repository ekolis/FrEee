using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using FrEee.Avalonia.ViewModels;

namespace FrEee.Avalonia.Views
{
	/// <summary>
	/// A layer of UI that can be pushed and popped on a stack.
	/// </summary>
	public class ViewLayer
	{
		/// <summary>
		/// The stack of view layers.
		/// </summary>
		private static Stack<ViewLayer> Stack { get; } = new Stack<ViewLayer>();

		/// <summary>
		/// Adds a new layer.
		/// </summary>
		/// <param name="layer">The layer to add.</param>
		public static void Push(ViewLayer layer)
		{
			Stack.Push(layer);
			GameWindow.Instance.Load(layer);
		}

		/// <summary>
		/// Removes the top layer.
		/// </summary>
		/// <returns>The layer removed.</returns>
		public static ViewLayer Pop()
		{
			var layer = Stack.Pop();
			if (Stack.Any())
				GameWindow.Instance.Load(Stack.Peek());
			else
				GameWindow.Instance.Close(); // exit the game
			return layer;
		}

		public static bool Any() => Stack.Any();

		public Control? Top { get; set; }

		public Control? Bottom { get; set; }

		public Control? Left { get; set; }

		public Control? Right { get; set; }

		public Control? Center { get; set; }

		/// <summary>
		/// The main menu which appears when the game is first loaded.
		/// </summary>
		public static ViewLayer MainMenu { get; } =
			new ViewLayer()
			{
				Left = new Image
				{
					Source = new Bitmap("Pictures/Splash.jpg")
				},
				Right = new MainMenu
				{
					DataContext = new MainMenuViewModel()
				},
			};

		public static ViewLayer Strategy { get; } =
			new ViewLayer()
			{
				Center = new TextBlock
				{
					Text = "This is the main system map and stuff."
				},
				Left = new TextBlock
				{
					Text = "The map tabs go here."
				},
				Right = new TextBlock
				{
					Text = "Here is where we put the selected item report and the galaxy map."
				},
				Top = new TextBlock
				{
					Text = "Here be the header with all sorts of nice status stuff and command butons and such."
				},
				Bottom = new TextBlock
				{
					Text = "I dunno, maybe some more detailed tooltips?"
				}
			};
	}
}
