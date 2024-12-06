using System;

namespace CatThreadService.Data;

public interface IThreadRepo
{
    IEnumerable<Threads> GetAllThread();
    Threads GetThreadById(int id);
    void CreateThread(Threads thread);
    void UpdateThread(Threads thread);
    void DeleteThread(int id);
    bool SaveChanges();

    IEnumerable<Threads> GetThreadsByCategoryId(int categoryId);
}
