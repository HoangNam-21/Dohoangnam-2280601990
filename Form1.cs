using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BtapBuoi5
{
    public partial class Word : Form
    {
        public Word()
        {
            InitializeComponent();
            cmbFont.SelectedIndexChanged += new EventHandler(cmbFont_SelectedIndexChanged);
            cmbSize.SelectedIndexChanged += new EventHandler(cmbSize_SelectedIndexChanged);
            this.Load += new EventHandler(Word_Load);
        }

        private void loadFont()
        {
            foreach (FontFamily fontFamily in new InstalledFontCollection().Families)
            {
                cmbFont.Items.Add(fontFamily.Name);
            }
            cmbFont.SelectedItem = "Tahoma";
        }

        private void loadSize()
        {
            int[] sizeValues = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            cmbSize.ComboBox.DataSource = sizeValues;
            cmbSize.SelectedItem = 14;
        }

        private void Word_Load(object sender, EventArgs e)
        {
            loadFont();
            loadSize();
            rtbVanBan.Font = new Font("Tahoma", 14, FontStyle.Regular);
        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TạoVănBảnMới()
        {
            rtbVanBan.Clear();
            rtbVanBan.Font = new Font("Tahoma", 14, FontStyle.Regular);
        }

        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TạoVănBảnMới();
        }

        private void mởTệpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbVanBan.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|RichText files (*.rtf)|*.rtf";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                try
                {
                    if (Path.GetExtension(selectedFileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        rtbVanBan.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        rtbVanBan.LoadFile(selectedFileName, RichTextBoxStreamType.RichText);
                    }
                    MessageBox.Show("Tập tin đã được mở thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình mở tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string currentFilePath = string.Empty;

        private void lưuNộiDungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Rich Text Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";
                saveFileDialog.DefaultExt = "rtf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    rtbVanBan.SaveFile(currentFilePath);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                rtbVanBan.SaveFile(currentFilePath);
                MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbVanBan.SelectionFont != null && cmbFont.SelectedItem != null)
            {
                Font currentFont = rtbVanBan.SelectionFont;
                string newFontFamily = cmbFont.SelectedItem.ToString();
                rtbVanBan.SelectionFont = new Font(newFontFamily, currentFont.Size, currentFont.Style);
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbVanBan.SelectionFont != null && cmbSize.SelectedItem != null)
            {
                Font currentFont = rtbVanBan.SelectionFont;
                float newSize;
                if (float.TryParse(cmbSize.SelectedItem.ToString(), out newSize))
                {
                    rtbVanBan.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                }
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (rtbVanBan.SelectionFont != null)
            {
                Font oldFont = rtbVanBan.SelectionFont;
                FontStyle newStyle;
                if (oldFont.Style.HasFlag(FontStyle.Bold))
                    newStyle = oldFont.Style & ~FontStyle.Bold;
                else
                    newStyle = oldFont.Style | FontStyle.Bold;

                rtbVanBan.SelectionFont = new Font(oldFont.FontFamily, oldFont.Size, newStyle);
            }
        }

        private void cmbFont_Click(object sender, EventArgs e)
        {
            if (rtbVanBan.SelectionFont != null && cmbFont.SelectedItem != null)
            {
                Font currentFont = rtbVanBan.SelectionFont;
                string newFontFamily = cmbFont.SelectedItem.ToString();
                rtbVanBan.SelectionFont = new Font(newFontFamily, currentFont.Size, currentFont.Style);
            }
        }

        private void cmbSize_Click(object sender, EventArgs e)
        {
            if (rtbVanBan.SelectionFont != null && cmbSize.SelectedItem != null)
            {
                Font currentFont = rtbVanBan.SelectionFont;
                float newSize;
                if (float.TryParse(cmbSize.SelectedItem.ToString(), out newSize))
                {
                    rtbVanBan.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                }
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (rtbVanBan.SelectionFont != null)
            {
                Font oldFont = rtbVanBan.SelectionFont;
                FontStyle newStyle;
                if (oldFont.Style.HasFlag(FontStyle.Italic))
                    newStyle = oldFont.Style & ~FontStyle.Italic;
                else
                    newStyle = oldFont.Style | FontStyle.Italic;
                rtbVanBan.SelectionFont = new Font(oldFont.FontFamily, oldFont.Size, newStyle);
            }
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (rtbVanBan.SelectionFont != null)
            {
                Font oldFont = rtbVanBan.SelectionFont;
                FontStyle newStyle;
                if (oldFont.Style.HasFlag(FontStyle.Underline))
                    newStyle = oldFont.Style & ~FontStyle.Underline;
                else
                    newStyle = oldFont.Style | FontStyle.Underline;
                rtbVanBan.SelectionFont = new Font(oldFont.FontFamily, oldFont.Size, newStyle);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            TạoVănBảnMới();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LưuNộiDung();
        }

        private void LưuNộiDung()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Rich Text Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";
                saveFileDialog.DefaultExt = "rtf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    rtbVanBan.SaveFile(currentFilePath);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                rtbVanBan.SaveFile(currentFilePath);
                MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
