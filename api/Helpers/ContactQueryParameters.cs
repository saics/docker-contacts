using System;

namespace Contacts.API.Helpers
{
    public class ContactQueryParameters
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;
        private string _sortColumn = "FirstName";
        private string _sortOrder = "ASC";

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(Math.Max(1, value), MaxPageSize);
        }

        public string SortColumn
        {
            get => _sortColumn;
            set
            {
                _sortColumn = value?.ToUpperInvariant() switch
                {
                    "FIRSTNAME" => "FirstName",
                    "LASTNAME" => "LastName",
                    "EMAIL" => "Email",
                    "PHONENUMBER" => "PhoneNumber",
                    "CREATEDDATETIME" => "CreatedDateTime",
                    "LASTMODIFIEDDATETIME" => "LastModifiedDateTime",
                    _ => "FirstName"
                };
            }
        }

        public string SortOrder
        {
            get => _sortOrder;
            set => _sortOrder = value?.ToUpperInvariant() == "DESC" ? "DESC" : "ASC";
        }

        public string? SearchFirstName { get; set; }
        public string? SearchLastName { get; set; }

        public void Validate()
        {
            if (PageNumber < 1) PageNumber = 1;
            if (string.IsNullOrEmpty(SortColumn)) SortColumn = "FirstName";

            SearchFirstName = SearchFirstName?.Trim();
            SearchLastName = SearchLastName?.Trim();
        }
    }
}