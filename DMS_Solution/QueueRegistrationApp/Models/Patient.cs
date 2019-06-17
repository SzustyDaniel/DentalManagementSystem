﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace QueueRegistrationApp.Models
{
    public class Patient: BindableBase
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string queueType;
        public string QueueType
        {
            get { return queueType; }
            set { SetProperty(ref queueType, value); }
        }

        private int lineNumber;
        public int LineNumber
        {
            get { return lineNumber; }
            set { SetProperty(ref lineNumber, value); }
        }
    }
}
