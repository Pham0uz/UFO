using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ufo.commander.ViewModels
{
    public class UserVM : ViewModelBase
    {
        private ICommanderBL commander = BLFactory.GetCommander();
        private User user;

        public UserVM(User user)
        {
            this.user = user;
        }

        #region Properties
        public string EMail
        {
            get { return user.EMail; }
            set
            {
                user.EMail = value;
                RaisePropertyChangedEvent("EMail");
            }
        }

        public string PasswordHash
        {
            get { return user.PasswordHash; }
            set
            {
                user.PasswordHash = value;
                RaisePropertyChangedEvent("PasswordHash");
            }
        }

        #endregion
    }
}
