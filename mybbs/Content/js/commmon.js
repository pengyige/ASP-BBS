$(function () {
    //ajax注销
    $("#logout_a").click(function () {
        $.ajax({
            url: "/Login/LogoutUser",
            type: "POST",
            dataType: "json",
            success: function (message) {
                if (message.value == "-1") {
                    alert('请先登入!');
                } else {
                    alert("注销成功");
                    window.location.reload();
                }
            }
        })
    });




})