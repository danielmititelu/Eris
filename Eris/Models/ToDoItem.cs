namespace Eris.Models
{
    public partial class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
