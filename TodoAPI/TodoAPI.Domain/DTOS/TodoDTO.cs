namespace TodoAPI.DTOS
{
    public class TodoDTO
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
}
