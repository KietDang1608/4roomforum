using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class Threads
{
    [Key]
    [Required]
    public int ThreadId { get; set; }
    public int CategoryID { get; set; }
    public string ThreadTitle { get; set; }
    public string ThreadContent { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly CreatedDate { get; set; }
    public int ViewCount { get; set; }
    public int IsPinned { get; set; }
    public int IsClosed { get; set; }

    public override string ToString()
    {
        return "Thread [ThreadId=" + ThreadId +
               ", CategoryID=" + CategoryID +
               ", ThreadTitle=" + ThreadTitle +
               ", ThreadContent=" + ThreadContent +
               ", CreatedBy=" + CreatedBy +
               ", CreatedDate=" + CreatedDate +
               ", ViewCount=" + ViewCount +
               ", IsPinned=" + IsPinned +
               ", IsClosed=" + IsClosed + "]";
    }
}