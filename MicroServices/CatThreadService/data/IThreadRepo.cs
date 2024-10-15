using System;

namespace CatThreadService.Data;

public interface IThreadRepo
{
    IEnumerable<Thread> GetAllThread();
    Thread GetThreadById(int id);
    void CreateThread(Thread thread);
    void UpdateThread(Thread thread);
    void DeleteThread(int id);
    bool SaveChanges();
}
