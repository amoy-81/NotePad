using System.Windows.Forms;

namespace NotePad
{
    public partial class Form1 : Form
    {
        string? fileName;
        bool isSaved = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void saveClick(object? sender, EventArgs? e)
        {
            if (fileName == null)
            {
                saveFileDialog1.Filter = "Text Files (*.txt)|*.txt";
                saveFileDialog1.DefaultExt = "txt";
                DialogResult result = saveFileDialog1.ShowDialog();

                if (result == DialogResult.Cancel)
                    return;

                fileName = saveFileDialog1.FileName;
            }

            System.IO.File.WriteAllText(fileName, Body.Text);
            this.Text = fileName;
            isSaved = true;
        }

        private void saveAsClick(object sender, EventArgs e)
        {
            fileName = null;
            saveClick(null, null);
        }

        private void doYouWantToSave()
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                saveClick(null, null);
        }

        private void newClick(object sender, EventArgs e)
        {
            if (isSaved == false && Body.Text.Length > 0)
                doYouWantToSave();

            this.Text = "NotePad";
            Body.Text = "";
            fileName = null;
            isSaved = false;
        }

        private void textOnChange(object sender, EventArgs e)
        {
            isSaved = false;
            if (this.Text.Contains("*") == false)
                this.Text += "*";
        }

        private void openClick(object sender, EventArgs e)
        {
            if (isSaved == false && Body.Text.Length > 0)
                doYouWantToSave();

            openFileDialog1.Filter = "Text Files (*.txt)|*.txt";
            DialogResult openResult = openFileDialog1.ShowDialog();
            if (openResult == DialogResult.Cancel) 
                return;

            fileName = openFileDialog1.FileName;
            Body.Text = System.IO.File.ReadAllText(fileName);
            this.Text = fileName;
            isSaved = true;
        }
    }
}
