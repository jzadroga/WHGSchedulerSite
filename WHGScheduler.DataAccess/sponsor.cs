using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.DataAccess
{
    public partial class sponsor
    {
        public static List<sponsor> GetList(bool activeOnly)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return (activeOnly) ? context.sponsors.Where(sp => sp.status.statusName == "active").ToList() : context.sponsors.ToList(); 
        }

        public static sponsor Get(int id)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return context.sponsors.Where(sp => sp.sponsorID == id).FirstOrDefault();
        }

        public static void Delete(int sponsorID)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            var deleteSponsor = context.sponsors.Where(sp => sp.sponsorID == sponsorID).FirstOrDefault();

            deleteSponsor.dateModified = DateTime.Now;
            deleteSponsor.statusID = status.GetDeleteStatus("sponsor");

            context.SubmitChanges();
        }

        public static void Save(sponsor sponsorObj)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            if (sponsorObj.sponsorID != 0)
            {
                var updateSponsor = context.sponsors.Where(sp => sp.sponsorID == sponsorObj.sponsorID).FirstOrDefault();
                if(updateSponsor != null)
                {
                    updateSponsor.name = sponsorObj.name;
                    updateSponsor.logoImage = sponsorObj.logoImage;
                    updateSponsor.websiteUrl = sponsorObj.websiteUrl;
                    updateSponsor.description = sponsorObj.description;
                    updateSponsor.dateModified = DateTime.Now;
                }
            }
            else
            {
                sponsorObj.dateCreated = DateTime.Now;
                sponsorObj.dateModified = DateTime.Now;
                sponsorObj.statusID = status.GetActiveStatus("sponsor");

                context.sponsors.InsertOnSubmit(sponsorObj);
            }

            context.SubmitChanges();
        }
    }
}
