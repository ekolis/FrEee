using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Utility
{
    /// <summary>
    /// Progress towards completing something.
    /// </summary>
    public class ResourceProgress : IEnumerable<KeyValuePair<Resource, Progress>>
    {
        #region Public Constructors

        public ResourceProgress(ResourceQuantity value, ResourceQuantity maximum, ResourceQuantity incrementalProgress)
        {
            Value = value;
            Maximum = maximum;
            IncrementalProgress = incrementalProgress;
        }

        #endregion Public Constructors

        #region Public Properties

        public ResourceQuantity IncrementalProgress { get; set; }
        public ResourceQuantity Maximum { get; set; }
        public ResourceQuantity Value { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IEnumerator<KeyValuePair<Resource, Progress>> GetEnumerator()
        {
            foreach (var r in Value.Keys.Union(Maximum.Keys).Union(IncrementalProgress.Keys))
                yield return new KeyValuePair<Resource, Progress>(r, new Progress(Value[r], Maximum[r], IncrementalProgress[r]));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("{0} / {1} ({2:+#;-#;0} per turn", Value, Maximum, IncrementalProgress);
        }

        #endregion Public Methods
    }
}
