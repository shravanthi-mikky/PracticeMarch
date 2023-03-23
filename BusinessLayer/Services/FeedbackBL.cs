using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        IFeedbackRL iFeedbackRL;
        public FeedbackBL(IFeedbackRL iFeedbackRL)
        {
            this.iFeedbackRL = iFeedbackRL;
        }

        public FeedbackModel AddFeedback(FeedbackModel Prod)
        {
            try
            {
                return iFeedbackRL.AddFeedback(Prod);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public object RetriveFeedback(long ProductId)
        {
            try
            {
                return iFeedbackRL.RetriveFeedback(ProductId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
