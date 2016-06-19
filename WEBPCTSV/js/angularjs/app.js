var app = angular.module("myapp", ['ngSanitize']);


app.controller("detailPaper", function ($scope) {
    $scope.changeselect = function () {
        var valueSelectReason = $scope.selectReason;
        var textSelectReason = $("#selectReasonDetail :selected").text().trim();
        if (textSelectReason == 'Chọn lý do')
        {
            $('#buttonSendRequest').prop( "disabled", true );
        } else {
            $('#buttonSendRequest').prop("disabled", false);
        }
        if (textSelectReason == 'khác') {
            $('#divReasonDetail').html('<textarea name="otherReason" class="form-control" style="width:80%;margin-top:10px" placeholder="nhập lý do khác"></textarea>');
        }
        else {
            $('#divReasonDetail').html('');
        }
        $.ajax({
            url: "/ManagePaper/GetStringInfoReasonRequest",
            type: "post",
            data: { 'idReason': valueSelectReason },
            success: function (result) {
                $('#noteReason').html(result);
            }
        });
    }
})


app.controller("editStudent", function ($scope, $http) {
    $scope.ChangeSelectFaculty = function () {
        var idFaculty = $scope.selectFaculty;
        var idCourse = $scope.selectCourse;
        $.ajax({
            url: "/ManageStudent/GetListClass",
            type: "post",
            data: { 'idFaculty': idFaculty, 'idCourse': idCourse },
            success: function (result) {
                $("#selectClass").html(result);
            }
        });
    }
})

app.controller("editStudent2", function ($scope, $http) {
    $scope.ChangeSelectProvince = function () {
        $.ajax({
            url: "/ManageStudent/GetDistrict",
            type: "post",
            data: { 'idProvince': $scope.selectPlaceSchoolProvince },
            success: function (result) {
                $("#selectPlaceSchoolDistrict").html(result);
            }
        });
    }
});

app.controller("editStudent4", function ($scope, $http) {
    $scope.ChangeSelectLanguage = function (idLanguage, idStudent) {
        $.ajax({
            url: "/ManageStudent/UpdateSecondLanguage",
            type: "post",
            data: { 'idLanguage': idLanguage, 'idStudent': idStudent },
            success: function (result) {
            }
        });
    }

    $scope.ChangeSelectSocialPolicyBeneficiary = function (idSocialPolicyBeneficiary, idStudent) {
        $.ajax({
            url: "/ManageStudent/UpdateSocialPolicyBeneficiaryStudent",
            type: "post",
            data: { 'idSocialPolicyBeneficiary': idSocialPolicyBeneficiary, 'idStudent': idStudent },
            success: function (result) {
            }
        });
    }

    $scope.ChangeSelectTypePaper = function (idTypePaper, idStudent) {
        $.ajax({
            url: "/ManageStudent/UpdateTypePaperStudent",
            type: "post",
            data: { 'idTypePaper': idTypePaper, 'idStudent': idStudent },
            success: function (result) {
            }
        });
    }
});


app.controller("editStudentSocialActivity", function ($scope, $http, $sce) {


    $scope.ChangeSelectIdRegency = function (idRegency, idStudent) {
        $.ajax({
            url: "/ManageStudent/UpdateRegency",
            type: "post",
            data: { 'idRegency': idRegency, 'idStudent': idStudent },
            success: function (result) {
            }
        });
    }


    $scope.ChangeCheckBoxYouthUnion = function (idStudent) {
        $.ajax({
            url: "/ManageStudent/UpdateYouthUnion",
            type: "post",
            data: { 'idStudent': idStudent },
            success: function (result) {
            }
        });
    }

    $scope.ChangeCheckBoxPoliticalParty = function (idStudent) {
        $.ajax({
            url: "/ManageStudent/UpdatePoliticalParty",
            type: "post",
            data: { 'idStudent': idStudent },
            success: function (result) {
            }
        });
    }

    function GetSocialActivity() {
        var responsePromise = $http.post("/SocialActivity/GetListSocialActivityNotStudent", { "page": $scope.pageSocialActivity, "idStudent": $scope.idStudent });
        responsePromise.success(function (data, status, headers, config) {
            $scope.listSocialActivity = $sce.trustAsHtml(data);
        });
        responsePromise.error(function (data, status, headers, config) {
            alert("AJAX failed!");
        });
    };

    $scope.GetListSocialActivity = function () {
        GetSocialActivity();
    }

    $scope.NextPage = function () {
        $scope.pageSocialActivity = $scope.pageSocialActivity + 1;
        GetSocialActivity();
    }
    $scope.PreviousPage = function () {
        if ($scope.pageSocialActivity > 1) {
            $scope.pageSocialActivity = $scope.pageSocialActivity - 1;
            GetSocialActivity();
        }
    }

    $scope.AddSocialActivity = function (idSocialActivity) {
        var responsePromise = $http.post("/SocialActivity/AddStudentSocialActivity", { "idStudent": $scope.idStudent, "idSocialActivity": idSocialActivity });
        responsePromise.success(function (data, status, headers, config) {
            location.reload();
        });
        responsePromise.error(function (data, status, headers, config) {
            alert("AJAX failed!");
        });
    }

    $scope.DeleteStudentSocialActivity = function (idStudentSocialActivity) {
        if (confirm("Bạn có muốn xóa hoạt động này không ?")) {
            var responsePromise = $http.post("/SocialActivity/DeleteStudentSocialActivity", { "idStudentSocialActivity": idStudentSocialActivity });
            responsePromise.success(function (data, status, headers, config) {
                location.reload();
            });
            responsePromise.error(function (data, status, headers, config) {
                alert("AJAX failed!");
            });
        }
    }

});

app.controller("selectPlace", function ($scope, $http) {
    $scope.SelectProvince = function (idSelectProvince, idSelectDistrict) {
        var selectProvince = document.getElementById(idSelectProvince);
        var selectDistrict = document.getElementById(idSelectDistrict);
        idProvince = selectProvince.options[selectProvince.selectedIndex].value;
        $.ajax({
            url: "/ManageStudent/GetDistrict",
            type: "post",
            data: { 'idProvince': idProvince },
            success: function (result) {
                selectDistrict.innerHTML = result;
            }
        });
    }

    $scope.SelectDistrict = function (idSelectDistrict, idSelectWard) {
        var selectWard = document.getElementById(idSelectWard);
        var selectDistrict = document.getElementById(idSelectDistrict);
        var idDistrict = selectDistrict.options[selectDistrict.selectedIndex].value;
        $.ajax({
            url: "/ManageStudent/GetWard",
            type: "post",
            data: { 'idDistrict': idDistrict },
            success: function (result) {
                selectWard.innerHTML = result;
            }
        });
    }
})

app.controller("listAccount", function ($scope, $http) {
    $scope.ChangeGroupAccount = function (idAccount, nameSelect) {
        var select = document.getElementById(nameSelect);
        var idGroup = select.options[select.selectedIndex].value;
        $.ajax({
            url: "/ManageAccount/ChangeDecentralizationGroupAccount",
            type: "post",
            data: { 'idAccount': idAccount, 'idGroup': idGroup },
            success: function (result) {
            }
        });
    }

    $scope.ResetPassword = function (idAccount) {
        $.ajax({
            url: "/ManageAccount/ResetPassword",
            type: "post",
            data: { 'idAccount': idAccount },
            success: function (result) {
                alert("Reset mật khẩu thành công");
            }
        });
    }
})

app.controller("accountInformation", function ($scope, $http) {

    $scope.ChangePassword = function () {
        var oldPassword = $scope.oldPassword;
        var newPassword = $scope.newPassword;
        var reNewPassword = $scope.reNewPassword;
        if ((oldPassword == "") || (newPassword == "")) {
            $('#info-changepassword').html('bạn chưa nhập mật khảu');
        }
        else if (newPassword != reNewPassword) {
            $('#info-changepassword').html('mật khẩu mới không trùng khớp');
        }
        else {
            $.ajax({
                url: "/ManageAccount/ChangePassword",
                type: "post",
                data: { 'oldPassword': oldPassword, 'newPassword': newPassword },
                success: function (result) {
                    $('#info-changepassword').html(result);
                }
            });
        }
    }
})

app.controller("decentraizationManage", function ($scope, $http) {
    //Decentralization Manager
    $scope.ChangeDecentralization = function (idDecentralization, idGroup, idFuntion) {
        var checkBox = document.getElementById("checkBox_" + idFuntion);
        if (checkBox.value != 0) {
            $.ajax({
                url: "/ManageDecentralization/UpdateIsAccept",
                type: "post",
                data: { 'idDecentralization': idDecentralization },
                success: function (result) {
                }
            });
        } else {
            $.ajax({
                url: "/ManageDecentralization/AddDecentralization",
                type: "post",
                data: { 'idGroup': idGroup, 'idFuntion': idFuntion },
                success: function (result) {
                    checkBox.value = result;
                }
            });
        }
    }
})

app.controller("group", function ($scope, $http) {
    // Group
    $scope.AddGroup = function () {
        var nameGroup = $scope.inputNameGroup;
        $.ajax({
            url: "/ManageDecentralization/AddGroup",
            type: "post",
            data: { 'nameGroup': nameGroup },
            success: function (result) {
                location.reload();
            }
        });
    }
})

app.controller("addReasonRequest", function ($scope, $http) {
    //_AddReasonRequest
    $scope.DeleteReason = function (idReason) {
        $.ajax({
            url: "/ManagePaper/DeleteReasonRequest",
            type: "post",
            data: { 'idReason': idReason },
            success: function (result) {
            }
        });
    }
});

app.controller("editPaper", function ($scope, $http) {
    //EditPaper

    $scope.PrintHtml = function (contentEditPaper) {
        var printContents = document.getElementById(contentEditPaper).innerHTML;
        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
        location.reload();
    }

    $scope.AddReasonRequest = function (idPaper) {

        var contentReason = $scope.reasonRequestEditPaper;

        var isimportant = $scope.isImportantEditPaper;
        var priceRequest = $scope.priceRequestEditPaper;
        var waittingPeriod = $scope.waittingPeriodEditPaper;
        $.ajax({
            url: "/ManagePaper/AddReasonRequest",
            type: "post",
            data: { 'idPaper': idPaper, 'reason': contentReason},
            success: function (result) {
                $('#reasonsEditPaper').html(result);
            }
        });
    }

    $scope.UpdatePaperContent = function (idPaper, contentEditPaper) {
        var contentPaper = document.getElementById(contentEditPaper).innerHTML;
        $.ajax({
            url: "/ManagePaper/UpdateContentPaper",
            type: "post",
            data: { 'content': contentPaper, 'idPaper': idPaper },
            success: function (result) {
                if (result == 1) {
                    alert("Lưu lại thành công");
                }
            }
        });
    };
})

app.controller("ManageSocialActivity", function ($scope, $http) {
    $scope.DeleteSocialActivity = function (idSocialActivity) {
        if (confirm("Bạn có muốn xóa hoạt động này không ?")) {
            var responsePromise = $http.post("/SocialActivity/DeleteSocialActivity", { "idSocialActivity": idSocialActivity });
            responsePromise.success(function (data, status, headers, config) {
                location.reload();
            });
            responsePromise.error(function (data, status, headers, config) {

            });
        }
    }
});

app.controller("ManageClass", function ($scope, $http, $sce) {
    $scope.ChangeSelect = function () {
        var responsePromise = $http.post("/ManageClass/GetListClass", { "idFaculty": $scope.faculty, "idCourse": $scope.course });
        responsePromise.success(function (data, status, headers, config) {
            $scope.contentlistclass = $sce.trustAsHtml(data);
        });
        responsePromise.error(function (data, status, headers, config) {
        });

        var responsePromise = $http.post("/ManageClass/GetNameClass", { "idFaculty": $scope.faculty, "idCourse": $scope.course });
        responsePromise.success(function (data, status, headers, config) {
            $scope.FirstNameClass = data;
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }

    $scope.AddClass = function () {
        var responsePromise = $http.post("/ManageClass/AddClass", { "idFaculty": $scope.faculty, "idCourse": $scope.course, "ClassName": ($scope.FirstNameClass + $scope.LastNameClass) });
        responsePromise.success(function (data, status, headers, config) {
            $scope.ChangeSelect();
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }
});

app.controller("StudentShipFaculty", function ($scope, $http, $sce) {

    

    $scope.SaveMoneyStudentShipSchoolFaculty = function (idFaculty, idStudentShipSchool, idInputMoney) {
        var totalMoney = document.getElementById(idInputMoney).value;
        var responsePromise = $http.post("/StudentShip/UpdateMoneyStudentShipShoolFaculty", { "idFaculty": idFaculty, "idStudentShipSchool": idStudentShipSchool, "totalMoney": totalMoney });
        responsePromise.success(function (data, status, headers, config) {
            alert("Lưu lại thành công");
            location.reload();
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }
    $scope.SaveMoneyStudentShipSchoolFacultyCLC = function (idClass, idStudentShipSchool, idInputMoney,idTuitionFee) {
        var totalMoney = document.getElementById(idInputMoney).value;
        var tuitionFee = document.getElementById(idTuitionFee).value;
        var responsePromise = $http.post("/StudentShip/UpdateMoneyStudentShipShoolFacultyCLC", { "idClass": idClass, "idStudentShipSchool": idStudentShipSchool, "totalMoney": totalMoney,"tuitionFee":tuitionFee });
        responsePromise.success(function (data, status, headers, config) {
            alert("Lưu lại thành công");
            location.reload();
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }

    
});

app.controller("SelectSemesterStudentShip", function ($scope, $http, $sce) {
    $scope.ChoiseSemester = function () {
        var responsePromise = $http.post("/StudentShip/GetMoneyStudentShipSchool", { "idSemester": $scope.idSemester });
        responsePromise.success(function (data, status, headers, config) {
            $scope.totalMoney = data;
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }

    
});


app.controller("ScoteStudent", function ($scope, $http, $sce) {
    $scope.UpdateIsAcceptConsider = function (idLearningOutCome, idbutton) {

        var button = document.getElementById(idbutton);
        var responsePromise = $http.post("/StudentShip/UpdateIsAcceptConsider", { "idLearningOutCome": idLearningOutCome });
        responsePromise.success(function (data, status, headers, config) {
            if (button.className == 'btn btn-default')
            {
                button.className = "btn btn-danger";
                button.innerHTML = "Hủy xác nhận";
            } else {
                button.className = "btn btn-default";
                button.innerHTML = "Xác nhận";
            }
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }
    $scope.UpdateIsDisEnableConsider = function (idLearningOutCome, idbutton) {
        var button = document.getElementById(idbutton);
        var responsePromise = $http.post("/StudentShip/UpdateIsDisEnableConsider", { "idLearningOutCome": idLearningOutCome });
        responsePromise.success(function (data, status, headers, config) {
            if (button.className == 'btn btn-default') {
                button.className = "btn btn-danger";
                button.innerHTML = "Cho phép";
            } else {
                button.className = "btn btn-default";
                button.innerHTML = "Loại";
            }
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }
});


app.controller("ListStudentShipSchoolFaculty", function ($scope, $http, $sce) {
    $scope.ChangeCountStudentShip = function (idStudentShipSchoolFaculty) {
        console.log($scope.countStudent);
        var responsePromise = $http.post("/StudentShip/GetMoneyByCountStudentShip", { "idStudentShipSchoolFaculty": idStudentShipSchoolFaculty, "countStudentShip": $scope.countStudent });
        responsePromise.success(function (data, status, headers, config) {
            $scope.totalMoneyStudentShip = data;
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }
});


app.controller("ListSendRequestPaper", function ($scope, $http, $sce) { 
    $scope.ChangeSelectPaper = function () {
        location.href = '/ManagePaper/DetailPaper?id=' + $scope.selectPaper;
    }
});

app.controller("ListAllRequestPaper", function ($scope, $http, $sce) {
    $scope.ChangeSelectRequestStatus = function () {
        location.href = '/ManageRequest/ListAllRequestPaper?value=' + $scope.requestStatus + '&page=1';
    }
});

app.controller("manageMessage", function ($scope, $http, $sce) {
    $scope.Readed = function (idMessage) {
        var responsePromise = $http.post("/ManageMessage/ChangeIsReaded", { "idMessage": idMessage });
        responsePromise.success(function (data, status, headers, config) {
            $scope.totalMoneyStudentShip = data;
        });
        responsePromise.error(function (data, status, headers, config) {
        });
    }
});







