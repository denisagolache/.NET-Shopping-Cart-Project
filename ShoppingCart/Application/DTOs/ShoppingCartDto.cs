using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public int TotalItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
