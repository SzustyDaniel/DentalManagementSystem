using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QueueRegisteringClient.Models;
using QueueRegisteringClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.QueueModels;
using QueueRegisteringClient.Services;
using System.Net.Http;
using Common.ManagementModels;
using QueueRegisteringClient.Properties;
using System.Windows.Threading;

namespace QueueRegisteringClient.ViewModels
{
    public class SelectQueueComponentViewModel : BindableBase
    {

        #region Properties
        private IViewsDialog views;
        private IEventAggregator eventAggregator;
        private IClientHttpActions clientHttp;
        private DispatcherTimer timer;

        /*
         * The primary model used by the view model
         */
        private Patient _model;
        public Patient Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        /*
         * For Checking the working time of the nurse queue in the system 
         */
        private ScheduleModel nurseSchedule;
        public ScheduleModel NurseScheduleModel
        {
            get { return nurseSchedule; }
            set { SetProperty(ref nurseSchedule, value); }
        }

        /*
         * For checking the working time of the pharmacy queue in the system
         */
        private ScheduleModel pharmacySchedule;
        public ScheduleModel PharmacyScheduleModel
        { 
            get { return pharmacySchedule; }
            set { SetProperty(ref pharmacySchedule, value); }
        }

        /*
         * Property for the Obeserve can execute of the nurse command
         */
        private bool _nurseQueueValid;
        public bool NurseQueueValid
        {
            get { return _nurseQueueValid; }
            set { SetProperty(ref _nurseQueueValid, value); }
        }

        /*
         * Property for the Obeserve can execute of the pharmacy command
         */
        private bool _pharmacyQueueValid;
        public bool PharmacyQueueValid
        {
            get { return _pharmacyQueueValid; }
            set { SetProperty(ref _pharmacyQueueValid, value); }
        }

        public DayOfWeek CurrentDay { get; set; }
        #endregion

        #region Constructor

        public SelectQueueComponentViewModel(IEventAggregator ea,IClientHttpActions clientHttpActions, IViewsDialog viewsDialog)
        {
            // Set depandancies
            clientHttp = clientHttpActions;
            eventAggregator = ea;
            views = viewsDialog;

            // Set properties and register events
            CurrentDay = DateTime.Now.DayOfWeek;
            PharmacyQueueValid = false;
            NurseQueueValid = false;
            eventAggregator.GetEvent<SendPatientEvent>().Subscribe(LoadModel);
            GetScheduleAsync();

            // Timer Setup
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += CheckSchedules;

            NurseQueueValid = CheckTime(NurseScheduleModel);
            PharmacyQueueValid = CheckTime(PharmacyScheduleModel);
        }

        #endregion

        #region commands
        // Nurse queue enter command
        private DelegateCommand _enterNurseQueue;
        public DelegateCommand EnterNurseQueueCommand =>
            _enterNurseQueue ?? (_enterNurseQueue = new DelegateCommand(ExecuteEnterNurseQueueCommandAsync)).ObservesCanExecute(() => NurseQueueValid);

        private void ExecuteEnterNurseQueueCommandAsync()
        {
            EnterToQueueActionsAsync(ServiceType.Nurse);
        }

        // Pharmacist queue enter command
        private DelegateCommand _enterPharmacyQueue;
        public DelegateCommand EnterPharmacyQueueCommand =>
            _enterPharmacyQueue ?? (_enterPharmacyQueue = new DelegateCommand(ExecuteEnterPharmacyQueueCommandAsync)).ObservesCanExecute(() => PharmacyQueueValid);

        private void ExecuteEnterPharmacyQueueCommandAsync()
        {
            EnterToQueueActionsAsync(ServiceType.Pharmacist);
        }

        private DelegateCommand _goBackcommand;
        public DelegateCommand GoBackCommand =>
            _goBackcommand ?? (_goBackcommand = new DelegateCommand(ExecuteGoBackCommand));

        void ExecuteGoBackCommand()
        {
            eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);
        }
        #endregion

        #region Methods

        /*
         * Catch model sent from another view-model
         */
        private void LoadModel(Patient obj)
        {
            Model = obj;
            eventAggregator.GetEvent<SendPatientEvent>().Unsubscribe(LoadModel); // stop listening to the event
            timer.Start(); // start timer after you received the model
        }

        /*
         * Enter the client to the appropriate queue
         */
        private async void EnterToQueueActionsAsync(ServiceType service)
        {
            try
            {
                EnqueuePosition enqueuePosition = new EnqueuePosition() { ServiceType = service, UserID = Model.CustomerID };
                Model.LineNumber = await clientHttp.RegisterToQueueAsync(enqueuePosition);
                Model.QueueType = service;

                timer.Stop();
                eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.display);  // switch the current view to display
                eventAggregator.GetEvent<SendPatientEvent>().Publish(Model);            // send it the current model
            }
            catch (HttpRequestException e)
            {
                views.ShowErrorDialog(e.Message);
                timer.Stop();
                eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);  // switch the current view to display
            }
            
            
        }

        /*
         * Get the queues schedule from the ManagementAPI for the queues
         */
        private async void GetScheduleAsync()
        {


            // Check if there is a need to get the schedule for the 
            if((Settings.Default.CurrentDay.DayOfWeek != CurrentDay) || (NurseScheduleModel == null || PharmacyScheduleModel == null))
            {


                if (Settings.Default.CurrentDay.DayOfWeek != CurrentDay)
                {
                    // Save the current day of work
                    Settings.Default.CurrentDay = DateTime.Now;
                    Settings.Default.Save();
                }

                try
                {
                    var workSchedules = await clientHttp.GetSchedulesAsync(CurrentDay);

                    for (int i = 0; i < workSchedules.Count; i++)
                    {
                        if (workSchedules[i].Type == ServiceType.Nurse)
                            NurseScheduleModel = workSchedules[i];
                        else if (workSchedules[i].Type == ServiceType.Pharmacist)
                            PharmacyScheduleModel = workSchedules[i];
                    }

                    NurseQueueValid = CheckTime(NurseScheduleModel);
                    PharmacyQueueValid = CheckTime(PharmacyScheduleModel);
                }
                catch (Exception e)
                {
                    views.ShowErrorDialog(e.Message);
                    timer.Stop();
                    eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);  // switch the current view to display
                }
            }
        }

        /*
         * Check the times of the schedule and return if the current time is in the 
         * working window of the queue
         */
        private bool CheckTime(ScheduleModel schedule)
        {
            if (schedule == null)
                return false;

            return (schedule.WorkingHours.StartTime <= DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay <= schedule.WorkingHours.EndTime) ? true : false;
        }

        /*
         * Tick event of the dispacher timer
         * This activates the CheckTime for the queues
         */
        private void CheckSchedules(object sender, EventArgs e)
        {
            NurseQueueValid = CheckTime(NurseScheduleModel);
            PharmacyQueueValid = CheckTime(PharmacyScheduleModel);
        }

        #endregion
    }
}
