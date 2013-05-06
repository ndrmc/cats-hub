using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DRMFSS.BLL.MetaModels;

namespace DRMFSS.BLL
{
   
    partial class SMS
    {
        public static void SendSMS(int fdpId, string text)
        {
            DRMFSSEntities1 db = new DRMFSSEntities1();
            var contacts = (from contact in db.Contacts
                            where contact.FDPID == fdpId
                            select contact).ToList();
            foreach (Contact contact in contacts)
            {
  //              INSERT SMS (InOutInd, MobileNumber, Text, RequestDate, SendAfterDate, Status, StatusDate, Attempts, EventTag)
  //VALUES ('O', @MobileNumber, @SMSMessage, @Today, @SendAfterDate, 'pending', @Today, 0, 'SEND_SMS')
                SMS sms = new SMS();
                sms.Attempts = 0;
                sms.EventTag = "SEND_SMS";
                sms.InOutInd = "O";
                sms.LastAttemptDate = null;
                sms.MobileNumber = contact.PhoneNo;
                sms.Text = text;
                sms.RequestDate = DateTime.Today;
                sms.SendAfterDate = DateTime.Today;
                sms.Status = "pending";
                sms.StatusDate = DateTime.Today;

                db.SMS.AddObject(sms);
                //try
                //{
                    
                //}
                //catch (Exception e)
                //{

                //}

            }

            db.SaveChanges();

        }
    }
}
