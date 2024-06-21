using JDM_Casus_Blok4.Interfaces;
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
        public IProvider Provider { get; set; }

        Feedback(int id, string message, IProvider prodider)
        {
            Id = id;
            Message = message;
            Provider = prodider;
        }



        
    }
}
