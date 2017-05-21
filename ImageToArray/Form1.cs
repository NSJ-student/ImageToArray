using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageToArray
{
    delegate UInt16 RGBConvert(int red, int green, int blue, bool swap);

    public partial class ImageToArray : Form
    {
        OpenFileDialog dialog;
		Bitmap SelectedImage;
		RGBConvert ConvertFunc;

        public ImageToArray()
        {
            InitializeComponent();

            dialog = new OpenFileDialog();
			dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

			if (rb565RGB.Checked)
			{
				ConvertFunc = new RGBConvert(RGB565Convert);
			}
			else
			{
				ConvertFunc = new RGBConvert(RGB888Convert);
			}
        }

        private void btnImageLoad_Click(object sender, EventArgs e)
        {
            DialogResult result = dialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                tbImagePath.Text = dialog.FileName;
				SelectedImage = new Bitmap(dialog.FileName);
            }
        }

		private UInt16 RGB565Convert(int red, int green, int blue, bool swap)
		{
			int temp = (((red >> 3) << 11) | ((green >> 2) << 5) | (blue >> 3));
			// SWAP 시킨 16bit로 출력
            if (swap)   return (UInt16)((temp & 0x00FF) << 8 | (temp & 0xFF00) >> 8);
			// 그냥 출력
            else        return (UInt16)temp;
		}

        private UInt16 RGB888Convert(int red, int green, int blue, bool swap)
		{
			int temp = (((red) << 16) | ((green) << 8) | (blue));
			// SWAP 시킨 32bit로 출력
            if (swap)   return (UInt16)((temp & 0x00FF) << 24 | (temp & 0xFF00) << 16 | (temp & 0xFF0000) >> 8);
			// 그냥 출력
            else        return (UInt16)temp;
		}

        private void btnConvert_Click(object sender, EventArgs e)
		{
			if (dialog.CheckFileExists == true)
			{
				rtbArrayCode.Clear();
				string temp = "그림 크기: " + SelectedImage.Size.Width.ToString() + " X "
									+ SelectedImage.Size.Height.ToString() + "\n";
				for (int y = 0; y < SelectedImage.Size.Height; y++)
				{
					for (int x = 0; x < SelectedImage.Size.Width; x++)
					{
						Color clr = SelectedImage.GetPixel(x, y);
						temp += "0x" + ConvertFunc(clr.R, clr.G, clr.B, chkSwap.Checked).ToString("X") + ", ";
					}
				}
				rtbArrayCode.Text += temp;
			}
        }

        private void btnShowImage_Click(object sender, EventArgs e)
        {
            if(dialog.CheckFileExists == true)
			{
				ShowImage ImageForm = new ShowImage(SelectedImage);
				ImageForm.Visible = true;
            }
        }

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (dialog.CheckFileExists == true)
			{
				string FileName; 
				if(tbSaveName.Text != "")
				{
					FileName = tbSaveName.Text + ".bin";
				}
				else
				{
					FileName = "image.bin";
				}
				FileStream file = File.Create(FileName);
				BinaryWriter writer = new BinaryWriter(file);


                writer.Write( (byte)SelectedImage.Size.Width );
                writer.Write( (byte)SelectedImage.Size.Height );

				for (int y = 0; y < SelectedImage.Size.Height; y++)
				{
					for (int x = 0; x < SelectedImage.Size.Width; x++)
					{
						Color clr = SelectedImage.GetPixel(x, y);

						writer.Write(ConvertFunc(clr.R, clr.G, clr.B, chkSwap.Checked));
					}
				}

				writer.Close();
				file.Close();
				MessageBox.Show("[" + FileName + "] Save Completely!", "Save");
				tbSaveName.Clear();
			}
		}

		private void rb565RGB_CheckedChanged(object sender, EventArgs e)
		{
			if (rb565RGB.Checked)
			{
                rbNormalRGB.Checked = false;
				ConvertFunc = new RGBConvert(RGB565Convert);
			}
			else
            {
                rbNormalRGB.Checked = true;
				ConvertFunc = new RGBConvert(RGB888Convert);
			}
		}

		private void rbNormalRGB_CheckedChanged(object sender, EventArgs e)
		{
            if (rbNormalRGB.Checked)
			{
                rb565RGB.Checked = false;
                ConvertFunc = new RGBConvert(RGB888Convert);
			}
			else
            {
                rb565RGB.Checked = true;
                ConvertFunc = new RGBConvert(RGB565Convert);
			}
		}
    }
}
