

prevurl = "";
$(document).ready(function () {
    // Check validate email subscription
    $("#fSubscribe")
                            .validate(
                                    {
                                        rules: {
                                            email: {
                                                required: true,
                                                email: true
                                            }
                                        },
                                        messages: {
                                            email: {
                                                required: "Chưa nhập địa chỉ email!",
                                                email: "Nhập sai định dạng địa chỉ email!"
                                            }
                                        },

                                        submitHandler: function (form) {
                                            subscribe();
                                        }
                                    });

    // Hover to show dropdown in bootstrap
    $('ul.nav li.dropdown').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
    });
    $(function () {
        $(window).scroll(function () {
            // set distance user needs to scroll before we fadeIn navbar
            if ($(this).scrollTop() > $('#wrapper-header').height() + 60) {
                $('.navbar-slide').addClass('navbar-show');
            } else if ($('#wrapper-header').css('display') != 'none') {
                $('.navbar-slide').removeClass('navbar-show');
            }
        });

    });
    //set menu slide side
    $('#my-menu').mmenu();

    // Show search bar
    $('.search-area-background').click(function () {
        $('.search-area > div').removeClass('search-show');
        $(this).fadeOut();
        $('#btn-search i').removeClass('fa-remove');
        $('#btn-search i').addClass('fa-search');
        $('#header-nav-sub-search').removeClass('fa-remove');
        $('#header-nav-sub-search').addClass('fa-search');
    });

    $('#header-nav-sub-search, #btn-search').click(function () {
        if ($('.search-area-background').css('display') == 'none') {
            $('.search-area-background').fadeIn();
            $('.search-area > div').addClass('search-show');
            $('#header-nav-sub-search').removeClass('fa-search');
            $('#header-nav-sub-search').addClass('fa-remove');
            $('#btn-search i').removeClass('fa-search');
            $('#btn-search i').addClass('fa-remove');
        } else {
            $('.search-area-background').fadeOut();
            $('.search-area > div').removeClass('search-show');
            $('#header-nav-sub-search').removeClass('fa-remove');
            $('#header-nav-sub-search').addClass('fa-search');
            $('#btn-search i').removeClass('fa-remove');
            $('#btn-search i').addClass('fa-search');
        }
    });
    $('#header-nav-sub-login').click(function () {
        $('#modal-login').modal("show");
    });
    $('#modal-login-submit').click(function () {
        doLogin(prevurl);
    });

    $('#modal-login input').keypress(function (e) {
        // Prevent submit form by enter button
        if (e.keyCode == 13) {
            e.preventDefault();
            doLogin(prevurl);
        }
    });
});
function doConduct(isLogged) {
    if (isLogged) {
        location.href = '../DanhGiaRenLuyen';
    } else {
        prevurl = "../DanhGiaRenLuyen";
        $('#modal-login').modal("show");
    }
}

function loadIconLoading(element) {
    // Load icon when do ajax request
    element.html('<div class="cssload-spin-box"></div>');
}
function unloadIconLoading(element) {
    // Load icon when do ajax request
    element.html('');
}
function doLogin(prevurl) {
    $.ajax({
        type: 'POST',
        data: $('#fLoginAjax').serialize(),
        url: '../Account/LoginAjax',
        beforeSend: function () {
            $('.modal-message').fadeOut();
            $('.modal-message').html("");
            $('.cssload-container').fadeIn();
            loadIconLoading($('.cssload-container'));
        },
        success: function (response) {
            if (response.success == true) {
                typeAlert = 'alert-success';
                if (prevurl != "")
                    location.href = prevurl;
                else {
                    location.reload();
                }
            } else {
                typeAlert = 'alert-danger';
                $.when($('.cssload-container').fadeOut()).then(function () {
                    $('.modal-message').html('<div class="alert ' + typeAlert + '">' + response.responseText + '</div>')
                    $('.modal-message').fadeIn();
                });
            }
        },
        error: function (response) {
            $('.modal-message').html('<div class="alert alert-danger">Lỗi trong quá trình lấy dữ liệu!</div>')
            $('.cssload-container').fadeOut();
            $('.modal-message').fadeIn();
        }
    });
}

function subscribe() {
    $.ajax({
        url: "../Account/Subscrible",
        type: "post",
        dateType: "text",
        data: {
            mail: $('#email').val()
        },
        success: function (result) {
            $('.modal-announcement-message').html(result);
            $('#modal-announcement').modal('show');
        },
        error: function (result) {
            $('.modal-announcement-message').html('Lỗi trong quá trình gửi dữ liệu!');
            $('#modal-announcement').modal('show');
        }
    });
}
