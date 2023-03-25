using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class PaymentBL : IPaymentBL
    {
        IPaymentRL iPaymentBL;
        public PaymentBL(IPaymentRL iPaymentBL)
        {
            this.iPaymentBL = iPaymentBL;
        }

        public bool Payment(PaymentModel payModel)
        {
            try
            {
                return iPaymentBL.Payment(payModel);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
