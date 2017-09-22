using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ZLIS.SkinBuilder
{
    public partial class MonthView : DayView
    {
        #region Members 

        private ImageObject headerImageObject = new ImageObject();
        private Color lineColor = Color.FromArgb(93, 140, 201);
        private Color currentDayLineColor = Color.FromArgb(238, 147, 17);
        private Color selectedDayBackgroundColor = Color.FromArgb(230, 237, 247);

        private int daysOfPage = 35;
        private int daysOfMonth = 31;
        private int linesOfPage = 5;

        private DateTime firstDateOfPage = DateTime.Now;
        private DateTime lastDateOfPage = DateTime.Now;

        private DateTime selectedDate = DateTime.MinValue;

        private DayOfWeek[] dsysOfWeek = new DayOfWeek[]
        {
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday
        };

        #endregion 

        #region Properties

        public DateTime FirstDateOfPage 
        {
            get { return this.firstDateOfPage; }
        }

        public DateTime LastDateOfPage
        {
            get { return this.lastDateOfPage; }
        }

        public ImageObject HeaderImageObject 
        {
            get { return this.headerImageObject; }
            set 
            {
                this.headerImageObject = value;
                this.Invalidate();
            }
        }

        public Color GridLineColor
        {
            get { return this.lineColor; }
            set 
            {
                this.lineColor = value;
                this.Invalidate();
            }
        }

        public Color CurrentDayGridLineColor
        {
            get { return this.currentDayLineColor; }
            set
            {
                this.currentDayLineColor = value;
                this.Invalidate();
            }
        }

        public override DateTime StartDate
        {
            get
            {
                return base.StartDate;
            }
            set
            {
                base.StartDate = value;
                this.GetDaysOfMonth(value.Year, value.Month);
                this.GetDaysOnePage();
                this.GetLinesOnePage();
            }
        }
        #endregion 

        public MonthView()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.UserMouse, true);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            base.scrollbar.Visible = false;
            base.hourLabelWidth = 22;
        }

        #region Override Base Methods

        /// <summary>
        /// Draw Left Header
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        protected override void DrawHourLabels(PaintEventArgs e, Rectangle rect)
        {
            int linesOfPage = this.linesOfPage;
            int lineHeight = rect.Height / linesOfPage - this.dayHeadersHeight;
            Rectangle leftRect = rect;
            leftRect.Y += this.dayHeadersHeight;
            leftRect.Width = this.hourLabelWidth;
            leftRect.Height = lineHeight;
            StringFormat strFmt = new StringFormat();
            strFmt.Alignment = StringAlignment.Center;
            strFmt.LineAlignment = StringAlignment.Center;
            strFmt.Trimming = StringTrimming.EllipsisCharacter;
            strFmt.FormatFlags = StringFormatFlags.DirectionVertical;
            for (int i = 0; i < linesOfPage; i++)
            {
                DrawLeftRectBackground(e.Graphics, leftRect);

                DateTime date1 = this.firstDateOfPage.AddDays(i * 7);
                DateTime date2 = this.firstDateOfPage.AddDays((i + 1) * 7 - 1);
                string text = string.Format("{0} - {1}", string.Format("{0}/{1}", date1.Month, date1.Day), string.Format("{0}/{1}", date2.Month, date2.Day));
                e.Graphics.DrawString(text, Renderer.BaseFont, new SolidBrush(this.ForeColor), leftRect, strFmt);
                leftRect.Y += lineHeight;
                leftRect.Y += this.dayHeadersHeight;
            }
        }

        /// <summary>
        /// Draw Days
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        protected override void DrawDays(PaintEventArgs e, Rectangle rect)
        {
            Rectangle dayRect = rect;
            dayRect.Width = rect.Width / 7;
            dayRect.Height = rect.Height / this.linesOfPage;
            DateTime date = this.firstDateOfPage;
            for (int i = 0; i < this.daysOfPage; i++)
            {
                Rectangle headerRect = dayRect;
                headerRect.Height = this.dayHeadersHeight;
                this.DrawDayHeader(e.Graphics, headerRect, date);

                Rectangle contentRect = dayRect;
                contentRect.Y += this.dayHeadersHeight;
                contentRect.Height = dayRect.Height - headerRect.Height;
                this.DrawDayContent(e.Graphics, contentRect, date);

                if ((i + 1) % 7 == 0)
                {
                    dayRect.X = rect.Left;
                    dayRect.Y += dayRect.Height;
                }
                else
                {
                    dayRect.X += dayRect.Width;
                }

                date = date.AddDays(1);
            }
        }

        /// <summary>
        /// Draw Top Header
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        protected override void DrawDayHeaders(PaintEventArgs e, Rectangle rect)
        {
        //    base.DrawDayHeaders(e, rect);

            int dayWidth = rect.Width / 7;
            Rectangle textRect = rect;
            textRect.Width = dayWidth;
            StringFormat strFmt = new StringFormat();
            strFmt.Alignment = StringAlignment.Center;
            strFmt.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < 7; i++)
            {
                DayOfWeek dayOfWeek = dsysOfWeek[i];
                if (this.firstDayOfWeek == DayOfWeek.Monday)
                {
                    if (i == 6)
                        dayOfWeek = dsysOfWeek[0];
                    else
                        dayOfWeek = dsysOfWeek[i + 1];
                }

                string sTodaysName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek);
                if (rect.Width < 105)
                    sTodaysName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(dayOfWeek);

                e.Graphics.DrawString(sTodaysName, Renderer.BaseFont, new SolidBrush(this.ForeColor), textRect, strFmt);

                textRect.X += dayWidth;
            }
        }

        #endregion

        #region Mouse Events 

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            DateTime currentSelectedDate = this.selectedDate;
            this.GetSelectedDate(e.X, e.Y);
            if (currentSelectedDate != this.selectedDate)
                this.Invalidate();
        }

        #endregion

        #region Help Methods

        private void DrawLeftRectBackground(Graphics g, Rectangle rect)
        {
            using (SolidBrush brush = new SolidBrush(this.BackColor))
                g.FillRectangle(brush, rect);

            using (Pen aPen = new Pen(Color.FromArgb(205, 219, 238)))
                g.DrawLine(aPen, rect.Left, rect.Top + (int)rect.Height / 2, rect.Right, rect.Top + (int)rect.Height / 2);

            using (Pen aPen = new Pen(Color.FromArgb(141, 174, 217)))
                g.DrawRectangle(aPen, rect);

            Rectangle leftPart = new Rectangle(rect.Left + 1, rect.Top + 1, (int)(rect.Width / 2) - 1, rect.Height -2);
            Rectangle rightPart = new Rectangle(rect.Left + (int)(rect.Width / 2) - 1, rect.Top + 1, rect.Width / 2 - 1, rect.Height - 2);

            using (LinearGradientBrush aGB = new LinearGradientBrush(leftPart, Color.FromArgb(228, 236, 246), Color.FromArgb(214, 226, 241), LinearGradientMode.Horizontal))
                g.FillRectangle(aGB, leftPart);

            using (LinearGradientBrush aGB = new LinearGradientBrush(rightPart, Color.FromArgb(194, 212, 235), Color.FromArgb(208, 222, 239), LinearGradientMode.Horizontal))
                g.FillRectangle(aGB, rightPart);
        }

        private void DrawDayHeader(Graphics g, Rectangle rect, DateTime date)
        {
            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Center;
            m_Format.FormatFlags = StringFormatFlags.NoWrap;
            m_Format.LineAlignment = StringAlignment.Center;

            StringFormat m_Formatdd = new StringFormat();
            m_Formatdd.Alignment = StringAlignment.Near;
            m_Formatdd.FormatFlags = StringFormatFlags.NoWrap;
            m_Formatdd.LineAlignment = StringAlignment.Center;

            using (SolidBrush brush = new SolidBrush(Renderer.BackColor))
                g.FillRectangle(brush, rect);

            using (Pen aPen = new Pen(Renderer.BaseRectColor))
                g.DrawRectangle(aPen, rect);

            Rectangle topPart = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, (int)(rect.Height / 2) - 1);
            Rectangle lowPart = new Rectangle(rect.Left + 1, rect.Top + (int)(rect.Height / 2), rect.Width - 1, (int)(rect.Height / 2));

            using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Renderer.HeaderStartColor, Renderer.HeaderMiddleColor1, LinearGradientMode.Vertical))
                g.FillRectangle(aGB, topPart);

            using (LinearGradientBrush aGB = new LinearGradientBrush(lowPart, Renderer.HeaderMiddleColor2, Renderer.HeaderEndColor, LinearGradientMode.Vertical))
                g.FillRectangle(aGB, lowPart);

            if (date.Date.Equals(this.StartDate))
            {
                topPart.Inflate((int)(-topPart.Width / 4 + 1), 1); //top left orange area
                topPart.Offset(rect.Left - topPart.Left + 1, 1);
                topPart.Inflate(1, 0);
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(247, 207, 114), Color.FromArgb(251, 230, 148), LinearGradientMode.Horizontal))
                {
                    topPart.Inflate(-1, 0);
                    g.FillRectangle(aGB, topPart);
                }

                topPart.Offset(rect.Right - topPart.Right, 0);        //top right orange
                topPart.Inflate(1, 0);
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(251, 230, 148), Color.FromArgb(247, 207, 114), LinearGradientMode.Horizontal))
                {
                    topPart.Inflate(-1, 0);
                    g.FillRectangle(aGB, topPart);
                }

                using (Pen aPen = new Pen(Color.FromArgb(128, 240, 154, 30))) //center line
                    g.DrawLine(aPen, rect.Left, topPart.Bottom - 1, rect.Right, topPart.Bottom - 1);

                topPart.Inflate(0, -1);
                topPart.Offset(0, topPart.Height + 1); //lower right
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(240, 157, 33), Color.FromArgb(250, 226, 142), LinearGradientMode.BackwardDiagonal))
                    g.FillRectangle(aGB, topPart);

                topPart.Offset(rect.Left - topPart.Left + 1, 0); //lower left
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(240, 157, 33), Color.FromArgb(250, 226, 142), LinearGradientMode.ForwardDiagonal))
                    g.FillRectangle(aGB, topPart);
                using (Pen aPen = new Pen(Color.FromArgb(238, 147, 17)))
                    g.DrawRectangle(aPen, rect);
            }

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            //get short dayabbr. if narrow dayrect
            string sTodaysName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
            if (rect.Width < 105)
                sTodaysName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(date.DayOfWeek);

            rect.Offset(2, 1);

            using (Font fntDayDate = new Font("Segoe UI", 9, FontStyle.Bold))
                g.DrawString(date.ToString("M/d"), fntDayDate, SystemBrushes.WindowText, rect, m_Formatdd);
        }

        private void DrawDayContent(Graphics g, Rectangle rect, DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(this.StartDate.Year, this.StartDate.Month, 1);
            DateTime lastDayOfMonth = new DateTime(this.StartDate.Year, this.StartDate.Month, this.daysOfMonth);

            if (date >= firstDayOfMonth && date <= lastDayOfMonth)
            {
             //   if (date.Date.DayOfWeek != DayOfWeek.Sunday &&
             //       date.Date.DayOfWeek != DayOfWeek.Saturday)
                    g.FillRectangle(Brushes.White, rect);
            }
            else
                g.FillRectangle(new SolidBrush(Color.FromArgb(209, 209, 209)), rect);

            if (date == this.selectedDate)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(156, 230, 237, 247)), rect);
            }

            Renderer.DrawDayGripper(g, rect, 2, date == this.StartDate);

            if (cachedAppointments.ContainsKey(date.ToString(dateTimeKeyFormat)))
            {
                AppointmentList list = (AppointmentList)cachedAppointments[date.ToString(dateTimeKeyFormat)];
                string content = string.Empty;
                Color textColor = this.ForeColor;
                foreach (Appointment app in list)
                {
                    content += app.Title;
                    content += "\n";
                    textColor = app.TextColor;
                }

                StringFormat strFmt = new StringFormat();
                strFmt.Alignment = StringAlignment.Near;
                strFmt.LineAlignment = StringAlignment.Near;
                strFmt.Trimming = StringTrimming.Character;
                g.DrawString(content, Renderer.BaseFont, new SolidBrush(textColor), rect, strFmt);
            }
        }

        private void GetDaysOnePage()
        {
            DateTime currentMonth = this.StartDate;
            int daysOfMonth = this.daysOfMonth;

            DateTime firstDate = currentMonth;
            firstDate = firstDate.AddDays(-(firstDate.Day - 1));
            DateTime lastDate = currentMonth;
            lastDate = lastDate.AddDays(daysOfMonth - lastDate.Day);

            this.daysOfPage = daysOfMonth;
            if (this.firstDayOfWeek == DayOfWeek.Sunday)
            {
                this.firstDateOfPage = firstDate.AddDays(-(firstDate.DayOfWeek - DayOfWeek.Sunday));
                this.lastDateOfPage = lastDate.AddDays(DayOfWeek.Saturday - lastDate.DayOfWeek);

                this.daysOfPage += firstDate.DayOfWeek - DayOfWeek.Sunday;
                this.daysOfPage += DayOfWeek.Saturday - lastDate.DayOfWeek;
            }
            else
            {
                this.firstDateOfPage = firstDate.AddDays(-(firstDate.DayOfWeek - DayOfWeek.Monday));
                this.lastDateOfPage = lastDate.AddDays(DayOfWeek.Saturday + 1 - lastDate.DayOfWeek);

                this.daysOfPage += firstDate.DayOfWeek - DayOfWeek.Monday;
                this.daysOfPage += DayOfWeek.Saturday + 1 - lastDate.DayOfWeek;
            }

            this.firstDateToShow = this.firstDateOfPage;
            this.DaysToShow = this.daysOfPage;
        }

        private void GetLinesOnePage()
        {
            this.linesOfPage = this.daysOfPage / 7;
            if (this.daysOfPage % 7 != 0)
                this.linesOfPage++;
        }

        private void GetDaysOfMonth(int year, int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    this.daysOfMonth = 31;
                    break;
                case 2:
                    {
                        if (year % 4 == 0)
                        {
                            this.daysOfMonth = 29;
                            break;
                        }
                        else
                        {
                            this.daysOfMonth = 28;
                            break;
                        }
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    this.daysOfMonth = 30;
                    break;
            }
        }

        private void GetSelectedDate(int x, int y)
        {
            Point pt = new Point(x, y);

            Rectangle dayRect = new Rectangle(this.hourLabelWidth, this.dayHeadersHeight, this.ClientRectangle.Width - this.hourLabelWidth, this.ClientRectangle.Height - this.dayHeadersHeight);
            dayRect.Width = dayRect.Width / 7;
            dayRect.Height = dayRect.Height / this.linesOfPage;
            DateTime date = this.firstDateOfPage;
            for (int i = 0; i < this.daysOfPage; i++)
            {
                if (dayRect.Contains(pt))
                {
                    this.selectedDate = date;
                    break;
                }

                if ((i + 1) % 7 == 0)
                {
                    dayRect.X = this.hourLabelWidth;
                    dayRect.Y += dayRect.Height;
                }
                else
                {
                    dayRect.X += dayRect.Width;
                }

                date = date.AddDays(1);
            }
        }

        #endregion
    }
}
