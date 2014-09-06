using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chummer
{
    public partial class SmallSkillControl : UserControl
    {
        /// <summary>
        /// Fired when the user decides to roll the dice
        /// </summary>
        public event EventHandler DiceClick
        {
            add { this.cmdRoll.Click += value; }
            remove { this.cmdRoll.Click -= value; }
        }

        /// <summary>
        /// The skill information
        /// </summary>
        public Skill Skill 
        {
            get { return this._skill; }
            set
            {
                this._skill = value;
                this.lblSkillName.Text = this.Skill.Name + " : " + this.Skill.TotalRating.ToString();
            }
        }
        private Skill _skill;
        private Form _parent;

        public SmallSkillControl(Form parent)
        {
            this._parent = parent;
            InitializeComponent();
            // setup controls
        }

        private void cmdRoll_Click(object sender, EventArgs e)
        {
            // pass the appropriate information onto the dice roller
            if (this._parent is frmGMDashboard)
            {
                frmGMDashboard dash = this._parent as frmGMDashboard;
                dash.DiceRoller.NumberOfDice = this.chkUseSpecial.Checked ? Skill.TotalRating + 2 : Skill.TotalRating;
                // apply appropriate limit here
                dash.DiceRoller.EdgeUse = DiceRollerControl.EdgeUses.None;
                dash.DiceRoller.NumberOfEdge = Convert.ToInt32(((Attribute)dash.CurrentNPC.EDG).TotalValue);
            }
            else
            {
                // we have the individual player's skill's
                frmPlayerDashboard dash = this._parent as frmPlayerDashboard;
                dash.DiceRoller.NumberOfDice = this.chkUseSpecial.Checked ? Skill.TotalRating + 2 : Skill.TotalRating;
                dash.DiceRoller.EdgeUse = DiceRollerControl.EdgeUses.None;
                dash.DiceRoller.NumberOfEdge = Convert.ToInt32(dash.CurrentNPC.EDG);
            }
        }
    }
}
