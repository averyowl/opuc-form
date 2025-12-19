using OpucForm.States.Drafts;

namespace OpucForm.States
{
    public sealed class FormState
    {

        private FormDraft _currentDraft =  new FormDraft();

        public FormDraft Property
        {
            get => _currentDraft;
            set
            {
                _currentDraft = value;
                NotifyStateChanged();
            }
        }
        
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}