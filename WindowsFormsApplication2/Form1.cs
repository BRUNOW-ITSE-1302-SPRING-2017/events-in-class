using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class LinkObject
    {
        public string Link { get; set; }
        public string Description { get; set; }
    }

    public partial class Form1 : Form
    {
        List<LinkObject> linkList = new List<LinkObject>();

        public Form1()
        {
            InitializeComponent();

            var link1 = new LinkObject();
            link1.Link = "https://apple.com";
            link1.Description = "The Apple website";

            var link2 = new LinkObject();
            link2.Link = "https://techmill.co";
            link2.Description = "The TechMill website";

            var link3 = new LinkObject();
            link3.Link = "http://stokedenton.com";
            link3.Description = "The Stoke Denton website";

            linkList.Add(link1);
            linkList.Add(link2);
            linkList.Add(link3);
            
            foreach (Control thisControl in this.Controls)
            {
                if (thisControl.GetType() == typeof(Label))
                {
                    thisControl.MouseEnter += new EventHandler(handleMouseEnter);
                    thisControl.MouseLeave += new EventHandler(handleMouseLeave);
                }
                else if (thisControl.GetType() == typeof(LinkLabel))
                {
                    var linklabel = (LinkLabel)thisControl;
                    linklabel.LinkClicked += new LinkLabelLinkClickedEventHandler(handleLinkClicked);
                    linklabel.MouseEnter += new EventHandler(handleMouseEnterLinkLabel);
                    linklabel.MouseLeave += new EventHandler(handleMouseLeaveLinkLabel);

                }
            }

            setupLinkLabels();
        }

        private void handleLinkClicked(object sender, EventArgs e)
        {
            var linkLabel = (LinkLabel)sender;
            var url = linkLabel.Text;
            linkLabel.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(url);

            LinkObject linkObjectToRemove = new LinkObject();

            foreach (var link in linkList)
            {
                if (link.Link == url)
                {
                    linkObjectToRemove = link;

                    break;
                }
            }
            linkList.Remove(linkObjectToRemove);
            linkList.Insert(0, linkObjectToRemove);

            setupLinkLabels();
        }

        private void handleMouseEnterLinkLabel(object sender, EventArgs e)
        {
            var linkLabel = (LinkLabel)sender;
            var url = linkLabel.Text;
            LinkObject linkObjectToShow = new LinkObject();

            foreach (var link in linkList)
            {
                if (link.Link == url)
                {
                    linkObjectToShow = link;

                    break;
                }
            }
            linkDescriptionLabel.Text = linkObjectToShow.Description;
        }

        private void handleMouseLeaveLinkLabel(object sender, EventArgs e)
        {
            linkDescriptionLabel.Text = "";
        }

        private void setupLinkLabels()
        {
            linkLabel1.Text = linkList[0].Link;
            linkLabel2.Text = linkList[1].Link;
            linkLabel3.Text = linkList[2].Link;
        }

        private void handleMouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.White;
        }

        private void handleMouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.Black;
        }
    }
}
