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
		}

        private void ShowImage_Paint(object sender, PaintEventArgs e)
        {
//			e.Graphics.DrawImage(OpenBitmap, 10, 10);

            Bitmap bitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            for(int y=0; y<OpenBitmap.Height; y++)
            {
                for(int x=0; x<OpenBitmap.Width; x++)
                {
                    bitmap.SetPixel(x, y, OpenBitmap.GetPixel(x, y));
                }
            }
            e.Graphics.DrawImage(bitmap, 10, 10);
            this.Size = new Size(OpenBitmap.Width + 40, OpenBitmap.Height + 80);
        }
    }
}
