using AdionFA.UI.Infrastructure.Base;
using System;

namespace AdionFA.UI.Infrastructure.Model.Base
{
    public class EntityBaseVM : ViewModelBase
    {
        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get => _createdOn;
            set => SetProperty(ref _createdOn, value);
        }


        private DateTime? _updatedOn;
        public DateTime? UpdatedOn
        {
            get => _updatedOn;
            set => SetProperty(ref _updatedOn, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }
    }
}
