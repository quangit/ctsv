using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WEBPCTSV
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // Admin routes
            #region Controller manage news scholarship
            routes.MapRoute(
               "ManageNewsScholarship",
               "QuanLy/QuanLyHocBong",
               new { controller = "ManageNewsScholarship", action = "NewsScholarship" }
               );
            routes.MapRoute(
               "AddNewsScholarship",
               "QuanLy/QuanLyHocBong/Them",
               new { controller = "ManageNewsScholarship", action = "AddNewsScholarship" }
               );
            routes.MapRoute(
               "UpdateNewsScholarship",
               "QuanLy/QuanLyHocBong/CapNhat",
               new { controller = "ManageNewsScholarship", action = "UpdateNewsScholarship" }
               );
            routes.MapRoute(
               "UpdateDeleteNewsScholarship",
               "QuanLy/QuanLyHocBong/Xoa",
               new { controller = "ManageNewsScholarship", action = "DeleteNewsScholarship" }
               );
            #endregion
            #region Controller manage news
            routes.MapRoute(
               "ManageNews",
               "QuanLy/QuanLyTinTuc",
               new { controller = "ManageNews", action = "News" }
               );
            routes.MapRoute(
               "AddNews",
               "QuanLy/QuanLyTinTuc/Them",
               new { controller = "ManageNews", action = "AddNews" }
               );
            routes.MapRoute(
               "UpdateNews",
               "QuanLy/QuanLyTinTuc/CapNhat",
               new { controller = "ManageNews", action = "UpdateNews" }
               );
            routes.MapRoute(
               "DeleteNews",
               "QuanLy/QuanLyTinTuc/Xoa",
               new { controller = "ManageNews", action = "DeleteNews" }
               );
            routes.MapRoute(
              "PinToTop",
              "QuanLy/QuanLyTinTuc/GhimTin",
              new { controller = "ManageNews", action = "PinToTop" }
              );
            #endregion
            #region Controller manage news job
            routes.MapRoute(
               "ManageNewsJob",
               "QuanLy/QuanLyTuyenDung",
               new { controller = "ManageNewsJob", action = "NewsJob" }
               );
            routes.MapRoute(
               "AddNewsJob",
               "QuanLy/QuanLyTuyenDung/Them",
               new { controller = "ManageNewsJob", action = "AddNewsJob" }
               );
            routes.MapRoute(
               "UpdateNewsJob",
               "QuanLy/QuanLyTuyenDung/CapNhat",
               new { controller = "ManageNewsJob", action = "UpdateNewsJob" }
               );
            routes.MapRoute(
               "DeleteNewsJob",
               "QuanLy/QuanLyTuyenDung/Xoa",
               new { controller = "ManageNewsJob", action = "DeleteNewsJob" }
               );
            #endregion
            #region Controller manage news event
            routes.MapRoute(
               "ManageNewsEvent",
               "QuanLy/QuanLySuKien",
               new { controller = "ManageNewsEvent", action = "NewsEvent" }
               );
            routes.MapRoute(
               "AddNewsEvent",
               "QuanLy/QuanLySuKien/Them",
               new { controller = "ManageNewsEvent", action = "AddNewsEvent" }
               );
            routes.MapRoute(
               "UpdateNewsEvent",
               "QuanLy/QuanLySuKien/CapNhat",
               new { controller = "ManageNewsEvent", action = "UpdateNewsEvent" }
               );
            routes.MapRoute(
               "DeleteNewsEvent",
               "QuanLy/QuanLySuKien/Xoa",
               new { controller = "ManageNewsEvent", action = "DeleteNewsEvent" }
               );
            #endregion
            #region Controller manage alumni
            routes.MapRoute(
               "ManageNewsAlumni",
               "QuanLy/QuanLyCuuSinhVien",
               new { controller = "ManageAlumni", action = "NewsAlumni" }
               );
            routes.MapRoute(
               "AddNewsAlumni",
               "QuanLy/QuanLyCuuSinhVien/Them",
               new { controller = "ManageAlumni", action = "AddNewsAlumni" }
               );
            routes.MapRoute(
               "UpdateNewsAlumni",
               "QuanLy/QuanLyCuuSinhVien/CapNhat",
               new { controller = "ManageAlumni", action = "UpdateNewsAlumni" }
               );
            routes.MapRoute(
               "DeleteNewsAlumni",
               "QuanLy/QuanLyCuuSinhVien/Xoa",
               new { controller = "ManageAlumni", action = "DeleteNewsAlumni" }
               );
            routes.MapRoute(
               "AlumniInfo",
               "QuanLy/ThongTinCSV",
               new { controller = "ManageAlumni", action = "AlumniInfo" }
               );
            #endregion
            #region Controller manage document
            routes.MapRoute(
               "ManageDocument",
               "QuanLy/QuanLyVanBan",
               new { controller = "ManageDocument", action = "Document" }
               );
            routes.MapRoute(
               "AddDocument",
               "QuanLy/QuanLyVanBan/Them",
               new { controller = "ManageDocument", action = "AddDocument" }
               );
            routes.MapRoute(
               "UpdateDocument",
               "QuanLy/QuanLyVanBan/CapNhat",
               new { controller = "ManageDocument", action = "UpdateDocument" }
               );
            routes.MapRoute(
               "DeleteDocument",
               "QuanLy/QuanLyVanBan/Xoa",
               new { controller = "ManageDocument", action = "DeleteDocument" }
               );
            #endregion
            #region Controller manage resource
            routes.MapRoute(
               "ManageResource",
               "QuanLy/CauHinhWebsite",
               new { controller = "ManageResource", action = "ManageResource" }
               );
            #endregion
            #region Controller manage banner
            routes.MapRoute(
               "ManageBanner",
               "QuanLy/QuanLyBanner",
               new { controller = "ManageBanner", action = "Banner" }
               );
            routes.MapRoute(
               "AddBanner",
               "QuanLy/QuanLyBanner/Them",
               new { controller = "ManageBanner", action = "AddBanner" }
               );
            routes.MapRoute(
               "UpdateBanner",
               "QuanLy/QuanLyBanner/CapNhat",
               new { controller = "ManageBanner", action = "UpdateBanner" }
               );
            routes.MapRoute(
               "DeleteBanner",
               "QuanLy/QuanLyBanner/Xoa",
               new { controller = "ManageBanner", action = "DeleteBanner" }
               );
            #endregion
            #region manage lecturer
            routes.MapRoute(
               "ListLecturer",
               "QuanLy/QuanLyGVCN",
               new { controller = "ManageLecturer", action = "ListLecturer" }
               );
            routes.MapRoute(
               "AddLecturer",
               "QuanLy/QuanLyGVCN/Them",
               new { controller = "ManageLecturer", action = "AddLecturer" }
               );
            routes.MapRoute(
               "ImportExcel",
               "QuanLy/QuanLyGVCN/ImportExcel",
               new { controller = "ManageLecturer", action = "ImportExcel" }
               );
            routes.MapRoute(
               "ExportExcel",
               "QuanLy/QuanLyGVCN/ExportExcel",
               new { controller = "ManageLecturer", action = "ExportExcel" }
               );
            routes.MapRoute(
               "DeleteLecturerAjax",
               "QuanLy/QuanLyGVCN/Xoa",
               new { controller = "ManageLecturer", action = "DeleteLecturerAjax" }
               );
            routes.MapRoute(
               "UpdateLecturer",
               "QuanLy/QuanLyGVCN/CapNhat",
               new { controller = "ManageLecturer", action = "UpdateLecturer" }
               );
            routes.MapRoute(
               "GetLecturerDocumentSemesterList",
               "QuanLy/QuanLyGVCN/GiayToNopTrongKy",
               new { controller = "ManageLecturer", action = "GetLecturerDocumentSemesterList" }
               );
            routes.MapRoute(
               "DetailClassOwnerAjax",
               "QuanLy/QuanLyGVCN/ChiTietLopChuNhiem",
               new { controller = "ManageLecturer", action = "DetailClassOwnerAjax" }
               );
            routes.MapRoute(
               "CapNhatLopChuNhiem",
               "QuanLy/QuanLyGVCN/CapNhatLopChuNhiem",
               new { controller = "ManageLecturer", action = "UpdateClassOwnerAjax" }
               );
            routes.MapRoute(
               "DeleteClassOwnerAjax",
               "QuanLy/QuanLyGVCN/XoaLopChuNhiem",
               new { controller = "ManageLecturer", action = "DeleteClassOwnerAjax" }
               );
            routes.MapRoute(
               "AddClassOwnerAjax",
               "QuanLy/QuanLyGVCN/ThemLopChuNhiem",
               new { controller = "ManageLecturer", action = "AddClassOwnerAjax" }
               );
            #endregion
            #region Controller manage banner
            routes.MapRoute(
               "ManageQuestionr",
               "QuanLy/QuanLyCauHoi",
               new { controller = "ManageQuestion", action = "Question" }
               );
            routes.MapRoute(
               "AddCommonQuestion",
               "QuanLy/QuanLyCauHoi/Them",
               new { controller = "ManageQuestion", action = "AddCommonQuestion" }
               );
            routes.MapRoute(
               "AnswerQuestion",
               "QuanLy/QuanLyCauHoi/CapNhat",
               new { controller = "ManageQuestion", action = "AnswerQuestion" }
               );
            routes.MapRoute(
               "DeleteQuestion",
               "QuanLy/QuanLyCauHoi/Xoa",
               new { controller = "ManageQuestion", action = "DeleteQuestion" }
               );
            #endregion
            #region Controller manage conduct
            routes.MapRoute(
              "ImportConductResult",
              "QuanLy/QuanLyRenLuyen/ImportConductResult",
              new { controller = "ManageConduct", action = "ImportConductResult" }
              );
            routes.MapRoute(
              "ManageConduct",
              "QuanLy/QuanLyRenLuyen",
              new { controller = "ManageConduct", action = "Index" }
              );
            routes.MapRoute(
              "ConductFaculty",
              "QuanLy/QuanLyRenLuyen/DanhSachLop",
              new { controller = "ManageConduct", action = "ConductFaculty" }
              );
            routes.MapRoute(
              "ConductClassList",
              "QuanLy/QuanLyRenLuyen/DanhGiaLop",
              new { controller = "ManageConduct", action = "ConductClassList" }
              );
            routes.MapRoute(
              "ConductEvaluate",
              "QuanLy/QuanLyRenLuyen/ChinhSuaSinhVien",
              new { controller = "ManageConduct", action = "ConductEvaluate" }
              );
            routes.MapRoute(
              "ConductStudentSubmit",
              "QuanLy/QuanLyRenLuyen/ChinhSua",
              new { controller = "ManageConduct", action = "ConductStudentSubmit" }
              );
            routes.MapRoute(
              "ConductEvent",
              "QuanLy/QuanLyRenLuyen/DotXetRenLuyen",
              new { controller = "ManageConduct", action = "ConductEvent" }
              );
            routes.MapRoute(
              "AddConductEvent",
              "QuanLy/QuanLyRenLuyen/DotXetRenLuyen/Them",
              new { controller = "ManageConduct", action = "AddConductEvent" }
              );
            routes.MapRoute(
              "DeleteConductEvent",
              "QuanLy/QuanLyRenLuyen/DotXetRenLuyen/Xoa",
              new { controller = "ManageConduct", action = "DeleteConductEvent" }
              );
            routes.MapRoute(
              "UpdateConductEventSchedule",
              "QuanLy/QuanLyRenLuyen/DotXetRenLuyen/CapNhatLich",
              new { controller = "ManageConduct", action = "UpdateConductEventSchedule" }
              );
            routes.MapRoute(
              "UpdateConductEvent",
              "QuanLy/QuanLyRenLuyen/DotXetRenLuyen/CapNhat",
              new { controller = "ManageConduct", action = "UpdateConductEvent" }
              );
            routes.MapRoute(
              "AddConductEventSchedule",
              "QuanLy/QuanLyRenLuyen/DotXetRenLuyen/ThemLich",
              new { controller = "ManageConduct", action = "AddConductEventSchedule" }
              );
            routes.MapRoute(
              "DeleteConductEventSchedule",
              "QuanLy/QuanLyRenLuyen/DotXetRenLuyen/XoaLich",
              new { controller = "ManageConduct", action = "DeleteConductEventSchedule" }
              );
            routes.MapRoute(
              "ConductForm",
              "QuanLy/QuanLyRenLuyen/MauXetRenLuyen",
              new { controller = "ManageConduct", action = "ConductForm" }
              );
            routes.MapRoute(
              "AddConductForm",
              "QuanLy/QuanLyRenLuyen/MauXetRenLuyen/Them",
              new { controller = "ManageConduct", action = "AddConductForm" }
              );
            routes.MapRoute(
              "UpdateConductForm",
              "QuanLy/QuanLyRenLuyen/MauXetRenLuyen/CapNhat",
              new { controller = "ManageConduct", action = "UpdateConductForm" }
              );
            routes.MapRoute(
              "DeleteConductForm",
              "QuanLy/QuanLyRenLuyen/MauXetRenLuyen/Xoa",
              new { controller = "ManageConduct", action = "DeleteConductForm" }
              );
            routes.MapRoute(
              "DeleteConductItem",
              "QuanLy/QuanLyRenLuyen/MauXetRenLuyen/XoaMucDanhGia",
              new { controller = "ManageConduct", action = "DeleteConductItem" }
              );
            #endregion
            #region controller Account
            routes.MapRoute(
               "LoginAdmin",
               "QuanLy",
               new { controller = "Account", action = "LoginAdmin" }
               );
            routes.MapRoute(
               "LoginAdminOldRoute",
               "Admin",
               new { controller = "Account", action = "LoginAdmin" }
               );
            routes.MapRoute(
              "Profile",
              "QuanLy/ThongTin",
              new { controller = "ManageAccount", action = "AccountInformation" }
              );
            #endregion
            #region controller ManageRequest
            routes.MapRoute(
               "ListAllRequestPaper",
               "QuanLy/DanhSachYeuCau",
               new { controller = "ManageRequest", action = "ListAllRequestPaper" }
               );
            #endregion

            //Client routes
            #region controller Account
            routes.MapRoute(
               "ClientLogin",
               "Login",
               new { controller = "Account", action = "Login" }
               );
            routes.MapRoute(
               "ClientLoginAjax",
               "LoginAjax",
               new { controller = "Account", action = "LoginAjax" }
               );
            routes.MapRoute(
               "ClientLogout",
               "Logout",
               new { controller = "Account", action = "Logout" }
               );
            #endregion

            #region controller conduct
            routes.MapRoute(
               "ClientConductHome",
               "DanhGiaRenLuyen",
               new { controller = "Conduct", action = "Home" }
               );
            routes.MapRoute(
               "ClientConductEvaluate",
               "DanhGiaRenLuyen/DanhGiaCaNhan",
               new { controller = "Conduct", action = "ConductEvaluate" }
               );
            routes.MapRoute(
               "ClientConductEvaluateStudent",
               "DanhGiaRenLuyen/DanhGiaSinhVien",
               new { controller = "Conduct", action = "ConductEvaluateStudent" }
               );
            routes.MapRoute(
               "ClientClassEvaluate",
               "DanhGiaRenLuyen/DanhGiaLop",
               new { controller = "Conduct", action = "ClassEvaluate" }
               );
            routes.MapRoute(
               "ClientConductSummary",
               "DanhGiaRenLuyen/BangDiem",
               new { controller = "Conduct", action = "ConductSummary" }
               );
            routes.MapRoute(
               "ClientConductDetail",
               "DanhGiaRenLuyen/KetQuaDanhGia",
               new { controller = "Conduct", action = "ConductDetail" }
               );
            routes.MapRoute(
               "ClientClassesEvaluate",
               "DanhGiaRenLuyen/DanhSachLop",
               new { controller = "Conduct", action = "ClassesEvaluate" }
               );
            routes.MapRoute(
               "ClientFacultyEvaluate",
               "DanhGiaRenLuyen/DanhSachKhoa",
               new { controller = "Conduct", action = "FacultyEvaluate" }
               );
            routes.MapRoute(
               "ConductApproveStudent",
               "DanhGiaRenLuyen/DuyetDiemSinhVien",
               new { controller = "Conduct", action = "ConductApproveStudent" }
               );
            routes.MapRoute(
               "ExportListStudent",
               "DanhGiaRenLuyen/XuatBangDiemTapThe",
               new { controller = "Conduct", action = "ExportListStudent" }
               );
            #endregion

            #region controller Introduction
            routes.MapRoute(
               "Introduction",
               "GioiThieu",
               new { controller = "Introduction", action = "FunctionAndDuty" }
               );
            routes.MapRoute(
               "FunctionAndDuty",
               "GioiThieu/ChucNangNhiemVu",
               new { controller = "Introduction", action = "FunctionAndDuty" }
               );
            routes.MapRoute(
               "HumanResource",
               "GioiThieu/PhanCongNhiemVu",
               new { controller = "Introduction", action = "HumanResource" }
               );
            #endregion

            #region controller news
            routes.MapRoute(
               "ClientListNews",
               "ThongBao",
               new { controller = "News", action = "ListNews" }
               );
            routes.MapRoute(
               "ClientDetailNews",
               "ThongBao/ChiTiet",
               new { controller = "News", action = "DetailNews" }
               );

            routes.MapRoute(
               "ClientListNewsLegalEducation",
               "GiaoDucPhapLuat",
               new { controller = "News", action = "ListNewsLegalEducation" }
               );
            routes.MapRoute(
               "ClientDetailNewsLegalEducation",
               "GiaoDucPhapLuat/ChiTiet",
               new { controller = "News", action = "DetailNewsLegalEducation" }
               );
            routes.MapRoute(
               "ClientListNewsMedicalStudent",
               "YTeHocDuong",
               new { controller = "News", action = "ListNewsMedicalStudent" }
               );
            routes.MapRoute(
               "ClientDetailNewsMedicalStudent",
               "YTeHocDuong/ChiTiet",
               new { controller = "News", action = "DetailNewsMedicalStudent" }
               );
            routes.MapRoute(
               "ClientListNewsRelationCompany",
               "HTSV-QHDN",
               new { controller = "News", action = "ListNewsRelationCompany" }
               );
            routes.MapRoute(
               "ClientDetailNewsRelationCompany",
               "HTSV-QHDN/ChiTiet",
               new { controller = "News", action = "DetailNewsRelationCompany" }
               );
            routes.MapRoute(
               "ClientListNewsAlumni",
               "CuuSinhVien/BaiViet",
               new { controller = "News", action = "ListNewsAlumni" }
               );
            routes.MapRoute(
               "ClientDetailNewsAlumni",
               "CuuSinhVien/ChiTiet",
               new { controller = "News", action = "DetailNewsAlumni" }
               );
            routes.MapRoute(
               "ClientAlumniIntro",
               "CuuSinhVien",
               new { controller = "News", action = "AlumniIntro" }
               );
            routes.MapRoute(
               "ClientAlumniStar",
               "CuuSinhVien/GuongMatCSV",
               new { controller = "News", action = "AlumniStar" }
               );
            #endregion

            #region controller scholarship
            routes.MapRoute(
               "ClientNewsScholarShip",
               "HocBong",
               new { controller = "NewsScholarShip", action = "ListNewsScholarship" }
               );
            routes.MapRoute(
               "ClientDetailNewsScholarShip",
               "HocBong/ChiTiet",
               new { controller = "NewsScholarShip", action = "DetailNewsScholarship" }
               );
            #endregion

            #region controller news event
            routes.MapRoute(
               "ClientNewsEvent",
               "SuKien",
               new { controller = "NewsEvent", action = "ListNewsEvent" }
               );
            routes.MapRoute(
               "ClientDetailNewsEvent",
               "SuKien/ChiTiet",
               new { controller = "NewsEvent", action = "DetailNewsEvent" }
               );
            #endregion

            #region controller news job
            routes.MapRoute(
               "ClientNewsJob",
               "TuyenDung",
               new { controller = "NewsJob", action = "ListNewsJob" }
               );
            routes.MapRoute(
               "ClientDetailNewsJob",
               "TuyenDung/ChiTiet",
               new { controller = "NewsJob", action = "DetailNewsJob" }
               );
            #endregion

            #region controller document
            routes.MapRoute(
               "ClientDocument",
               "VanBanBieuMau",
               new { controller = "Document", action = "ListDocument" }
               );
            routes.MapRoute(
               "ClientDetailDocument",
               "VanBanBieuMau/ChiTiet",
               new { controller = "Document", action = "DetailDocument" }
               );
            #endregion

            #region controller lecturer
            routes.MapRoute(
               "ClientLecturerList",
               "CongTac/DanhSachGVCN",
               new { controller = "Lecturer", action = "LecturerList" }
               );
            routes.MapRoute(
              "ClientLecturerOperation",
              "CongTac/HoatDongGVCN",
              new { controller = "Lecturer", action = "LecturerOperation" }
              );
            #endregion

            #region controller question
            routes.MapRoute(
              "ClientRequestQuestion",
              "TuVan/DatCauHoi",
              new { controller = "Question", action = "RequestQuestion" }
              );
            routes.MapRoute(
             "ClientDetailQuestion",
             "TuVan/DanhSachCauHoi/ChiTiet",
             new { controller = "Question", action = "DetailQuestion" }
             );
            routes.MapRoute(
              "ClientListQuestion",
              "TuVan/DanhSachCauHoi",
              new { controller = "Question", action = "ListQuestion" }
              );
            routes.MapRoute(
              "ClientCommonQuestion",
              "TuVan/CauHoiThuongGap",
              new { controller = "Question", action = "CommonQuestion" }
              );
            #endregion

            #region controller search
            routes.MapRoute(
               "ClientSearchResult",
               "TimKiem/KetQuaTimKiem",
               new { controller = "Search", action = "SearchResult" }
               );
            #endregion

            #region controller home
            routes.MapRoute(
               "ClientHome",
               "TrangChu",
               new { controller = "Home", action = "Index" }
               );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            #endregion

            

        }
    }
}