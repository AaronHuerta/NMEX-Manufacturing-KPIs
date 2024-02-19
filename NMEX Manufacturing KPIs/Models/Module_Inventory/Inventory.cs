namespace NMEX_Manufacturing_KPIs.Models.Module_Inventory
{
    public class Inventory
    {
        public int Inventory_Id { get; set; }
        public string DeviceType { get; set; }
        public string SerialNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Location { get; set; }
        public string Version { get; set; }
        public string Model { get; set; }
        public bool Active { get; set; }

    }
}
