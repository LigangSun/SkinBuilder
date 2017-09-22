/* Developed by Ertan Tike (ertan.tike@moreum.com) */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZLIS.SkinBuilder
{
    public class DayView : Control
    {
        #region Variables

        protected TextBox editbox;
        protected VScrollBar scrollbar;
        protected DrawTool drawTool;
        protected SelectionTool selectionTool;
        protected int allDayEventsHeaderHeight = 20;

        protected DateTime workStart;
        protected DateTime workEnd;

        protected DayOfWeek firstDayOfWeek = DayOfWeek.Sunday;

        #endregion

        #region Constants

        protected int hourLabelWidth = 50;
        protected int hourLabelIndent = 2;
        protected int dayHeadersHeight = 20;
        protected int appointmentGripWidth = 5;
        protected int horizontalAppointmentHeight = 20;

        protected const string dateTimeKeyFormat = "M/d";

        #endregion

        #region c.tor

        public DayView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.UserMouse, true);

            scrollbar = new VScrollBar();
            scrollbar.SmallChange = halfHourHeight;
            scrollbar.LargeChange = halfHourHeight * 2;
            scrollbar.Dock = DockStyle.Right;
            scrollbar.Visible = allowScroll;
            scrollbar.Scroll += new ScrollEventHandler(scrollbar_Scroll);
            AdjustScrollbar();
            scrollbar.Value = (startHour * 2 * halfHourHeight);

            this.Controls.Add(scrollbar);

            editbox = new TextBox();
            editbox.Multiline = true;
            editbox.Visible = false;
            editbox.BorderStyle = BorderStyle.None;
            editbox.KeyUp += new KeyEventHandler(editbox_KeyUp);
            editbox.Margin = Padding.Empty;

            this.Controls.Add(editbox);

            drawTool = new DrawTool();
            drawTool.DayView = this;

            selectionTool = new SelectionTool();
            selectionTool.DayView = this;
            selectionTool.Complete += new EventHandler(selectionTool_Complete);

            activeTool = drawTool;

            UpdateWorkingHours();

            this.Renderer = new Office12Renderer();
        }

        #endregion

        #region Properties

        public DayOfWeek FirstDayOfWeek 
        {
            get { return this.firstDayOfWeek; }
            set
            {
                if (value != DayOfWeek.Sunday || value != DayOfWeek.Monday)
                    throw new Exception("The first day of the week can only be Sunday or Monday!");

                this.firstDayOfWeek = value;
                this.Invalidate();
            }
        }

        protected int halfHourHeight = 18;

        [System.ComponentModel.DefaultValue(18)]
        public int HalfHourHeight
        {
            get
            {
                return halfHourHeight;
            }
            set
            {
                halfHourHeight = value;
                OnHalfHourHeightChanged();
            }
        }

        private void OnHalfHourHeightChanged()
        {

            AdjustScrollbar();
            Invalidate();
        }

        private AbstractRenderer renderer;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public AbstractRenderer Renderer
        {
            get
            {
                return renderer;
            }
            set
            {
                renderer = value;
                OnRendererChanged();
            }
        }

        private void OnRendererChanged()
        {
            this.Font = renderer.BaseFont;
            this.Invalidate();
        }

        private int daysToShow = 1;

        [System.ComponentModel.DefaultValue(1)]
        public int DaysToShow
        {
            get
            {
                return daysToShow;
            }
            set
            {
                daysToShow = value;
                OnDaysToShowChanged();
            }
        }

        protected virtual void OnDaysToShowChanged()
        {
            if (this.CurrentlyEditing)
                FinishEditing(true);

            Invalidate();
        }

        private SelectionType selection;

        [System.ComponentModel.Browsable(false)]
        public SelectionType Selection
        {
            get
            {
                return selection;
            }
        }

        private DateTime startDate;

        public virtual DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnStartDateChanged();
            }
        }

        protected DateTime firstDateToShow = DateTime.MinValue;

        protected virtual void OnStartDateChanged()
        {
            startDate = startDate.Date;

            selectedAppointment = null;
            selectedAppointmentIsNew = false;
            selection = SelectionType.DateRange;

            Invalidate();
        }

        private int startHour = 8;

        [System.ComponentModel.DefaultValue(8)]
        public int StartHour
        {
            get
            {
                return startHour;
            }
            set
            {
                startHour = value;
                OnStartHourChanged();
            }
        }

        protected virtual void OnStartHourChanged()
        {
            scrollbar.Value = (startHour * 2 * halfHourHeight);

            Invalidate();
        }

        private Appointment selectedAppointment;

        [System.ComponentModel.Browsable(false)]
        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
        }

        private DateTime selectionStart;

        public DateTime SelectionStart
        {
            get { return selectionStart; }
            set { selectionStart = value; }
        }

        private DateTime selectionEnd;

        public DateTime SelectionEnd
        {
            get { return selectionEnd; }
            set { selectionEnd = value; }
        }

        private ITool activeTool;

        [System.ComponentModel.Browsable(false)]
        public ITool ActiveTool
        {
            get { return activeTool; }
            set { activeTool = value; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool CurrentlyEditing
        {
            get
            {
                return editbox.Visible;
            }
        }

        private int workingHourStart = 8;

        [System.ComponentModel.DefaultValue(8)]
        public int WorkingHourStart
        {
            get
            {
                return workingHourStart;
            }
            set
            {
                workingHourStart = value;
                UpdateWorkingHours();
            }
        }

        private int workingMinuteStart = 30;

        [System.ComponentModel.DefaultValue(30)]
        public int WorkingMinuteStart
        {
            get
            {
                return workingMinuteStart;
            }
            set
            {
                workingMinuteStart = value;
                UpdateWorkingHours();
            }
        }

        private int workingHourEnd = 18;

        [System.ComponentModel.DefaultValue(18)]
        public int WorkingHourEnd
        {
            get
            {
                return workingHourEnd;
            }
            set
            {
                workingHourEnd = value;
                UpdateWorkingHours();
            }
        }

        private int workingMinuteEnd = 30;

        [System.ComponentModel.DefaultValue(30)]
        public int WorkingMinuteEnd
        {
            get { return workingMinuteEnd; }
            set
            {
                workingMinuteEnd = value;
                UpdateWorkingHours();
            }
        }

        private void UpdateWorkingHours()
        {
            workStart = new DateTime(1, 1, 1, workingHourStart, workingMinuteStart, 0);
            workEnd = new DateTime(1, 1, 1, workingHourEnd, workingMinuteEnd, 0);

            Invalidate();
        }

        bool selectedAppointmentIsNew;

        public bool SelectedAppointmentIsNew
        {
            get
            {
                return selectedAppointmentIsNew;
            }
        }

        private bool allowScroll = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool AllowScroll
        {
            get
            {
                return allowScroll;
            }
            set
            {
                allowScroll = value;
            }
        }

        private bool allowInplaceEditing = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool AllowInplaceEditing
        {
            get
            {
                return allowInplaceEditing;
            }
            set
            {
                allowInplaceEditing = value;
            }
        }

        private bool allowNew = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool AllowNew
        {
            get
            {
                return allowNew;
            }
            set
            {
                allowNew = value;
            }
        }

        #endregion

        private int HeaderHeight
        {
            get
            {
                return dayHeadersHeight + allDayEventsHeaderHeight;
            }
        }

        #region Event Handlers

        void editbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                FinishEditing(true);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                FinishEditing(false);
            }
        }

        void selectionTool_Complete(object sender, EventArgs e)
        {
            if (selectedAppointment != null)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(EnterEditMode));
            }
        }

        void scrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();

            if (editbox.Visible)
                //scroll text box too
                editbox.Top += e.OldValue - e.NewValue;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            AdjustScrollbar();
        }

        private void AdjustScrollbar()
        {
            scrollbar.Maximum = (2 * halfHourHeight * 25) - this.Height + this.HeaderHeight;
            scrollbar.Minimum = 0;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Flicker free
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Capture focus
            this.Focus();

            if (CurrentlyEditing)
            {
                FinishEditing(false);
            }

            if (selectedAppointmentIsNew)
            {
                RaiseNewAppointment();
            }

            ITool newTool = null;

            Appointment appointment = GetAppointmentAt(e.X, e.Y);

            if (appointment == null)
            {
                if (selectedAppointment != null)
                {
                    selectedAppointment = null;
                    Invalidate();
                }

                newTool = drawTool;
                selection = SelectionType.DateRange;
            }
            else
            {
                newTool = selectionTool;
                selectedAppointment = appointment;
                selection = SelectionType.Appointment;

                Invalidate();
            }

            if (activeTool != null)
            {
                activeTool.MouseDown(e);
            }

            if ((activeTool != newTool) && (newTool != null))
            {
                newTool.Reset();
                newTool.MouseDown(e);
            }

            activeTool = newTool;

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
        //    if (activeTool != null)
        //        activeTool.MouseMove(e);

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (activeTool != null)
                activeTool.MouseUp(e);

            base.OnMouseUp(e);
        }

        protected System.Collections.Hashtable cachedAppointments = new System.Collections.Hashtable();

        protected virtual void OnResolveAppointments(ResolveAppointmentsEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Resolve app");

            if (ResolveAppointments != null)
                ResolveAppointments(this, args);

            this.allDayEventsHeaderHeight = 0;

            // cache resolved appointments in hashtable by days.
            cachedAppointments.Clear();

            if ((selectedAppointmentIsNew) && (selectedAppointment != null))
            {
                if ((selectedAppointment.StartDate > args.StartDate) && (selectedAppointment.StartDate < args.EndDate))
                {
                    args.Appointments.Add(selectedAppointment);
                }
            }

            foreach (Appointment appointment in args.Appointments)
            {
                string key = DateTime.MinValue.ToString(dateTimeKeyFormat);
                AppointmentList list;

                if (appointment.StartDate.Day == appointment.EndDate.Day)
                {
                    key = appointment.StartDate.ToString(dateTimeKeyFormat);
                }
                else
                {
                    // use -1 for exceeding one more than day
                    key = DateTime.MinValue.ToString(dateTimeKeyFormat);

                    /* ALL DAY EVENTS IS NOT COMPLETE
                    this.allDayEventsHeaderHeight += horizontalAppointmentHeight;
                     */
                }

                list = (AppointmentList)cachedAppointments[key];

                if (list == null)
                {
                    list = new AppointmentList();
                    cachedAppointments[key] = list;
                }

                list.Add(appointment);
            }
        }

        internal void RaiseSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        internal void RaiseAppointmentMove(AppointmentEventArgs e)
        {
            if (AppoinmentMove != null)
                AppoinmentMove(this, e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((allowNew) && char.IsLetterOrDigit(e.KeyChar))
            {
                if ((this.Selection == SelectionType.DateRange))
                {
                    if (!selectedAppointmentIsNew)
                        EnterNewAppointmentMode(e.KeyChar);
                }
            }
        }

        private void EnterNewAppointmentMode(char key)
        {
            Appointment appointment = new Appointment();
            appointment.StartDate = selectionStart;
            appointment.EndDate = selectionEnd;
            appointment.Title = key.ToString();

            selectedAppointment = appointment;
            selectedAppointmentIsNew = true;

            activeTool = selectionTool;

            Invalidate();

            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(EnterEditMode));
        }

        private delegate void StartEditModeDelegate(object state);

        private void EnterEditMode(object state)
        {
            if (!allowInplaceEditing)
                return;

            if (this.InvokeRequired)
            {
                Appointment selectedApp = selectedAppointment;

                System.Threading.Thread.Sleep(200);

                if (selectedApp == selectedAppointment)
                    this.Invoke(new StartEditModeDelegate(EnterEditMode), state);
            }
            else
            {
                StartEditing();
            }
        }

        internal void RaiseNewAppointment()
        {
            NewAppointmentEventArgs args = new NewAppointmentEventArgs(selectedAppointment.Title, selectedAppointment.StartDate, selectedAppointment.EndDate);

            if (NewAppointment != null)
            {
                NewAppointment(this, args);
            }

            selectedAppointment = null;
            selectedAppointmentIsNew = false;

            Invalidate();
        }

        #endregion

        #region Public Methods

        public void Clear()
        {
            this.appointmentViews.Clear();
        }

        public void StartEditing()
        {
            if (!selectedAppointment.Locked && appointmentViews.ContainsKey(selectedAppointment))
            {
                if (!selectedAppointment.Editable)
                    return;

                Rectangle editBounds = appointmentViews[selectedAppointment].Rectangle;

                editBounds.Inflate(-3, -3);
                editBounds.X += appointmentGripWidth - 2;
                editBounds.Width -= appointmentGripWidth - 5;

                editbox.Bounds = editBounds;
                editbox.Text = selectedAppointment.Title;
                editbox.Visible = true;
                editbox.SelectionStart = editbox.Text.Length;
                editbox.SelectionLength = 0;

                editbox.Focus();
            }
        }

        public void FinishEditing(bool cancel)
        {
            editbox.Visible = false;

            if (!cancel)
            {
                if (selectedAppointment != null)
                    selectedAppointment.Title = editbox.Text;
            }
            else
            {
                if (selectedAppointmentIsNew)
                {
                    selectedAppointment = null;
                    selectedAppointmentIsNew = false;
                }
            }

            Invalidate();
            this.Focus();
        }

        public DateTime GetTimeAt(int x, int y)
        {
            int dayWidth = (this.Width - (scrollbar.Width + hourLabelWidth)) / daysToShow;

            int hour = (y - this.HeaderHeight + scrollbar.Value) / halfHourHeight;
            x -= hourLabelWidth;

            DateTime date = GetStartDateTime(startDate);

            date = date.Date;
            date = date.AddDays(x / dayWidth);

            if ((hour > 0) && (hour < 24 * 2))
                date = date.AddMinutes((hour * 30));

            return date;
        }

        public Appointment GetAppointmentAt(int x, int y)
        {
            if (y < this.HeaderHeight)
                return null;

            foreach (AppointmentView view in appointmentViews.Values)
                if (view.Rectangle.Contains(x, y))
                    return view.Appointment;

            return null;
        }

        #endregion

        #region Drawing Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            // resolve appointments on visible date range.
            this.firstDateToShow = GetStartDateTime(this.StartDate);
            ResolveAppointmentsEventArgs args = new ResolveAppointmentsEventArgs(this.firstDateToShow, this.firstDateToShow.AddDays(daysToShow));
            OnResolveAppointments(args);

            using (SolidBrush backBrush = new SolidBrush(renderer.BackColor))
                e.Graphics.FillRectangle(backBrush, this.ClientRectangle);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Calculate visible rectangle
            Rectangle rectangle = new Rectangle(0, 0, this.Width - scrollbar.Width, this.Height);
            if (!this.scrollbar.Visible)
                rectangle.Width = this.Width;

            Rectangle hourLabelRectangle = rectangle;

            hourLabelRectangle.Y += this.HeaderHeight;
            hourLabelRectangle.Height -= this.HeaderHeight;
            DrawHourLabels(e, hourLabelRectangle);

            Rectangle daysRectangle = rectangle;
            daysRectangle.X += hourLabelWidth;
            daysRectangle.Y += this.HeaderHeight;
            daysRectangle.Width -= hourLabelWidth;
            daysRectangle.Height -= this.HeaderHeight;

            if (e.ClipRectangle.IntersectsWith(daysRectangle))
            {
                DrawDays(e, daysRectangle);
            }

            Rectangle headerRectangle = rectangle;

            headerRectangle.X += hourLabelWidth;
            headerRectangle.Width -= hourLabelWidth;
            headerRectangle.Height = dayHeadersHeight;

            if (e.ClipRectangle.IntersectsWith(headerRectangle))
                DrawDayHeaders(e, headerRectangle);
        }

        protected virtual void DrawHourLabels(PaintEventArgs e, Rectangle rect)
        {
            e.Graphics.SetClip(rect);

            for (int m_Hour = 0; m_Hour < 24; m_Hour++)
            {
                Rectangle hourRectangle = rect;

                hourRectangle.Y = rect.Y + (m_Hour * 2 * halfHourHeight) - scrollbar.Value;
                hourRectangle.X += hourLabelIndent;
                hourRectangle.Width = hourLabelWidth;

                if (hourRectangle.Y > this.HeaderHeight / 2)
                    renderer.DrawHourLabel(e.Graphics, hourRectangle, m_Hour);
            }

            e.Graphics.ResetClip();
        }

        protected virtual void DrawDayHeaders(PaintEventArgs e, Rectangle rect)
        {
            int dayWidth = rect.Width / daysToShow;

            // one day header rectangle
            Rectangle dayHeaderRectangle = new Rectangle(rect.Left, rect.Top, dayWidth, rect.Height);
            DateTime headerDate = GetStartDateTime(startDate);

            for (int day = 0; day < daysToShow; day++)
            {
                renderer.DrawDayHeader(e.Graphics, dayHeaderRectangle, headerDate, this.StartDate);

                dayHeaderRectangle.X += dayWidth;
                headerDate = headerDate.AddDays(1);
            }
        }

        private Rectangle GetHourRangeRectangle(DateTime start, DateTime end, Rectangle baseRectangle)
        {
            Rectangle rect = baseRectangle;

            int startY;
            int endY;

            startY = (start.Hour * halfHourHeight * 2) + ((start.Minute * halfHourHeight) / 30);
            endY = (end.Hour * halfHourHeight * 2) + ((end.Minute * halfHourHeight) / 30);

            rect.Y = startY - scrollbar.Value + this.HeaderHeight;

            rect.Height = endY - startY;

            if (rect.Y < 0)
            {
                rect.Height += rect.Y;
                rect.Y = 0;
            }

            return rect;
        }

        private void DrawDay(PaintEventArgs e, Rectangle rect, DateTime time)
        {
            //renderer.DrawDayBackground(e.Graphics, rect);

            Rectangle workingHoursRectangle = GetHourRangeRectangle(workStart, workEnd, rect);

            if (workingHoursRectangle.Y < this.HeaderHeight)
                workingHoursRectangle.Y = this.HeaderHeight;

            if (!((time.DayOfWeek == DayOfWeek.Saturday) || (time.DayOfWeek == DayOfWeek.Sunday))) //weekends off -> no working hours
                renderer.DrawHourRange(e.Graphics, workingHoursRectangle, false, false);

            if ((selection == SelectionType.DateRange) && (time.Day == selectionStart.Day))
            {
                Rectangle selectionRectangle = GetHourRangeRectangle(selectionStart, selectionEnd, rect);

                renderer.DrawHourRange(e.Graphics, selectionRectangle, false, true);
            }

            e.Graphics.SetClip(rect);

            for (int hour = 0; hour < 24 * 2; hour++)
            {
                int y = rect.Top + (hour * halfHourHeight) - scrollbar.Value;

                using (Pen pen = new Pen(((hour % 2) == 0 ? renderer.HourSeperatorColor : renderer.HalfHourSeperatorColor)))
                    e.Graphics.DrawLine(pen, rect.Left, y, rect.Right, y);

                if (y > rect.Bottom)
                    break;
            }

            bool today = (time == this.StartDate) ? true : false;

            renderer.DrawDayGripper(e.Graphics, rect, appointmentGripWidth, today);

            e.Graphics.ResetClip();

            DrawAppointments(e, rect, time);
        }

        internal Dictionary<Appointment, AppointmentView> appointmentViews = new Dictionary<Appointment, AppointmentView>();

        private void DrawAppointments(PaintEventArgs e, Rectangle rect, DateTime time)
        {
            DateTime timeStart = time.Date;
            DateTime timeEnd = timeStart.AddHours(24);
            timeEnd = timeEnd.AddSeconds(-1);

            AppointmentList appointments = (AppointmentList)cachedAppointments[time.ToString(dateTimeKeyFormat)];

            if (appointments != null)
            {
                HalfHourLayout[] layout = GetMaxParalelAppointments(appointments);
                List<Appointment> drawnItems = new List<Appointment>();

                for (int halfHour = 0; halfHour < 24 * 2; halfHour++)
                {
                    HalfHourLayout hourLayout = layout[halfHour];

                    if ((hourLayout != null) && (hourLayout.Count > 0))
                    {
                        for (int appIndex = 0; appIndex < hourLayout.Count; appIndex++)
                        {
                            Appointment appointment = hourLayout.Appointments[appIndex];

                            if (drawnItems.IndexOf(appointment) < 0)
                            {
                                Rectangle appRect = rect;
                                int appointmentWidth;
                                AppointmentView view;

                                appointmentWidth = rect.Width / appointment.m_ConflictCount;

                                int lastX = 0;

                                foreach (Appointment app in hourLayout.Appointments)
                                {
                                    if ((app != null) && (appointmentViews.ContainsKey(app)))
                                    {
                                        view = appointmentViews[app];

                                        if (lastX < view.Rectangle.X)
                                            lastX = view.Rectangle.X;
                                    }
                                }

                                if ((lastX + (appointmentWidth * 2)) > (rect.X + rect.Width))
                                    lastX = 0;

                                appRect.Width = appointmentWidth - 5;

                                if (lastX > 0)
                                    appRect.X = lastX + appointmentWidth;

                                appRect = GetHourRangeRectangle(appointment.StartDate, appointment.EndDate, appRect);

                                view = new AppointmentView();
                                view.Rectangle = appRect;
                                view.Appointment = appointment;

                                appointmentViews[appointment] = view;

                                e.Graphics.SetClip(rect);

                                renderer.DrawAppointment(e.Graphics, appRect, appointment, appointment == selectedAppointment, appointmentGripWidth);

                                e.Graphics.ResetClip();

                                drawnItems.Add(appointment);
                            }
                        }
                    }
                }
            }
        }

        private HalfHourLayout[] GetMaxParalelAppointments(List<Appointment> appointments)
        {
            HalfHourLayout[] appLayouts = new HalfHourLayout[24 * 2];

            foreach (Appointment appointment in appointments)
            {
                appointment.m_ConflictCount = 1;
            }

            foreach (Appointment appointment in appointments)
            {
                int firstHalfHour = appointment.StartDate.Hour * 2 + (appointment.StartDate.Minute / 30);
                int lastHalfHour = appointment.EndDate.Hour * 2 + (appointment.EndDate.Minute / 30);

                for (int halfHour = firstHalfHour; halfHour < lastHalfHour; halfHour++)
                {
                    HalfHourLayout layout = appLayouts[halfHour];

                    if (layout == null)
                    {
                        layout = new HalfHourLayout();
                        layout.Appointments = new Appointment[20];
                        appLayouts[halfHour] = layout;
                    }

                    layout.Appointments[layout.Count] = appointment;
                    layout.Count++;

                    // update conflicts
                    foreach (Appointment app2 in layout.Appointments)
                    {
                        if (app2 != null)
                            if (app2.m_ConflictCount < layout.Count)
                                app2.m_ConflictCount = layout.Count;
                    }
                }
            }

            return appLayouts;
        }

        protected virtual void DrawDays(PaintEventArgs e, Rectangle rect)
        {
            int dayWidth = rect.Width / daysToShow;

			/* ALL DAY EVENTS IS NOT COMPLETE
			
            AppointmentList longAppointments = (AppointmentList)cachedAppointments[-1];

            int y = dayHeadersHeight;

            if (longAppointments != null)
            {
                Rectangle backRectangle = rect;
                backRectangle.Y = y;
                backRectangle.Height = allDayEventsHeaderHeight;

                renderer.DrawAllDayBackground(e.Graphics, backRectangle);

                foreach (Appointment appointment in longAppointments)
                {
                    Rectangle appointmenRect = rect;

                    appointmenRect.Width = (dayWidth * (appointment.EndDate.Day - appointment.StartDate.Day));
                    appointmenRect.Height = horizontalAppointmentHeight;
                    appointmenRect.X += (appointment.StartDate.Day - startDate.Day) * dayWidth;
                    appointmenRect.Y = y;

                    renderer.DrawAppointment(e.Graphics, appointmenRect, appointment, appointment == selectedAppointment, appointmentGripWidth);

                    y += horizontalAppointmentHeight;
                }
            }
            */

            DateTime time = GetStartDateTime(startDate);
            Rectangle rectangle = rect;
            rectangle.Width = dayWidth;

            appointmentViews.Clear();

            for (int day = 0; day < daysToShow; day++)
            {
                DrawDay(e, rectangle, time);

                rectangle.X += dayWidth;
                time = time.AddDays(1);
            }
        }

        #endregion

        #region Mouse Event 

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (this.scrollbar != null && this.scrollbar.Visible)
            {
                int delta = e.Delta;
                int maxHeight = this.halfHourHeight * 24 * 2;
                if (Math.Abs(delta) > this.halfHourHeight * 2)
                {
                    if (delta < 0)
                        delta = -this.halfHourHeight * 2;
                    else
                        delta = this.halfHourHeight * 2;
                }
                if (Math.Abs(delta) > maxHeight)
                {
                    if (delta < 0)
                        delta = -maxHeight;
                    else
                        delta = maxHeight;
                }

                int newPos = this.scrollbar.Value;
                newPos -= delta;
                if (newPos < 0)
                    this.scrollbar.Value = 0;
                else if (newPos > this.scrollbar.Maximum)
                    this.scrollbar.Value = this.scrollbar.Maximum;
                else
                    this.scrollbar.Value = newPos;

                this.Invalidate();
            }
        }

        #endregion 

        #region Internal Utility Classes

        class HalfHourLayout
        {
            public int Count;
            public Appointment[] Appointments;
        }

        internal class AppointmentView
        {
            public Appointment Appointment;
            public Rectangle Rectangle;
        }

        protected class AppointmentList : List<Appointment>
        {
        }

        #endregion

        #region Helper Method 
        public DateTime GetStartDateTime(DateTime current)
        {
            if (daysToShow == 1)
                return this.StartDate;

            double deltaDay = -(double)((int)(current.DayOfWeek) - (int)firstDayOfWeek);
            if (firstDayOfWeek != DayOfWeek.Monday)
                deltaDay = -(double)current.DayOfWeek;

            this.firstDateToShow = current.AddDays(deltaDay);

            return this.firstDateToShow;
        }
        #endregion

        #region Events

        public event EventHandler SelectionChanged;
        public event ResolveAppointmentsEventHandler ResolveAppointments;
        public event NewAppointmentEventHandler NewAppointment;
        public event EventHandler<AppointmentEventArgs> AppoinmentMove;

        #endregion
    }
}
