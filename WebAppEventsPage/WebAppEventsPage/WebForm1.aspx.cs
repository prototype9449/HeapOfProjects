using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEventsPage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Write(drowList.Items[drowList.SelectedIndex].Text);
            myListBox.ClearSelection();
        }

        protected void OnClick(object sender, EventArgs e)
        {
            bulletList.Click += bulletList_Click;
            bulletList.SelectedIndexChanged += bulletList_SelectedIndexChanged;
            panel.Controls.Add(new Button(){Text = "hi there", Width = 100});
            //Response.Redirect("WebForm2.aspx");
            var table = new Table();
            for (int j = 0; j < 10; j++)
            {
                var row = new TableRow();
                for (int i = 0; i < 10; i++)
                {
                    row.Cells.Add(new TableCell {Text = i.ToString()});
                }
                table.Rows.Add(row);
            }
            Place.Controls.Add(table);

            this.FindControl("btn").Visible = false;
        }

        void bulletList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }

        void bulletList_Click(object sender, BulletedListEventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }
    }
}