using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.Domain.Interfaces
{
    public interface IEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
