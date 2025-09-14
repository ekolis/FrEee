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
		var size = VM.Scale;

		// location of map tile (0,0) relative to the upper left corner of the map
		var xoffset = -VM.MinX;
		var yoffset = -VM.MinY;

		await using (var ctx = await helper_canvas.GetContext2DAsync())
		{
			await ctx.ClearRectAsync(0, 0, VM.Width, VM.Height);
			await ctx.SetTransformAsync(size, 0, 0, size, xoffset * size, yoffset * size);
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
					await ctx.MoveToAsync(src.Location.X + 0.5, src.Location.Y + 0.5);
					await ctx.LineToAsync(dest.Location.X + 0.5, dest.Location.Y + 0.5);
					await ctx.StrokeAsync();
				}
			}
		}

		await base.OnAfterRenderAsync(firstRender);
	}
}
