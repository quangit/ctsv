
// editStudent 
function ChangeSelectFaculty() {
    var idFaculty = $("#selectFaculty").val();
    var idCourse = $("#selectCourse").val();
    $.ajax({
        url: "/ManageStudent/GetListClass",
        type: "post",
        data: { 'idFaculty': idFaculty, 'idCourse': idCourse },
        success: function (result) {
            $("#selectClass").html(result);
        }
    });
}


// EditStudent 2
function ChangeSelectProvince() {
    var idProvince = $("#selectPlaceSchoolProvince").val();
    $.ajax({
        url: "/ManageStudent/GetDistrict",
        type: "post",
        data: { 'idProvince': idProvince },
        success: function (result) {
            $("#selectPlaceSchoolDistrict").html(result);
        }
    });
}




// EditStudent4

function ChangeSelectLanguage(idLanguage, idStudent) {
    $.ajax({
        url: "/ManageStudent/UpdateSecondLanguage",
        type: "post",
        data: { 'idLanguage': idLanguage, 'idStudent': idStudent },
        success: function (result) {
        }
    });
}

function ChangeSelectSocialPolicyBeneficiary(idSocialPolicyBeneficiary, idStudent) {
    $.ajax({
        url: "/ManageStudent/UpdateSocialPolicyBeneficiaryStudent",
        type: "post",
        data: { 'idSocialPolicyBeneficiary': idSocialPolicyBeneficiary, 'idStudent': idStudent },
        success: function (result) {
        }
    });
}

function ChangeSelectTypePaper(idTypePaper, idStudent) {
    $.ajax({
        url: "/ManageStudent/UpdateTypePaperStudent",
        type: "post",
        data: { 'idTypePaper': idTypePaper, 'idStudent': idStudent },
        success: function (result) {
        }
    });
}

//Student Social Activity 

function ChangeSelectIdRegency(idRegency, idStudent) {
    $.ajax({
        url: "/ManageStudent/UpdateRegency",
        type: "post",
        data: { 'idRegency': idRegency, 'idStudent': idStudent },
        success: function (result) {
        }
    });
}


function ChangeCheckBoxYouthUnion(idStudent) {
    $.ajax({
        url: "/ManageStudent/UpdateYouthUnion",
        type: "post",
        data: { 'idStudent': idStudent },
        success: function (result) {
        }
    });
}

function ChangeCheckBoxPoliticalParty(idStudent) {
    $.ajax({
        url: "/ManageStudent/UpdatePoliticalParty",
        type: "post",
        data: { 'idStudent': idStudent },
        success: function (result) {
        }
    });
}

// _Select Place

function SelectProvince(idSelectProvince,idSelectDistrict) {
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

function SelectDistrict(idSelectDistrict,idSelectWard) {
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

//_ListAccount

function ChangeGroupAccount(idAccount, nameSelect) {
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

function ResetPassword(idAccount) {
    $.ajax({
        url: "/ManageAccount/ResetPassword",
        type: "post",
        data: { 'idAccount': idAccount },
        success: function (result) {
            alert("Reset mật khẩu thành công");
        }
    });
}

//Account Info
//$('#btn-changePassword').click(function () {
//    var oldPassword = $('#oldPassword').val();
//    var newPassword = $('#newPassword').val();
//    var reNewPassword = $('#reNewPassword').val();
//    if ((oldPassword == "") || (newPassword == "")) {
//        $('#info-changepassword').html('bạn chưa nhập mật khảu');
//    }
//    else if (newPassword != reNewPassword) {
//        $('#info-changepassword').html('mật khẩu mới không trùng khớp');
//    }
//    else {
//        $.ajax({
//            url: "/ManageAccount/ChangePassword",
//            type: "post",
//            data: { 'oldPassword': oldPassword, 'newPassword': newPassword },
//            success: function (result) {
//                $('#info-changepassword').html(result);
//            }
//        });
//    }
//})

//Decentralization Manager
function ChangeDecentralization(idDecentralization, idGroup, idFuntion) {
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

// Group
function AddGroup() {
    var nameGroup = $('#inputNameGroup').val();
    $.ajax({
        url: "/ManageDecentralization/AddGroup",
        type: "post",
        data: { 'nameGroup': nameGroup },
        success: function (result) {
            location.reload();
        }
    });
}


//_AddReasonRequest
function DeleteReason(idReason) {
    $.ajax({
        url: "/ManagePaper/DeleteReasonRequest",
        type: "post",
        data: { 'idReason': idReason },
        success: function (result) {
        }
    });
}


//EditPaper

function PrintHtml() {
    var printContents = document.getElementById("contentEditPaper").innerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}

function AddReasonRequest(idPaper) {
    var contentReason = $("#reasonRequestEditPaper").val();
    var isimportant = $("#isImportantEditPaper").prop('checked');
    var priceRequest = $("#priceRequestEditPaper").val();
    var waittingPeriod = $("#waittingPeriodEditPaper").val();
    $.ajax({
        url: "/ManagePaper/AddReasonRequest",
        type: "post",
        data: { 'idPaper': idPaper, 'reason': contentReason, 'isImportant': isimportant, 'priceRequest': priceRequest, 'waittingPeriod': waittingPeriod },
        success: function (result) {
            $('#reasonsEditPaper').html(result);
        }
    });
}

function UpdatePaperContent(idPaper) {
    var contentPaper = $("#contentEditPaper").html();
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


