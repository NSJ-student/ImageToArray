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
		BackgroundWorker LoadWorker;

        public ImageToArray()
        {
            InitializeComponent();

			pbProgress.Visible = false;
            dialog = new OpenFileDialog();
			dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png|VGD Files(*.vgd)|*.vgd";

			if (rb565RGB.Checked)
			{
				ConvertFunc = new RGBConvert(RGB565Convert);
			}
			else
			{
				ConvertFunc = new RGBConvert(RGB888Convert);
			}
			LoadWorker = new BackgroundWorker();
			LoadWorker.WorkerReportsProgress = true;
			LoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ConvertImageComplete);
			LoadWorker.DoWork += new DoWorkEventHandler(ConvertImage);
			LoadWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgressBar);
		}

		private void UpdateProgressBar(object obj, ProgressChangedEventArgs arg)
		{
			pbProgress.Value = arg.ProgressPercentage;
		}

		private void ConvertImage(object obj, DoWorkEventArgs arg)
		{
			StringBuilder builder = new StringBuilder(SelectedImage.Size.Width * SelectedImage.Size.Height * 20 + 100);
			builder.Append( "그림 크기: " + SelectedImage.Size.Width.ToString() + " X "
								+ SelectedImage.Size.Height.ToString() + "\n");
			UInt16[] pixArr = new UInt16[SelectedImage.Size.Width * SelectedImage.Size.Height];

			double total = SelectedImage.Size.Height * SelectedImage.Size.Width;
			for (int y = 0; y < SelectedImage.Size.Height; y++)
			{
				for (int x = 0; x < SelectedImage.Size.Width; x++)
				{
					Color clr = SelectedImage.GetPixel(x, y);
					pixArr[y*SelectedImage.Size.Width + x] = ConvertFunc(clr.R, clr.G, clr.B, chkSwap.Checked);
					builder.Append( "0x" + pixArr[y * SelectedImage.Size.Width + x].ToString("X") + ", " );
				}
				LoadWorker.ReportProgress((int)(y * 100 / SelectedImage.Size.Height));
			}
			arg.Result = builder.ToString();
		}

		private void ConvertImageComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			rtbArrayCode.Text = (string)arg.Result;
			MessageBox.Show("Conversion Complete!");

			pbProgress.Visible = false;
		}


        private void btnImageLoad_Click(object sender, EventArgs e)
        {
            DialogResult result = dialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                tbImagePath.Text = dialog.FileName;
				string ext = Path.GetExtension(dialog.FileName);
				if (ext == ".vgd")
                {
					SelectedImage = VgdToBitmap(dialog.FileName);

				}
				else
				{
					SelectedImage = new Bitmap(dialog.FileName);
				}
            }
        }
		private Bitmap VgdToBitmap(string fileName)
		{
			FileStream file = File.OpenRead(fileName);
			BinaryReader reader = new BinaryReader(file);
			if(file.Length - 784 < 3840 * 2160 * 4)
            {
				return new Bitmap(640, 480);
			}

			Bitmap result = new Bitmap(3840, 2160);

			byte[] buffer = new byte[3840 * 2160 * 4];
			reader.Read(buffer, 0, 784);
			int readByte = reader.Read(buffer, 0, 3840 * 2160 * 4);
			if(readByte < 3840 * 2160 * 4)
			{
				return result;
            }

			for (int y = 0; y < 2160; y++)
			{
				for (int x = 0; x < 3840; x++)
				{
					int index = x * y * 4;
					/*
					int r = buffer[index+3]+ (buffer[index + 2]<<8)&0x03;
					int g = (buffer[index+2]>>2) + (buffer[index + 1]<<6)&0x0F;
					int b = (buffer[index + 1] >> 4) & 0x0F + (buffer[index+0]<<4);
					*/
					int r = buffer[index + 3];
					int g = buffer[index + 2];
					int b = buffer[index + 1];
					System.Drawing.Color pixCol = Color.FromArgb(r, g, b);

					result.SetPixel(x, y, pixCol);
				}
			}

			return result;
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
				pbProgress.Visible = true;
				LoadWorker.RunWorkerAsync(null);
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
