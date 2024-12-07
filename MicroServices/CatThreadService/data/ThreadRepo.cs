using System;
using MicroServices.CatThreadService.Data;

namespace CatThreadService.Data;
public class ThreadRepo : IThreadRepo
{
    private readonly AppDBContext _context;

    public ThreadRepo (AppDBContext context)
    {  _context = context; }

    public IEnumerable<Threads> GetAllThread()
    {  
        return _context.Threads.ToList(); 
    }
    public Threads GetThreadById(int id)
    {
        return _context.Threads.FirstOrDefault(h => h.ThreadId == id);
    }


    void IThreadRepo.CreateThread(Threads thread)
    {
        if (thread == null)
        {
            throw new ArgumentNullException(nameof(thread));
        }
        _context.Threads.Add(thread);
        _context.SaveChanges();
    }

    void IThreadRepo.UpdateThread(Threads thread)
    {
        if (thread == null)
        {
            throw new ArgumentNullException(nameof(thread));
        }

      
        var existingThread = _context.Threads.Find(thread.ThreadId);

       
        if (existingThread == null)
        {
            throw new InvalidOperationException("Thread not found");
        }

       
        existingThread.CategoryID = thread.CategoryID;
        existingThread.ThreadTitle = thread.ThreadTitle;
        existingThread.ThreadContent = thread.ThreadContent;
        existingThread.CreatedBy = thread.CreatedBy;
        existingThread.CreatedDate = thread.CreatedDate;
        existingThread.ViewCount = thread.ViewCount;
        existingThread.IsPinned = thread.IsPinned;
        existingThread.IsClosed = thread.IsClosed;
      
        _context.SaveChanges();
    }

    void IThreadRepo.DeleteThread(int id)
    {
        var thread = _context.Threads.FirstOrDefault(h => h.ThreadId == id);
        if (thread == null)
        {
            throw new ArgumentNullException(nameof(thread));
        }
        _context.Threads.Remove(thread);
        _context.SaveChanges();
    }

    bool IThreadRepo.SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public IEnumerable<Threads> GetThreadsByCategoryId(int categoryId)
    {
        return _context.Threads.Where(h => h.CategoryID == categoryId).ToList();
    }
}