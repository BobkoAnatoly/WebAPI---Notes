using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model.Models
{
    public class User: BaseModel.BaseModel
    {
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
