public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public string CreatedDate { get; set; }

    public override string ToString()
    {
        return "Category [CategoryId=" + CategoryId + ", CategoryName=" + CategoryName + ", Description=" + Description + ", CreatedBy=" + CreatedBy + ", CreatedDate=" + CreatedDate + "]";
    }
}
