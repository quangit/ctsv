﻿function update() {
    GetData();
    setTimeout("update()", 100);
}


function GetData() {
    try {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", "/StudentShip/GetProgressExportStudentShip", false);
        xhr.setRequestHeader("Content-Type",
        "application/x-www-form-urlencoded");
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    //Posting Back the Result
                    postMessage(xhr.responseText);
                }
            }
        };
        xhr.send(null);
    } catch (e) {
        postMessage(null);
    }
}

update();