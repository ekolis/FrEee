using System;
using System.Collections.Generic;

namespace FrEee.WinForms.DataGridView
{
    /// <summary>
    /// Configuration for a grid view.
    /// </summary>
    [Serializable]
    public class GridConfig
    {
        #region Public Constructors

        public GridConfig()
        {
            Columns = new List<GridColumnConfig>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<GridColumnConfig> Columns { get; private set; }
        public string Name { get; set; }

        #endregion Public Properties
    }
}
