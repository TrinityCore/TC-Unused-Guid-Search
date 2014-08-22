using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UnusedGuidSearcher
{
    public partial class ResultForm : Form
    {
        private readonly IEnumerable<int> _missingGuids;

        public ResultForm(IEnumerable<int> missingGuids)
        {
            _missingGuids = missingGuids;

            InitializeComponent();
        }

        private void ResultFormLoad(object sender, EventArgs e)
        {
            foreach (var guid in _missingGuids)
                ResultBox.AppendText(guid + "," + Environment.NewLine);
        }
    }
}
