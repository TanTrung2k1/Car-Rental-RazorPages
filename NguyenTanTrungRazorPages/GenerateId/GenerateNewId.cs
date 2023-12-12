using BusinessObjects.Models;
using System.Linq;

namespace NguyenTanTrungRazorPages.GenerateId
{
    public class GenerateNewId
    {
        private CarRentalSystemDBContext _context;

        public GenerateNewId()
        {
            _context = new CarRentalSystemDBContext();
        }
        public string GenerateCustomerId()
        {
            
            string prefix = "C";
            int nextId = 1;

            // get the latest ID from the database
            string latestId = _context.Customers.Max(c => c.CustomerId);

            if (!string.IsNullOrEmpty(latestId))
            {
                // extract the numeric part of the latest ID
                int latestNumericId = int.Parse(latestId.Substring(2));

                // increment the numeric part of the latest ID
                nextId = latestNumericId + 1;
            }

            // format the next ID with the prefix and numeric part
            string newId = $"{prefix}{nextId.ToString("D5")}";

            return newId;
        }

        public string GenerateCarId()
        {
            string prefix = "CA";
            int nextId = 1; 
            string latestId = _context.Cars.Max(c => c.CarId);
            if (!string.IsNullOrEmpty(latestId))
            {
                int latestNumericId = int.Parse(latestId.Substring(2));
                nextId = latestNumericId + 1;
            }
            return $"{prefix}{nextId.ToString("D4")}";
        }

        public string GenerateStaffId()
        {
            string prefix = "NV";
            int nextId = 1;
            string latestId = _context.StaffAccounts.Max(s => s.StaffId);
            if (!string.IsNullOrEmpty(latestId))
            {
                int latestNumericId = int.Parse(latestId.Substring(2));
                nextId = latestNumericId + 1;
            }
            return $"{prefix}{nextId.ToString("D4")}";
        }
    }
}
