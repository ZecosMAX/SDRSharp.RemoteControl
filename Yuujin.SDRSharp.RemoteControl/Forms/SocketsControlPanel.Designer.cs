namespace Yuujin.SDRSharp.RemoteControl.Forms
{
    partial class SocketsControlPanel
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SocketsControlPanel));
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            statusLabel = new Label();
            startServerButton = new Button();
            listenStartButton = new Button();
            stopServerButton = new Button();
            listenStopButton = new Button();
            checkBox1 = new CheckBox();
            connectedClientsLabel = new Label();
            panel1 = new Panel();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 1;
            label1.Text = "Listening Endpoint";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 29);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 2;
            label2.Text = "Address";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(62, 26);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(170, 23);
            textBox1.TabIndex = 4;
            textBox1.Text = "127.0.0.1:1604";
            toolTip1.SetToolTip(textBox1, resources.GetString("textBox1.ToolTip"));
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(120, 3);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 5;
            label3.Text = "Raw Sockets Settings";
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusLabel.BorderStyle = BorderStyle.Fixed3D;
            statusLabel.Location = new Point(6, 57);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(226, 23);
            statusLabel.TabIndex = 6;
            statusLabel.Text = "Status: ";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // startServerButton
            // 
            startServerButton.Location = new Point(7, 88);
            startServerButton.Name = "startServerButton";
            startServerButton.Size = new Size(110, 23);
            startServerButton.TabIndex = 7;
            startServerButton.Text = "Start Server";
            startServerButton.UseVisualStyleBackColor = true;
            startServerButton.Click += startServerButton_Click;
            // 
            // listenStartButton
            // 
            listenStartButton.Location = new Point(7, 117);
            listenStartButton.Name = "listenStartButton";
            listenStartButton.Size = new Size(110, 23);
            listenStartButton.TabIndex = 8;
            listenStartButton.Text = "Start Listeining";
            listenStartButton.UseVisualStyleBackColor = true;
            // 
            // stopServerButton
            // 
            stopServerButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            stopServerButton.Location = new Point(122, 88);
            stopServerButton.Name = "stopServerButton";
            stopServerButton.Size = new Size(110, 23);
            stopServerButton.TabIndex = 9;
            stopServerButton.Text = "Stop Server";
            stopServerButton.UseVisualStyleBackColor = true;
            // 
            // listenStopButton
            // 
            listenStopButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listenStopButton.Location = new Point(122, 117);
            listenStopButton.Name = "listenStopButton";
            listenStopButton.Size = new Size(110, 23);
            listenStopButton.TabIndex = 10;
            listenStopButton.Text = "Stop Listening";
            listenStopButton.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBox1.AutoSize = true;
            checkBox1.CheckAlign = ContentAlignment.MiddleRight;
            checkBox1.Location = new Point(77, 146);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(155, 19);
            checkBox1.TabIndex = 11;
            checkBox1.Text = "Notify connected clients";
            toolTip1.SetToolTip(checkBox1, "Causes server to send the data to all connected clients about any \r\nparameter change (e.g. Frequency, Bandwidth, Modulation, etc...)\r\n");
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // connectedClientsLabel
            // 
            connectedClientsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            connectedClientsLabel.AutoSize = true;
            connectedClientsLabel.BorderStyle = BorderStyle.FixedSingle;
            connectedClientsLabel.Location = new Point(128, 176);
            connectedClientsLabel.Name = "connectedClientsLabel";
            connectedClientsLabel.Size = new Size(104, 17);
            connectedClientsLabel.TabIndex = 12;
            connectedClientsLabel.Text = "Connected clients";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Location = new Point(7, 188);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 105);
            panel1.TabIndex = 13;
            // 
            // SocketsControlPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(connectedClientsLabel);
            Controls.Add(panel1);
            Controls.Add(checkBox1);
            Controls.Add(listenStopButton);
            Controls.Add(stopServerButton);
            Controls.Add(listenStartButton);
            Controls.Add(startServerButton);
            Controls.Add(statusLabel);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            MinimumSize = new Size(240, 300);
            Name = "SocketsControlPanel";
            Size = new Size(240, 300);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private Label statusLabel;
        private Button startServerButton;
        private Button listenStartButton;
        private Button stopServerButton;
        private Button listenStopButton;
        private CheckBox checkBox1;
        private Label connectedClientsLabel;
        private Panel panel1;
        private ToolTip toolTip1;
    }
}
