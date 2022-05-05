using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace lab3.Models
{
    public class department
    {

         [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.None)]
       
        public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; } 
        public virtual ICollection<student> Students { get; set; }
        public department()
        {
            Students = new HashSet<student>();
        }
    }
}
