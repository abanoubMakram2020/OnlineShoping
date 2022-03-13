using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoping.Domain.Entities
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
 
    }
}
