namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ConductFormBO
    {
        private readonly DSAContext dsaContext;

        public ConductFormBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public int Add(string name, string note)
        {
            try
            {
                ConductForm conductForm = new ConductForm(name, note);
                this.dsaContext.ConductForms.Add(conductForm);
                this.dsaContext.SaveChanges();
                return conductForm.IdConductForm;
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteConductForm(int id)
        {
            using (var context = this.dsaContext.Database.BeginTransaction())
            {
                try
                {
                    ConductForm conductForm = this.dsaContext.ConductForms.Find(id);
                    if (conductForm != null)
                    {
                        this.dsaContext.ConductItems.RemoveRange(
                            this.dsaContext.ConductItems.Where(i => i.IdConductForm == id));
                        this.dsaContext.ConductResults.RemoveRange(
                            this.dsaContext.ConductResults.Where(c => c.ConductEvent.IdConductForm == id));
                        this.dsaContext.ConductSchedules.RemoveRange(
                            this.dsaContext.ConductSchedules.Where(s => s.ConductEvent.IdConductForm == id));
                        this.dsaContext.ConductEvents.RemoveRange(
                            this.dsaContext.ConductEvents.Where(e => e.IdConductForm == id));
                        this.dsaContext.ConductForms.Remove(conductForm);
                        this.dsaContext.SaveChanges();
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

        public ConductForm GetConductForm(int idConductForm)
        {
            return this.dsaContext.ConductForms.Find(idConductForm);
        }

        public List<ConductForm> GetListConductForm()
        {
            try
            {
                return this.dsaContext.ConductForms.OrderByDescending(c => c.IdConductForm).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateConductForm(int idConductForm, string name, string note)
        {
            try
            {
                ConductForm conductForm = this.dsaContext.ConductForms.Find(idConductForm);
                conductForm.Name = name;
                conductForm.Note = note;
                this.dsaContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}