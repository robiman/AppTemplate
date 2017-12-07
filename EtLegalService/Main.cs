using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EtLegalService.Models.Operational;
using EtLegalService.Context;
using System.Net.Mail;
using System.Net;
using EtLegalService.Models.Master;

namespace EtLegalService
{
    class Main
    {
        public void main()
        {
            string Email = "birukam@ethiopianairlines.com";
            EtLegalContext db = new EtLegalContext();

            DateTime nowDate =new DateTime();
            nowDate = DateTime.Now;

            List<FollowUpBO> followUps = db.folowup.Include(f => f.etlowyer).Include(f => f.etlowyer1).Where(f=>f.DateofNextAppointment==nowDate.AddDays(1) || f.DateofNextAppointment==nowDate).ToList();


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Email);

            foreach(FollowUpBO followup in followUps)
            {
                List<EtLawyerBO> etlawyer = db.etlawyer.Where(e => e.IDNo == followup.AssignedETCounsel|| e.IDNo == followup.AssignedLocalCounsel).ToList();
                foreach(EtLawyerBO etLawyer in etlawyer)
                {
                    mail.To.Add(etLawyer.OutLookEmail);
                    mail.To.Add(etLawyer.OtherEmail);
                }
                mail.Body = "Dears <br/>You have registerd case followup appointment for case file number "+followup.courtC.FileNumber+". Please click the below link for the detail.<br/> <br/> Thank you!";
                mail.IsBodyHtml = true;
                mail.Subject = "Campaign for your approval";
            }
            
            //mail.CC.Add("");
               

           

            SmtpClient smtpClient = new SmtpClient("svhqsgw02.ethiopianairlines.com", 25);
            //SmtpClient smtpClient = new SmtpClient("localhost", 25);
            smtpClient.Credentials = new NetworkCredential("ServiceAlert", "abcd@1234");
            smtpClient.Send(mail);

        }
    }
}
