using CodeX.Core.DAL;
using CodeX.Core.Entity;
using System;
using System.Threading.Tasks;

namespace CodeX.Code.BAL
{
    public class PersonalBAL
    {
        PersonalDAL objPersonalDAL;
        public PersonalBAL()
        {
            objPersonalDAL = new PersonalDAL();
        }

        public void PersonalSave(Personal personal)
        {
            objPersonalDAL.PersonalSave(personal);
        }
    }
}
