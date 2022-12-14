using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel model,int bookId, int userId);
        public List<GetFeedbackDataModel> GetAllFeedbacks(int bookId);

    }
}
