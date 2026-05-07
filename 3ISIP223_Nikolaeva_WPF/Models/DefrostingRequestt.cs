using _3ISIP223_Nikolaeva_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class DefrostingRequest
    {
        public string FullDescription { get
            {
                switch(TargetTypeID)
                {
                    case 1:
                        {
                            var book = Core.Context.Book.FirstOrDefault(b => b.ID == TargetTypeID);
                            return $"Заявка на разморозку {ComplaintTargetType.Name} {book.Name} от {User.NickName} {Date:dd MMMM yyyy}";
                        }
                        case 2:
                        {
                            var user = Core.Context.User.FirstOrDefault(b => b.ID == TargetTypeID);
                            return $"Заявка на разморозку {ComplaintTargetType.Name} {user.NickName} {Date:dd MMMM yyyy}";

                        }
                    default: return null;
                }
            } 
        }
        public Visibility VisIsConfirmed => IsConfirmed == null ? Visibility.Visible : Visibility.Collapsed;

        public Visibility VisIsClosed => IsConfirmed != null ? Visibility.Visible : Visibility.Collapsed;
    }
}
