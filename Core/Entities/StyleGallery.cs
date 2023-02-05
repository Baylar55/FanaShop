using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StyleGallery:BaseEntity
    {
        public string PhotoName { get; set; }
        public string CoverLetter { get; set; }
        public int Order { get; set; }
    }
}
