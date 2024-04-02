namespace NMEX_Manufacturing_KPIs.Models.Module_User
{
    public class Users
    {
        public int User_id { get; set; }
        public string FirstName { get; set; }
        public string LastNamePaternal { get; set; }
        public string LastNameMaternal { get; set; }
        public string Email { get; set; }
        public string EmailNormalized { get; set; }
        public string Password { get; set; }
        public bool Privilege { get; set; }
        public bool Active { get; set; }
        public int Plant_id { get; set; }
        public string IdUserNissan { get; set; }

        //View properties
        public string Plant { get; set; }
    }
}
