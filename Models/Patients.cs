namespace PatientWebApi.Models
{
    public class Patients
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }
        //public string FullName { get; set; }
        public string FullName {
            get
            {
                if (FirstName!="" && LastName!="")
                {
                    return FirstName+" "+LastName;
                }
                return string.Empty;
            }
        }
        public short Age { get; set; } = 0;
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public short IsActive { get; set; } = 1;
        public short IsDelete { get; set; } = 0;
        public short IsUpdate { get; set; } = 0;


    }
}
