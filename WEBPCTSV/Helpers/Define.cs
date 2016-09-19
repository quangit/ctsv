namespace WEBPCTSV.Helpers
{
    using System.Collections.Generic;

    public class Define
    {
        public static Dictionary<string, string> linkDetailNews = new Dictionary<string, string>()
                                                                      {
                                                                          {
                                                                              "HocBongKhuyenKhich",
                                                                              "/HocBong/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "HocBongKhac",
                                                                              "/HocBong/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "BieuMau",
                                                                              "/VanBanBieuMau/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "VanBanBo",
                                                                              "/VanBanBieuMau/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "VanBanDHDN",
                                                                              "/VanBanBieuMau/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "VanBanTruong",
                                                                              "/VanBanBieuMau/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "VanBanCTSV",
                                                                              "/VanBanBieuMau/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "ThongBao",
                                                                              "/ThongBao/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "TuyenDung",
                                                                              "/TuyenDung/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "TinTuc",
                                                                              "/ThongBao/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "SuKien",
                                                                              "/SuKien/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "LegalEducation",
                                                                              "/GiaoDucPhapLuat/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "MedicalStudent",
                                                                              "/YTeHocDuong/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "Alumni",
                                                                              "/CuuSinhVien/ChiTiet?id="
                                                                          },
                                                                          {
                                                                              "RelationCompany",
                                                                              "/HTSV-QHDN/ChiTiet?id="
                                                                          }
                                                                      };

        public static Dictionary<string, string> typeDocument = new Dictionary<string, string>()
                                                                    {
                                                                        {
                                                                            string.Empty,
                                                                            "Tất cả văn bản biểu mẫu"
                                                                        },
                                                                        {
                                                                            "VanBan",
                                                                            "Tất cả văn bản biểu mẫu"
                                                                        },
                                                                        {
                                                                            "BieuMau",
                                                                            "Biểu Mẫu"
                                                                        },
                                                                        {
                                                                            "VanBanBo",
                                                                            "Văn bản Bộ và Nhà nước"
                                                                        },
                                                                        {
                                                                            "VanBanDHDN",
                                                                            "Văn bản Đại Học Đà Nẵng"
                                                                        },
                                                                        {
                                                                            "VanBanTruong",
                                                                            "Văn bản Trường Bách Khoa Đà Nẵng"
                                                                        },
                                                                        {
                                                                            "VanBanCTSV",
                                                                            "Văn bản Phòng Công Tác Sinh Viên"
                                                                        }
                                                                    };

        public static Dictionary<string, string> typeNews = new Dictionary<string, string>()
                                                                {
                                                                    {
                                                                        "HocBongKhuyenKhich",
                                                                        "Học bổng khuyến khích học tập"
                                                                    },
                                                                    {
                                                                        "HocBongKhac",
                                                                        "Học bổng khác"
                                                                    },
                                                                    {
                                                                        "BieuMau", "Biểu Mẫu"
                                                                    },
                                                                    {
                                                                        "VanBanBo",
                                                                        "Văn bản Bộ và Nhà nước"
                                                                    },
                                                                    {
                                                                        "VanBanDHDN",
                                                                        "Văn bản Đại Học Đà Nẵng"
                                                                    },
                                                                    {
                                                                        "VanBanTruong",
                                                                        "Văn bản Trường Bách Khoa Đà Nẵng"
                                                                    },
                                                                    {
                                                                        "VanBanCTSV",
                                                                        "Văn bản Phòng Công Tác Sinh Viên"
                                                                    },
                                                                    {
                                                                        "ThongBao",
                                                                        "Thông báo"
                                                                    },
                                                                    {
                                                                        "TuyenDung",
                                                                        "Tuyển dụng"
                                                                    },
                                                                    { "TinTuc", "Tin tức" },
                                                                    { "SuKien", "Sự kiện" },
                                                                    {
                                                                        "LegalEducation",
                                                                        "Giáo dục pháp luật"
                                                                    },
                                                                    {
                                                                        "MedicalStudent",
                                                                        "Y tế học đường"
                                                                    },
                                                                    {
                                                                        "Alumni",
                                                                        "Tin tức cựu sinh viên"
                                                                    },
                                                                    {
                                                                        "RelationCompany",
                                                                        "Hỗ trợ sinh viên - quan hệ doanh nghiệp"
                                                                    }
                                                                };

        public static Dictionary<string, string> typeRequestQuestion = new Dictionary<string, string>()
                                                                           {
                                                                               {
                                                                                   "student",
                                                                                   "Sinh viên"
                                                                               },
                                                                               {
                                                                                   "old-student",
                                                                                   "Cựu sinh viên"
                                                                               },
                                                                               {
                                                                                   "parent",
                                                                                   "Phụ huynh"
                                                                               },
                                                                               {
                                                                                   "hightschool-student",
                                                                                   "Học sinh phổ thông"
                                                                               },
                                                                               {
                                                                                   "other",
                                                                                   "khác"
                                                                               },
                                                                           };

        public static Dictionary<string, string> typeScholarShip = new Dictionary<string, string>()
                                                                       {
                                                                           {
                                                                               "HocBong",
                                                                               "Tất cả học bổng"
                                                                           },
                                                                           {
                                                                               "HocBongKhuyenKhich",
                                                                               "Học bổng khuyến khích học tập"
                                                                           },
                                                                           {
                                                                               "HocBongKhac",
                                                                               "Học bổng khác"
                                                                           }
                                                                       };

        public class ItemType
        {
            public static int CommentOne = 6;

            public static int CommentTwo = 8;

            public static int Item = 4;

            public static int Section = 2;
        }

        public class Role
        {
            public static int Admin = 1;

            public static int DSAStaff = 5;

            public static int Lecturer = 4;

            public static int Prefect = 3;

            public static int Student = 2;
        }
    }
}