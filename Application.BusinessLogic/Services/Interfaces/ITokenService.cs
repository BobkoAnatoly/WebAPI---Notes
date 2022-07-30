using Application.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
