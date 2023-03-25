using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;

namespace BusinessLayer.Interfaces
{
    public interface IPaymentBL
    {
        public bool Payment(PaymentModel payModel);
    }
}
