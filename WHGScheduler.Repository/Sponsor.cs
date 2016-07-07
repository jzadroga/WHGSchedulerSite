using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHGScheduler.DataAccess;
using WHGScheduler.Repository.Models;

namespace WHGScheduler.Repository
{
    public class Sponsor
    {
        public static List<SponsorModel> GetList(bool activeOnly)
        {
            var sponsorList = sponsor.GetList(activeOnly);

            return sponsorList.Select( sp => new SponsorModel()
            {
                id = sp.sponsorID,
                description = sp.description,
                logoImage = sp.logoImage,
                name = sp.name
            }).ToList();
        }

        public static SponsorModel GetByID(int sponsorID)
        {
            var sponsorObj = sponsor.Get(sponsorID);

            return (sponsorObj == null) ? new SponsorModel() : new SponsorModel()
            {
                id = sponsorObj.sponsorID,
                description = sponsorObj.description,
                logoImage = sponsorObj.logoImage,
                name = sponsorObj.name
            };
        }

        public static void Save(SponsorModel obj)
        {
            sponsor.Save( new sponsor(){
                name = obj.name,
                sponsorID = obj.id,
                logoImage = obj.logoImage
            });
        }
    }
}
