namespace TestForDotNet.Core.Base
{
    public class ViewModelBase
    {
        public bool IsChanged { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsNew { get; private set; }

        public void SetChangedStatus()
        {
            if (IsChanged || IsNew || IsDeleted)
                return;

            IsChanged = !IsChanged;
        }

        public void MarkAsDeleted()
        {
            if (!IsDeleted)
                IsDeleted = !IsDeleted;
        }

        public void MarkAsNew()
        {
            if (!IsNew) 
                IsNew = !IsNew;
        }
    }
}