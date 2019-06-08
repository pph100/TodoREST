using System;
using Xamarin.Forms;

namespace TodoREST
{
    public class TodoItem
    {
        public string ID { get; set; }

        public string Author { get; set; }

        public string TodoTask { get; set; }

        public bool Done { get; set; }

        public string DttmCreated { get; set; }

        public string DttmLastUpdated { get; set; }

        public string CompletedBy { get; set; }

        public bool Urgent { get; set; }

        // public string Due { get; set; }

        public DateTime DueDate { get; set; }

    }
}
