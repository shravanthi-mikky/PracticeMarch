using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeedback(FeedbackModel Prod);
        public object RetriveFeedback(long ProductId);
    }
}
