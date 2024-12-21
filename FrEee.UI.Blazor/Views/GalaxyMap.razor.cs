using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excubo.Blazor.Canvas;
using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Views;

public partial class GalaxyMap
{
	private Canvas helper_canvas;
	private ElementReference normal_canvas;
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		var size = VM.Scale + 1;
		var xoffset = VM.Width / 2d;
		var yoffset = VM.Height / 2d;

		await using (var ctx = await helper_canvas.GetContext2DAsync())
		{
			await ctx.ClearRectAsync(0, 0, VM.Width, VM.Height);
			await ctx.SetTransformAsync(size, 0, 0, size, (xoffset + 0.5) * size, (yoffset + 0.5) * size);
			await ctx.RestoreAsync();
			await ctx.SaveAsync();
			await ctx.StrokeStyleAsync("white");
			await ctx.LineWidthAsync(0.1);
			foreach (var connections in VM.WarpGraph.Connections)
			{
				var src = connections.Key;
				foreach (var dest in connections.Value)
				{
					// TODO: display one way warps differently (arrows, incomplete lines, gradients?)
					await ctx.MoveToAsync(src.Location.X + 1, src.Location.Y - 1);
					await ctx.LineToAsync(dest.Location.X + 1, dest.Location.Y - 1);
					await ctx.StrokeAsync();
				}
			}
		}

		await base.OnAfterRenderAsync(firstRender);
	}
}
