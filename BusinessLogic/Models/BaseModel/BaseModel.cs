
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models.BaseModel
{
    public abstract class BaseModel<T>
    {
        [Key]

        public T Id { get; set; }
        public string Name { get; set; }
        public DateTime Dateadded { get; set; }
        public DateTime DateModified { get; set; }

    }
}
