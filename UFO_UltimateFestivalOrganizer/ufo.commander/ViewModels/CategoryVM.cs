using NLog;
using swk5.ufo.dal;
using swk5.ufo.server;

namespace ufo.commander.ViewModels
{
    public class CategoryVM : ViewModelBase
    {
        Logger Logger = LogManager.GetCurrentClassLogger();

        private ICommanderBL commander = BLFactory.GetCommander();
        private Category category;

        public CategoryVM(Category category)
        {
            this.category = category;
        }

        #region Properties

        public string Name
        {
            get { return category.CategoryName; }
            set
            {
                category.CategoryName = value;
                RaisePropertyChangedEvent(nameof(Name));
                // better version = RaisePropertyChangedEven(nameof(Name));
            }
        }

        public Category Category { get { return category; } }

        #endregion
    }
}