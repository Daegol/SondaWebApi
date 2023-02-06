using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbEntities
{
    public class AllegroToken : BaseEntity
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
        public string Scope { get; set; }
        public string AllegroApi { get; set; }
        public string Jti { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
