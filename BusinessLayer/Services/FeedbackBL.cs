using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBL:IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public FeedbackModel AddFeedback(FeedbackModel model, int userId)
        {
            try
            {
                return feedbackRL.AddFeedback(model, userId);   
            }catch(Exception e)
            {
                throw e;
            }
        }

    }
}
