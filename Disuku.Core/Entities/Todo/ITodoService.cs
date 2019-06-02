namespace Disuku.Core.Services.Todo
{
    public interface ITodoService
    {
        void CreateTodo(ulong userId, string details);
        void CompleteTodo(ulong userId, int id);
        void ListTodo(ulong userId);
        void DeleteTodo(ulong userId, int id);
    }
}