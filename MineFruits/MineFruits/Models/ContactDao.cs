using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MineFruits.Models
{
    public class ContactDao
    {
        MineFruitsDB db = null;
        public ContactDao()
        {
            db = new MineFruitsDB();
        }
        //public Contact GetActiveContact()
        //{
        //    return db.Contacts.Single(x => x.Status == true);
        //}
        public int InsertFeeBack(Feedback feedBack)
        {
            db.Feedbacks.Add(feedBack);
            db.SaveChanges();
            return feedBack.FeedbackID;
        }
    }
}