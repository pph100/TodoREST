using System;

namespace TodoREST
{
    public class TodoItem
    {
        public string ID { get; set; }

        public string Author { get; set; }

        public string TodoTask { get; set; }

        public bool Done { get; set; }

        public String DttmCreated { get; set; }

        public String DttmLastUpdated { get; set; }

        public String CompletedBy { get; set; }
    }
}
