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

        public FeedbackModel AddFeedback(FeedbackModel model,int bookId, int userId)
        {
            try
            {
                return feedbackRL.AddFeedback(model, bookId, userId);   
            }catch(Exception e)
            {
                throw e;
            }
        }
        public List<GetFeedbackDataModel> GetAllFeedbacks(int bookId)
        {
            try
            {
                return feedbackRL.GetAllFeedbacks(bookId);
            }catch(Exception e)
            {
                throw e;
            }
        }

    }
}
