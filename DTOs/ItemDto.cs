namespace Aråstock.DTOs
{
    public class ItemDto
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public int UnitID { get; set; }
        public DateTime Created { get; set; }
        public int TotalAmountInStock { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string UnitName { get; set; }


    }
}
