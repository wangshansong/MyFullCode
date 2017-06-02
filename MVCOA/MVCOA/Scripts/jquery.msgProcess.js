

//依赖于jQuery文件 如：jquery.min.js
(function ($) {
    $.extend($, {
        //procAjaxData追加在JQuery对象中，用于全局Ajax返回消息的处理
        procAjaxData: function (data, funcSuc, funcErr) {
            dataObj = JSON.parse(data);
            if (!dataObj) {
                return;
            }
            switch (dataObj.Statu) {
                case "ok":
                    alert("OK:" + dataObj.Msg);
                    if (funcSuc) funcSuc(dataObj);
                    break;
                case "err":
                    alert("ERR:" + dataObj.Msg);
                    if (funcErr) funcErr(dataObj);
                    break;
                case "nologin":
                    alert(dataObj.Msg);
                    $.alertMsg(data.Msg, "系统提示", function () {
                        if (window.top) //如果有父窗口，则跳转到最顶层的父窗口
                            window.top.location = data.BackUrl;
                        else
                            window.location = data.BackUrl;
                    });
                case "noPermission":
                    alert(dataObj.Msg);
                    $.alertMsg(data.Msg, "系统提示", function () {
                        if (window.top) //如果有父窗口，则跳转到最顶层的父窗口
                            window.top.location = data.BackUrl;
                        else
                            window.location = data.BackUrl;
                    });
                    break;
            }
        }
    });
}(jQuery));

//EasyUI消息提示框，依赖于EasyUI文件
function ShowMsg(msgContent) {
    $.messager.show({
        title: '提示',
        msg: msgContent,
        timeout: 1000,
        showType: 'fade',
        style: {
            right: '',
            bottom: ''
        }
    });
}

//判断一个字符是否能转成JSON
function checkIsJSON(s) {
    try {
        JSON.parse(s);
        return true;
    } catch (e) {
        return false;
    }
}