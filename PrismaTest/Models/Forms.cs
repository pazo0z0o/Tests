namespace Models
{
    public class Forms
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } // Limit 150 characters 
        public DateTime DateOfCreation { get; set; } 
        public DateTime LastUpdated { get; set; } //Can be null until form is updated--Everytime a new Field Line is created
                                                   //we need to update it 

        //relation
        public List<Fields> Fields { get; set; } = new();

        //constructors

        public Forms()
        {

        }

        public Forms(string title, string descr)
        {
            Title = title;
            Description = descr;
            DateOfCreation = DateTime.Now.Date;

        }
    
    }
}

