using DBAToolKit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBAToolKit.Tools
{
    public partial class Get_SqlJobsTimelineFilter : Form
    {
        public List<JobFilter> CategoriesToDisplay { get; set; }
        public Get_SqlJobsTimelineFilter()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void Get_SqlJobsTimelineFilter_Load(object sender, EventArgs e)
        {
            checkedListCategories.DataSource = CategoriesToDisplay;
            checkedListCategories.DisplayMember = "CategoryName";
            for (int i = 0; i < checkedListCategories.Items.Count; ++i)
            {
                checkedListCategories.SetItemChecked(i, ((JobFilter)checkedListCategories.Items[i]).IsChecked);
            }
        }

        private void checkedListCategories_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            CategoriesToDisplay[e.Index].IsChecked = (e.NewValue != CheckState.Unchecked);
        }
    }
}
