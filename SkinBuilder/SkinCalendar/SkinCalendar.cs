using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinCalendar : UserControl
    {
        #region Members 

        private SkinCalendarStyle style = SkinCalendarStyle.Week;
        private MonthView monthView = null;
        private DayView weakView = null;

        #endregion Members 

        #region Properties 

        public DayView DayView 
        {
            get { return this.dayView; }
            set { this.dayView = value; }
        }

        public DayView WeekView
        {
            get
            {
                this.CreateWeekView();
                return this.weakView; 
            }
            set { this.weakView = value; }
        }

        public MonthView MonthView
        {
            get 
            {
                this.CreateMonthView();
                return this.monthView; 
            }
            set { this.monthView = value; }
        }

        public SkinCalendarStyle CalendarStyle
        {
            set
            {
                this.style = value;
                switch (this.style)
                {
                    case SkinCalendarStyle.Day:
                        this.ShowDayView();
                        break;
                    case SkinCalendarStyle.Week:
                        this.ShowWeakView();
                        break;
                    case SkinCalendarStyle.Month:
                        this.ShowMonthView();
                        break;
                }
            }
            get { return this.style; }
        }

        #endregion 

        public SkinCalendar()
        {
            InitializeComponent();

            this.style = SkinCalendarStyle.Week;
            this.dayView.StartDate = DateTime.Now;
        }

        public void Clear()
        {
            this.DayView.Clear();
            this.monthView.Clear();
            this.weakView.Clear();
        }

        #region Private Method 

        private void ShowMonthView()
        {
            this.CreateMonthView();

            this.Controls.Clear();
            this.Controls.Add(this.monthView);
            this.monthView.Visible = true;
        }

        private void ShowWeakView()
        {
            this.CreateWeekView();

            this.Controls.Clear();
            this.Controls.Add(this.weakView);
            this.weakView.Visible = true;
        }

        private void ShowDayView()
        {
            this.Controls.Clear();
            this.Controls.Add(this.dayView);
            this.dayView.Visible = true;
        }

        private void CreateMonthView()
        {
            if (this.monthView == null)
            {
                this.monthView = new MonthView();
                this.monthView.Dock = DockStyle.Fill;
                this.monthView.Font = this.Font;
                this.monthView.Name = "MonthView";
                this.monthView.Font = this.Font;
                this.monthView.Parent = this;
                this.monthView.StartDate = DateTime.Now;
            }
        }

        private void CreateWeekView()
        {
            if (this.weakView == null)
            {
                this.weakView = new DayView();

                DrawTool drawTool1 = new ZLIS.SkinBuilder.DrawTool();
                drawTool1.DayView = this.weakView;
                this.weakView.ActiveTool = drawTool1;
                this.weakView.Dock = System.Windows.Forms.DockStyle.Fill;
                this.weakView.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.weakView.Location = new System.Drawing.Point(0, 0);
                this.weakView.Name = "weakView";
                this.weakView.SelectionEnd = new System.DateTime(((long)(0)));
                this.weakView.SelectionStart = new System.DateTime(((long)(0)));
                this.weakView.Size = new System.Drawing.Size(383, 349);
                this.weakView.StartDate = new System.DateTime(((long)(0)));
                this.weakView.TabIndex = 0;
                this.weakView.Text = "weakView";
                this.weakView.DaysToShow = 7;
                this.WeekView.StartDate = DateTime.Now;
            }
        }

        #endregion
    }

    public enum SkinCalendarStyle
    {
        Day,
        Week,
        Month
    }
}
