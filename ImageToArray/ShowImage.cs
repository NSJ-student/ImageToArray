using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToArray
{
    public partial class ShowImage : Form
    {
		Bitmap OpenBitmap;

		public ShowImage(Bitmap bitmap)
        {
            InitializeComponent();
			OpenBitmap = bitmap;
			this.ClientSize = new Size(OpenBitmap.Width, OpenBitmap.Height);
		}

        private void ShowImage_Paint(object sender, PaintEventArgs e)
        {
			e.Graphics.DrawImage(OpenBitmap, new Rectangle(0,0,OpenBitmap.Width, OpenBitmap.Height));
        }
    }
}
