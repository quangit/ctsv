using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class TypePaperBo
    {
        DSAContext context = new DSAContext();
        public List<TypePaper> GetListPaper()
        {
            return context.TypePapers.ToList();
        }

        public bool UpdateTypePaperStudent(int idTypePaper , int idStudent)
        {
            try
            {
                TypePaperStudent typePaperStudent = null;
                try
                {
                    typePaperStudent = context.TypePaperStudents.Where(t => t.IdTypePaper == idTypePaper && t.IdStudent == idStudent).First();
                    if(typePaperStudent!=null)
                    {
                        context.TypePaperStudents.Remove(typePaperStudent);
                        context.SaveChanges();
                        return true;
                    }
                }
                catch { }

                typePaperStudent = new TypePaperStudent();
                typePaperStudent.IdStudent = idStudent;
                typePaperStudent.IdTypePaper = idTypePaper;
                typePaperStudent.SubmitTime = DateTime.Now;
                context.TypePaperStudents.Add(typePaperStudent);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public void UpdateLinkFileTypePaperStudent(int idTypePapeStudent,string linkFile)
        {
            TypePaperStudent typePaperStudent = context.TypePaperStudents.Single(t => t.IdTypePaperStudent == idTypePapeStudent);
            typePaperStudent.LinkFile = linkFile;
            context.SaveChanges();
        }

        public string GetLinkFileTypePaperStudent(int idTypePapeStudent)
        {
            return context.TypePaperStudents.Single(t => t.IdTypePaperStudent == idTypePapeStudent).LinkFile;
        }

        public void Insert(FormCollection form)
        {
            TypePaper typePaper = new TypePaper();
            typePaper.TypePaperName = form["TypePaperName"];
            typePaper.Note = form["Note"];
            context.TypePapers.Add(typePaper);
            context.SaveChanges();
        }

        public void Update(int idTypePaper, FormCollection form)
        {
            TypePaper typePaper = context.TypePapers.Single(t=>t.IdTypePaper == idTypePaper);
            typePaper.TypePaperName = form["TypePaperName"];
            typePaper.Note = form["Note"];
            context.SaveChanges();
        }

        public void Delete(int idTypePaper)
        {
            TypePaper typePaper = context.TypePapers.Single(t => t.IdTypePaper == idTypePaper);
            context.TypePapers.Remove(typePaper);
            context.SaveChanges();
        }
    }
}