﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IPaymentRL
    {
        public bool Payment(PaymentModel payModel);
    }
}
