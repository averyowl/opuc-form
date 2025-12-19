using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using OpucForm.Shared;

namespace OpucForm.Components.Pages
{
    public partial class Home : ComponentBase
    {

        [Inject] private IJSRuntime JS { get; set; } = default!;

        private bool SameAsHomeAddress { get; set; } = true;
        private string _ssn = "";
        private string? SelectedFileName;
        // Uploaded document vars
        public string QualificationProgram { get; set; } = "";
        private UploadedDocumentDto? UploadedDocument { get; set; }

        /* ================= QUALIFICATION TYPE: Income ================= */
        public int? NumberOfHouseholdMembers { get; set; }
        /* ================= QUALIFICATION TYPE: Income ================= */

        // Step 2 Data
        public string enteredZip { get; set; } = "";
        public string selectedProvider { get; set; } = "";
        public string applicantPhone { get; set; } = "";

        // Step 4
        public bool ShowSubmitModal { get; set; } = false;
        private void OpenSubmitModal()
        {
            ShowSubmitModal = true;
        }

        private void CloseSubmitModal()
        {
            ShowSubmitModal = false;
        }

        // Runs when user submits application in Step4Model.razor
        private async Task ConfirmSubmitAsync()
        {
            ShowSubmitModal = false;
            Console.WriteLine("Application submitted!");
            Console.WriteLine("Applicant Entered Zip: " + enteredZip); // Working
            Console.WriteLine("Applicant Selected Provider: " + selectedProvider); // Working
            Console.WriteLine("Applicant Phone Number or internet service account number: "); // Todo
            Console.WriteLine("Applicant qualify through child or dependant?: (Y/N)"); // Todo - If yes, entered child or depedants info below
            Console.WriteLine("Applicant Qualification Program: " + QualificationProgram); // Working
            Console.WriteLine("Applicant Income Number of people in household: " + NumberOfHouseholdMembers);

            // If applicant selected a document, upload:
            if (UploadedDocument != null)
            {
                await UploadDocumentAsync();
            }
            else
            {
                Console.WriteLine("No file attached to application, skipping...");
            }

            // TODO: submit application data

        }

        // Handler for getting selected document in Step2Model
        private void HandleDocumentSelected(UploadedDocumentDto doc)
        {
            UploadedDocument = doc;
        }

        private Task OnQualificationChanged(string value)
        {
            QualificationProgram = value;
            return Task.CompletedTask;
        }

        private Task OnDocumentSelected(UploadedDocumentDto doc)
        {
            UploadedDocument = doc;
            return Task.CompletedTask;
        }

        // Upload attached document to server (wwwroot/uploads/ for now)
        private async Task UploadDocumentAsync()
        {
            if (UploadedDocument == null)
                return;

            var uploadsPath = Path.Combine(
                Environment.CurrentDirectory,
                "wwwroot",
                "uploads");

            Directory.CreateDirectory(uploadsPath);

            var filePath = Path.Combine(uploadsPath, UploadedDocument.FileName);

            await File.WriteAllBytesAsync(filePath, UploadedDocument.Data);

            Console.WriteLine($"Uploaded file: {UploadedDocument.FileName}");
        }


        // DB
        //  string zipCode = "";


        // Form Viewing
        private bool showApplicationBody = false;

        // ================== Pagination ==================
        private int CurrentStep = 1;
        private int TotalSteps = 4;

        private async Task HandleNextOrSubmit()
        {
            if (CurrentStep < TotalSteps)
            {
                CurrentStep++;
                await ScrollToTop();
            }
            else
            {
                // On Step 4, open confirmation modal
                OpenSubmitModal();
            }
        }

        private async Task GoBack()
        {
            if (CurrentStep > 1)
                CurrentStep--;

            await ScrollToTop();
        }

        private async Task GoToStep(int step)
        {
            CurrentStep = step;
            await ScrollToTop();
        }

        private string GetBulletClass(int step)
        {
            if (step < CurrentStep) return "bullet past";        // Completed steps
            if (step == CurrentStep) return "bullet current";    // Current step
            return "bullet future";                              // Upcoming steps
        }

        // Percentage of progress (0% = step 1 not started, 100% = step 4 completed)
        private double ProgressPercentage
        {
            get
            {
                // Step 1 starts full (100% of the first bullet), then progresses gradually
                if (CurrentStep == 1)
                    return 100 * 0.5; // half filled
                return ((CurrentStep - 1) / (double)(TotalSteps - 1)) * 100;
            }
        }

        // When user enters a zip & selects a provider to apply to, move to step 2.
        private Task HandleZipEntered(string zip)
        {
            enteredZip = zip;
            return Task.CompletedTask;
        }
        private async Task HandleProviderSelected(string provider)
        {
            selectedProvider = provider; // update provider
            CurrentStep = 2; // move to step 2
            await ScrollToTop();

        }

        // Function to scroll smoothly to top when user navigates form.
        private async Task ScrollToTop()
        {
            if (JS != null)
            {
                await JS.InvokeVoidAsync("scrollToTop");
            }
        }

        // ================== Pagination ==================

        // May not be needed anymore if were not using xxx-xx-xxxx format for SSNs
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

    }
}
