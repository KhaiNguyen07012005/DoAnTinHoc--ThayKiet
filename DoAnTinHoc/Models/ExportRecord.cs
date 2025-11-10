namespace DoAnTinHoc.Models
{
    public class ExportRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int EmployeeId { get; set; }        
        public string Note { get; set; } = string.Empty;
        public List<ExportItem> Items { get; set; } = new();
    }

    public class ExportItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }  
    }
}
