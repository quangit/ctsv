using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class ConductFormBO
    {
        private DSAContext dsaContext;
        public ConductFormBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public List<ConductForm> GetListConductForm()
        {
            try
            {
                return dsaContext.ConductForms.OrderByDescending(c => c.IdConductForm).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ConductForm GetConductForm(int idConductForm)
        {
            return dsaContext.ConductForms.Find(idConductForm);
        }

        public bool UpdateConductForm(int idConductForm, string name, string note)
        {
            try
            {
                ConductForm conductForm = dsaContext.ConductForms.Find(idConductForm);
                conductForm.Name = name;
                conductForm.Note = note;
                dsaContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Add(string name, string note)
        {
            try
            {
                ConductForm conductForm = new ConductForm(name, note);
                dsaContext.ConductForms.Add(conductForm);
                dsaContext.SaveChanges();
                return conductForm.IdConductForm;
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteConductForm(int id)
        {
            using (var context = dsaContext.Database.BeginTransaction())
            {
                try
                {

                    ConductForm conductForm = dsaContext.ConductForms.Find(id);
                    if (conductForm != null)
                    {
                        dsaContext.ConductItems.RemoveRange(dsaContext.ConductItems.Where(i => i.IdConductForm == id));
                        dsaContext.ConductResults.RemoveRange(dsaContext.ConductResults.Where(c => c.ConductEvent.IdConductForm == id));
                        dsaContext.ConductSchedules.RemoveRange(dsaContext.ConductSchedules.Where(s => s.ConductEvent.IdConductForm == id));
                        dsaContext.ConductEvents.RemoveRange(dsaContext.ConductEvents.Where(e => e.IdConductForm == id));
                        dsaContext.ConductForms.Remove(conductForm);
                        dsaContext.SaveChanges();
                        context.Commit();
                        return true;
                    }
                    else
                    {
                        context.Rollback();
                        return false;
                    }
                }
                catch
                {
                    context.Rollback();
                    return false;
                }
            }
        }
    }
}