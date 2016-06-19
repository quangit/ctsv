function GetGradeEvaluated(point) {
    if (point >= 90) {
        return "Xuất sắc";
    }
    else if (80 <= point && point < 90) {
        return "Giỏi";
    }
    else if (65 <= point && point < 80) {
        return "Khá";
    }
    else if (50 <= point && point < 65) {
        return "Trung bình";
    }
    else if (35 <= point && point < 50) {
        return "Yếu";
    }
    else {
        return "Kém";
    }
}