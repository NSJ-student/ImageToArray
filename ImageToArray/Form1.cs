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
    public partial class ImageToArray : Form
    {
        OpenFileDialog dialog;
		Bitmap SelectedImage;
		BackgroundWorker LoadWorker;
        BackgroundWorker VgdWorker;
        bool vgdSelected;

        public ImageToArray()
        {
            InitializeComponent();

			pbProgress.Visible = false;
            dialog = new OpenFileDialog();
			dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png|VGD Files(*.vgd)|*.vgd";
            
			LoadWorker = new BackgroundWorker();
			LoadWorker.WorkerReportsProgress = true;
			LoadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ConvertImageComplete);
			LoadWorker.DoWork += new DoWorkEventHandler(ConvertImage);
			LoadWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgressBar);

            VgdWorker = new BackgroundWorker();
            VgdWorker.WorkerReportsProgress = true;
            VgdWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ConvertVgdComplete);
            VgdWorker.DoWork += new DoWorkEventHandler(ConvertVgd);
            VgdWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgressBar);

            vgdSelected = false;
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

            if (rb565RGB.Checked)
            {
                UInt16[] pixArr = new UInt16[SelectedImage.Size.Width * SelectedImage.Size.Height];

                double total = SelectedImage.Size.Height * SelectedImage.Size.Width;
                for (int y = 0; y < SelectedImage.Size.Height; y++)
                {
                    for (int x = 0; x < SelectedImage.Size.Width; x++)
                    {
                        Color clr = SelectedImage.GetPixel(x, y);
                        
                        pixArr[y * SelectedImage.Size.Width + x] = RGB565Convert(clr.R, clr.G, clr.B, chkSwap.Checked);
                        builder.Append("0x" + pixArr[y * SelectedImage.Size.Width + x].ToString("X") + ", ");
                    }
                    LoadWorker.ReportProgress((int)(y * 100 / SelectedImage.Size.Height));
                }
            }
            else
            {
                UInt32[] pixArr = new UInt32[SelectedImage.Size.Width * SelectedImage.Size.Height];

                double total = SelectedImage.Size.Height * SelectedImage.Size.Width;
                for (int y = 0; y < SelectedImage.Size.Height; y++)
                {
                    for (int x = 0; x < SelectedImage.Size.Width; x++)
                    {
                        Color clr = SelectedImage.GetPixel(x, y);

                        pixArr[y * SelectedImage.Size.Width + x] = RGB888Convert(clr.R, clr.G, clr.B, chkSwap.Checked);
                        builder.Append("0x" + pixArr[y * SelectedImage.Size.Width + x].ToString("X") + ", ");
                    }
                    LoadWorker.ReportProgress((int)(y * 100 / SelectedImage.Size.Height));
                }
            }
			arg.Result = builder.ToString();
		}

		private void ConvertImageComplete(object obj, RunWorkerCompletedEventArgs arg)
		{
			rtbArrayCode.Text = (string)arg.Result;
			MessageBox.Show("Conversion Complete!");

			pbProgress.Visible = false;
		}

        private void ConvertVgd(object obj, DoWorkEventArgs arg)
        {
            FileStream file = File.OpenRead(dialog.FileName);
            BinaryReader reader = new BinaryReader(file);

            byte[] tmp_buffer = new byte[784];
            reader.Read(tmp_buffer, 0, 784);
            int height = tmp_buffer[2] + (tmp_buffer[3]<<8);
            int width = (int)(file.Length - 784) / (4*height);

            Bitmap result = new Bitmap(width, height);

            byte[] buffer = new byte[width * height * 4];
            int readByte = reader.Read(buffer, 0, width * height * 4);
            if (readByte < width * height * 4)
            {
                arg.Result = result;
                return;
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = ((y* width) +x)*4;
                    /*
                    int pixel = 0;
                    pixel += buffer[index + 0];
                    pixel += buffer[index + 1]<<8;
                    pixel += buffer[index + 2]<<16;
                    pixel += buffer[index + 3]<<24;
                    int r = (pixel >> 0) & 0xFF;
                    int g = (pixel >> 8) & 0xFF;
                    int b = (pixel >> 16) & 0xFF;
                    //b = b * 255 / 1023;
                    //g = g * 255 / 1023;
                    //r = r * 255 / 1023;
                    System.Drawing.Color pixCol = Color.FromArgb(r, g, b);
                    int r = buffer[index + 2];
					int g = buffer[index + 1];
					int b = buffer[index + 0];
                    int a = buffer[index + 3];
                    System.Drawing.Color pixCol = Color.FromArgb(a, r, g, b);
                    */
                    int r = buffer[index + 2];
                    int g = buffer[index + 1];
                    int b = buffer[index + 0];
                    System.Drawing.Color pixCol = Color.FromArgb(r, g, b);

                    result.SetPixel(x, y, pixCol);
                }
                VgdWorker.ReportProgress((int)(y * 100 / height));
            }

            result.Save("test.bmp");
            arg.Result = result;
        }

        private void ConvertVgdComplete(object obj, RunWorkerCompletedEventArgs arg)
        {
            SelectedImage = (Bitmap)arg.Result;
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
                    vgdSelected = true;
                    pbProgress.Visible = true;
                    VgdWorker.RunWorkerAsync(null);
                }
				else
                {
                    vgdSelected = false;
                    SelectedImage = new Bitmap(dialog.FileName);
				}
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

        private UInt32 RGB888Convert(int red, int green, int blue, bool swap)
		{
			int temp = (((red) << 16) | ((green) << 8) | (blue));
			// SWAP 시킨 32bit로 출력
            if (swap)   return (UInt32)((temp & 0x00FF) << 24 | (temp & 0xFF00) << 16 | (temp & 0xFF0000) >> 8);
			// 그냥 출력
            else        return (UInt32)temp;
		}

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (vgdSelected)
            {
                return;
            }
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
            if (vgdSelected)
            {
                return;
            }
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

                if((SelectedImage.Size.Width > 255) ||
                    (SelectedImage.Size.Height > 255))
                {
                    rtbArrayCode.Text = "** warning: image size out of range";
                }

                writer.Write( (byte)SelectedImage.Size.Width );
                writer.Write( (byte)SelectedImage.Size.Height);

				for (int y = 0; y < SelectedImage.Size.Height; y++)
				{
					for (int x = 0; x < SelectedImage.Size.Width; x++)
					{
						Color clr = SelectedImage.GetPixel(x, y);

                        if (rb565RGB.Checked)
                        {
                            writer.Write(RGB565Convert(clr.R, clr.G, clr.B, chkSwap.Checked));
                        }
                        else
                        {
                            writer.Write(RGB888Convert(clr.R, clr.G, clr.B, chkSwap.Checked));
                        }
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
			}
			else
            {
                rbNormalRGB.Checked = true;
			}
		}

		private void rbNormalRGB_CheckedChanged(object sender, EventArgs e)
		{
            if (rbNormalRGB.Checked)
			{
                rb565RGB.Checked = false;
			}
			else
            {
                rb565RGB.Checked = true;
			}
		}
    
	}
}
