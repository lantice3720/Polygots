using System;
using System.Drawing;
using System.Windows.Forms;
using Polygots.Properties;

namespace Polygots
{
    partial class PolygotForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void texture_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Resources.RectPng, 0, 0, this.Width, this.Height);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var Random = new Random();
            this.Location = new Point(Random.Next(0, Screen.PrimaryScreen.Bounds.Width - this.Width), Random.Next(0, Screen.PrimaryScreen.Bounds.Height - this.Height));
            this.Size = new Size(64, 64);
            this.texture.Paint += texture_Paint;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Size = new Size(96, 96);
            Console.WriteLine("Mouse Down");
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.texture = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // texture
            // 
            this.texture.BackColor = System.Drawing.Color.Transparent;
            this.texture.Location = new System.Drawing.Point(0, 0);
            this.texture.Name = "texture";
            this.texture.Size = new System.Drawing.Size(64, 64);
            this.texture.TabIndex = 0;
            // 
            // PolygotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(64, 64);
            this.Controls.Add(this.texture);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PolygotForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Polygot";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.ResumeLayout(false);

        }

        #endregion

        private Panel texture;
    }
}

