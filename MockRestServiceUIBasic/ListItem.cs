using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MockRestService.Model;

namespace MockRestServiceUIBasic
{
    public partial class ListItem : UserControl
    {
        private Boolean isSelected;

        private MockRule mockRule = new PathRule();

        public ListItem()
        {
            InitializeComponent();
            applyStyles();
        }

        public Boolean IsSelected
        {
            get { return this.isSelected; }
            set { this.isSelected = value; this.OnPropertyChanged("IsSelected"); }
        }

        private void OnPropertyChanged(String name)
        {
            applyStyles();
        }

        private void applyStyles()
        {
            if (this.isSelected)
            {
                this.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this.BackColor = Color.PowderBlue;
            }

            if (mockRule != null)
            {
                if (mockRule is HeaderRule)
                {
                    label1.BackColor = Color.Purple;
                }
                else if (mockRule is PathRule)
                {
                    label1.BackColor = Color.RoyalBlue;
                }
                else if (mockRule is QueryRule)
                {
                    label1.BackColor = Color.Tan;
                }
            }
        }
    }
}
