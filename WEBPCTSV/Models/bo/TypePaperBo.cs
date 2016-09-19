namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class TypePaperBo
    {
        readonly DSAContext context = new DSAContext();

        public void Delete(int idTypePaper)
        {
            TypePaper typePaper = this.context.TypePapers.Single(t => t.IdTypePaper == idTypePaper);
            this.context.TypePapers.Remove(typePaper);
            this.context.SaveChanges();
        }

        public string GetLinkFileTypePaperStudent(int idTypePapeStudent)
        {
            return this.context.TypePaperStudents.Single(t => t.IdTypePaperStudent == idTypePapeStudent).LinkFile;
        }

        public List<TypePaper> GetListPaper()
        {
            return this.context.TypePapers.ToList();
        }

        public void Insert(FormCollection form)
        {
            TypePaper typePaper = new TypePaper();
            typePaper.TypePaperName = form["TypePaperName"];
            typePaper.Note = form["Note"];
            this.context.TypePapers.Add(typePaper);
            this.context.SaveChanges();
        }

        public void Update(int idTypePaper, FormCollection form)
        {
            TypePaper typePaper = this.context.TypePapers.Single(t => t.IdTypePaper == idTypePaper);
            typePaper.TypePaperName = form["TypePaperName"];
            typePaper.Note = form["Note"];
            this.context.SaveChanges();
        }

        public void UpdateLinkFileTypePaperStudent(int idTypePapeStudent, string linkFile)
        {
            TypePaperStudent typePaperStudent =
                this.context.TypePaperStudents.Single(t => t.IdTypePaperStudent == idTypePapeStudent);
            typePaperStudent.LinkFile = linkFile;
            this.context.SaveChanges();
        }

        public bool UpdateTypePaperStudent(int idTypePaper, int idStudent)
        {
            try
            {
                TypePaperStudent typePaperStudent = null;
                try
                {
                    typePaperStudent =
                        this.context.TypePaperStudents.Where(
                            t => t.IdTypePaper == idTypePaper && t.IdStudent == idStudent).First();
                    if (typePaperStudent != null)
                    {
                        this.context.TypePaperStudents.Remove(typePaperStudent);
                        this.context.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                }

                typePaperStudent = new TypePaperStudent();
                typePaperStudent.IdStudent = idStudent;
                typePaperStudent.IdTypePaper = idTypePaper;
                typePaperStudent.SubmitTime = DateTime.Now;
                this.context.TypePaperStudents.Add(typePaperStudent);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}