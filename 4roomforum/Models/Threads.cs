using System;

namespace _4roomforum.Models;

public class Threads
{

    public int ThreadId { get; set; }
    public int CategoryID { get; set; }
    public string ThreadTitle { get; set; }
    public string ThreadContent { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly CreatedDate { get; set; }
    public int ViewCount { get; set; }
    public int IsPinned { get; set; }
    public int IsClosed { get; set; }

}

