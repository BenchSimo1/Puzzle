
using System.Collections.Generic;
using System.Drawing;

namespace Puzzle
{
    partial class PuzzleForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupCells = new System.Windows.Forms.GroupBox();
            this.timerLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.labelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBindingSource
            // 
            this.labelBindingSource.DataSource = typeof(System.Windows.Forms.Label);
            // 
            // groupCells
            // 
            this.groupCells.Location = new System.Drawing.Point(37, 35);
            this.groupCells.Name = "groupCells";
            this.groupCells.Size = new System.Drawing.Size(387, 328);
            this.groupCells.TabIndex = 9;
            this.groupCells.TabStop = false;
            this.groupCells.Text = "puzzleBox";
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.Location = new System.Drawing.Point(430, 188);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(0, 49);
            this.timerLabel.TabIndex = 11;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 900;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PuzzleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.groupCells);
            this.Name = "PuzzleForm";
            this.Text = "Puzzle";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PuzzleForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.labelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource labelBindingSource;
        private System.Windows.Forms.GroupBox groupCells;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Timer timer1;
    }
}

