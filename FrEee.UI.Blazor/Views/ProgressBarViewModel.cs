using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Views
{
    public class ProgressBarViewModel
    {
        public long Maximum { get; set; } = 0;

        public long Value { get; set; } = 0;

        public long Increment { get; set; } = 0;

        public Color BarColor { get; set; } = Color.Blue;

        public string LeftText { get; set; } = "";

        public string CenterText { get; set; } = "";

        public string RightText { get; set; } = "";

        public Action OnClick { get; set; } = () => { };

        public Color IncrementColor1 => Color.FromArgb(BarColor.A / 2, BarColor);

        public Color IncrementColor2 => Color.FromArgb(BarColor.A / 4, BarColor);

        public Color IncrementColor3 => Color.FromArgb(BarColor.A / 8, BarColor);
    }
}
