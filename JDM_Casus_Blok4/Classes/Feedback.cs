
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int ProviderId { get; set; }

        public Feedback(int id, string message, int providerId)
        {
            Id = id;
            Message = message;
            ProviderId = providerId;
        }

        public Feedback(string message, int providerId)
        {
            Message = message;
            ProviderId = providerId;
        }

        //feedBackType is een string dat "Assessment" of "Exercise" kan zijn
        // om onderscheid te maken in de database
        public void SaveFeedback(int feedbackProviderId, string feedBackType, int assessmentId)
        {
            DAL.Dal Dal = DAL.Dal.Instance;
            Dal.CreateFeedback(this, feedBackType, assessmentId);
        }

    }
}
