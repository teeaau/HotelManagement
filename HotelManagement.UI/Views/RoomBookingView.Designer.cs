
namespace HotelManagement.UI.Views
{
    partial class RoomBookingView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRoom = new System.Windows.Forms.Label();
            this.btnBook = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRoom
            // 
            this.lblRoom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblRoom.Location = new System.Drawing.Point(0, 136);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(187, 40);
            this.lblRoom.TabIndex = 2;
            this.lblRoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBook
            // 
            this.btnBook.BackgroundImage = global::HotelManagement.UI.Properties.Resources.images__1_;
            this.btnBook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBook.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.btnBook.FlatAppearance.BorderSize = 10;
            this.btnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBook.Location = new System.Drawing.Point(0, 0);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(187, 136);
            this.btnBook.TabIndex = 3;
            this.btnBook.UseVisualStyleBackColor = true;
            // 
            // RoomBookingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(191)))), ((int)(((byte)(159)))));
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.lblRoom);
            this.Name = "RoomBookingView";
            this.Size = new System.Drawing.Size(187, 176);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Button btnBook;
    }
}
