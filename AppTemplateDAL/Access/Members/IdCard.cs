using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECASDAL.Access.Members
{
    public class IdImages
    {
        public byte[] image { get; set; }
        public byte[] signiture { get; set; }
    }
    public class IDCard
    {
        EALIDCardDataEntities enti = new EALIDCardDataEntities();
        public IdImages GetIdcard(string empid)
        {
            try
            {
                ActiveEmployee obj = enti.ActiveEmployees.Where(a => a.IDNo == "000" + empid).Single();
                IdImages img = new IdImages();
                img.image = obj.Photo;
                img.signiture = obj.Signature;
                return img;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
