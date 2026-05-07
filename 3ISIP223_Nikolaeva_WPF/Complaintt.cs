using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class Complaint
    {
        public string TargetName
        {
            get
            {
                switch (TargetTypeID)
                {
                    case 1:
                        {
                            return Core.Context.Book.FirstOrDefault(b => b.ID == TargetID).Name;
                      
                        }
                    case 2:
                        {
                            return Core.Context.User.FirstOrDefault(u => u.ID == TargetID).NickName;
                        }

                    case 3:
                        {
                            return Core.Context.Review.FirstOrDefault(u => u.ID == TargetID).User.NickName;
                        }
                    default:
                        {
                            return null;
                        }
                }

            }
        }

        public string FullDescription { get
            {
                return $"Жалоба №{ID} от {User.NickName} на {ComplaintTargetType.Name} {TargetName} от {Date} по причине {ComplaintReason.Name}";
            } }
    }
}
