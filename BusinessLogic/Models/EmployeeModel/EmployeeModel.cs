
using BusinessLogic.Models.BaseModel;

namespace BusinessLogic.Models.EmployeeModel
{
    public class EmployeeModel : BaseModel<Guid>
    {
        //
        public decimal Salary { get; set; }
    }
}
