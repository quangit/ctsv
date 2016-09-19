namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class ClassBO
    {
        readonly DSAContext context = new DSAContext();

        private readonly DSAContext dsaContext;

        public ClassBO()
        {
        }

        public ClassBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public void Delete(int idClass)
        {
            Class cl = this.context.Classes.Single(c => c.IdClass == idClass);
            this.context.Classes.Remove(cl);
            this.context.SaveChanges();
        }

        public List<Class> Get()
        {
            return this.context.Classes.ToList();
        }

        public Class Get(string className)
        {
            try
            {
                return this.context.Classes.Single(c => c.ClassName.Equals(className));
            }
            catch
            {
            }

            return null;
        }

        public List<Class> Get(int idFaculty, int idCourse)
        {
            return this.context.Classes.Where(c => c.IdCourse == idCourse && c.IdFaculty == idFaculty).ToList();
        }

        public Class GetClass(int idClass)
        {
            try
            {
                return this.dsaContext.Classes.SingleOrDefault(c => c.IdClass == idClass);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Class> GetClassByFaculty(int idFaculty)
        {
            try
            {
                return this.context.Classes.Where(c => c.IdFaculty == idFaculty).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<Class> GetClassCLC()
        {
            List<Class> listClassCLC = this.context.Classes.Where(c => c.ClassName.ToUpper().Contains("CLC")).ToList();
            return listClassCLC;
        }

        // Get list class have admission year minimum is courseAdmission
        public List<Class> GetClassesByIdFaculty(int idFaculty)
        {
            try
            {
                return
                    this.dsaContext.Classes.Where(c => c.Faculty.IdFaculty == idFaculty)
                        .OrderBy(c => c.ClassName)
                        .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Class> GetClassesByLecturerNumber(string lecturerNumber)
        {
            try
            {
                List<Class> classes = new List<Class>();
                LecturerBO lecturerBO = new LecturerBO(this.dsaContext);
                Lecturer lecturer = lecturerBO.GetLecturerByNumber(lecturerNumber);
                List<LecturerClass> lecturerClasses = lecturerBO.GetLecturerClasses(lecturer.IdLecturer);
                foreach (LecturerClass lecturerClass in lecturerClasses)
                {
                    classes.Add(lecturerClass.Class);
                }

                return classes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetNameClass(int idCourse)
        {
            Course course = this.context.Courses.Single(c => c.IdCourse == idCourse);
            return course.CouseName;
        }

        public string GetStringSelectListCourse(int idFaculty, int idCourse)
        {
            List<Class> listClass =
                this.context.Classes.Where(c => c.IdFaculty == idFaculty && c.IdCourse == idCourse).ToList();
            string stringSelectClass = "<select name='selectClass' class='form-control'>";
            foreach (Class classs in listClass)
            {
                stringSelectClass += "<option value=" + classs.IdClass + ">" + classs.ClassName + "<option>";
            }

            stringSelectClass += "</select>";
            return stringSelectClass;
        }

        public void Insert(int idFaculty, int idCourse, string ClassName, int NumberMonthSchool, string MoneyOfMonth)
        {
            try
            {
                Class c = new Class();
                c.IdCourse = idCourse;
                c.IdFaculty = idFaculty;
                c.ClassName = ClassName;
                c.TotalNumberStudent = 0;
                c.NumberMonthSchool = NumberMonthSchool;
                c.MoneyOfMonth = MoneyOfMonth;
                this.context.Classes.Add(c);
                this.context.SaveChanges();
            }
            catch
            {
            }
        }

        public void Insert(FormCollection form)
        {
            Class c = new Class();
            c.ClassName = form["classname"];
            if (form["idCourse"] != string.Empty) c.IdCourse = Convert.ToInt32(form["idCourse"]);
            if (form["idFaculty"] != string.Empty) c.IdFaculty = Convert.ToInt32(form["idFaculty"]);
            if (form["NumberMonthSchool"] != string.Empty) c.NumberMonthSchool = Convert.ToInt32(form["NumberMonthSchool"]);
            c.MoneyOfMonth = form["MoneyOfMonth"];
            this.context.Classes.Add(c);
            this.context.SaveChanges();
        }

        public Class Insert(string className, string courseName, string facultyNumber)
        {
            try
            {
                Class c = new Class();
                c.ClassName = className;
                Course course = new CourseBo().Get(courseName);
                c.IdCourse = course.IdCourse;
                Faculty faculty = new FacultyBO().Get(facultyNumber);
                c.IdFaculty = faculty.IdFaculty;
                this.context.Classes.Add(c);
                this.context.SaveChanges();
                return c;
            }
            catch
            {
            }

            return null;
        }

        public bool IsStudentInClass(int idClass, int idStudent)
        {
            try
            {
                Class classFind = this.GetClass(idClass);
                Student student = new StudentBO(this.dsaContext).GetStudent(idStudent);
                if (student.Class.IdClass == classFind.IdClass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool IsStudentsInSameClass(int idStudent, int idStudentComp)
        {
            try
            {
                StudentBO studentBO = new StudentBO(this.dsaContext);
                Student student = studentBO.GetStudent(idStudent);
                Student studentComp = studentBO.GetStudent(idStudentComp);
                if (student.Class.IdClass == studentComp.Class.IdClass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public void Update(int idClass, FormCollection form)
        {
            Class cl = this.context.Classes.Single(c => c.IdClass == idClass);
            cl.ClassName = form["ClassName"];
            if (form["NumberMonthSchool"] != string.Empty) cl.NumberMonthSchool = Convert.ToInt32(form["NumberMonthSchool"]);
            cl.MoneyOfMonth = form["MoneyOfMonth"];
            this.context.SaveChanges();
        }
    }
}