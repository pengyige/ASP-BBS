$(function () {
    //ajax发送回复消息
    $("#send_reply").click(function () {
        var postId = $("#send_reply").attr("value");
        var replyMessage = $("#text_replyMessage").val();
        if (replyMessage == "") {
            alert("回复消息不能为空");
            return;
        }


        $.ajax({
            url: "/Reply/AddReply",
            type: "POST",
            dataType: "json",
            data: { "postId": postId, "replyMessage": replyMessage },
            success: function (message) {
                if (message.value == "-1") {
                    alert("回复异常!");
                } else if (message.value == "0") {
                    alert("请先登入后回复!");
                } else if (message.value == "1") {
                    alert("发送成功!");
                }
            }
        });
    });






  
});