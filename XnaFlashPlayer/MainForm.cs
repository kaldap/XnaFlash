﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XnaFlashPlayer
{
    public partial class MainForm : Form
    {
        private FormWindowState puvodniStav = FormWindowState.Normal;
        private Rectangle puvodniOblast;

        public MainForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Player_Play;
            this.puvodniOblast = Bounds;
            this.středníKvalitaToolStripMenuItem.PerformClick();
            this.opakovatToolStripMenuItem.PerformClick();
            this.přehráváníToolStripMenuItem.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int msaa = flashPlayer.GraphicsDevice.PresentationParameters.MultiSampleCount;
            středníKvalitaToolStripMenuItem.Enabled = msaa >= 4;
            vysokáKvalitaToolStripMenuItem.Enabled = msaa >= 8;
            nejvyššíKvalitaToolStripMenuItem.Enabled = msaa >= 16;
        }
        private void NastavCelouObrazovku(bool celaObrazovka)
        {
            if (celaObrazovka)
            {
                if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None)
                {
                    this.puvodniOblast = Bounds;
                    this.puvodniStav = this.WindowState;
                    this.TopMost = true;
                    this.mainMenu.Visible = false;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;                    
                }
            }
            else if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.TopMost = false;
                this.mainMenu.Visible = true;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = this.puvodniStav;
                this.Bounds = this.puvodniOblast;
            }
        }

        private void otevřítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openSwf.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            přehráváníToolStripMenuItem.Enabled = flashPlayer.Open(openSwf.FileName);
        }
        private void zavřítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.Close();
            přehráváníToolStripMenuItem.Enabled = false;
        }
        private void uložitSnímekObrazovkyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveJpeg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            flashPlayer.Snapshot(saveJpeg.FileName);
        }
        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void celáObrazovkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            celáObrazovkaToolStripMenuItem.Checked = !celáObrazovkaToolStripMenuItem.Checked;
            NastavCelouObrazovku(celáObrazovkaToolStripMenuItem.Checked);
        }
        private void nízkáKvalitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.SetQuality(0);
            nízkáKvalitaToolStripMenuItem.Checked = true;
            středníKvalitaToolStripMenuItem.Checked = false;
            vysokáKvalitaToolStripMenuItem.Checked = false;
            nejvyššíKvalitaToolStripMenuItem.Checked = false;
        }
        private void středníKvalitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.SetQuality(1);
            nízkáKvalitaToolStripMenuItem.Checked = false;
            středníKvalitaToolStripMenuItem.Checked = true;
            vysokáKvalitaToolStripMenuItem.Checked = false;
            nejvyššíKvalitaToolStripMenuItem.Checked = false;
        }
        private void vysokáKvalitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.SetQuality(2);
            nízkáKvalitaToolStripMenuItem.Checked = false;
            středníKvalitaToolStripMenuItem.Checked = false;
            vysokáKvalitaToolStripMenuItem.Checked = true;
            nejvyššíKvalitaToolStripMenuItem.Checked = false;
        }
        private void nejvyššíKvalitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.SetQuality(3);
            nízkáKvalitaToolStripMenuItem.Checked = false;
            středníKvalitaToolStripMenuItem.Checked = false;
            vysokáKvalitaToolStripMenuItem.Checked = false;
            nejvyššíKvalitaToolStripMenuItem.Checked = true;
        }

        private void pozastavitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pozastavitToolStripMenuItem.Checked = !pozastavitToolStripMenuItem.Checked;
            flashPlayer.Pause(pozastavitToolStripMenuItem.Checked);
        }
        private void návratToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.Rewind();
        }
        private void předchozíSnímekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.PrevFrame();
        }
        private void dalšíSnímekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flashPlayer.NextFrame();
        }
        private void opakovatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opakovatToolStripMenuItem.Checked = !opakovatToolStripMenuItem.Checked;
            flashPlayer.SetLooping(opakovatToolStripMenuItem.Checked);
        }
    }
}
