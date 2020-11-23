using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Helpers
{
    public class CodeGenerator
    {
        private DatabaseContext db = new DatabaseContext();
        public int ReturnExpertCode()
        {

            Expert expert = db.Experts.Where(c => c.IsDeleted == false).OrderByDescending(current => current.Code).FirstOrDefault();

            if (expert != null)
            {
                return expert.Code + 1;
            }
            return 1;
        }

 
    }
}