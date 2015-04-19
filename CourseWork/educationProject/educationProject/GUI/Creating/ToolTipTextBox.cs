using System;
using System.Windows.Controls;

namespace educationProject
{
    public partial class MainWindow
    {
        private void ToolTip_RemoveText(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text == textBox.ToolTip.ToString())
            {
                textBox.Text = "";
            }
        }

        private void ToolTip_AddText(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                textBox.Text = textBox.ToolTip.ToString();
            }
        }
       
    }
}
