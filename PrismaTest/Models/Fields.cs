using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Fields
    {//TODO: Field Model...

        //has foreign key of FormId in this 1-M relationship
        public int Id { get; set; }
        public string Note { get; set; }  //Must only be numeric -- will fix with regex later 

        //relationship
        public int FormId { get; set; }
        public Forms Form { get; set; } = null!;

        //constructors

        public Fields()
        {

        }

        public Fields(string field)
        {
            Note = field;
        }

    }
}
