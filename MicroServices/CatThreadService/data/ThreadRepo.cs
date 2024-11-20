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
        throw new NotImplementedException();
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
}