using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel Prod);
        public object RetriveFeedback(long ProductId);
    }
}
