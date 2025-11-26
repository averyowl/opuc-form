using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;

namespace OpucForm.Components.Pages
{
    public partial class Home : ComponentBase
    {

        [Inject] private IJSRuntime JS { get; set; } = default!;

       

        private bool SameAsHomeAddress { get; set; } = true;
        private string? QualificationProgram { get; set; } = "";
        private IJSObjectReference? _mapboxModule;
        private string _ssn = "";
        private string? SelectedFileName;


        // DB
        private string? zipCode;


        // Form Viewing
        private bool showApplicationBody = false;

        // ================== Pagination ==================
        private int CurrentStep = 1;
        private int TotalSteps = 4;

        private void GoNext()
        {
            if (CurrentStep < TotalSteps)
                CurrentStep++;
        }

        private void GoBack()
        {
            if (CurrentStep > 1)
                CurrentStep--;
        }

        private void GoToStep(int step)
        {
            CurrentStep = step;
        }

        private string GetBulletClass(int step)
        {
            if (step < CurrentStep) return "bullet past";        // Completed steps
            if (step == CurrentStep) return "bullet current";    // Current step
            return "bullet future";                              // Upcoming steps
        }
        // ================== Pagination ==================

        public string SSN

        {
            get => _ssn;
            set
            {
                // Remove non-digits
                var digits = new string(value.Where(char.IsDigit).ToArray());

                // Limit to 9 digits
                if (digits.Length > 9)
                    digits = digits[..9];

                // Add dashes
                if (digits.Length > 5)
                    _ssn = $"{digits[..3]}-{digits.Substring(3, 2)}-{digits[5..]}";
                else if (digits.Length > 3)
                    _ssn = $"{digits[..3]}-{digits[3..]}";
                else
                    _ssn = digits;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _mapboxModule = await JS.InvokeAsync<IJSObjectReference>("import", "./js/mapbox.js");
                await _mapboxModule.InvokeVoidAsync("initMapbox");
              

            }
        }


        private async Task OnMailingAddressToggleChanged()
        {
            if (!SameAsHomeAddress && _mapboxModule is not null)
            {
                // Re-initialize Mapbox autofill for mailing address fields
                await _mapboxModule.InvokeVoidAsync("initMailingAddressAutocomplete");
            }
        }

        private void OnFileSelected(InputFileChangeEventArgs e)
        {
            SelectedFileName = e.FileCount > 0
                ? e.File.Name
                : null;
        }

        private void CheckZip()
        {
            // For now, simulate a "valid" ZIP check
            // Later, this will query the DB or API
            if (!string.IsNullOrWhiteSpace(zipCode))
            {
                showApplicationBody = true;
            }
            else
            {
                showApplicationBody = false;
            }
        }


    }
}
