using DBAToolKit.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DBAToolKit.Helpers
{
    public partial class SelectItemsToCopy : Form
    {
        public List<ItemToCopy> ItemsToCopy { get; set; }
        public SelectItemsToCopy()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void SelectItemsToCopy_Load(object sender, EventArgs e)
        {
            checkedListItemsToCopy.DataSource = ItemsToCopy;
            checkedListItemsToCopy.DisplayMember = "Name";
            for (int i = 0; i < checkedListItemsToCopy.Items.Count; ++i)
            {
                checkedListItemsToCopy.SetItemChecked(i, ((ItemToCopy)checkedListItemsToCopy.Items[i]).IsChecked);
            }
        }
        private void checkedListItemsToCopy_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ItemsToCopy[e.Index].IsChecked = (e.NewValue != CheckState.Unchecked);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListItemsToCopy.Items.Count; ++i)
            {
                checkedListItemsToCopy.SetItemChecked(i, chkSelectAll.Checked);
            }
        }
    }
}
