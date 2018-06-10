$(function () {
    var editor = CKEDITOR.replace('content');

    //ajax提交发帖
    $('#sendpost').click(function () {
        var title = $('#title').val();
        var content = CKEDITOR.instances.content.getData();


        if (title == "") {
            alert("标题不能为空");
            return;
        };

        if (content == "") {
            alert("内容不能为空");
            return;
        };

        $.ajax({
            url: "/Post/SendPost",
            type: "POST",
            dataType: "json",
            data: { "title": title, "content": content },
            success: function (message) {
                if (message.value == "1") {
                    alert("发帖成功！");
                } else if (message.value == "-1") {
                    alert("您还没有登入，请先登入!");
                } else {
                    alert("发送帖子异常!");
                }
            }
        });
    });

    //ajax上传图片
    $('#post_pic_btn').click(function () {
        var formData = new FormData($("#pic_form")[0]);
        if ($('#select_img').val() == "") {
            alert("请先选择图片");
            return 0;
        }


        $.ajax({
            url: "/Upload/UserPic",
            type: "POST",
            data: formData,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (message) {
                if (message.value == "1") {
                    alert("上传成功");
                } else if (message.value == "-1") {
                    alert("请先登入!");
                } else {
                    alert("上传出现异常!");
                }
            }
        });
    });



    //ajax删除帖子
    $("#btn_deletePost").click(function () {
        var postId = $("#btn_deletePost").attr("value");
        $.ajax({
            url: "/Post/DeletePost",
            type: "POST",
            dateType: "json",
            data: { "postId": postId },
            success: function (message) {
                if (message.value == "1") {
                    alert("删除成功!");
                } else {
                    alert("删除失败!");
                }
            },
            error: function (message) {
                alert("发送异常!");
            }
        });
    });
})